using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Persona
    {
        public string rut { get; set; }
        public string nombres { get; set; }
        public string email { get; set; }
        public decimal celular { get; set; }

        public Persona()
        {
            Init();
        }

        private void Init()
        {
            rut = "";
            nombres = "";
            email = "";
            celular = 0;
        }
    }
}
