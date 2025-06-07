using Microsoft.EntityFrameworkCore;
using SGE.Domain.Entities;

namespace SGE.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<UserRol> UsersRoles => Set<UserRol>();
    // Puedes añadir los demás DbSet si planeas usarlos

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRol>()
            .HasIndex(ur => new { ur.UsuarioId, ur.RolId })
            .IsUnique();
    }
}
