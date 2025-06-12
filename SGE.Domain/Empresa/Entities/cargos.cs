using SGE.Domain.Empleados.Entities;

namespace SGE.Domain.Empresa.Entities;
public class cargos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string? descripcion { get; set; }
        public int? departamento_id { get; set; }

        public departamentos? departamento { get; set; }
        public ICollection<informacion_laboral_empleado> informacion_laboral { get; set; }
    }