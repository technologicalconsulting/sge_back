namespace SGE.Domain.Auth.Entities;

public class roles_permisos
    {
        public int id { get; set; }
        public int rol_id { get; set; }
        public int permiso_id { get; set; }

        public roles rol { get; set; }
        public permisos permiso { get; set; }
    }