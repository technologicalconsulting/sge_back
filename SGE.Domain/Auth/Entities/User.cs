namespace SGE.Domain.Auth.Entities;


public class users
    {
        public int id { get; set; }
        public int? empleado_id { get; set; }
        public string numero_identificacion { get; set; }
        public string usuario { get; set; }
        public string password_hash { get; set; }
        public int intentos_fallidos { get; set; }
        public bool bloqueado { get; set; }
        public string estado { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime? fecha_ultimo_login { get; set; }

        // Propiedades de navegación
        public ICollection<users_roles> users_roles { get; set; }
        public ICollection<users_permisos> users_permisos { get; set; }
        public ICollection<codigos_verificacion> codigos_verificacion { get; set; }
        public ICollection<historial_eventos_usuario> historial_eventos_usuario { get; set; }
    }