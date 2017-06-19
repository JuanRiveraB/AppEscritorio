using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Formulario
    {
        public decimal idFormulario { get; set; }
        public decimal duracionTratamiento { get; set; }
        public string detalleFrecuencia { get; set; }
        public string estado { get; set; }
        public decimal idMedicamento { get; set; }
        public decimal idTratamiento { get; set; }

        public Formulario()
        {
            Init();
        }

        private void Init()
        {
            idFormulario = 0;
            duracionTratamiento = 0;
            detalleFrecuencia = "";
            estado = "";
            idMedicamento = 0;
            idTratamiento = 0;
        }
    }
}
