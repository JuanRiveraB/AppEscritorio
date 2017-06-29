using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ControlStockDB
    {
        public bool insertarCS(Entidades.ControlStock cs)
        {
            try
            {
                string sqlInsert = "INSERT INTO CONTROLSTOCK (ID_CS, NOMBRE, DESCRIPCION, FECHA, CANTIDAD, ID_MEDICAMENTO, USUARIO_ID) "
                                 + "VALUES (SEQ_CONTROLSTOCK.NEXTVAL, '"+ cs.nombre +"', '"+ cs.descripcion + "', TO_DATE('"+ cs.fecha.ToString() + "','dd/mm/yyyy hh24:mi:ss'), " + cs.cantidad +", "+ cs.idMedicamento +", '"+ cs.idUsuario+"')";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlInsert;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Entidades.ControlStockInforme> obtenerControles()
        {
            try
            {
                string sqlSelect = "SELECT C.ID_CS, C.NOMBRE, C.DESCRIPCION, C.FECHA, C.CANTIDAD, C.USUARIO_ID, U.NOMBRES ||' '|| U.AP_PATERNO AS NOMBRE_FAR, C.ID_MEDICAMENTO, M.NOMBRE_COMERCIAL FROM CONTROLSTOCK C JOIN MEDICAMENTO M ON (C.ID_MEDICAMENTO = M.ID_MCTOS) JOIN USUARIO U ON (C.USUARIO_ID = U.RUT)";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.ControlStockInforme> csList = new List<Entidades.ControlStockInforme>();
                while (dr.Read())
                {
                    Entidades.ControlStockInforme cs = new Entidades.ControlStockInforme();
                    cs.idControl = Convert.ToDecimal(dr["ID_CS"]);
                    cs.nombre = dr["NOMBRE"].ToString();
                    cs.descrip = dr["DESCRIPCION"].ToString();
                    try
                    {
                        cs.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    }
                    catch (Exception)
                    {
                        cs.fecha = null;
                    }
                    cs.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    cs.rutFar = dr["USUARIO_ID"].ToString();
                    cs.nombres = dr["NOMBRE_FAR"].ToString();
                    cs.idMedica = Convert.ToDecimal(dr["ID_MEDICAMENTO"]);
                    cs.nombreMed = dr["NOMBRE_COMERCIAL"].ToString();
                    csList.Add(cs);
                }
                return csList;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
