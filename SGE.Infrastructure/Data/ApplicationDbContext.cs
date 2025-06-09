using Microsoft.EntityFrameworkCore;
using SGE.Domain.Auth.Entities;

namespace SGE.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Tablas principales
    public DbSet<Users> Users => Set<Users>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<UserRol> UsersRoles => Set<UserRol>();

    // Necesarios para GetPermisosPorUsuario
    public DbSet<Permiso> Permisos => Set<Permiso>();
    public DbSet<Modulo> Modulos => Set<Modulo>();
    public DbSet<UserPermiso> UsersPermisos => Set<UserPermiso>();
    public DbSet<RolPermiso> RolesPermisos => Set<RolPermiso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>().ToTable("users");
        modelBuilder.Entity<Rol>().ToTable("roles");
        modelBuilder.Entity<UserRol>().ToTable("users_roles");
        modelBuilder.Entity<Permiso>().ToTable("permisos");
        modelBuilder.Entity<RolPermiso>().ToTable("roles_permisos");
        modelBuilder.Entity<UserPermiso>().ToTable("users_permisos");
        modelBuilder.Entity<Modulo>().ToTable("modulos");
        modelBuilder.Entity<ModuloActivado>().ToTable("modulos_activados");
        modelBuilder.Entity<Empleado>().ToTable("empleado");
        modelBuilder.Entity<CodigoVerificacion>().ToTable("codigos_verificacion");
        modelBuilder.Entity<HistorialEventoUsuario>().ToTable("historial_eventos_usuario");
    }
}
