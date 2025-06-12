namespace SGE.Domain.Auth.Entities;


public class modulos_activados
    {
        public int id { get; set; }
        public int modulo_id { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_activacion { get; set; }

        public modulos modulo { get; set; }
    }