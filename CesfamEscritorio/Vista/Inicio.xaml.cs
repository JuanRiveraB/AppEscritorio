using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.IO;
using System.Collections.Generic;

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
            cargarNivel();
            cargaInicial();
        }

        private void cargarNivel()
        {
            Negocio.UsuarioN un = new Negocio.UsuarioN();
            if (un.nivelUsuario(rut).Equals("Administrador"))
            {
                mCrearFarmaco.IsEnabled = true;
                mCrearFarmaco.Visibility = Visibility.Visible;
            }
            else
            {
                mCrearFarmaco.IsEnabled = false;
                mCrearFarmaco.Visibility = Visibility.Collapsed;
            }
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
            try
            {
                Negocio.PrescripcionN prn = new Negocio.PrescripcionN();
                string rutPer = txtPRut.Text + "-" + txtPDv.Text.ToLower();
                ComboBoxItem typeItem = (ComboBoxItem)cboEstadoInicio.SelectedItem;
                string valorCbo = typeItem.Content.ToString();
                if (valorCbo == "Emitido")
                {
                    dgPrescrip.ItemsSource = prn.listarPresEmitidos(rutPer);
                }
                else if (valorCbo == "Reservar")
                {
                    dgPrescrip.ItemsSource = prn.listarPresReservar(rutPer);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar");
            }
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
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                Entidades.Medicamento m = new Entidades.Medicamento();
                pr = (Entidades.PrescripcionPersonalizada)dgPrescrip.SelectedItem;
                p = pn.obtenerPres(pr.idPrescripcion);
                p.estado = valorDgCboEstado;
                if (p != null && p.estado != "Emitido" && valorDgCboEstado.Trim() != string.Empty)
                {
                    if (p.estado == "Completado")
                    {
                        m = mn.obtenerMedicamento(p.idMedicamento);
                        if (m.stock >= p.cantidad)
                        {
                            if (pn.modificarPres(p))
                            {
                                decimal cantidad = m.stock - pr.cantidad;
                                m.stock = cantidad;
                                if (mn.modificarMedicamento(m) == true)
                                {
                                    MessageBox.Show("Exito al Completar");
                                    valorDgCboEstado = "";
                                    llenarGrid();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se puede completar porque el medicamento no tiene la cantidad requerida, recomiende reservar");
                            valorDgCboEstado = "";
                            llenarGrid();
                        }
                    }
                    else if (p.estado == "Reservar")
                    {
                        if (pn.modificarPres(p))
                        {
                            MessageBox.Show("Exito al reservar");
                            valorDgCboEstado = "";
                            llenarGrid();
                        }
                    }
                    else if (p.estado == "Cancelar")
                    {
                        if (pn.modificarPres(p))
                        {
                            MessageBox.Show("Exito al Cancelar");
                            valorDgCboEstado = "";
                            llenarGrid();
                        }
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
                    case "Cancelar":
                        dgLisPres.ItemsSource = pn.listarPres("Cancelar");
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

        private void ExportToPdf(DataGrid grid)
        {
            try
            {
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                Document pdfcommande = new Document(iTextSharp.text.PageSize.A4.Rotate(), 6, 3, 3, 3);
                string Directorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                PdfWriter writer = PdfWriter.GetInstance(pdfcommande, new FileStream(Directorio + "\\Informe.pdf", FileMode.Create));
                pdfcommande.Open();
                iTextSharp.text.Paragraph firstpara = new iTextSharp.text.Paragraph("Informe de Prescripciones");
                iTextSharp.text.Paragraph firstpara2 = new iTextSharp.text.Paragraph(" ");
                foreach (DataGridColumn column in grid.Columns)
                {
                    table.AddCell(new Phrase(column.Header.ToString()));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        Entidades.PrescripcionPersonalizada ps = new Entidades.PrescripcionPersonalizada();
                        ps = (Entidades.PrescripcionPersonalizada)item;
                        if (ps != null)
                        {
                            table.AddCell(new Phrase(ps.idPrescripcion.ToString()));
                            table.AddCell(new Phrase(ps.rutPersona));
                            table.AddCell(new Phrase(ps.nomPersona));
                            table.AddCell(new Phrase(ps.diagnostico));
                            table.AddCell(new Phrase(ps.fechaPres.ToString()));
                            table.AddCell(new Phrase(ps.rutMedico));
                            table.AddCell(new Phrase(ps.nomMedico));
                            table.AddCell(new Phrase(ps.nomMedicamento));
                            table.AddCell(new Phrase(ps.formaFarmaceutica));
                            table.AddCell(new Phrase(ps.cantidad.ToString()));
                            table.AddCell(new Phrase(ps.estado));
                            /*float[] widths = new float[] { 150f, 150f, 150f, 160f, 160f, 160f, 160f, 160f, 160f, 160f, 160f };
                            table.SetWidths(widths);*/
                        }
                    }
                    pdfcommande.Add(firstpara);
                    pdfcommande.Add(firstpara2);
                    pdfcommande.Add(table);
                    pdfcommande.Close();
                    writer.Close();
                    MessageBox.Show("Informe creado y enviado a escritorio en formato pdf");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo Crear el informe");
            }
        }

        private void btnExportarPres_Click(object sender, RoutedEventArgs e)
        {
            ExportToPdf(dgLisPres);
        }

        private void mCrearFarmaco_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void bntRecordar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                enviarInformes();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al generar recordatorios");
            }
        }

        private void enviarInformes()
        {
            try
            {
                Negocio.PrescripcionN pn = new Negocio.PrescripcionN();
                Negocio.Validadores v = new Negocio.Validadores();
                List<string> emails = new List<string>();
                foreach (var item in pn.obtenerReservadasAviso())
                {
                    if (item.email.Trim() != string.Empty)
                    {
                        emails.Add(item.email);
                    }
                }
                v.enviarCorreoRecordatorio(emails);
            }
            catch (Exception)
            {

            }
        }
    }
}
