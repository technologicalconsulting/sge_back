namespace SGE.Domain.Auth.Entities;


 public class UserPermiso
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PermisoId { get; set; }

        public Users? Usuario { get; set; }
        public Permiso? Permiso { get; set; }
    }