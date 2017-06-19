using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Cargo
    {
        public decimal idCargo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal nivelId { get; set; }

        public Cargo()
        {
            Init();
        }

        private void Init()
        {
            idCargo = 0;
            nombre = "";
            descripcion = "";
            nivelId = 0;
        }
    }
}
