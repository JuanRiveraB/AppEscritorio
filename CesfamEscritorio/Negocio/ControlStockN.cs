using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControlStockN
    {
        public bool insertarCS(Entidades.ControlStock cs)
        {
            try
            {
                Datos.ControlStockDB cdb = new Datos.ControlStockDB();
                if (cdb.insertarCS(cs))
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
