namespace SGE.Domain.Auth.Entities;

public class users_permisos
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int permiso_id { get; set; }

        public users usuario { get; set; }
        public permisos permiso { get; set; }
    }

