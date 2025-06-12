namespace SGE.Domain.Auth.Entities;

public class historial_eventos_usuario
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public string tipo_evento { get; set; }
        public DateTime fecha_evento { get; set; }
        public string? ip { get; set; }
        public string? navegador { get; set; }
        public bool? exito { get; set; }
        public string? razon { get; set; }
        public string? motivo { get; set; }
        public DateTime? fecha_cambio { get; set; }

        public users usuario { get; set; }
    }