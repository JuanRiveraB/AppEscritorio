using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ComunaDB
    {
        public Modelo.Comuna obtenerComuna(decimal idComuna)
        {
            Modelo.Comuna c = new Modelo.Comuna();
            try
            {
                string sqlSelect = "SELECT * FROM COMUNA WHERE ID_COMUNA = " + idComuna;
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idComuna = Convert.ToDecimal(dr["ID_COMUNA"]);
                    c.nombre = dr["NOMBRE"].ToString();
                }
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Modelo.Comuna> listarComunas()
        {
            try
            {
                string sqlSelect = "SELECT * FROM COMUNA";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Modelo.Comuna> comunas = new List<Modelo.Comuna>();
                while (dr.Read())
                {
                    Modelo.Comuna c = new Modelo.Comuna();
                    c.idComuna = Convert.ToDecimal(dr["ID_COMUNA"]);
                    c.nombre = dr["NOMBRE"].ToString();
                    comunas.Add(c);
                }
                return comunas;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
