using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Paciente
    {
        public string rutPaciente { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string sexo { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string email { get; set; }
        public decimal cel { get; set; }
        public string direccion { get; set; }
        public decimal comunaId { get; set; }

        public Paciente()
        {
            Init();
        }

        private void Init()
        {
            rutPaciente = "";
            nombres = "";
            apellidoPaterno = "";
            apellidoMaterno = "";
            sexo = "F";
            email = "";
            cel = 0;
            direccion = "";
            comunaId = 0;
        }
    }
}
