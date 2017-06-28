using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class UsuarioDB
    {
        public bool validarUsuario(string rut, string pass)
        {
            Entidades.Usuario u = new Entidades.Usuario();
            try
            {
                string sqlSelect = "SELECT * FROM USUARIO WHERE RUT = '" + rut + "' AND CONTRASENA = '" + pass + "'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    u.rutUsuario = dr["RUT"].ToString();
                    u.nombres = dr["NOMBRES"].ToString();
                    u.contrasena = dr["CONTRASENA"].ToString();
                }
                if (u.rutUsuario != String.Empty || u.rutUsuario != "")
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Entidades.Usuario obtenerDatos(string rut)
        {
            Entidades.Usuario u = new Entidades.Usuario();
            try
            {
                string sqlSelect = "SELECT * FROM USUARIO WHERE RUT = '" + rut + "'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    u.rutUsuario = dr["RUT"].ToString();
                    u.nombres = dr["NOMBRES"].ToString();
                    u.apellidoPaterno = dr["AP_PATERNO"].ToString();
                    u.apellidoMaterno = dr["AP_MATERNO"].ToString();
                    u.sexo = dr["SEXO"].ToString();
                    u.fechaNacimiento = Convert.ToDateTime(dr["FECHA_NAC"]);
                    u.email = dr["EMAIL"].ToString();
                    u.cel = Convert.ToDecimal(dr["CEL"]);
                    u.direccion = dr["DIRECCION"].ToString();
                    u.comunaId = Convert.ToDecimal(dr["COMUNA_ID"]);
                    u.contrasena = dr["CONTRASENA"].ToString();
                    u.nivelId = Convert.ToDecimal(dr["NIVEL_ID"]);
                }
                return u;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool modificarUsuario(Entidades.Usuario usuario)
        {
            try
            {
                string sqlSelect = "UPDATE USUARIO SET CEL= " + usuario.cel + ", EMAIL= '" + usuario.email + "' WHERE RUT = '" + usuario.rutUsuario + "'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
