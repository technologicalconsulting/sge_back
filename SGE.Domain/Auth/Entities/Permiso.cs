namespace SGE.Domain.Auth.Entities;

public class Permiso
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        public string Accion { get; set; } = null!;
        public string? Descripcion { get; set; }

        public Modulo? Modulo { get; set; }
        public ICollection<RolPermiso> Roles { get; set; } = new List<RolPermiso>();
        public ICollection<UserPermiso> Usuarios { get; set; } = new List<UserPermiso>();
    }