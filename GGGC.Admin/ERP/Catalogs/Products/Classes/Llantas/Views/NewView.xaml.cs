using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GGGC.Admin.ERP.Catalogs.Products.Classes.Llantas.ViewModels;
using Telerik.Windows.Controls;
using GGGC.Modules.Data;

namespace GGGC.Admin.ERP.Catalogs.Products.Classes.Llantas.Views
{
    /// <summary>
    /// Lógica de interacción para NewView.xaml
    /// </summary>
    public partial class NewView : UserControl
    {
        private int _errors = 0;
        //private Student _student = new Student();
        private ProductViewModel _producto = new ProductViewModel();
        private AccesoDatos sCen;
        private AccesoDatos sLRG;
        private AccesoDatos sCLT;

        private int IDAcum = 0;
        private int IDAmor = 0;
        private int IDMisc = 0;
        private int IDRef = 0;
        private int intLinea = 0;
        private int intMarca = 0;
        private const int intCLASE = 2;
        private const int intGRUPO = 4;

        public NewView()
        {
            InitializeComponent();
            StyleManager.SetTheme(radTabControl, new Windows8TouchTheme());
            //StyleManager.SetTheme(radTabControl, new Windows8TouchTheme());
            //_student.Divisions = new List<string>();
            //_student.Divisions.Add(string.Empty);
            //_student.Divisions.Add("Fifth");
            //_student.Divisions.Add("Sixth");
            //_student.Divisions.Add("Seventh");
           // grdStudentData.DataContext = _student;
            grdStudentData.DataContext = _producto;
            cargarLineas();
            // DataContext = ProductViewModel;
        }

        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Insert logic of the page
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }

        private void cargarLineas()
        {
            //try
            //{
            //  MessageBox.Show("en lineas ");
            sCen = new AccesoDatos(6);
            System.Data.DataSet ds = new System.Data.DataSet();
            string sSQL = "SELECT LineID, LineDescription AS Linea FROM  Inventory_Line WHERE ClassID = 2 AND GroupID=4 ORDER BY LineDescription ";

            //   MessageBox.Show("sql " + sSQL);

            //ds = sCen.BaseDatos.ConsultaDataset("spInventario");
            //DataTable tbl = sCen.BaseDatos.Consulta(sSQL);             
            //  string sSQL = "SELECT MarcaID, Marca FROM tblMarcas ORDER BY Marca ASC";

            //cboLine.ItemsSource = sCen.BaseDatos.Consulta(sSQL);
            cboLine.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.Bas
            //   MessageBox.Show("consulta");
            cboLine.SelectedValuePath = "LineID";
            cboLine.DisplayMemberPath = "Linea";
            cboLine.SelectedIndex = -1;

            sSQL = "SELECT * FROM  Inventory_Brand WHERE BrandID IN(103,104,200) ORDER BY Brand ";
            cboMarca.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
            cboMarca.SelectedValuePath = "BrandID";
            cboMarca.DisplayMemberPath = "Brand";
            cboMarca.SelectedIndex = -1;

            sSQL = "SELECT * FROM  Inventory_Unit ORDER BY UnitID ";
            cboUnidad.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
            cboUnidad.SelectedValuePath = "UnitID";
            cboUnidad.DisplayMemberPath = "Unit";
            cboUnidad.SelectedIndex = 0;

            sSQL = "SELECT * FROM  Inventory_Tax ORDER BY TaxID ";
            cboImpuesto.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
            cboImpuesto.SelectedValuePath = "TaxID";
            cboImpuesto.DisplayMemberPath = "Tax";
            cboImpuesto.SelectedIndex = 1;

            sSQL = "SELECT * FROM  Inventory_Status ORDER BY StatusID ";
            cboEstatus.ItemsSource = sCen.BaseDatos.Consulta(sSQL).DefaultView; //datos.BaseDatos.Consulta(sSQL);
            cboEstatus.SelectedValuePath = "StatusID";
            cboEstatus.DisplayMemberPath = "Status";
            cboEstatus.SelectedIndex = 0;




            //}
            //catch (Exception ex)
            //{
            //    // MessageBox.Show("Error en cargarDatos: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }


        private void txtCodigo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtCodigo.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
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

        private void txtDescripcion_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtDescripcion.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboLine.IsDropDownOpen = true;
            }

        }

        private void cboLine_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboLine.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboMarca.IsDropDownOpen = true;
            }
        }

        private void cboMarca_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboMarca.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboImpuesto.IsDropDownOpen = true;
            }

        }

        private void cboImpuesto_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboImpuesto.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboUnidad.IsDropDownOpen = true;
            }
        }

        private void cboUnidad_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboUnidad.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboEstatus.IsDropDownOpen = true;
            }

        }

        private void cboLine_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cboLine.SelectedIndex >= 0)
            {
                string strLinea = cboLine.SelectedValue.ToString();
                intLinea = Convert.ToInt32(strLinea);

                this.txtID.Text = intCLASE.ToString() + intGRUPO.ToString() + intLinea.ToString() + intMarca.ToString() + IDRef.ToString();
                //this.txtID.
            }


        }

        private void cboMarca_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cboMarca.SelectedIndex >= 0)
            {
                string strMarca = cboMarca.SelectedValue.ToString();

                switch (strMarca)
                {
                    case "200":
                        strMarca = "5";
                        break;

                    case "104":
                        strMarca = "8";
                        break;

                    case "103":
                        strMarca = "7";
                        break;


                }
                intMarca = Convert.ToInt32(strMarca);

                this.txtID.Text = intCLASE.ToString() + intGRUPO.ToString() + intLinea.ToString() + intMarca.ToString() + IDRef.ToString();
                //this.txtID.
            }
        }
    }
}
