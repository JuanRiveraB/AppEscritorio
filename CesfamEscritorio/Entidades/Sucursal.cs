using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sucursal
    {
        public decimal idSucursal { get; set; }
        public string direccion { get; set; }
        public decimal telefono { get; set; }
        public decimal idComuna { get; set; }
        
        public Sucursal()
        {
            Init();
        }

        private void Init()
        {
            idSucursal = 0;
            direccion = "";
            telefono = 0;
            idComuna = 0;
        }
    }
}
