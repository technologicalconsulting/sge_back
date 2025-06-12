namespace SGE.Domain.Auth.Entities;

public class codigos_verificacion
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public string codigo { get; set; }
        public string tipo { get; set; }
        public DateTime fecha_generacion { get; set; }
        public DateTime expiracion { get; set; }
        public bool usado { get; set; }

        public users usuario { get; set; }
    }
