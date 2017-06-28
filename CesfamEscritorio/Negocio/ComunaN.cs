using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ComunaN
    {
        public Entidades.Comuna cargarComuna(decimal idComuna)
        {
            try
            {
                Entidades.Comuna c = new Entidades.Comuna();
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

        public List<Entidades.Comuna> listarComunas()
        {
            try
            {
                Datos.ComunaDB cdb = new Datos.ComunaDB();
                List<Entidades.Comuna> comunas = cdb.listarComunas();
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
