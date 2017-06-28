using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class MedicamentoDB
    {
        public List<Entidades.Medicamento> obtenerPresTodas()
        {
            try
            {
                string sqlSelect = "SELECT * FROM MEDICAMENTO";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.Medicamento> mList = new List<Entidades.Medicamento>();
                while (dr.Read())
                {
                    Entidades.Medicamento m = new Entidades.Medicamento();
                    m.idMedicamento = Convert.ToDecimal(dr["ID_MCTOS"]);
                    m.nombreComercial = dr["NOMBRE_COMERCIAL"].ToString();
                    m.laboratorio = dr["LABORATORIO"].ToString();
                    m.ean13 = dr["EAN13"].ToString();
                    m.formaFarmaceutica = dr["FORMA_FARMACEUTICA"].ToString();
                    m.stock = Convert.ToDecimal(dr["STOCK"]);
                    m.idSucursal = Convert.ToDecimal(dr["SUCURSAL_ID"]);
                    mList.Add(m);
                }
                return mList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Entidades.Medicamento obtenerMedicamento(decimal idMed)
        {
            Entidades.Medicamento m = new Entidades.Medicamento();
            try
            {
                string sqlSelect = "SELECT * FROM MEDICAMENTO WHERE ID_MCTOS = " + idMed;
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m.idMedicamento = Convert.ToDecimal(dr["ID_MCTOS"]);
                    m.nombreComercial = dr["NOMBRE_COMERCIAL"].ToString();
                    m.laboratorio = dr["LABORATORIO"].ToString();
                    m.ean13 = dr["EAN13"].ToString();
                    m.formaFarmaceutica = dr["FORMA_FARMACEUTICA"].ToString();
                    m.stock = Convert.ToDecimal(dr["STOCK"]);
                    m.idSucursal = Convert.ToDecimal(dr["SUCURSAL_ID"]);
                }
                return m;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool modificarMed(Entidades.Medicamento med)
        {
            try
            {
                string sqlSelect = "UPDATE MEDICAMENTO SET STOCK = " + med.stock + " WHERE ID_MCTOS = " + med.idMedicamento;
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

        public bool AgregarMed(Entidades.Medicamento med)
        {
            try
            {
                string sqlInsert = "INSERT INTO MEDICAMENTO (ID_MCTOS, NOMBRE_COMERCIAL, LABORATORIO, EAN13, FORMA_FARMACEUTICA, STOCK, SUCURSAL_ID) "
                                 + "VALUES (SEQ_MEDICAMENTO.NEXTVAL, '"+ med.nombreComercial +"', '"+ med.laboratorio+"', '"+ med.ean13 +"', '"+ med.formaFarmaceutica +"', "+ med.stock+", '10000')";
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

        public bool consultarSiExiste(Entidades.Medicamento med)
        {
            try
            {
                Entidades.Medicamento m = new Entidades.Medicamento();
                string sqlInsert = "SELECT * FROM MEDICAMENTO WHERE NOMBRE_COMERCIAL = '"+ med.nombreComercial+"' AND LABORATORIO = '"+ med.laboratorio+"' AND EAN13 = '"+ med.ean13+"' AND FORMA_FARMACEUTICA = '"+ med.formaFarmaceutica +"'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlInsert;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m.idMedicamento = Convert.ToDecimal(dr["ID_MCTOS"]);
                    m.nombreComercial = dr["NOMBRE_COMERCIAL"].ToString();
                    m.laboratorio = dr["LABORATORIO"].ToString();
                    m.ean13 = dr["EAN13"].ToString();
                    m.formaFarmaceutica = dr["FORMA_FARMACEUTICA"].ToString();
                    m.stock = Convert.ToDecimal(dr["STOCK"]);
                    m.idSucursal = Convert.ToDecimal(dr["SUCURSAL_ID"]);
                }
                if (m.nombreComercial.Equals(med.nombreComercial) && m.laboratorio.Equals(med.laboratorio) && m.ean13.Equals(med.ean13) && m.formaFarmaceutica.Equals(med.formaFarmaceutica))
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
    }
}
