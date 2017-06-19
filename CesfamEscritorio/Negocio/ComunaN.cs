using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ComunaN
    {
        public Modelo.Comuna cargarComuna(decimal idComuna)
        {
            try
            {
                Modelo.Comuna c = new Modelo.Comuna();
                Datos.ComunaDB cdb = new Datos.ComunaDB();
                c = cdb.obtenerComuna(idComuna);
                if (c != null)
                {
                    return c;
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

        public List<Modelo.Comuna> listarComunas()
        {
            try
            {
                Datos.ComunaDB cdb = new Datos.ComunaDB();
                List<Modelo.Comuna> comunas = cdb.listarComunas();
                if (comunas != null)
                {
                    return comunas;
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
    }
}
