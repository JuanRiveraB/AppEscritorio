using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Medicamento
    {
        public decimal idMedicamento { get; set; }
        public string nombreComercial { get; set; }
        public string laboratorio { get; set; }
        public decimal ean13 { get; set; }
        public decimal stock { get; set; }
        public string indicaciones { get; set; }
        public decimal idFarmacia { get; set; }

        public Medicamento()
        {
            Init();
        }

        private void Init()
        {
            idMedicamento = 0;
            nombreComercial = "";
            laboratorio = "";
            ean13 = 0;
            stock = 0;
            indicaciones = "";
            idFarmacia = 0;
        }
    }
}
