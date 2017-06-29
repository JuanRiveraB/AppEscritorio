using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PrescripcionAviso
    {
        public decimal idPres { get; set; }
        public string estado { get; set; }
        public decimal cantidad { get; set; }
        public decimal idMedica { get; set; }
        public decimal stock { get; set; }
        public string email { get; set; }

        public PrescripcionAviso()
        {
            Init();
        }

        private void Init()
        {
            idPres = 0;
            estado = "";
            cantidad = 0;
            idMedica = 0;
            stock = 0;
            email = "";
        }
    }
}
