using System;
using System.Collections.Generic;
using System.Data;
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
using Core.Common.UI.Core;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
//using  Telerik.win;
using GGGC.Modules.Data;

namespace GGGC.Admin.ERP.Modules.Sales.Estimates.Views
{
    /// <summary>
    /// Lógica de interacción para QueryView.xaml
    /// </summary>
    public partial class QueryView : UserControlViewBase
    {
        System.Data.DataTable dt = new DataTable();
        public QueryView()
        {
            InitializeComponent();
            //this.data = Enumerable.Range(0, 100).ToList();
            //cargarDatos();
            this.rgv.ItemsSource = DataEncabezado;

            //this.listBox.ItemsSource = this.data.Take(this.radDataPager.PageSize).ToList();

        }

        private void test()
        {
            //Telerik.Windows.Controls.GridView.gridviewt
            //GridViewTemplate firstChildtemplate = new GridViewTemplate();
            //firstChildtemplate.DataSource = dtblLowSampleTestResult;//It's a datatable which contains the lower tests
            //gridWorkingList.MasterTemplate.Templates.Add(firstChildtemplate);

            //GridViewRelation relation = new GridViewRelation(gridWorkingList.MasterTemplate, firstChildtemplate);
            //relation.ChildTemplate = firstChildtemplate;
            //relation.RelationName = "LowerTests";
            //relation.ParentColumnNames.Add("sample_test_result_id");
            //relation.ChildColumnNames.Add("low_samp_test_res_rid");

            //gridWorkingList.Relations.Add(relation);
        }


        DataTable _DataEncabezado;
        public DataTable DataEncabezado
        {
            get
            {
                AccesoDatos sCen = new AccesoDatos(83);
                string sSQL = "SELECT * FROM viewAdjustments";

                //rgv.ItemsSource = sCen.BaseDatos.Consulta(sSQL);

               // dt = sCen.BaseDatos.Consulta(sSQL);
                _DataEncabezado = sCen.BaseDatos.Consulta(sSQL);
                //}



                return _DataEncabezado;
            }
        }

        DataTable _DataDetalle;
        public DataTable DataDetalle
        {
            get
            {
                AccesoDatos sCen = new AccesoDatos(83);
                string sSQL = "SELECT * FROM [Inventory_AdjustmentDetail_14]";
                //rgv.ItemsSource = sCen.BaseDatos.Consulta(sSQL);
                // dt = sCen.BaseDatos.Consulta(sSQL);
                _DataDetalle = sCen.BaseDatos.Consulta(sSQL);

                //_DataDetalle = new OrdersDataTable();
                //GetOrdersRows().ToList().ForEach(_DataDetalle.ImportRow);
                //}


                //rgv.HIE
                return _DataDetalle;
            }
        }


        private void relacion()
        {
            //this.productsTableAdapter.Fill(this.nwindDataSet.Products);
            //this.suppliersTableAdapter.Fill(this.nwindDataSet.Suppliers);

            //radGridView1.DataSource = nwindDataSet.Suppliers;
           // GridViewTemplate 
            //rgv.temGridViewTemplate template = new GridViewTemplate();
            //template.DataSource = nwindDataSet.Products;
            //radGridView1.MasterTemplate.Templates.Add(template);

            //GridViewRelation relation = new GridViewRelation(radGridView1.MasterTemplate);
            //relation.ChildTemplate = template;
            //relation.RelationName = "SuppliersProducts";
            //relation.ParentColumnNames.Add("SupplierID");
            //relation.ChildColumnNames.Add("SupplierID");
            //radGridView1.Relations.Add(relation);
        }


        //public System.Data.DataRow[] GetRows()
        //{
        //    if (DataEncabezado.Rows.Count==0)
        //    {
        //        return new System.Data.DataRow[0];
        //    }
        //    else
        //    {
        //        return ((System.Data.DataRow[])(base.GetChildRows(this.Table.ChildRelations["FK_Orders_Customers"])));
        //    }
        //}
        private void cargarDatos()
        {
            try
            {

                //GridViewTableDefinition definition = new GridViewTableDefinition();
                //definition.Relation = new PropertyRelation("Header_Details");
                //rgv.ChildTableDefinitions.Add(definition);
                //((Telerik.Windows.Controls.RadGridView)sender).ItemsSource = ds2;
                
                AccesoDatos sCen = new AccesoDatos(83);
                string sSQL = "SELECT * FROM viewAdjustments";
                
                //rgv.ItemsSource = sCen.BaseDatos.Consulta(sSQL);

                dt = sCen.BaseDatos.Consulta(sSQL);

                rgv.ItemsSource = dt;

                //this.data = Enumerable.Range(0, 100).ToList();
                //this.radDataPager.ItemCount = dt.Rows.Count;
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
                //  this.rgv.Columns["Linea"].AggregateFunctions.Add(CountFunction);

                //CountFunction f = new CountFunction();
                //((GridViewDataColumn)this.rgv.Columns["Linea"]).AggregateFunctions.Add(f);

                ////Total miles: {0:F2}
                //this.rgv.Columns["Linea"].AggregateFunctions.Add(f);
                //this.rgv.Columns[0].AggregateFunctions.Add(new Telerik.Windows.Data.CountFunction { Caption = "Count (Red Flags):" });
                //this.rgv.CalculateAggregates();
                //playersGrid.Columns[0].AggregateFunctions.Add(aggregate);
                //var aggregate = new AggregateFunction<Player, int>()
                //{
                //    AggregationExpression = players =>
                //     players.Where(player => player.ToString().StartsWith("B")).Select(x => x.Number).Sum(),
                //    Caption = "Total B"

                //};

            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error en cargarDatos: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        private void radDataPager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            if (this.dt != null)
            {
                this.rgv.ItemsSource = this.dt.ToString().Skip(e.NewPageIndex * this.radDataPager.PageSize).Take(this.radDataPager.PageSize).ToList();
            }
        }
    }

}
