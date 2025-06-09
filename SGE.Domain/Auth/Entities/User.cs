namespace SGE.Domain.Auth.Entities;

public class Users
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; }
    public string NumeroIdentificacion { get; set; } = null!;
    public string Usuario { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int IntentosFallidos { get; set; } = 0;
    public bool Bloqueado { get; set; } = false;
    public string Estado { get; set; } = "Activo";
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public DateTime? FechaUltimoLogin { get; set; }

    // Propiedades de navegación
    public Empleado? Empleado { get; set; }
    public ICollection<UserRol> Roles { get; set; } = new List<UserRol>();
    public ICollection<UserPermiso> Permisos { get; set; } = new List<UserPermiso>();
    public ICollection<CodigoVerificacion> CodigosVerificacion { get; set; } = new List<CodigoVerificacion>();
    public ICollection<HistorialEventoUsuario> Eventos { get; set; } = new List<HistorialEventoUsuario>();
}
