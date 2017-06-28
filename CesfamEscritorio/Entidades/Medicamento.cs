using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medicamento
    {
        public decimal idMedicamento { get; set; }
        public string nombreComercial { get; set; }
        public string laboratorio { get; set; }
        public string ean13 { get; set; }
        public string formaFarmaceutica { get; set; }
        public decimal stock { get; set; }
        public decimal idSucursal { get; set; }

        public Medicamento()
        {
            Init();
        }

        private void Init()
        {
            idMedicamento = 0;
            nombreComercial = "";
            laboratorio = "";
            ean13 = "";
            formaFarmaceutica = "";
            stock = 0;
            idSucursal = 0;
        }
    }
}
