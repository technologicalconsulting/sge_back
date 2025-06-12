using SGE.Domain.Clientes.Entities;

namespace SGE.Domain.Empleados.Entities;

public class empleado_cliente
    {
        public int id { get; set; }
        public int empleado_id { get; set; }
        public int cliente_id { get; set; }
        public DateTime fecha_asignacion { get; set; }
        public DateTime? fecha_fin { get; set; }
        public string estado { get; set; }

        public empleado empleado { get; set; }
        public clientes? cliente { get; set; }
    }