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
            cargaInicial();
        }

        //Cargar el nombre de usuario concatenado
        private void nomUsuario()
        {
            Negocio.UsuarioN un = new Negocio.UsuarioN();
            lblUsuario.Content = un.nombreCompleto(this.rut);
        }

        private void cargaInicial()
        {
            //DesHabilita Agregar
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
                        }
                    }

                }else
                {
                    lblMsjAumen.Content = "No pueden haber campos vacíos";
                }
            }
            catch (Exception)
            {

                lblMsjAumen.Content = "Error al Aumentar";
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
                    }else
                    {
                        lblMesjAgregar.Content = "Este medicamento ya existe";
                    }

                }else
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
            gMedInicio.IsEnabled = false;
            gMedInicio.Visibility = Visibility.Collapsed;
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
                    }else
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
    }
}
