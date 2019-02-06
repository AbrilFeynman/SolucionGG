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
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Filtering.Editors;
using GGGC.Modules.Data;

namespace GGGC.Admin.Modules.Ektelesis.LRG.Views.History
{
    /// <summary>
    /// Lógica de interacción para CLTView.xaml
    /// </summary>
    public partial class CLTView : UserControl
    {
        private delegate void UpdateDelegate(string i);
        private delegate void NoArgDelegate();

        public delegate void UpdateStatusBarEventHandler(string message);
        public event UpdateStatusBarEventHandler UpdateStatusBar;
        public CLTView()
        {
            InitializeComponent();
            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(focus_IsVisibleChanged);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.txtFolio.IsEnabled = false;
            Refresh(txtFolio);

            this.btnBuscar.IsEnabled = false;
            Refresh(btnBuscar);

            //this.rgv.Visibility=Visibility.Hidden;
            //Refresh(rgv);

            cargarDatos(txtFolio.Text.Trim());



            this.btnBuscar.IsEnabled = true;
            Refresh(btnBuscar);


            this.txtFolio.IsEnabled = true;
            Refresh(txtFolio);

            this.rgv.Visibility = Visibility.Visible;
            Refresh(rgv);



            this.txtFolio.Focus();
            //this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(focus_IsVisibleChanged);
            // Refresh(txtFolio);
            this.txtFolio.SelectAll();
            //Refresh(txtFolio);
        }

        void focus_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate()
                {
                    txtFolio.Focus();
                }));
            }
        }

        private void cargarDatos(string strCliente)
        {
            try
            {
                AccesoDatos sCen = new AccesoDatos(10);
                string sSQL = "SELECT * FROM gg_fncVentasPorCliente('" + strCliente + "') ";
                rgv.ItemsSource = sCen.BaseDatos.Consulta(sSQL);
                //  rgv.Columns[0].IsVisible = false;
                //[Codigo_Tipo_De_Agrupacion_De_Lineas] AS Codigo,
                //radGridView1.DataSource = sCen.BaseDatos.Consulta(sSQL);
                //radGridView1.BestFitColumns();
                //this.rgv.EnableAlternatingRowColor = true;
                //this.radGridView1.EnableGrouping = true;
                //this.radGridView1.EnableFiltering = true;
                //this.radGridView1.AutoGenerateColumns = true;
                //  this.rgv.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                ((GridViewDataColumn)this.rgv.Columns["Fecha"]).DataFormatString = "dd-MMM-yyyy";
                ((GridViewDataColumn)this.rgv.Columns["Total"]).DataFormatString = "{0:C2}";
                ((GridViewDataColumn)this.rgv.Columns["PLista"]).DataFormatString = "{0:C2}";
                ((GridViewDataColumn)this.rgv.Columns["PVenta"]).DataFormatString = "{0:C2}";



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

            //  this.lblRows.Text = rgv.Items.Count.ToString() + " Registros";
            this.rgv.Columns["Fecha"].ShowFieldFilters = false;
            this.rgv.Columns["Fecha"].IsFilterable = false;
            //  this.rgv.Columns["Fecha"].fo
            this.rgv.Columns["Estatus"].IsFilterable = false;
            this.rgv.Columns["Estatus"].ShowFieldFilters = false;
            //this.rgv.Columns["Unidad"].IsFilterable = false;
            //this.rgv.Columns["Unidad"].ShowFieldFilters = false;

            this.rgv.Columns["Nombre"].Width = 250;
            this.rgv.Columns["Nivel"].Background = Brushes.LightGray;

            //this.rgv.Columns["Unidad"].Width = 50;
            //this.rgv.Columns["Estatus"].Width = 50;


            //}
        }

        public static void Refresh(DependencyObject obj)
        {

            obj.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle,

                (NoArgDelegate)delegate { });

        }

        private void rgv_Filtered(object sender, Telerik.Windows.Controls.GridView.GridViewFilteredEventArgs e)
        {
            //  this.lblRows.Text = rgv.Items.Count.ToString() + " Registros";
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                Button_Click(sender, e);
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
    }
}
