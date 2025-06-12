using SGE.Domain.Empresa.Entities;

namespace SGE.Domain.Empleados.Entities;

 public class empleado
    {
        public int id { get; set; }
        public string primer_nombre { get; set; }
        public string? segundo_nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string? apellido_materno { get; set; }
        public string tipo_documento { get; set; }
        public string numero_identificacion { get; set; }
        public string email_personal { get; set; }
        public string? email_corporativo { get; set; }
        public string? telefono { get; set; }
        public string? direccion { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public string? genero { get; set; }
        public string estado { get; set; }
        public DateTime fecha_registro { get; set; }

        public ICollection<empresa> empresas { get; set; }
        public ICollection<empleado_cliente> empleados_clientes { get; set; }
        public ICollection<informacion_laboral_empleado> informacion_laboral { get; set; }
    }
