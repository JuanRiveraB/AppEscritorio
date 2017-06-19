using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtRut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.K || e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            Negocio.Validadores v = new Negocio.Validadores();
            Negocio.UsuarioN un = new Negocio.UsuarioN();
            string cadena = txtRut.Text + "-" + txtDv.Text.ToLower();
            if (txtRut.Text.Trim() != "" && txtDv.Text.Trim() != "")
            {
                if (v.ValidaRut(cadena) == true)
                {
                    try
                    {
                        string rut = cadena;
                        if (un.validarUsuario(rut, txtPass.Password))
                        {
                            Inicio ini = new Inicio(rut);
                            ini.Show();
                            this.Close();
                        }
                        else
                        {
                            lblMsj.Content = "Compruebe sí escribió bien sus credenciales";
                        }
                    }
                    catch (Exception)
                    {
                        lblMsj.Content = "Error Desconocido, solicite ayuda a Soporte";
                    }
                }
                else
                {
                    lblMsj.Content = "El Rut ingresado es incorrecto";
                }
            }
            else
            {
                lblMsj.Content = "No puede dejar campos vacios";
            }
        }
    }
}
