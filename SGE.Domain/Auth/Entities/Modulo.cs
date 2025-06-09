namespace SGE.Domain.Auth.Entities;


public class Modulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int? PadreId { get; set; }
        public int Orden { get; set; } = 0;
        public string? Icono { get; set; }

        public Modulo? Padre { get; set; }
        public ICollection<Modulo> Submodulos { get; set; } = new List<Modulo>();
        public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
    }