using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class PrescripcionDB
    {
        public List<Entidades.PrescripcionPersonalizada> obtenerPresEmitidas(string rutPer)
        {
            try
            {
                string sqlSelect = "SELECT PR.ID_PS, PE.RUT AS RUT_RETIRA, PE.NOMBRES ||' '|| PE.APELLIDOS AS NOMBRE_COMPLETO, PR.DIAGNOSTICO, PR.FECHA_PRES, PR.CANTIDAD, M.RUT AS RUT_MEDICO, M.NOMBRES || ' ' || M.AP_PATERNO AS NOMBRE_MEDICO, ME.NOMBRE_COMERCIAL, ME.FORMA_FARMACEUTICA, PR.ESTADO FROM PRESCRIPCION PR JOIN PERSONA PE ON (PR.PERSONA_ID = PE.RUT) JOIN MEDICO M ON (PR.MEDICO_ID = M.RUT) JOIN MEDICAMENTO ME ON (PR.MEDICAMENTO_ID = ME.ID_MCTOS) WHERE PE.RUT = '"+ rutPer + "' AND PR.ESTADO = 'Emitido'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.PrescripcionPersonalizada> ppList = new List<Entidades.PrescripcionPersonalizada>();
                while (dr.Read())
                {
                    Entidades.PrescripcionPersonalizada pp = new Entidades.PrescripcionPersonalizada();
                    pp.idPrescripcion = Convert.ToDecimal(dr["ID_PS"]);
                    pp.rutPersona = dr["RUT_RETIRA"].ToString();
                    pp.nomPersona = dr["NOMBRE_COMPLETO"].ToString();
                    pp.diagnostico = dr["DIAGNOSTICO"].ToString();
                    try
                    {
                        pp.fechaPres = Convert.ToDateTime(dr["FECHA_PRES"].ToString());
                    }
                    catch (Exception)
                    {
                        pp.fechaPres = null;
                    }
                    pp.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    pp.rutMedico = dr["RUT_MEDICO"].ToString();
                    pp.nomMedico = dr["NOMBRE_MEDICO"].ToString();
                    pp.nomMedicamento = dr["NOMBRE_COMERCIAL"].ToString();
                    pp.formaFarmaceutica = dr["FORMA_FARMACEUTICA"].ToString();
                    pp.estado = dr["ESTADO"].ToString();
                    ppList.Add(pp);
                }
                return ppList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Entidades.PrescripcionPersonalizada> obtenerPresReservadas(string rutPer)
        {
            try
            {
                string sqlSelect = "SELECT PR.ID_PS, PE.RUT AS RUT_RETIRA, PE.NOMBRES ||' '|| PE.APELLIDOS AS NOMBRE_COMPLETO, PR.DIAGNOSTICO, PR.FECHA_PRES, PR.CANTIDAD, M.RUT AS RUT_MEDICO, M.NOMBRES || ' ' || M.AP_PATERNO AS NOMBRE_MEDICO, ME.NOMBRE_COMERCIAL, ME.FORMA_FARMACEUTICA, PR.ESTADO FROM PRESCRIPCION PR JOIN PERSONA PE ON (PR.PERSONA_ID = PE.RUT) JOIN MEDICO M ON (PR.MEDICO_ID = M.RUT) JOIN MEDICAMENTO ME ON (PR.MEDICAMENTO_ID = ME.ID_MCTOS) WHERE PE.RUT = '" + rutPer + "' AND PR.ESTADO = 'Reservar'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.PrescripcionPersonalizada> ppList = new List<Entidades.PrescripcionPersonalizada>();
                while (dr.Read())
                {
                    Entidades.PrescripcionPersonalizada pp = new Entidades.PrescripcionPersonalizada();
                    pp.idPrescripcion = Convert.ToDecimal(dr["ID_PS"]);
                    pp.rutPersona = dr["RUT_RETIRA"].ToString();
                    pp.nomPersona = dr["NOMBRE_COMPLETO"].ToString();
                    pp.diagnostico = dr["DIAGNOSTICO"].ToString();
                    try
                    {
                        pp.fechaPres = Convert.ToDateTime(dr["FECHA_PRES"].ToString());
                    }
                    catch (Exception)
                    {
                        pp.fechaPres = null;
                    }
                    pp.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    pp.rutMedico = dr["RUT_MEDICO"].ToString();
                    pp.nomMedico = dr["NOMBRE_MEDICO"].ToString();
                    pp.nomMedicamento = dr["NOMBRE_COMERCIAL"].ToString();
                    pp.formaFarmaceutica = dr["FORMA_FARMACEUTICA"].ToString();
                    pp.estado = dr["ESTADO"].ToString();
                    ppList.Add(pp);
                }
                return ppList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Entidades.PrescripcionPersonalizada> obtenerPresTodas()
        {
            try
            {
                string sqlSelect = "SELECT PR.ID_PS, PE.RUT AS RUT_RETIRA, PE.NOMBRES ||' '|| PE.APELLIDOS AS NOMBRE_COMPLETO, PR.DIAGNOSTICO, PR.FECHA_PRES, PR.CANTIDAD, M.RUT AS RUT_MEDICO, M.NOMBRES || ' ' || M.AP_PATERNO AS NOMBRE_MEDICO, ME.NOMBRE_COMERCIAL, ME.FORMA_FARMACEUTICA, PR.ESTADO FROM PRESCRIPCION PR JOIN PERSONA PE ON (PR.PERSONA_ID = PE.RUT) JOIN MEDICO M ON (PR.MEDICO_ID = M.RUT) JOIN MEDICAMENTO ME ON (PR.MEDICAMENTO_ID = ME.ID_MCTOS)";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.PrescripcionPersonalizada> ppList = new List<Entidades.PrescripcionPersonalizada>();
                while (dr.Read())
                {
                    Entidades.PrescripcionPersonalizada pp = new Entidades.PrescripcionPersonalizada();
                    pp.idPrescripcion = Convert.ToDecimal(dr["ID_PS"]);
                    pp.rutPersona = dr["RUT_RETIRA"].ToString();
                    pp.nomPersona = dr["NOMBRE_COMPLETO"].ToString();
                    pp.diagnostico = dr["DIAGNOSTICO"].ToString();
                    try
                    {
                        pp.fechaPres = Convert.ToDateTime(dr["FECHA_PRES"].ToString());
                    }
                    catch (Exception)
                    {
                        pp.fechaPres = null;
                    }
                    pp.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    pp.rutMedico = dr["RUT_MEDICO"].ToString();
                    pp.nomMedico = dr["NOMBRE_MEDICO"].ToString();
                    pp.nomMedicamento = dr["NOMBRE_COMERCIAL"].ToString();
                    pp.formaFarmaceutica = dr["FORMA_FARMACEUTICA"].ToString();
                    pp.estado = dr["ESTADO"].ToString();
                    ppList.Add(pp);
                }
                return ppList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Entidades.PrescripcionPersonalizada> obtenerPresPorEstado(string estado)
        {
            try
            {
                string sqlSelect = "SELECT PR.ID_PS, PE.RUT AS RUT_RETIRA, PE.NOMBRES ||' '|| PE.APELLIDOS AS NOMBRE_COMPLETO, PR.DIAGNOSTICO, PR.FECHA_PRES, PR.CANTIDAD, M.RUT AS RUT_MEDICO, M.NOMBRES || ' ' || M.AP_PATERNO AS NOMBRE_MEDICO, ME.NOMBRE_COMERCIAL, ME.FORMA_FARMACEUTICA, PR.ESTADO FROM PRESCRIPCION PR JOIN PERSONA PE ON (PR.PERSONA_ID = PE.RUT) JOIN MEDICO M ON (PR.MEDICO_ID = M.RUT) JOIN MEDICAMENTO ME ON (PR.MEDICAMENTO_ID = ME.ID_MCTOS) WHERE PR.ESTADO = '" + estado + "'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.PrescripcionPersonalizada> ppList = new List<Entidades.PrescripcionPersonalizada>();
                while (dr.Read())
                {
                    Entidades.PrescripcionPersonalizada pp = new Entidades.PrescripcionPersonalizada();
                    pp.idPrescripcion = Convert.ToDecimal(dr["ID_PS"]);
                    pp.rutPersona = dr["RUT_RETIRA"].ToString();
                    pp.nomPersona = dr["NOMBRE_COMPLETO"].ToString();
                    pp.diagnostico = dr["DIAGNOSTICO"].ToString();
                    try
                    {
                        pp.fechaPres = Convert.ToDateTime(dr["FECHA_PRES"].ToString());
                    }
                    catch (Exception)
                    {
                        pp.fechaPres = null;
                    }
                    pp.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    pp.rutMedico = dr["RUT_MEDICO"].ToString();
                    pp.nomMedico = dr["NOMBRE_MEDICO"].ToString();
                    pp.nomMedicamento = dr["NOMBRE_COMERCIAL"].ToString();
                    pp.formaFarmaceutica = dr["FORMA_FARMACEUTICA"].ToString();
                    pp.estado = dr["ESTADO"].ToString();
                    ppList.Add(pp);
                }
                return ppList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Entidades.Prescripcion obtenerPrescrip(decimal idPres)
        {
            Entidades.Prescripcion p = new Entidades.Prescripcion();
            try
            {
                string sqlSelect = "SELECT * FROM PRESCRIPCION WHERE ID_PS = " + idPres;
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    p.idPrescripcion = Convert.ToDecimal(dr["ID_PS"]);
                    p.diagnostico = dr["DIAGNOSTICO"].ToString();
                    p.estado = dr["ESTADO"].ToString();
                    try
                    {
                        p.fechaPrescip = Convert.ToDateTime(dr["FECHA_PRES"].ToString());
                    }
                    catch (Exception)
                    {
                        p.fechaPrescip = null;
                    }
                    p.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    p.idPersona = dr["PERSONA_ID"].ToString();
                    p.idMedico = dr["MEDICO_ID"].ToString();
                    p.idMedicamento = Convert.ToDecimal(dr["MEDICAMENTO_ID"].ToString());
                }
                return p;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool modificarPres(Entidades.Prescripcion pres)
        {
            try
            {
                string sqlSelect = "UPDATE PRESCRIPCION SET ESTADO = '" + pres.estado + "' WHERE ID_PS = " + pres.idPrescripcion;
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

        public List<Entidades.PrescripcionAviso> obtenerPresReservadasAviso(decimal idMed)
        {
            try
            {
                string sqlSelect = "SELECT P.ID_PS, P.ESTADO, P.CANTIDAD, M.ID_MCTOS, M.STOCK, PER.EMAIL FROM PRESCRIPCION P JOIN MEDICAMENTO M ON (P.MEDICAMENTO_ID = M.ID_MCTOS) JOIN PERSONA PER ON (P.PERSONA_ID = PER.RUT) WHERE M.ID_MCTOS = "+ idMed +" AND P.ESTADO = 'Reservar'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.PrescripcionAviso> ppList = new List<Entidades.PrescripcionAviso>();
                while (dr.Read())
                {
                    Entidades.PrescripcionAviso pp = new Entidades.PrescripcionAviso();
                    pp.idPres = Convert.ToDecimal(dr["ID_PS"]);
                    pp.estado = dr["ESTADO"].ToString();
                    pp.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    pp.idMedica = Convert.ToDecimal(dr["ID_MCTOS"]);
                    pp.stock = Convert.ToDecimal(dr["STOCK"]);
                    try
                    {
                        pp.email = dr["EMAIL"].ToString();
                    }
                    catch (Exception)
                    {
                        pp.email = "";
                    }
                    ppList.Add(pp);
                }
                return ppList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Entidades.PrescripcionAviso> obtenerCorreosReserva()
        {
            try
            {
                string sqlSelect = "SELECT P.ID_PS, P.ESTADO, P.CANTIDAD, M.ID_MCTOS, M.STOCK, PER.EMAIL FROM PRESCRIPCION P JOIN MEDICAMENTO M ON (P.MEDICAMENTO_ID = M.ID_MCTOS) JOIN PERSONA PER ON (P.PERSONA_ID = PER.RUT) WHERE P.ESTADO = 'Reservar'";
                OracleCommand cmd = Datos.Conexion.conectar().CreateCommand();
                cmd.CommandText = sqlSelect;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<Entidades.PrescripcionAviso> ppList = new List<Entidades.PrescripcionAviso>();
                while (dr.Read())
                {
                    Entidades.PrescripcionAviso pp = new Entidades.PrescripcionAviso();
                    pp.idPres = Convert.ToDecimal(dr["ID_PS"]);
                    pp.estado = dr["ESTADO"].ToString();
                    pp.cantidad = Convert.ToDecimal(dr["CANTIDAD"]);
                    pp.idMedica = Convert.ToDecimal(dr["ID_MCTOS"]);
                    pp.stock = Convert.ToDecimal(dr["STOCK"]);
                    try
                    {
                        pp.email = dr["EMAIL"].ToString();
                    }
                    catch (Exception)
                    {
                        pp.email = "";
                    }
                    ppList.Add(pp);
                }
                return ppList;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
