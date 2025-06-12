namespace SGE.Domain.Auth.Entities;

public class users_roles
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int rol_id { get; set; }

        public users usuario { get; set; }
        public roles rol { get; set; }
    }
