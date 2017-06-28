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
    }
}
