using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Medico
    {
        public string rutMedico { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string sexo { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string email { get; set; }
        public decimal cel { get; set; }
        public string direccion { get; set; }
        public decimal comunaId { get; set; }
        public string especialidad { get; set; }
        public string password { get; set; }

        public Medico()
        {
            Init();
        }

        private void Init()
        {
            rutMedico = "";
            nombres = "";
            apellidoPaterno = "";
            apellidoMaterno = "";
            sexo = "";
            fechaNacimiento = System.DateTime.Now;
            email = "";
            cel = 0;
            direccion = "";
            comunaId = 0;
            especialidad = "";
            password = "";
        }
    }
}
