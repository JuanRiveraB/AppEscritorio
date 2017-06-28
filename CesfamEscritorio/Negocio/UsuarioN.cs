using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioN
    {
        public bool validarUsuario(string rut, string pass)
        {
            try
            {
                Datos.UsuarioDB uDB = new Datos.UsuarioDB();
                if (uDB.validarUsuario(rut, pass) == true)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public string nombreCompleto(string rut)
        {
            try
            {
                Datos.UsuarioDB uDB = new Datos.UsuarioDB();
                Entidades.Usuario u = new Entidades.Usuario();
                u = uDB.obtenerDatos(rut);
                if (u != null)
                {
                    string nombre = u.nombres + " " + u.apellidoPaterno + " " + u.apellidoMaterno;
                    return nombre;
                }
                else
                {
                    return "Problemas al cargar nombre";
                }
            }
            catch (Exception)
            {

                return "Error al cargar, Solicite ayuda";
            }
        }

        public Entidades.Usuario cargarUsuario(string rut)
        {
            try
            {
                Datos.UsuarioDB uDB = new Datos.UsuarioDB();
                Entidades.Usuario u = new Entidades.Usuario();
                u = uDB.obtenerDatos(rut);
                if (u != null)
                {
                    return u;
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

        public bool modificarusuario(Entidades.Usuario usuario)
        {
            try
            {
                Datos.UsuarioDB udb = new Datos.UsuarioDB();
                if (usuario != null)
                {
                    udb.modificarUsuario(usuario);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
