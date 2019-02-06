using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Xml.Serialization;

namespace GGGC.Admin.AZ.Remisiones.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(String osos)
        {
            
            InitializeComponent();
            try { export15(osos); }
            catch (SqlException ex) { MessageBox.Show("Revise su conexión a internet"); }

        }

        private void export15(string folio)
        {
            try {
                DataTable tablaOrden = GetHeader(folio);
                DataTable tablaDeatil = GetDetail(folio);

                RptRemision fac = new RptRemision(tablaOrden, tablaDeatil);
                this.ReportViewer1.Report = fac;
                this.ReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Folio no encontrado" );
            }

        }

        static DataTable GetHeader(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


            SqlConnection sqlconn = new SqlConnection(conect);

            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM RemisionHeader WHERE Folio_Remision = '" + folio + "' ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "RemisionHeader");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["RemisionHeader"];
            sqlconn.Close();

            return dtbl;
        }

        //public void ProcesarXML(string rutaXML)
        //{
        //    try
        //    {
               
        //        System.Windows.Forms.Application.DoEvents();

        //        string complemento;
              
                
        //        StreamReader objStreamReader = new StreamReader(rutaXML);
        //        XmlSerializer Xml = new XmlSerializer(Factura.GetType());
        //        Factura = (uCFDsLib.v32.Comprobante)Xml.Deserialize(objStreamReader);
        //        objStreamReader.Close();

        //        if (Factura.Complemento.Any != null && Factura.Complemento.Any.Length > 0)
        //        {
        //            complemento = Factura.Complemento.Any[0].OuterXml;

        //            Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(complemento));

        //            TimbreFiscalDigital timbre = new TimbreFiscalDigital();
               

        //            objStreamReader = new StreamReader(s);
        //            Xml = new XmlSerializer(timbre.GetType());
        //            timbre = (TimbreFiscalDigital)Xml.Deserialize(objStreamReader);
        //            objStreamReader.Close();

        //            XmlDocument xmldoc = new XmlDocument();
        //            xmldoc.InnerXml = complemento;

        //            XslCompiledTransform transformador = new XslCompiledTransform();
                  
        //            transformador.Load("cadenaoriginal_TFD_1_0.xslt");
               
        //            StringWriter CadenaOriginal = new StringWriter();
              
        //            transformador.Transform(xmldoc.CreateNavigator(), null, CadenaOriginal);
                   
        //            System.Windows.Forms.Application.DoEvents();
        //            strFilePDF = rutaXML.Substring(32);
        //            strFilePDF = strFilePDF.Replace(".xml", ".pdf");
        //            strFilePDF = strFilePDF.Replace("SIGN_", "");
        //            System.Data.DataRow fila = tblInformacion.Rows[0];
        //            strFilePDF = strFilePDF.Replace("LRG920502BG7", fila["Numero_De_Cliente"].ToString().Trim());
                             
        //            try
        //            {
        //                uDatos.AccesoDatos Datos;
        //                Datos = new uDatos.AccesoDatos();
                      
        //                List<System.Data.SqlClient.SqlParameter> Pars = new List<System.Data.SqlClient.SqlParameter>();
                        
        //                string strSerie = Factura.serie;
        //                string strFolio = Factura.folio;
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@Serie", strSerie));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@Folio", strFolio));
                  
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@Xml", "SIGN_" + version32.strNombreXML));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@RutaXml", rutaXML));
                       
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@uuid", timbre.UUID));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@fechaTimbrado", timbre.FechaTimbrado));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@noCertificadoSAT", timbre.noCertificadoSAT));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@selloSAT", timbre.selloSAT));
        //                 Pars.Add(new System.Data.SqlClient.SqlParameter("@CFDiXml", rutaXML));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@Timbrado", 1));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@id", Convert.ToDecimal(strNumeroDeDocumento)));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@numerodecliente", strNumeroDeClienteTemp));
        //                string strDuracion = String.Format("{0:0.00}", GetDateDifferenceInDays(mmm)) + " seg.";
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@Duracion", strDuracion));
        //                Pars.Add(new System.Data.SqlClient.SqlParameter("@NombrePDF", strFilePDF));
        //                 Datos.BaseDatos.Ejecuta("UPDATE tblCFDIs_temp SET Serie = @Serie, Folio = @Folio, NumeroDeCliente = @numerodecliente, UUID = @uuid, XML = @XML, RutaXML = @RutaXML, Timbrado = 1, FechaTimbrado = @fechaTimbrado, LastUpdate = GETDATE() WHERE (NumeroDeDocumento = @id)", Pars.ToArray());
        //                Datos.BaseDatos.Ejecuta("UPDATE tblCFDIs SET UUID = @uuid, FechaTimbrado = @fechaTimbrado, CertificadoSAT = @noCertificadoSAT, selloSAT = @selloSAT, NombreXmlTimbrado = @CFDiXml, Timbrado = @Timbrado, FechaTimbrarXml = GETDATE(),  Duracion = @Duracion, NombrePDF = @NombrePDF, Estatus_De_Replicacion = 1, Fecha_Y_Hora_De_Ultima_Actualizacion = GETDATE() WHERE (Numero_De_Documento = @id)", Pars.ToArray());
        //                 }
        //            catch (Exception ex)
        //            {
        //                GG.Log("Error al Guardar tblCFDIs:" + ex.Source + Environment.NewLine + "Pila: " + ex.StackTrace + Environment.NewLine + ex.Message);
        //                 }
        //            try
        //            {
        //                Vista(Factura, timbre, CadenaOriginal.ToString(), true);
        //                this.btnGenerar.Enabled = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                 GG.Log("Error. Origen:" + ex.Source + Environment.NewLine + "Pila: " + ex.StackTrace + Environment.NewLine + ex.Message);
        //                GG.LogDatabase(ex.Source, ex.StackTrace, ex.Message);
        //                MessageBox.Show("Ocurrió un Error, Favor de Reportarlo al Departamento de Sistemas. \n La aplicación se Cerrará ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                GC.Collect();
        //            }
                
        //            this.lblStatus.Text = "Documento " + Factura.serie + Factura.folio + " Generado Exitosamente en ";
        //            this.lblStatus.Refresh();
        //            System.Windows.Forms.Application.DoEvents();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}
        static DataTable GetDetail(string folio)
        {
            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";


            SqlConnection sqlconn = new SqlConnection(conect);

            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM RemisionDetail WHERE FolioRemision = '" + folio + "' ", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "RemisionDetail");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["RemisionDetail"];
            sqlconn.Close();

            return dtbl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
