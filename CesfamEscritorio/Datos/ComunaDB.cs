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
        public Entidades.Comuna obtenerComuna(decimal idComuna)
        {
            Entidades.Comuna c = new Entidades.Comuna();
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

        public List<Entidades.Comuna> listarComunas()
        {
            try
            {
                string sqlSelect = "SELECT * FROM COMUNA";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.Comuna> comunas = new List<Entidades.Comuna>();
                while (dr.Read())
                {
                    Entidades.Comuna c = new Entidades.Comuna();
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
