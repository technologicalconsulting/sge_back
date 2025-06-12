using SGE.Domain.Empleados.Entities;


namespace SGE.Domain.Empresa.Entities;
    public class empresa
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
        public string? telefono { get; set; }
        public string email { get; set; }
        public string? sitio_web { get; set; }
        public string? logo_url { get; set; }
        public DateTime? fecha_fundacion { get; set; }
        public string estado { get; set; }
        public DateTime fecha_registro { get; set; }
        public string? notas { get; set; }
        public int? representante_legal_id { get; set; }

        public empleado? representante_legal { get; set; }
    }