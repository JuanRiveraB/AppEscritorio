using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        public string rut { get; set; }
        public string valorDgCboEstado { get; set; }

        public Inicio(string rut)
        {
            this.rut = rut;
            InitializeComponent();
            nomUsuario();
            cargaInicial();
        }

        //Metodo que oculta o muestra la primera carga despues del login
        private void cargaInicial()
        {
            //Habilita inicio
            gInicio.IsEnabled = true;
            gInicio.Visibility = Visibility.Visible;
            //Deshabilita Perfil
            gPerfil.IsEnabled = false;
            gPerfil.Visibility = Visibility.Collapsed;
            //Deshabilita Listar
            gListarPres.IsEnabled = false;
            gListarPres.Visibility = Visibility.Collapsed;
        }

        //Boton que carga inicio
        private void mInicio_Click(object sender, RoutedEventArgs e)
        {
            cargaInicial();
        }

        //Boton Salir
        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        //Boton que cierra sesion y vuelve al login
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow log = new MainWindow();
            log.Show();
            this.Close();
        }

        //Cargar el nombre de usuario concatenado
        private void nomUsuario()
        {
            Negocio.UsuarioN un = new Negocio.UsuarioN();
            lblUsuario.Content = un.nombreCompleto(this.rut);
        }

        //Metodo que permite solo el uso de numeros en el textbox rut del paciente
        private void txtPRut_KeyDown(object sender, KeyEventArgs e)
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

        //Metodo que permite solo el uso de numeros y k en el textbox Dv del paciente
        private void txtPDv_KeyDown(object sender, KeyEventArgs e)
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

        //Boton que busca los formularios del paciente por su rut
        private void btnBuscarR_Click(object sender, RoutedEventArgs e)
        {
            llenarGrid();
        }

        private void llenarGrid()
        {
            Negocio.PrescripcionN prn = new Negocio.PrescripcionN();
            string rutPer = txtPRut.Text + "-" + txtPDv.Text.ToLower();
            dgPrescrip.ItemsSource = prn.listarPresEmitidos(rutPer);
        }

        private void mPerfil_Click(object sender, RoutedEventArgs e)
        {
            lblMsjP.Content = "";
            gInicio.IsEnabled = false;
            gInicio.Visibility = Visibility.Collapsed;
            //Esconde Listar Prescripcion por si se activo
            gListarPres.IsEnabled = false;
            gListarPres.Visibility = Visibility.Collapsed;
            // Activar grid y perfil
            gPerfil.IsEnabled = true;
            gPerfil.Visibility = Visibility.Visible;
            cargarPefil();
        }

        //Metodo que carga el perfil del usuario
        private void cargarPefil()
        {
            try
            {
                //Carga Usuario
                Negocio.UsuarioN un = new Negocio.UsuarioN();
                Entidades.Usuario u = new Entidades.Usuario();
                u = un.cargarUsuario(this.rut);
                //Carga Comuna
                Entidades.Comuna c = new Entidades.Comuna();
                Negocio.ComunaN cn = new Negocio.ComunaN();
                if (u != null)
                {
                    //Prepara los datos para mostralos por label
                    lblRut.Content = u.rutUsuario;
                    lblNombres.Content = u.nombres;
                    lblApellidos.Content = u.apellidoPaterno + " " + u.apellidoMaterno;
                    lblFecha.Content = u.fechaNacimiento.ToString("dd DE MMMM DEL yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToLower();
                    lblEmail.Content = u.email;
                    lblCel.Content = "+" + u.cel;
                    lblDireccion.Content = u.direccion;
                    c = cn.cargarComuna(u.comunaId);
                    lblComuna.Content = c.nombre;
                    lblMsjP.Content = "";
                }
                else
                {
                    lblMsjP.Content = "Error, al cargar perfil";
                }
            }
            catch (Exception)
            {

                lblMsjP.Content = "Error, Comuníquese a soporte";
            }
        }

        private void btnEditEmail_Click(object sender, RoutedEventArgs e)
        {
            lblEmail.Visibility = Visibility.Collapsed;
            txtEmail.Visibility = Visibility.Visible;
            txtEmail.Text = lblEmail.Content.ToString();
            btnCancelar.Visibility = Visibility.Visible;
            btnGuarda.Visibility = Visibility.Visible;
        }

        private void btnEditCel_Click(object sender, RoutedEventArgs e)
        {
            lblCel.Visibility = Visibility.Collapsed;
            txtCel.Visibility = Visibility.Visible;
            lblCod.Visibility = Visibility.Visible;
            string num = lblCel.Content.ToString().Substring(3);
            txtCel.Text = num;
            btnCancelar.Visibility = Visibility.Visible;
            btnGuarda.Visibility = Visibility.Visible;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //Cancela todo y cambia las variables de visibilidad
            txtEmail.Text = "";
            txtCel.Text = "";
            txtEmail.Visibility = Visibility.Collapsed;
            lblCod.Visibility = Visibility.Collapsed;
            txtCel.Visibility = Visibility.Collapsed;
            btnCancelar.Visibility = Visibility.Collapsed;
            btnGuarda.Visibility = Visibility.Collapsed;
            lblEmail.Visibility = Visibility.Visible;
            lblCel.Visibility = Visibility.Visible;
        }

        private void btnGuarda_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Negocio.UsuarioN un = new Negocio.UsuarioN();
                Entidades.Usuario u = new Entidades.Usuario();
                Negocio.Validadores v = new Negocio.Validadores();
                u = un.cargarUsuario(this.rut);
                if (u != null)
                {
                    if (txtEmail.Visibility == Visibility.Visible && txtCel.Visibility == Visibility.Visible)
                    {
                        if (txtEmail.Text.Trim() != "" && txtCel.Text.Trim() != "")
                        {
                            if (v.validarEmail(txtEmail.Text) == true)
                            {
                                u.email = txtEmail.Text;
                                string num = lblCod.Content + txtCel.Text;
                                decimal num2 = Convert.ToDecimal(num.Replace("+", ""));
                                u.cel = num2;
                                lblMsjP.Content = "";
                                un.modificarusuario(u);
                                //Limpia y refresca
                                txtEmail.Text = "";
                                txtCel.Text = "";
                                txtEmail.Visibility = Visibility.Collapsed;
                                lblCod.Visibility = Visibility.Collapsed;
                                txtCel.Visibility = Visibility.Collapsed;
                                btnCancelar.Visibility = Visibility.Collapsed;
                                btnGuarda.Visibility = Visibility.Collapsed;
                                lblEmail.Visibility = Visibility.Visible;
                                lblCel.Visibility = Visibility.Visible;
                                cargarPefil();
                            }
                            else
                            {
                                lblMsjP.Content = "Email no valido";
                            }

                        }
                        else
                        {
                            lblMsjP.Content = "No puede tener campos vacios el momento de cambiar";
                        }
                    }
                    else if (txtEmail.Visibility == Visibility.Visible)
                    {
                        if (txtEmail.Text.Trim() != "")
                        {
                            if (v.validarEmail(txtEmail.Text) == true)
                            {
                                u.email = txtEmail.Text;
                                lblMsjP.Content = "";
                                un.modificarusuario(u);
                                //Limpia y refresca
                                txtEmail.Text = "";
                                txtCel.Text = "";
                                txtEmail.Visibility = Visibility.Collapsed;
                                lblCod.Visibility = Visibility.Collapsed;
                                txtCel.Visibility = Visibility.Collapsed;
                                btnCancelar.Visibility = Visibility.Collapsed;
                                btnGuarda.Visibility = Visibility.Collapsed;
                                lblEmail.Visibility = Visibility.Visible;
                                lblCel.Visibility = Visibility.Visible;
                                cargarPefil();
                            }
                            else
                            {
                                lblMsjP.Content = "Email no valido";
                            }
                        }
                        else
                        {
                            lblMsjP.Content = "No puede tener campos vacios el momento de cambiar";
                        }
                    }
                    else
                    {
                        if (txtCel.Text.Trim() != "")
                        {
                            string num = lblCod.Content + txtCel.Text;
                            decimal num2 = Convert.ToDecimal(num.Replace("+", ""));
                            u.cel = num2;
                            lblMsjP.Content = "";
                            un.modificarusuario(u);
                            //Limpia y refresca
                            txtEmail.Text = "";
                            txtCel.Text = "";
                            txtEmail.Visibility = Visibility.Collapsed;
                            lblCod.Visibility = Visibility.Collapsed;
                            txtCel.Visibility = Visibility.Collapsed;
                            btnCancelar.Visibility = Visibility.Collapsed;
                            btnGuarda.Visibility = Visibility.Collapsed;
                            lblEmail.Visibility = Visibility.Visible;
                            lblCel.Visibility = Visibility.Visible;
                            cargarPefil();
                        }
                        else
                        {
                            lblMsjP.Content = "No puede tener campos vacios el momento de cambiar";
                        }
                    }
                }
                else
                {
                    lblMsjP.Content = "Error al guardar, Contacte soporte";
                }
            }
            catch (Exception)
            {

                lblMsjP.Content = "Error desconocido, Contacte a soporte";
            }
        }

        private void txtCel_KeyDown(object sender, KeyEventArgs e)
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

        private void btnGuardarPres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entidades.PrescripcionPersonalizada pr = new Entidades.PrescripcionPersonalizada();
                Negocio.PrescripcionN pn = new Negocio.PrescripcionN();
                Entidades.Prescripcion p = new Entidades.Prescripcion();
                pr = (Entidades.PrescripcionPersonalizada)dgPrescrip.SelectedItem;
                p = pn.obtenerPres(pr.idPrescripcion);
                p.estado = valorDgCboEstado;
                if (p != null && p.estado != "Emitido" && valorDgCboEstado.Trim() != string.Empty)
                {
                    if (pn.modificarPres(p))
                    {
                        MessageBox.Show("Exito al guardar");
                        valorDgCboEstado = "";
                        llenarGrid();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void cboEstados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var comboBox = sender as ComboBox;
                ComboBoxItem typeItem = (ComboBoxItem)comboBox.SelectedItem;
                valorDgCboEstado = typeItem.Content.ToString();
            }
            catch (Exception)
            {


            }
        }

        private void mListarPres_Click(object sender, RoutedEventArgs e)
        {
            //Esconde inicio
            gInicio.IsEnabled = false;
            gInicio.Visibility = Visibility.Collapsed;
            //Esconde Perfil
            gPerfil.IsEnabled = false;
            gPerfil.Visibility = Visibility.Collapsed;
            //Habilita ListarPres
            gListarPres.IsEnabled = true;
            gListarPres.Visibility = Visibility.Visible;
        }

        private void btnListarPres_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Negocio.PrescripcionN pn = new Negocio.PrescripcionN();
                Entidades.PrescripcionPersonalizada pp = new Entidades.PrescripcionPersonalizada();
                ComboBoxItem typeItem = (ComboBoxItem)cboListarPres.SelectedItem;
                string valorCbo = typeItem.Content.ToString();
                switch (valorCbo)
                {
                    case "Todas":
                        dgLisPres.ItemsSource = pn.listarPres(" ");
                        break;
                    case "Emitido":
                        dgLisPres.ItemsSource = pn.listarPres("Emitido");
                        break;
                    case "Completado":
                        dgLisPres.ItemsSource = pn.listarPres("Completado");
                        break;
                    case "Reservar":
                        dgLisPres.ItemsSource = pn.listarPres("Reservar");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {


            }
        }

        private void mCMedicamento_Click(object sender, RoutedEventArgs e)
        {
            ControlMedicamento cm = new ControlMedicamento(this.rut);
            cm.Owner = this;
            AplicarEfecto(this);
            cm.ShowDialog();
            bool?
            resultado = cm.DialogResult;
            if (resultado != null)
            {
                if (resultado == false)
                {
                    QuitarEfecto(this);
                }
            }
        }

        private void AplicarEfecto(Window win)

        {
            var objBlur = new System.Windows.Media.Effects.BlurEffect();
            win.Effect = objBlur;
        }

        private void QuitarEfecto(Window win)

        {
            win.Effect = null;
        }
    }
}
