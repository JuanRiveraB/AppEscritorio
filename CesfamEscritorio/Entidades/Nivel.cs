using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Nivel
    {
        public decimal idNivel { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public Nivel()
        {
            Init();
        }

        private void Init()
        {
            idNivel = 0;
            nombre = "";
            descripcion = "";
        }
    }
}
