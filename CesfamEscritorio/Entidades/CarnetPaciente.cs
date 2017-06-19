using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CarnetPaciente
    {
        public decimal idCp { get; set; }
        public string rutPaciente { get; set; }

        public CarnetPaciente()
        {
            Init();
        }

        private void Init()
        {
            idCp = 0;
            rutPaciente = "";
        }
    }
}
