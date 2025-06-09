namespace SGE.Domain.Auth.Entities;

public class CodigoVerificacion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public DateTime Expiracion { get; set; }
        public bool Usado { get; set; } = false;

        public Users? Usuario { get; set; }
    }