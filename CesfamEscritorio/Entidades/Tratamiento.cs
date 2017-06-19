using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Tratamiento
    {
        public decimal idTratamiento { get; set; }
        public string nombre { get; set; }
        public string detalle { get; set; }
        public decimal idCarnetPaciente { get; set; }
        public string rutMedico { get; set; }

        public Tratamiento()
        {
            Init();
        }

        private void Init()
        {
            idTratamiento = 0;
            nombre = "";
            detalle = "";
            idCarnetPaciente = 0;
            rutMedico = "";
        }
    }
}
