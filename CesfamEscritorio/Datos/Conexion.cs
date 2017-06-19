using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Data;

namespace Datos
{
    public class Conexion
    {
        private static OracleConnection cn { get; set; }
        private static string oradb = "Data Source=(DESCRIPTION="
                            + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))"
                            + "(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XE)));"
                            + "User id=Cesfam;Password=cesfam;";

        public static OracleConnection conectar() {

            try
            {
                if (cn == null)
                {
                    cn = new OracleConnection(oradb);
                    cn.Open();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error de conexión");
            }
            return cn;
        }

        public static void cerrar()
        {

            try
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al cerrar la conexión");
            }
        }

        public Conexion() 
        {
            
        }
    }
}
