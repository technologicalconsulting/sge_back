namespace SGE.Domain.Auth.Entities;


 public class HistorialEventoUsuario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string TipoEvento { get; set; } = null!;
        public DateTime FechaEvento { get; set; } = DateTime.UtcNow;
        public string? Ip { get; set; }
        public string? Navegador { get; set; }
        public bool? Exito { get; set; }
        public string? Razon { get; set; }
        public string? Motivo { get; set; }
        public DateTime? FechaCambio { get; set; }

        public Users? Usuario { get; set; }
    }
