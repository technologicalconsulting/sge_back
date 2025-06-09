namespace SGE.Domain.Auth.Entities;


public class ModuloActivado
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaActivacion { get; set; } = DateTime.UtcNow;

        public Modulo? Modulo { get; set; }
    }