namespace SGE.Domain.Auth.Entities;


public class Empleado
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; } = null!;
        public string? SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public string NumeroIdentificacion { get; set; } = null!;
        public string EmailPersonal { get; set; } = null!;
        public string? EmailCorporativo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public string Estado { get; set; } = "Activo";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public Users? Usuario { get; set; }
    }