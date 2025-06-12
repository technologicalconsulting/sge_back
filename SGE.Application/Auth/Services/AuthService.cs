using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGE.Application.Auth.DTOs;
using SGE.Application.Auth.Interfaces;
using SGE.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SGE.Domain.Auth.Entities;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace SGE.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.users
                .Include(u => u.users_roles)  // Lista de users_roles
                    .ThenInclude(ur => ur.rol) // Incluye entidad rol
                .FirstOrDefaultAsync(u => u.usuario == request.Usuario && u.estado == "Activo");

            if (user == null || user.bloqueado)
            {
                if (user != null)
                {
                    await RegistrarEventoAsync(user, "IntentoLogin", false, "Usuario bloqueado o inexistente");
                }
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.password_hash))
            {
                user.intentos_fallidos++;
                if (user.intentos_fallidos >= 5)
                {
                    user.bloqueado = true;
                    await RegistrarEventoAsync(user, "Bloqueo", false, "Demasiados intentos fallidos");
                }

                await RegistrarEventoAsync(user, "IntentoLogin", false, "Contraseña inválida");
                await _context.SaveChangesAsync();
                return null;
            }

            user.intentos_fallidos = 0;
            user.fecha_ultimo_login = DateTime.UtcNow;
            await RegistrarEventoAsync(user, "IntentoLogin", true);
            await _context.SaveChangesAsync();

            var permisos = await GetPermisosPorUsuario(user.id);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.usuario),
                new Claim("userId", user.id.ToString())
            };

            foreach (var userRol in user.users_roles)
            {
                if (userRol.rol != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRol.rol.nombre));
                }
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                NombreUsuario = user.usuario,
                Roles = user.users_roles.Select(r => r.rol!.nombre).ToList(),
                Menu = permisos
            };
        }

        private async Task<List<MenuModulo>> GetPermisosPorUsuario(int userId)
        {
            var query = await (
                from u in _context.users
                join ur in _context.users_roles on u.id equals ur.usuario_id
                join rp in _context.roles_permisos on ur.rol_id equals rp.rol_id
                join p in _context.permisos on rp.permiso_id equals p.id
                join m in _context.modulos on p.modulo_id equals m.id
                where u.id == userId
                select new { m.id, m.nombre, m.padre_id, p.accion }
            ).Union(
                from up in _context.users_permisos
                join p in _context.permisos on up.permiso_id equals p.id
                join m in _context.modulos on p.modulo_id equals m.id
                where up.usuario_id == userId
                select new { m.id, m.nombre, m.padre_id, p.accion }
            ).Distinct().ToListAsync();

            var agrupado = query
                .Where(x => x.accion == "ver")
                .GroupBy(x => x.padre_id)
                .Select(g => new MenuModulo
                {
                    Modulo = query.FirstOrDefault(x => x.id == g.Key)?.nombre ?? "General",
                    Submodulos = query
                        .Where(x => x.padre_id == g.Key)
                        .GroupBy(x => x.nombre)
                        .Select(sg => new SubModulo
                        {
                            Nombre = sg.Key,
                            Acciones = sg.Select(x => x.accion).Distinct().ToList()
                        }).ToList()
                }).ToList();

            return agrupado;
        }

        private async Task RegistrarEventoAsync(users user, string tipoEvento, bool exito, string? razon = null)
        {
            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var navegador = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

            var evento = new historial_eventos_usuario
            {
                usuario_id = user.id,
                tipo_evento = tipoEvento,
                ip = ip,
                navegador = navegador,
                exito = exito,
                razon = razon,
                fecha_evento = DateTime.UtcNow
            };

            _context.historial_eventos_usuario.Add(evento);
        }
    }
}
