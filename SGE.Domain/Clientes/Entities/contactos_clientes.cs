using SGE.Domain.Clientes.Entities;

namespace SGE.Domain.Clientes.Entities;
public class contactos_clientes
    {
        public int id { get; set; }
        public int cliente_id { get; set; }
        public string nombre { get; set; }
        public string? cargo { get; set; }
        public string? telefono { get; set; }
        public string email { get; set; }
        public string? notas { get; set; }

        public clientes cliente { get; set; }
    }