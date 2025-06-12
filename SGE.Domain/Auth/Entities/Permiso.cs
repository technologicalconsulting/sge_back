namespace SGE.Domain.Auth.Entities;


public class permisos
    {
        public int id { get; set; }
        public int modulo_id { get; set; }
        public string accion { get; set; }
        public string? descripcion { get; set; }

        public modulos modulo { get; set; }
        public ICollection<roles_permisos> roles_permisos { get; set; }
        public ICollection<users_permisos> users_permisos { get; set; }
    }
