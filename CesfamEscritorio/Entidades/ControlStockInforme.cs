using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ControlStockInforme
    {
        public decimal idControl { get; set; }
        public string nombre { get; set; }
        public string descrip { get; set; }
        public DateTime? fecha { get; set; }
        public decimal cantidad { get; set; }
        public string rutFar { get; set; }
        public string nombres { get; set; }
        public decimal idMedica { get; set; }
        public string nombreMed { get; set; }
        
        public ControlStockInforme()
        {
            Init();
        }

        private void Init()
        {
            idControl = 0;
            nombre = "";
            descrip = "";
            fecha = System.DateTime.Now;
            cantidad = 0;
            rutFar = "";
            nombreMed = "";
            idMedica = 0;
            nombreMed = "";
        }
    }
}
