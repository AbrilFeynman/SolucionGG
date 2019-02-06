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
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;
using   Telerik.Windows.Controls;
//    using Telerik.Windows.Controls.Chart;
//using Telerik.Windows.DataVisualization;
using GGGC.Modules.Data;

namespace GGGC.Admin.Modules.Ektelesis.LRG.Views
{
    /// <summary>
    /// Lógica de interacción para CV_B1.xaml
    /// </summary>
    public partial class CV_B1 : UserControl
    {
        public CV_B1()
        {
            InitializeComponent();
            cargarDatos();
          //  CrearGrafico();
           // TCAR();
            graph();
        }

        private void TCAR()
        {
            //BarSeries barSeries = new BarSeries("Performance", "RepresentativeName");
            //barSeries.Name = "Q1";
            //barSeries.DataPoints.Add(new CategoricalDataPoint(177, "Harley"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(128, "White"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(143, "Smith"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(111, "Jones"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(118, "Marshall"));
            //chart.Series.Add(barSeries);

            //BarSeries barSeries2 = new BarSeries("Performance", "RepresentativeName");
            //barSeries2.Name = "Q2";
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(153, "Harley"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(141, "White"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(130, "Smith"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(88, "Jones"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(109, "Marshall"));
            //chart.Series.Add(barSeries2);

            // this.chartTCAR.Area.View.Palette = KnownPalette.Cold;

            //barSeries = new SolidColorBrush(Colors.Orange);
            //barSeries2.Palette = SolidColorBrush(Colors.DarkRed);
        }

        private void graph()
        {
            AccesoDatos sCen = new AccesoDatos(107);

            int intMes = GlobalModule.intMESCOMOVAMOS;
            int lastDay = DateTime.DaysInMonth(2014, intMes);
            DateTime dtfecha =   new DateTime(2014, intMes, lastDay, 23, 59, DateTime.Now.Second);
            
            string strFecha = dtfecha.ToString("MM/dd/yyyy");
            string sSQL = "SELECT * FROM gg_ComoVamos('" + strFecha + "')";
            DataTable dt = new DataTable();
            dt = sCen.BaseDatos.Consulta(sSQL);
            
            a01.Value = Convert.ToInt32(dt.Rows[0]["VentasAcumuladasAlMesUnudades"].ToString());
            b01.Value = Convert.ToInt32(dt.Rows[0]["PresupuestoDelMesUnidades"].ToString());

            a02.Value = Convert.ToInt32(dt.Rows[1]["VentasAcumuladasAlMesUnudades"].ToString());
            b02.Value = Convert.ToInt32(dt.Rows[1]["PresupuestoDelMesUnidades"].ToString());

            a03.Value = Convert.ToInt32(dt.Rows[2]["VentasAcumuladasAlMesUnudades"].ToString());
            b03.Value = Convert.ToInt32(dt.Rows[2]["PresupuestoDelMesUnidades"].ToString());

            a04.Value = Convert.ToInt32(dt.Rows[3]["VentasAcumuladasAlMesUnudades"].ToString());
            b04.Value = Convert.ToInt32(dt.Rows[3]["PresupuestoDelMesUnidades"].ToString());

            a05.Value = Convert.ToInt32(dt.Rows[4]["VentasAcumuladasAlMesUnudades"].ToString());
            b05.Value = Convert.ToInt32(dt.Rows[4]["PresupuestoDelMesUnidades"].ToString());

            a06.Value = Convert.ToInt32(dt.Rows[5]["VentasAcumuladasAlMesUnudades"].ToString());
            b06.Value = Convert.ToInt32(dt.Rows[5]["PresupuestoDelMesUnidades"].ToString());

            a07.Value = Convert.ToInt32(dt.Rows[6]["VentasAcumuladasAlMesUnudades"].ToString());
            b07.Value = Convert.ToInt32(dt.Rows[6]["PresupuestoDelMesUnidades"].ToString());

            a08.Value = Convert.ToInt32(dt.Rows[7]["VentasAcumuladasAlMesUnudades"].ToString());
            b08.Value = Convert.ToInt32(dt.Rows[7]["PresupuestoDelMesUnidades"].ToString());

        //    a01.Label = "15";



            // a01.Value = 10;
            //b01.Value = 20;

        }

        private void CrearGrafico()
        {

            RadCartesianChart chart = new RadCartesianChart();
            chart.HorizontalAxis = new CategoricalAxis();
            chart.VerticalAxis = new LinearAxis() { Maximum = 100 };
            BarSeries line = new BarSeries();
           // line.Stroke = new SolidColorBrush(Colors.Orange);
            //line.StrokeThickness = 2;
            line.DataPoints.Add(new CategoricalDataPoint() { Value = 20 });
            line.DataPoints.Add(new CategoricalDataPoint() { Value = 40 });
            line.DataPoints.Add(new CategoricalDataPoint() { Value = 35 });
            line.DataPoints.Add(new CategoricalDataPoint() { Value = 40 });
            line.DataPoints.Add(new CategoricalDataPoint() { Value = 30 });
            line.DataPoints.Add(new CategoricalDataPoint() { Value = 50 });
            //line.PaletteMode
            chart.Series.Add(line);

            //chart.Palette = Windows8Palette;
            this.LayoutRoot.Children.Add(chart);

            //BarSeries barSeries = new BarSeries("Performance", "RepresentativeName");
            //barSeries.Name = "Q1";
            //barSeries.DataPoints.Add(new CategoricalDataPoint(177, "Harley"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(128, "White"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(143, "Smith"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(111, "Jones"));
            //barSeries.DataPoints.Add(new CategoricalDataPoint(118, "Marshall"));
            //this.chartTCAR.Series.Add(barSeries);

            //BarSeries barSeries2 = new BarSeries("Performance", "RepresentativeName");
            //barSeries2.Name = "Q2";
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(153, "Harley"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(141, "White"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(130, "Smith"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(88, "Jones"));
            //barSeries2.DataPoints.Add(new CategoricalDataPoint(109, "Marshall"));
            //this.chartTCAR.Series.Add(barSeries2);

            //// this.chartTCAR.Area.View.Palette = KnownPalette.Cold;

            //barSeries.Palette = new PaletteEntry(Color.Yellow, Color.Red);
            //barSeries2.Palette = new PaletteEntry(Color.Green, Color.Aqua);
        }

        private void cargarDatos()
        {
            try
            {
                AccesoDatos sCen = new AccesoDatos(107);
                string strFecha = Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy");
                string sSQL = "SELECT * FROM gg_ComoVamos('" +  strFecha + "')";
               // rgv.ItemsSource = sCen.BaseDatos.Consulta(sSQL);
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
    }
}
