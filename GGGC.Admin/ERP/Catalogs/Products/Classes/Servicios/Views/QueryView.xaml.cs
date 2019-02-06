using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Filtering.Editors;
using GGGC.Modules.Data;

namespace GGGC.Admin.ERP.Catalogs.Products.Classes.Servicios.Views
{
    /// <summary>
    /// Lógica de interacción para QueryView.xaml
    /// </summary>
    public partial class QueryView : UserControl
    {
        public QueryView()
        {
            InitializeComponent();
            cargarDatos();

           // StyleManager.SetTheme(radDataPager, new Windows8Theme());

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (GlobalModule.blnCRefaccion == true)
            {
                rgv.ItemsSource = null;
                cargarDatos();
                GlobalModule.blnCRefaccion = false;
            }
            else
            {
                GlobalModule.blnCRefaccion = false;
            }
            // code goes here
            //  int dbFolio = strFolio();
            //int gvFolio = 
        }

        public void Test()
        {
            MessageBox.Show("hello");
        }


        private int strFolio()
        {
            int folio = 0;
            try
            {
                //Series y Folios
                AccesoDatos sCen = new AccesoDatos(9);
                object Obj;
                string sSQL = "SELECT RefaccionID FROM tblConfig;";
                Obj = sCen.BaseDatos.ConsultaDato(sSQL); ;//
                if (Obj != null)
                {
                    if (!Convert.IsDBNull(Obj))
                    {
                        folio = Convert.ToInt32(Obj);
                        // folio = intCLASE.ToString() + intGRUPO.ToString() + intLinea.ToString() + intMarca.ToString() + IDRef.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Al Obtener el Folio: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Close();
            }
            return folio;
        }

        private void cargarDatos()
        {
            try
            {
                AccesoDatos sCen = new AccesoDatos(9);
                string sSQL = "SELECT * FROM vtaCatalogo_Servicios";

                DataTable dt = new DataTable();
                dt = sCen.BaseDatos.Consulta(sSQL);
                rgv.ItemsSource = dt;
              //  radDataPager.PagedSource = sCen.BaseDatos.Consulta(sSQL);


                //IEnumerable itemsSource = dt.AsEnumerable();


                //var pagedSource = new System.Windows.Data.CollectionView(itemsSource);
                //this.radDataPager.Source = pagedSource;
               // this.itemsControl.ItemsSource = pagedSource;


                //  rgv.Columns[0].IsVisible = false;
                //[Codigo_Tipo_De_Agrupacion_De_Lineas] AS Codigo,
                //radGridView1.DataSource = sCen.BaseDatos.Consulta(sSQL);
                //radGridView1.BestFitColumns();
                //this.radGridView1.EnableAlternatingRowColor = true;
                //this.radGridView1.EnableGrouping = true;
                //this.radGridView1.EnableFiltering = true;
                //this.radGridView1.AutoGenerateColumns = true;
                //this.radGridView1.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

                //this.rgv.Columns["Fecha"].ShowFieldFilters = false;
                //this.rgv.Columns["Fecha"].IsFilterable = false;
                //this.rgv.Columns["Estatus"].IsFilterable = false;

                //  this.lblRows.Text = rgv.Items.Count.ToString() + " Registros---";


            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error en cargarDatos: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rgv_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            //get the StringFilterEditor in your RadGridView
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;

                //e.Editor.Loaded += (s1, e1) =>
                //{

                //    var textBox = e.Editor.ChildrenOfType<TextBox>().Single();

                //    textBox.TextChanged += (s2, e2) =>
                //    {
                //        //e.Editor.ChildrenOfType<TextBox>().ToString().ToUpper();
                //        textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                //        // textBox.GetBindingExpression(TextBox.TextProperty).
                //        this.lblRows.Text = rgv.Items.Count.ToString() + " Registros";
                //    };

                //};
            }
        }

        private void rgv_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            //if (e.Column.UniqueName == "Descripcion")
            //{
            //    e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsGreaterThan;
            //    e.DefaultOperator2 = Telerik.Windows.Data.FilterOperator.IsLessThan;
            //}

            // if (e.Column.UniqueName == "Descripcion")
            //{
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsEqualTo);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotEqualTo);

            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.StartsWith);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.EndsWith);

            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsGreaterThanOrEqualTo);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsLessThanOrEqualTo);

            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNull);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotNull);

            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsGreaterThan);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsLessThan);

            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsEmpty);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotEmpty);

            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);

            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.Contains;
            e.DefaultOperator2 = Telerik.Windows.Data.FilterOperator.DoesNotContain;

            this.lblRows.Text = rgv.Items.Count.ToString() + " Registros";
            this.rgv.Columns["Fecha"].ShowFieldFilters = false;
            this.rgv.Columns["Fecha"].IsFilterable = false;
            this.rgv.Columns["Estatus"].IsFilterable = false;
            this.rgv.Columns["Estatus"].ShowFieldFilters = false;
            this.rgv.Columns["Unidad"].IsFilterable = false;
            this.rgv.Columns["Unidad"].ShowFieldFilters = false;

            this.rgv.Columns["Descripcion"].Width = 450;
            this.rgv.Columns["Unidad"].Width = 50;
            this.rgv.Columns["Estatus"].Width = 50;


            //}
        }

        private void rgv_Filtered(object sender, Telerik.Windows.Controls.GridView.GridViewFilteredEventArgs e)
        {
            this.lblRows.Text = rgv.Items.Count.ToString() + " Registros";
        }
    }
}
