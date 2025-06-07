public class User
{
    public int Id { get; set; }
    public int? EmpleadoId { get; set; }
    public string NumeroIdentificacion { get; set; } = "";
    public string Usuario { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public int IntentosFallidos { get; set; } = 0;
    public bool Bloqueado { get; set; } = false;
    public string Estado { get; set; } = "Activo";
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public DateTime? FechaUltimoLogin { get; set; }

    public ICollection<UserRol> Roles { get; set; } = new List<UserRol>();
}
