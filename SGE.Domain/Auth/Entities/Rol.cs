namespace SGE.Domain.Auth.Entities;


    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public ICollection<UserRol> Users { get; set; } = new List<UserRol>();
        public ICollection<RolPermiso> Permisos { get; set; } = new List<RolPermiso>();
    }