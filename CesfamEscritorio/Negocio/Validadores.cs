using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class Validadores
    {
        public bool ValidaRut(string rut)
        {
            try
            {
                rut = rut.Replace(".", "").ToUpper();
                Regex expresion = new Regex("^([0-9]+-[0-9K])$");
                string dv = rut.Substring(rut.Length - 1, 1);
                if (!expresion.IsMatch(rut))
                {
                    return false;
                }
                char[] charCorte = { '-' };
                string[] rutTemp = rut.Split(charCorte);
                if (dv != Digito(int.Parse(rutTemp[0])))
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string Digito(int rut)
        {
            try
            {
                int suma = 0;
                int multiplicador = 1;
                while (rut != 0)
                {
                    multiplicador++;
                    if (multiplicador == 8)
                        multiplicador = 2;
                    suma += (rut % 10) * multiplicador;
                    rut = rut / 10;
                }
                suma = 11 - (suma % 11);
                if (suma == 11)
                {
                    return "0";
                }
                else if (suma == 10)
                {
                    return "K";
                }
                else
                {
                    return suma.ToString();
                }
            }
            catch (Exception)
            {

                return "";
            }

        }

        public bool validarEmail(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
