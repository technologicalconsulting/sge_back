using Microsoft.EntityFrameworkCore;
using SGE.Domain.Auth.Entities;
using SGE.Domain.Clientes.Entities;
using SGE.Domain.Empleados.Entities;
using SGE.Domain.Empresa.Entities;

namespace SGE.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Usuarios y seguridad
        public DbSet<users> users => Set<users>();
        public DbSet<roles> roles => Set<roles>();
        public DbSet<users_roles> users_roles => Set<users_roles>();
        public DbSet<permisos> permisos => Set<permisos>();
        public DbSet<modulos> modulos => Set<modulos>();
        public DbSet<users_permisos> users_permisos => Set<users_permisos>();
        public DbSet<roles_permisos> roles_permisos => Set<roles_permisos>();
        public DbSet<modulos_activados> modulos_activados => Set<modulos_activados>();
        public DbSet<historial_eventos_usuario> historial_eventos_usuario => Set<historial_eventos_usuario>();
        public DbSet<codigos_verificacion> codigos_verificacion => Set<codigos_verificacion>();

        // Empleados
        public DbSet<empleado> empleado => Set<empleado>();
        public DbSet<informacion_laboral_empleado> informacion_laboral_empleado => Set<informacion_laboral_empleado>();

        // Empresa y organización
        public DbSet<departamentos> departamentos => Set<departamentos>();
        public DbSet<cargos> cargos => Set<cargos>();
        public DbSet<empresa> empresa => Set<empresa>();

        // Clientes
        public DbSet<clientes> clientes => Set<clientes>();
        public DbSet<contactos_clientes> contactos_clientes => Set<contactos_clientes>();
        public DbSet<empleado_cliente> empleado_cliente => Set<empleado_cliente>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tablas
            modelBuilder.Entity<users>().ToTable("users");
            modelBuilder.Entity<roles>().ToTable("roles");
            modelBuilder.Entity<users_roles>().ToTable("users_roles");
            modelBuilder.Entity<permisos>().ToTable("permisos");
            modelBuilder.Entity<roles_permisos>().ToTable("roles_permisos");
            modelBuilder.Entity<users_permisos>().ToTable("users_permisos");
            modelBuilder.Entity<modulos>().ToTable("modulos");
            modelBuilder.Entity<modulos_activados>().ToTable("modulos_activados");
            modelBuilder.Entity<historial_eventos_usuario>().ToTable("historial_eventos_usuario");
            modelBuilder.Entity<codigos_verificacion>().ToTable("codigos_verificacion");

            modelBuilder.Entity<empleado>().ToTable("empleado");
            modelBuilder.Entity<informacion_laboral_empleado>().ToTable("informacion_laboral_empleado");

            modelBuilder.Entity<departamentos>().ToTable("departamentos");
            modelBuilder.Entity<cargos>().ToTable("cargos");
            modelBuilder.Entity<empresa>().ToTable("empresa");

            modelBuilder.Entity<clientes>().ToTable("clientes");
            modelBuilder.Entity<contactos_clientes>().ToTable("contactos_clientes");
            modelBuilder.Entity<empleado_cliente>().ToTable("empleado_cliente");

            // Índices únicos compuestos
            modelBuilder.Entity<users_roles>()
                .HasIndex(ur => new { ur.usuario_id, ur.rol_id }).IsUnique();

            modelBuilder.Entity<roles_permisos>()
                .HasIndex(rp => new { rp.rol_id, rp.permiso_id }).IsUnique();

            modelBuilder.Entity<users_permisos>()
                .HasIndex(up => new { up.usuario_id, up.permiso_id }).IsUnique();
        }
    }
}
