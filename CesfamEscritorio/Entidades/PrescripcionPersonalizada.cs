using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PrescripcionPersonalizada
    {
        public decimal idPrescripcion { get; set; }
        public string rutPersona { get; set; }
        public string nomPersona { get; set; }
        public string diagnostico { get; set; }
        public DateTime? fechaPres { get; set; }
        public decimal cantidad { get; set; }
        public string rutMedico { get; set; }
        public string nomMedico { get; set; }
        public string nomMedicamento { get; set; }
        public string formaFarmaceutica { get; set; }
        public string estado { get; set; }

        public PrescripcionPersonalizada()
        {
            Init();
        }

        private void Init()
        {
            idPrescripcion = 0;
            rutPersona = "";
            nomPersona = "";
            diagnostico = "";
            cantidad = 0;
            rutMedico = "";
            nomMedico = "";
            nomMedicamento = "";
            formaFarmaceutica = "";
            estado = "";
        }
    }
}