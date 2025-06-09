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
using BCrypt.Net; // Asegúrate de instalar BCrypt.Net-Next

namespace SGE.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Roles).ThenInclude(r => r.Rol)
                .FirstOrDefaultAsync(u => u.Usuario == request.Usuario && u.Estado == "Activo");

            if (user == null || user.Bloqueado) return null;

            // Verificar contraseña con bcrypt
            bool passwordOk = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!passwordOk)
            {
                user.IntentosFallidos++;
                if (user.IntentosFallidos >= 5)
                {
                    user.Bloqueado = true;
                }
                await _context.SaveChangesAsync();
                return null;
            }

            user.IntentosFallidos = 0;
            user.FechaUltimoLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var permisos = await GetPermisosPorUsuario(user.Id);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Usuario),
                new Claim("userId", user.Id.ToString())
            };

            foreach (var rol in user.Roles.Select(r => r.Rol.Nombre))
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                NombreUsuario = user.Usuario,
                Roles = user.Roles.Select(r => r.Rol.Nombre).ToList(),
                Menu = permisos
            };
        }

        private async Task<List<MenuModulo>> GetPermisosPorUsuario(int userId)
        {
            var query = await (
                from u in _context.Users
                join ur in _context.UsersRoles on u.Id equals ur.UsuarioId
                join rp in _context.RolesPermisos on ur.RolId equals rp.RolId
                join p in _context.Permisos on rp.PermisoId equals p.Id
                join m in _context.Modulos on p.ModuloId equals m.Id
                where u.Id == userId
                select new { m.Id, m.Nombre, m.PadreId, p.Accion }
            ).Union(
                from up in _context.UsersPermisos
                join p in _context.Permisos on up.PermisoId equals p.Id
                join m in _context.Modulos on p.ModuloId equals m.Id
                where up.UsuarioId == userId
                select new { m.Id, m.Nombre, m.PadreId, p.Accion }
            ).Distinct().ToListAsync();

            var agrupado = query
                .Where(x => x.Accion == "ver")
                .GroupBy(x => x.PadreId)
                .Select(g => new MenuModulo
                {
                    Modulo = query.FirstOrDefault(x => x.Id == g.Key)?.Nombre ?? "General",
                    Submodulos = query.Where(x => x.PadreId == g.Key).GroupBy(x => x.Nombre).Select(sg => new SubModulo
                    {
                        Nombre = sg.Key,
                        Acciones = sg.Select(x => x.Accion).Distinct().ToList()
                    }).ToList()
                }).ToList();

            return agrupado;
        }
    }
}
