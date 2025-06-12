using SGE.Domain.Empleados.Entities;

namespace SGE.Domain.Clientes.Entities;

 public class clientes
    {
        public int id { get; set; }
        public string razon_social { get; set; }
        public string? nombre_comercial { get; set; }
        public string ruc { get; set; }
        public string tipo_empresa { get; set; }
        public string? sector { get; set; }
        public string direccion { get; set; }
        public string? ciudad { get; set; }
        public string pais { get; set; }
        public string? sitio_web { get; set; }
        public DateTime fecha_registro { get; set; }
        public string estado { get; set; }
        public decimal limite_credito { get; set; }
        public string? notas { get; set; }

        public ICollection<empleado_cliente> empleados_clientes { get; set; }
        public ICollection<contactos_clientes> contactos { get; set; }
    }