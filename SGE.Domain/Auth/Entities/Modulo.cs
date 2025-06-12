namespace SGE.Domain.Auth.Entities;

public class modulos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string? descripcion { get; set; }
        public int? padre_id { get; set; }
        public int orden { get; set; }
        public string? icono { get; set; }

        public modulos? padre { get; set; }
        public ICollection<modulos> submodulos { get; set; }
        public ICollection<permisos> permisos { get; set; }
        public ICollection<modulos_activados> modulos_activados { get; set; }
    }
