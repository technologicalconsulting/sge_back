using SGE.Domain.Empleados.Entities;
using SGE.Domain.Empresa.Entities;
using SGE.Domain.Clientes.Entities;

namespace SGE.Domain.Empleados.Entities;
public class informacion_laboral_empleado
    {
        public int id { get; set; }
        public int empleado_id { get; set; }
        public int? departamento_id { get; set; }
        public int? cargo_id { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public DateTime? fecha_salida { get; set; }
        public decimal? salario { get; set; }
        public string? tipo_contrato { get; set; }
        public int? supervisor_interno_id { get; set; }
        public int? supervisor_externo_id { get; set; }
        public string? notas { get; set; }

        public empleado? empleado { get; set; }
        public departamentos? departamento { get; set; }
        public cargos? cargo { get; set; }
        public empleado? supervisor_interno { get; set; }
        public clientes? supervisor_externo { get; set; }
    }
