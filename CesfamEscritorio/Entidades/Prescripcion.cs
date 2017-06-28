using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Prescripcion
    {
        public decimal idPrescripcion { get; set; }
        public string diagnostico { get; set; }
        public string estado { get; set; }
        public DateTime? fechaPrescip { get; set; }
        public decimal cantidad { get; set; }
        public string idPersona { get; set; }
        public string idMedico { get; set; }
        public decimal idMedicamento { get; set; }

        public Prescripcion()
        {
            Init();
        }

        private void Init()
        {
            idPrescripcion = 0;
            diagnostico = "";
            estado = "";
            cantidad = 0;
            idPersona = "";
            idMedico = "";
            idMedicamento = 0;
        }
    }
}
