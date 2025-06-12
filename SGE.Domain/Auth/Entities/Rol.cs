namespace SGE.Domain.Auth.Entities;

public class roles
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string? descripcion { get; set; }

        public ICollection<users_roles> users_roles { get; set; }
        public ICollection<roles_permisos> roles_permisos { get; set; }
    }
