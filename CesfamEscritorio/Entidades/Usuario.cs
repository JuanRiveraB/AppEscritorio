using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Usuario
    {
        public string rutUsuario { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string sexo { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string email { get; set; }
        public decimal cel { get; set; }
        public string direccion { get; set; }
        public decimal comunaId { get; set; }
        public string contrasena { get; set; }
        public decimal nivelId { get; set; }

        public Usuario()
        {
            Init();
        }

        private void Init()
        {
            rutUsuario = "";
            nombres = "";
            apellidoPaterno = "";
            apellidoMaterno = "";
            sexo = "";
            fechaNacimiento = System.DateTime.Now;
            email = "";
            cel = 0;
            direccion = "";
            comunaId = 0;
            contrasena = "";
            nivelId = 0;
        }
    }
}
