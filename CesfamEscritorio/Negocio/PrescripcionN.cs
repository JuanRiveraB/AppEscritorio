using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PrescripcionN
    {
        public List<Entidades.PrescripcionPersonalizada> listarPresEmitidos(string rut)
        {
            try
            {
                Datos.PrescripcionDB pdb = new Datos.PrescripcionDB();
                List<Entidades.PrescripcionPersonalizada> pres = pdb.obtenerPresEmitidas(rut);
                if (pres != null)
                {
                    return pres;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Entidades.PrescripcionPersonalizada> listarPresReservar(string rut)
        {
            try
            {
                Datos.PrescripcionDB pdb = new Datos.PrescripcionDB();
                List<Entidades.PrescripcionPersonalizada> pres = pdb.obtenerPresReservadas(rut);
                if (pres != null)
                {
                    return pres;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Entidades.PrescripcionPersonalizada> listarPres(string estado)
        {
            try
            {
                Datos.PrescripcionDB pdb = new Datos.PrescripcionDB();
                List<Entidades.PrescripcionPersonalizada> pres = null;
                if (estado.Trim().Equals(string.Empty))
                {
                    pres = pdb.obtenerPresTodas();
                    return pres;
                }
                else if (estado.Trim() != string.Empty)
                {
                    pres = pdb.obtenerPresPorEstado(estado);
                    return pres;
                }else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Entidades.Prescripcion obtenerPres(decimal idPres)
        {
            try
            {
                Datos.PrescripcionDB pdb = new Datos.PrescripcionDB();
                Entidades.Prescripcion p = new Entidades.Prescripcion();
                p = pdb.obtenerPrescrip(idPres);
                if (p != null)
                {
                    return p;
                }else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool modificarPres(Entidades.Prescripcion p)
        {
            try
            {
                Datos.PrescripcionDB pdb = new Datos.PrescripcionDB();
                return pdb.modificarPres(p);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Entidades.PrescripcionAviso> obtenerReservadas(decimal idMed)
        {
            try
            {
                Entidades.PrescripcionAviso pv = new Entidades.PrescripcionAviso();
                Datos.PrescripcionDB pdb = new Datos.PrescripcionDB();
                return pdb.obtenerPresReservadasAviso(idMed);

            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Entidades.PrescripcionAviso> obtenerReservadasAviso()
        {
            try
            {
                Entidades.PrescripcionAviso pv = new Entidades.PrescripcionAviso();
                Datos.PrescripcionDB pdb = new Datos.PrescripcionDB();
                return pdb.obtenerCorreosReserva();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
