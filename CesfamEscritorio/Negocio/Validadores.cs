using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        /// Encripta una cadena
        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        public void enviarCorreo(List<string> emails, Entidades.Medicamento m)
        {
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential ("cesfamti@gmail.com", "cesfamduoc");
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            MailMessage mail = new MailMessage();
            try
            {
                mail.From = new MailAddress("cesfamti@gmail.com", "Farmacia Cesfam", System.Text.Encoding.UTF8);
                mail.Subject = "Llegaron Medicamentos!";
                mail.Body = "Se le informa que el medicamento "+ m.nombreComercial +" que tenia reservado acaba de llegar, se pide que valla a retirar lo más pronto posible, antes de que se le de prioridad a otra persona. Este correo fue generado automaticamente, porfavor no intente responderlo, Atte. Cesfam";
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                foreach (var item in emails)
                {
                    mail.Bcc.Add(item);
                }
                mail.Bcc.Add("cesfamti@gmail.com");
                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
                
            }
        }

        public void enviarCorreoRecordatorio(List<string> emails)
        {
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential("cesfamti@gmail.com", "cesfamduoc");
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            MailMessage mail = new MailMessage();
            try
            {
                mail.From = new MailAddress("cesfamti@gmail.com", "Farmacia Cesfam", System.Text.Encoding.UTF8);
                mail.Subject = "Recordatorio de Medicamentos!";
                mail.Body = "Se recuerda al paciente o tutor que tiene una reserva de medicamento en Cesfam porfavor retirar lo más pronto posible cuando se le informe la llegada de su medicamento. Este correo fue generado automaticamente, porfavor no intente responderlo, Atte. Cesfam";
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                foreach (var item in emails)
                {
                    mail.Bcc.Add(item);
                }
                mail.Bcc.Add("cesfamti@gmail.com");
                SmtpServer.Send(mail);
            }
            catch (Exception)
            {

            }
        }
    }
}
