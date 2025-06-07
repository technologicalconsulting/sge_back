public class UserRol
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public User Usuario { get; set; } = null!;

    public int RolId { get; set; }
    public Rol Rol { get; set; } = null!;
}
