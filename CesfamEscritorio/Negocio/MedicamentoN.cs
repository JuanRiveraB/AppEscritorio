using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MedicamentoN
    {
        public List<Entidades.Medicamento> listarTodos()
        {
            try
            {
                Datos.MedicamentoDB mdb = new Datos.MedicamentoDB();
                if (mdb.obtenerPresTodas() != null)
                {
                    return mdb.obtenerPresTodas();
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

        public Entidades.Medicamento obtenerMedicamento(decimal idMed)
        {
            try
            {
                Datos.MedicamentoDB mdb = new Datos.MedicamentoDB();
                Entidades.Medicamento m = new Entidades.Medicamento();
                m = mdb.obtenerMedicamento(idMed);
                if (m != null)
                {
                    return m;
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

        public bool modificarMedicamento(Entidades.Medicamento m)
        {
            try
            {
                Datos.MedicamentoDB mdb = new Datos.MedicamentoDB();
                if (mdb.modificarMed(m))
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

        public bool agregarMedicamento(Entidades.Medicamento m)
        {
            try
            {
                Datos.MedicamentoDB mdb = new Datos.MedicamentoDB();
                if (mdb.AgregarMed(m))
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

        public bool consularSiExiste(Entidades.Medicamento med)
        {
            try
            {
                Datos.MedicamentoDB mdb = new Datos.MedicamentoDB();
                if (mdb.consultarSiExiste(med) == true)
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
