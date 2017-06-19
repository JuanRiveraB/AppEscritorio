using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DetalleCarnet
    {
        public decimal IdCarnetP { get; set; }
        public string indicacionesMedicas { get; set; }
        public DateTime entregaMedica { get; set; }

        public DetalleCarnet()
        {
            Init();
        }

        private void Init()
        {
            IdCarnetP = 0;
            indicacionesMedicas = "";
            entregaMedica = System.DateTime.Now;
        }
    }
}
