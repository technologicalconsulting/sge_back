namespace SGE.Domain.Auth.Entities;


    public class UserRol
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }

        public Users? Usuario { get; set; }
        public Rol? Rol { get; set; }
    }