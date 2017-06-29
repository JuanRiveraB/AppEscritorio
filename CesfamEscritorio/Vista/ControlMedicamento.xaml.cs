using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para ControlMedicamento.xaml
    /// </summary>
    public partial class ControlMedicamento : Window
    {
        public string rut { get; set; }
        public ControlMedicamento(string rut)
        {
            this.rut = rut;
            InitializeComponent();
            nomUsuario();
            cargarNivel();
            cargaInicial();
        }

        //Cargar el nombre de usuario concatenado
        private void nomUsuario()
        {
            Negocio.UsuarioN un = new Negocio.UsuarioN();
            lblUsuario.Content = un.nombreCompleto(this.rut);
        }

        private void cargarNivel()
        {
            Negocio.UsuarioN un = new Negocio.UsuarioN();
            if (un.nivelUsuario(rut).Equals("Administrador"))
            {
                btnEliminarMed.IsEnabled = true;
                btnEliminarMed.Visibility = Visibility.Visible;
            }
            else
            {
                btnEliminarMed.IsEnabled = false;
                btnEliminarMed.Visibility = Visibility.Collapsed;
            }
        }

        private void cargaInicial()
        {
            //Oculta Listar Controles
            gListrarCS.IsEnabled = false;
            gListrarCS.Visibility = Visibility.Collapsed;
            //Oculta Listar Med
            gListarMed.IsEnabled = false;
            gListarMed.Visibility = Visibility.Collapsed;
            //Oculta Eliminar
            gMedEliminar.IsEnabled = false;
            gMedEliminar.Visibility = Visibility.Collapsed;
            //Oculta Agregar
            gMedAgregar.IsEnabled = false;
            gMedAgregar.Visibility = Visibility.Collapsed;
            //CargaInicio
            gMedInicio.IsEnabled = true;
            gMedInicio.Visibility = Visibility.Visible;
            lblMsjAumen.Content = "";
            llenarcboMedAu();
        }

        private void llenarcboMedAu()
        {
            Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
            Entidades.Medicamento m = new Entidades.Medicamento();
            try
            {
                cboAuMed.ItemsSource = mn.listarTodos();
                cboAuMed.DisplayMemberPath = "nombreComercial";
                cboAuMed.SelectedValuePath = "idMedicamento";
            }
            catch (Exception)
            {


            }
        }

        private void llenarcboMedElim()
        {
            Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
            Entidades.Medicamento m = new Entidades.Medicamento();
            try
            {
                cboLisElim.ItemsSource = mn.listarTodos();
                cboLisElim.DisplayMemberPath = "nombreComercial";
                cboLisElim.SelectedValuePath = "idMedicamento";
            }
            catch (Exception)
            {


            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void cboAuMed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                Entidades.Medicamento m = new Entidades.Medicamento();
                m = (Entidades.Medicamento)cboAuMed.SelectedItem;
                //Cargar datos txt
                m = mn.obtenerMedicamento(m.idMedicamento);
                lblIdMedAu.Content = m.idMedicamento;
                txtNomCoAu.Text = m.nombreComercial;
                txtLabAu.Text = m.laboratorio;
                txtEANAu.Text = m.ean13;
                txtFFAu.Text = m.formaFarmaceutica;
                txtStockAu.Text = m.stock.ToString();
            }
            catch (Exception)
            {


            }
        }

        private void btnCancelarAu_Click(object sender, RoutedEventArgs e)
        {
            txtNomCoAu.Text = "";
            txtLabAu.Text = "";
            txtEANAu.Text = "";
            txtFFAu.Text = "";
            txtStockAu.Text = "";
            cboAuMed.SelectedIndex = -1;
            txtCanCCAu.Text = "";
            txtDesCCAu.Text = "";
            txtNomCCAu.Text = "";
            lblMsjAumen.Content = "";
        }

        private void btnAumentar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entidades.Medicamento m = new Entidades.Medicamento();
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                Entidades.ControlStock cs = new Entidades.ControlStock();
                Negocio.ControlStockN csn = new Negocio.ControlStockN();
                if (txtNomCCAu.Text.Trim() != string.Empty && txtDesCCAu.Text.Trim() != string.Empty && txtCanCCAu.Text.Trim() != string.Empty)
                {
                    //Se carga Medicamento
                    m.idMedicamento = Convert.ToDecimal(lblIdMedAu.Content);
                    m.nombreComercial = txtNomCoAu.Text.ToLower();
                    m.laboratorio = txtLabAu.Text.ToLower();
                    m.ean13 = txtEANAu.Text.ToLower();
                    m.formaFarmaceutica = txtFFAu.Text.ToLower();
                    m.stock = Convert.ToDecimal(txtStockAu.Text);
                    m.idSucursal = 10000;
                    //Se carga Control Stock
                    cs.nombre = txtNomCCAu.Text;
                    cs.descripcion = txtDesCCAu.Text;
                    cs.fecha = System.DateTime.Now;
                    cs.cantidad = Convert.ToDecimal(txtCanCCAu.Text);
                    cs.idMedicamento = m.idMedicamento;
                    cs.idUsuario = this.rut;
                    //Enviar datos
                    decimal cantidad = m.stock + cs.cantidad;
                    m.stock = cantidad;
                    if (csn.insertarCS(cs))
                    {
                        if (mn.modificarMedicamento(m))
                        {
                            MessageBox.Show("Aumentado correctamente");
                            txtNomCoAu.Text = "";
                            txtLabAu.Text = "";
                            txtEANAu.Text = "";
                            txtFFAu.Text = "";
                            txtStockAu.Text = "";
                            cboAuMed.SelectedIndex = -1;
                            txtCanCCAu.Text = "";
                            txtDesCCAu.Text = "";
                            txtNomCCAu.Text = "";
                            lblMsjAumen.Content = "";
                            enviarInformes(m);
                        }
                    }

                }
                else
                {
                    lblMsjAumen.Content = "No pueden haber campos vacíos";
                }
            }
            catch (Exception)
            {

                lblMsjAumen.Content = "Error al Aumentar";
            }
        }

        private void enviarInformes(Entidades.Medicamento m)
        {
            try
            {
                Negocio.PrescripcionN pn = new Negocio.PrescripcionN();
                Negocio.Validadores v = new Negocio.Validadores();
                List<string> emails = new List<string>();
                foreach (var item in pn.obtenerReservadas(m.idMedicamento))
                {
                    if (item.email.Trim() != string.Empty)
                    {
                        emails.Add(item.email);
                    }
                }
                v.enviarCorreo(emails, m);
            }
            catch (Exception)
            {
                
            }
        }

        private void txtCanCCAu_KeyDown(object sender, KeyEventArgs e)
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

        private void btnIrAumentar_Click(object sender, RoutedEventArgs e)
        {
            cargaInicial();
        }

        private void btnCancelarAgre_Click(object sender, RoutedEventArgs e)
        {
            txtNomCoAgre.Text = "";
            txtLabAgre.Text = "";
            txtEANAgre.Text = "";
            txtFFAgre.Text = "";
            txtStockAgre.Text = "";
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                Entidades.Medicamento m = new Entidades.Medicamento();
                //Cargar valores
                if (txtNomCoAgre.Text.Trim() != string.Empty && txtLabAgre.Text.Trim() != string.Empty && txtEANAgre.Text.Trim() != string.Empty && txtFFAgre.Text.Trim() != string.Empty && txtStockAgre.Text.Trim() != string.Empty)
                {
                    m.nombreComercial = txtNomCoAgre.Text.ToLower();
                    m.laboratorio = txtLabAgre.Text.ToLower();
                    m.ean13 = txtEANAgre.Text.ToLower();
                    m.formaFarmaceutica = txtFFAgre.Text.ToLower();
                    m.stock = Convert.ToDecimal(txtStockAgre.Text);
                    m.idSucursal = 10000;
                    if (mn.consularSiExiste(m) != true)
                    {
                        mn.agregarMedicamento(m);
                        txtNomCoAgre.Text = "";
                        txtLabAgre.Text = "";
                        txtEANAgre.Text = "";
                        txtFFAgre.Text = "";
                        txtStockAgre.Text = "";
                        MessageBox.Show("Agregado Correctamente");
                    }
                    else
                    {
                        lblMesjAgregar.Content = "Este medicamento ya existe";
                    }

                }
                else
                {
                    lblMesjAgregar.Content = "No puede dejar campos vacíos";
                }
            }
            catch (Exception)
            {

                lblMesjAgregar.Content = "Error al agregar, informe a soporte";
            }
        }

        private void txtStockAgre_KeyDown(object sender, KeyEventArgs e)
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

        private void btnIrAgregarMed_Click(object sender, RoutedEventArgs e)
        {
            //Oculta Listar CS
            gListrarCS.IsEnabled = false;
            gListrarCS.Visibility = Visibility.Collapsed;
            //Oculta Listar Med
            gListarMed.IsEnabled = false;
            gListarMed.Visibility = Visibility.Collapsed;
            //Oculta inicio
            gMedInicio.IsEnabled = false;
            gMedInicio.Visibility = Visibility.Collapsed;
            //Oculta Eliminar
            gMedEliminar.IsEnabled = false;
            gMedEliminar.Visibility = Visibility.Collapsed;
            //Carga agregar
            gMedAgregar.IsEnabled = true;
            gMedAgregar.Visibility = Visibility.Visible;
            lblMesjAgregar.Content = "";
        }

        private void btnDescontar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entidades.Medicamento m = new Entidades.Medicamento();
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                Entidades.ControlStock cs = new Entidades.ControlStock();
                Negocio.ControlStockN csn = new Negocio.ControlStockN();
                if (txtNomCCAu.Text.Trim() != string.Empty && txtDesCCAu.Text.Trim() != string.Empty && txtCanCCAu.Text.Trim() != string.Empty)
                {
                    //Se carga Medicamento
                    m.idMedicamento = Convert.ToDecimal(lblIdMedAu.Content);
                    m.nombreComercial = txtNomCoAu.Text.ToLower();
                    m.laboratorio = txtLabAu.Text.ToLower();
                    m.ean13 = txtEANAu.Text.ToLower();
                    m.formaFarmaceutica = txtFFAu.Text.ToLower();
                    m.stock = Convert.ToDecimal(txtStockAu.Text);
                    m.idSucursal = 10000;
                    //Se carga Control Stock
                    cs.nombre = txtNomCCAu.Text;
                    cs.descripcion = txtDesCCAu.Text;
                    cs.fecha = System.DateTime.Now;
                    cs.cantidad = Convert.ToDecimal(txtCanCCAu.Text);
                    cs.idMedicamento = m.idMedicamento;
                    cs.idUsuario = this.rut;
                    //Enviar datos
                    if (m.stock >= cs.cantidad)
                    {
                        decimal cantidad = m.stock - cs.cantidad;
                        m.stock = cantidad;
                        if (csn.insertarCS(cs))
                        {
                            if (mn.modificarMedicamento(m))
                            {
                                MessageBox.Show("Descontado correctamente");
                                txtNomCoAu.Text = "";
                                txtLabAu.Text = "";
                                txtEANAu.Text = "";
                                txtFFAu.Text = "";
                                txtStockAu.Text = "";
                                cboAuMed.SelectedIndex = -1;
                                txtCanCCAu.Text = "";
                                txtDesCCAu.Text = "";
                                txtNomCCAu.Text = "";
                                lblMsjAumen.Content = "";
                            }
                        }
                    }
                    else
                    {
                        lblMsjAumen.Content = "La Cantidad a Descontar no puede ser mayor al Stock";
                    }
                }
                else
                {
                    lblMsjAumen.Content = "No pueden haber campos vacíos";
                }
            }
            catch (Exception)
            {

                lblMsjAumen.Content = "Error al Aumentar";
            }
        }

        private void btnEliminarMed_Click(object sender, RoutedEventArgs e)
        {
            //Oculta Listar CS
            gListrarCS.IsEnabled = false;
            gListrarCS.Visibility = Visibility.Collapsed;
            //Oculta Listar Med
            gListarMed.IsEnabled = false;
            gListarMed.Visibility = Visibility.Collapsed;
            //Oculta Agregar
            gMedAgregar.IsEnabled = false;
            gMedAgregar.Visibility = Visibility.Collapsed;
            //CargaInicio
            gMedInicio.IsEnabled = false;
            gMedInicio.Visibility = Visibility.Collapsed;
            //Oculta Eliminar
            gMedEliminar.IsEnabled = true;
            gMedEliminar.Visibility = Visibility.Visible;
            lblMesjElim.Content = "";
            llenarcboMedElim();

        }

        private void btnCancelarElim_Click(object sender, RoutedEventArgs e)
        {
            txtNomCoElim.Text = "";
            txtLabElim.Text = "";
            txtEANElim.Text = "";
            txtFFElim.Text = "";
            txtStockElim.Text = "";
            cboLisElim.SelectedIndex = -1;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                Entidades.Medicamento m = new Entidades.Medicamento();
                //Cargar valores
                m = (Entidades.Medicamento)cboLisElim.SelectedItem;
                if (mn.eliminarMedicamento(m))
                {
                    txtNomCoElim.Text = "";
                    txtLabElim.Text = "";
                    txtEANElim.Text = "";
                    txtFFElim.Text = "";
                    txtStockElim.Text = "";
                    cboLisElim.SelectedIndex = -1;
                    lblMesjElim.Content = "";
                    MessageBox.Show("Elminado Correctamente");
                }else
                {
                    lblMesjAgregar.Content = "Error al eliminar, intente de nuevo";
                }
            }
            catch (Exception)
            {

                lblMesjAgregar.Content = "Error al eliminar, informe a soporte";
            }
        }

        private void cboLisElim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                Entidades.Medicamento m = new Entidades.Medicamento();
                m = (Entidades.Medicamento)cboLisElim.SelectedItem;
                //Cargar datos txt
                m = mn.obtenerMedicamento(m.idMedicamento);
                txtNomCoElim.Text = m.nombreComercial;
                txtLabElim.Text = m.laboratorio;
                txtEANElim.Text = m.ean13;
                txtFFElim.Text = m.formaFarmaceutica;
                txtStockElim.Text = m.stock.ToString();
            }
            catch (Exception)
            {
                
            }
        }

        private void btnPDFMed_Click(object sender, RoutedEventArgs e)
        {
            ExportToPdf(dgListarMed);
        }

        private void btnLisatrMedica_Click(object sender, RoutedEventArgs e)
        {
            //Oculta Listar CS
            gListrarCS.IsEnabled = false;
            gListrarCS.Visibility = Visibility.Collapsed;
            //Oculta inicio
            gMedInicio.IsEnabled = false;
            gMedInicio.Visibility = Visibility.Collapsed;
            //Oculta Eliminar
            gMedEliminar.IsEnabled = false;
            gMedEliminar.Visibility = Visibility.Collapsed;
            //Oculta agregar
            gMedAgregar.IsEnabled = false;
            gMedAgregar.Visibility = Visibility.Collapsed;
            //Carga Lisatar Med
            gListarMed.IsEnabled = true;
            gListarMed.Visibility = Visibility.Visible;
            listarMedicamentos();
        }

        private void listarMedicamentos()
        {
            try
            {
                Negocio.MedicamentoN mn = new Negocio.MedicamentoN();
                dgListarMed.ItemsSource = mn.listarTodos();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error al listar");
            }
        }

        private void ExportToPdf(DataGrid grid)
        {
            try
            {
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                Document pdfcommande = new Document(iTextSharp.text.PageSize.A4.Rotate(), 6, 3, 3, 3);
                string Directorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                PdfWriter writer = PdfWriter.GetInstance(pdfcommande, new FileStream(Directorio + "\\Informe Medicamentos.pdf", FileMode.Create));
                pdfcommande.Open();
                iTextSharp.text.Paragraph firstpara = new iTextSharp.text.Paragraph("Informe de Medicamentos");
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
                        Entidades.Medicamento m = new Entidades.Medicamento();
                        m = (Entidades.Medicamento)item;
                        if (m != null)
                        {
                            table.AddCell(new Phrase(m.idMedicamento.ToString()));
                            table.AddCell(new Phrase(m.nombreComercial));
                            table.AddCell(new Phrase(m.laboratorio));
                            table.AddCell(new Phrase(m.ean13));
                            table.AddCell(new Phrase(m.formaFarmaceutica));
                            table.AddCell(new Phrase(m.stock.ToString()));
                            table.AddCell(new Phrase(m.idSucursal.ToString()));
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

        private void btnListarControles_Click(object sender, RoutedEventArgs e)
        {
            //Oculta Listar Med
            gListarMed.IsEnabled = false;
            gListarMed.Visibility = Visibility.Collapsed;
            //Oculta inicio
            gMedInicio.IsEnabled = false;
            gMedInicio.Visibility = Visibility.Collapsed;
            //Oculta Eliminar
            gMedEliminar.IsEnabled = false;
            gMedEliminar.Visibility = Visibility.Collapsed;
            //Oculta agregar
            gMedAgregar.IsEnabled = false;
            gMedAgregar.Visibility = Visibility.Collapsed;
            //Carga Listar CS
            gListrarCS.IsEnabled = true;
            gListrarCS.Visibility = Visibility.Visible;
            listarControl();
        }

        private void btnLisCS_Click(object sender, RoutedEventArgs e)
        {
            ExportToPdfControl(dgLisControl);
        }

        private void listarControl()
        {
            try
            {
                Negocio.ControlStockN csn = new Negocio.ControlStockN();
                dgLisControl.ItemsSource = csn.obtenerControlInforme();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Cargar, infrome a soporte");
            }
        }

        private void ExportToPdfControl(DataGrid grid)
        {
            try
            {
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                Document pdfcommande = new Document(iTextSharp.text.PageSize.A4.Rotate(), 6, 3, 3, 3);
                string Directorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                PdfWriter writer = PdfWriter.GetInstance(pdfcommande, new FileStream(Directorio + "\\Informe Control de Stock.pdf", FileMode.Create));
                pdfcommande.Open();
                iTextSharp.text.Paragraph firstpara = new iTextSharp.text.Paragraph("Informe de Controles de Stock");
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
                        Entidades.ControlStockInforme c = new Entidades.ControlStockInforme();
                        c = (Entidades.ControlStockInforme)item;
                        if (c != null)
                        {
                            table.AddCell(new Phrase(c.idControl.ToString()));
                            table.AddCell(new Phrase(c.nombre));
                            table.AddCell(new Phrase(c.descrip));
                            table.AddCell(new Phrase(c.fecha.ToString()));
                            table.AddCell(new Phrase(c.cantidad.ToString()));
                            table.AddCell(new Phrase(c.rutFar));
                            table.AddCell(new Phrase(c.nombres));
                            table.AddCell(new Phrase(c.idMedica.ToString()));
                            table.AddCell(new Phrase(c.nombreMed));
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
    }
}
