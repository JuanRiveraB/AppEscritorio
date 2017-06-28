using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ControlStock
    {
        public decimal idControl { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public decimal cantidad { get; set; }
        public decimal idMedicamento { get; set; }
        public string idUsuario { get; set; }
        
        public ControlStock()
        {
            Init();
        }

        private void Init()
        {
            idControl = 0;
            nombre = "";
            descripcion = "";
            fecha = System.DateTime.Now;
            cantidad = 0;
            idMedicamento = 0;
            idUsuario = "";
        }
    }
}
