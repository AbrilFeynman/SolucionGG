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
using GGGC.Modules.Data;

namespace GGGC.Admin.Modules.Ektelesis.AUD.Views.Cancelaciones
{
    /// <summary>
    /// Interaction logic for LRGView.xaml
    /// </summary>
    public partial class LRGView : UserControl
    {
        AccesoDatos sCen = new AccesoDatos(9);
        public LRGView()
        {
            InitializeComponent();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                //Button_Click(sender, e);
                // txtFolio.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //if (ue.Tag != null && ue.Tag.ToString == "IgnoreEnterKeyTraversal")
                //{
                //    //ignore
                //}
                //else
                //{
                //    e.Handled = true;
                //    ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //}
            }
        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    this.btnBuscar.Enabled = false;
        //    this.btnBuscar.Refresh();
        //    this.txtNumeroDeFactura.Enabled = false;
        //    this.txtNumeroDeFactura.Refresh();
        //    string strFolio = "";
        //    if (blnMostrarFactura(this.txtNumeroDeFactura.Text.Trim()))
        //    {

        //        if (bytCodigoDeFormaDePago == 1)
        //        {
        //            MessageBox.Show("La Factura " + this.txtNumeroDeFactura.Text + " es de Contado. Solo se pueden Aplicar Recibos a Facturas de Crédito.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            this.txtNumeroDeCliente.Text = strNumeroDeCliente;


        //            strFolio = FolioFiscal(this.txtNumeroDeFactura.Text.Trim());

        //            if (strFolio != "")
        //            {
        //                this.lblFolioFiscal.Text = FolioFiscal(this.txtNumeroDeFactura.Text.Trim());
        //            }
        //            else
        //            {
        //                MessageBox.Show("No se pudo Obtener el Folio Fisca de la Factura " + this.txtNumeroDeFactura.Text + ". Favor de comunicarse a Departamento de Sistemas para Reportar el Problema", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        //
        //        //this.txtUUID.Text = this.lblFolioFiscal.Text;
        //        //if (txtUUID.Text.Trim() != "")
        //        //{
        //        //    Clipboard.SetText(this.txtUUID.Text);
        //        //    Clipboard.SetText(this.txtUUID.Text);
        //        //    txtUUID.Focus();
        //        //}
        //    }

        //    this.txtNumeroDeFactura.Enabled = true;
        //    this.txtNumeroDeFactura.Refresh();
        //    this.btnBuscar.Enabled = true;
        //    this.btnBuscar.Refresh();
        //    txtNumeroDeFactura.Focus();
        //    txtNumeroDeFactura.SelectAll();

        //    //this.dtFecha.IsEnabled = false;
        //    //Refresh(dtFecha);

        //    //this.btnBuscar.IsEnabled = false;
        //    //Refresh(btnBuscar);

        //    ////this.rgv.Visibility=Visibility.Hidden;
        //    ////Refresh(rgv);

        //    //DateTime? date = dtFecha.SelectedDate;

        //    //if (date == null)
        //    //{

        //    //    //  MostrarMensaje();

        //    //    //   if (MessageBox.Show("Debe seleccionar una fecha",
        //    //    //"GGGC",

        //    //    // MessageBoxButton.OK) == MessageBoxResult.OK)
        //    //    //   {
        //    //    //       // Matches.Remove(selectedMatch);
        //    //    //       //SelectedMatch = null;
        //    //    //       this.dtFecha.Focus();

        //    //    //   }





        //    //    MostrarMensaje();

        //    //    //      var mySettings = new MetroDialogSettings()
        //    //    //{
        //    //    //    AffirmativeButtonText = "Hi",
        //    //    //    NegativeButtonText = "Go away!",
        //    //    //    FirstAuxiliaryButtonText = "Cancel"
        //    //    //    //ColorScheme = UseAccentForDialogsMenuItem.IsChecked ? MetroDialogColorScheme.Accented : MetroDialogColorScheme.Theme
        //    //    //};

        //    //    //MessageDialogResult result = await this.ShowMessageAsync("Hello!", "Welcome to the world of metro!",
        //    //    //    MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);

        //    //    //if (result != MessageDialogResult.FirstAuxiliary)
        //    //    //    await this.ShowMessageAsync("Result", "You said: " + (result == MessageDialogResult.Affirmative ? mySettings.AffirmativeButtonText : mySettings.NegativeButtonText +
        //    //    //        Environment.NewLine + Environment.NewLine + "This dialog will follow the Use Accent setting."));

        //    //    // var metroWindow = (MetroWindow) Application.Current.MainWindow
        //    //    // MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    //    //{
        //    //    //   // Matches.Remove(selectedMatch);
        //    //    //    //SelectedMatch = null;
        //    //    //}


        //    //    //MessageDialogResult result = await dialogService.AskQuestionAsync("Delete Match", "Are you sure you want to delete this Match record?");
        //    //    //if (result == MessageDialogResult.Affirmative)
        //    //    //{
        //    //    //    // Matches.Remove(selectedMatch);
        //    //    //    //SelectedMatch = null;
        //    //    //}

        //    //}
        //    //else
        //    //{
        //    //    cargarDatos();//ttp://pan.baidu.com/
        //    //    this.rgv.Visibility = Visibility.Visible;
        //    //    Refresh(rgv);

        //    //}

        //    //this.btnBuscar.IsEnabled = true;
        //    //Refresh(btnBuscar);


        //    //this.dtFecha.IsEnabled = true;
        //    //Refresh(dtFecha);


        //    //this.dtFecha.Focus();
        //    //this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(focus_IsVisibleChanged);
        //    // Refresh(txtFolio);
        //    // this.dtFecha.SelectAll();
        //    //Refresh(txtFolio);
        //}



        //private bool blnMostrarFactura(string strFolio)
        //{
        //    string sSQL = "SELECT * FROM Facturas_Y_Devoluciones WHERE (Codigo_Tipo_De_Documento = 1  AND Folio_Del_Documento = '" + strFolio + "');";
        //    //Estatus_De_Documento = 1
        //    try
        //    {
        //        using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(sCen.BaseDatos.Cnn()))
        //        {
        //            using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(sSQL, Cnn))
        //            {
        //                try
        //                {
        //                    Cmd.CommandType = CommandType.Text;
        //                    Cmd.CommandText = sSQL;
        //                    Cnn.Open();
        //                    System.Data.SqlClient.SqlDataReader reader = Cmd.ExecuteReader();

        //                    if (reader.HasRows)
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            //String.Format("{0:C2}",
        //                            this.lblFacturaCliente.Text = reader.GetString(reader.GetOrdinal("Nombre"));
        //                            this.lblMontoFactura.Text = reader.GetDecimal(reader.GetOrdinal("Total")).ToString("0,000.00");
        //                            this.lblFechaDeFactura.Text = reader.GetDateTime(reader.GetOrdinal("Fecha_Del_Documento")).ToString("dd/MMMM/yyyy");
        //                            bytNumeroDeSucursal = reader.GetByte(reader.GetOrdinal("Numero_Corto_De_Sucursal"));
        //                            decNumeroDeDocumentoDeFactura = reader.GetDecimal(reader.GetOrdinal("Numero_De_Documento"));
        //                            strNumeroDeCliente = reader.GetString(reader.GetOrdinal("Numero_De_Cliente"));

        //                            if (strNumeroDeCliente == "100006")
        //                            {
        //                                stIDGC = reader.GetString(reader.GetOrdinal("Id_Radial"));
        //                                // strNumeroDeCliente = reader.GetString(reader.GetOrdinal("Id_Radial"));
        //                            }

        //                            bytEstatusDeDocumento = reader.GetByte(reader.GetOrdinal("Numero_Corto_De_Sucursal"));
        //                            bytCodigoDeFormaDePago = reader.GetByte(reader.GetOrdinal("Codigo_De_Forma_De_Pago"));
        //                        }
        //                        reader.Close();
        //                        return true;
        //                    }
        //                    else
        //                    {
        //                        //limpiarControles();
        //                        this.lblFacturaCliente.Text = "";
        //                        this.lblMontoFactura.Text = "";
        //                        this.lblFolioFiscal.Text = "";
        //                        bytNumeroDeSucursal = 0;
        //                        decNumeroDeDocumentoDeFactura = 0;
        //                        strNumeroDeCliente = "";
        //                        stIDGC = "";
        //                        bytCodigoDeFormaDePago = 0;
        //                        bytEstatusDeDocumento = 0;
        //                        return false;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
        //                    //return false;
        //                }
        //                finally
        //                {
        //                    if (Cnn.State == ConnectionState.Open)
        //                    {
        //                        Cnn.Close();

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
        //        // return false;
        //    }
        //}

        //private void txtNumeroDeFactura_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == Convert.ToChar(Keys.Enter))
        //    {
        //        //this.btnBuscar.Focus();
        //        btnBuscar_Click(sender, e);
        //    }



        //}
    }
}
