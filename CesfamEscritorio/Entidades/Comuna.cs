using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Comuna
    {
        public decimal idComuna { get; set; }
        public string nombre { get; set; }

        public Comuna()
        {
            Init();
        }

        private void Init()
        {
            idComuna = 0;
            nombre = "";
        }
    }
}
