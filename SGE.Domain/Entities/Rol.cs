public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string? Descripcion { get; set; }

    public ICollection<UserRol> Usuarios { get; set; } = new List<UserRol>();
}
