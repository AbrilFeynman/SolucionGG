namespace GGGC.Admin.AZ.Inventarios
{
    partial class RptSucursales
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Drawing.ColorPalette colorPalette1 = new Telerik.Reporting.Drawing.ColorPalette();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            this.graph1 = new Telerik.Reporting.Graph();
            this.cartesianCoordinateSystem1 = new Telerik.Reporting.CartesianCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.barSeries1 = new Telerik.Reporting.BarSeries();
            this.textBox1 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(4.6000003814697266D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1});
            this.detail.Name = "detail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.CalculatedFields.AddRange(new Telerik.Reporting.CalculatedField[] {
            new Telerik.Reporting.CalculatedField("Field1", typeof(string), "Fields.Base"),
            new Telerik.Reporting.CalculatedField("Field2", typeof(int), "Fields.Cantidad"),
            new Telerik.Reporting.CalculatedField("Field3", typeof(string), "Fields.Articulo")});
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // graph1
            // 
            graphGroup1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Field1"));
            graphGroup1.Name = "categoryGroup1";
            graphGroup1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Field1", Telerik.Reporting.SortDirection.Asc));
            this.graph1.CategoryGroups.Add(graphGroup1);
            colorPalette1.Colors.Add(System.Drawing.Color.DodgerBlue);
            colorPalette1.Colors.Add(System.Drawing.Color.Black);
            colorPalette1.Colors.Add(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))));
            colorPalette1.Colors.Add(System.Drawing.Color.Maroon);
            colorPalette1.Colors.Add(System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0))))));
            this.graph1.ColorPalette = colorPalette1;
            this.graph1.CoordinateSystems.Add(this.cartesianCoordinateSystem1);
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Series.Add(this.barSeries1);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.7000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(2.2000000476837158D));
            // 
            // cartesianCoordinateSystem1
            // 
            this.cartesianCoordinateSystem1.Name = "cartesianCoordinateSystem1";
            this.cartesianCoordinateSystem1.XAxis = this.graphAxis1;
            this.cartesianCoordinateSystem1.YAxis = this.graphAxis2;
            // 
            // graphAxis1
            // 
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "GraphAxis1";
            this.graphAxis1.Scale = categoryScale1;
            // 
            // graphAxis2
            // 
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.Name = "GraphAxis2";
            this.graphAxis2.Scale = numericalScale1;
            // 
            // barSeries1
            // 
            this.barSeries1.CategoryGroup = graphGroup1;
            this.barSeries1.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.barSeries1.DataPointLabelConnectorStyle.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.barSeries1.DataPointLabelConnectorStyle.Padding.Top = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.barSeries1.LegendItem.Value = "=Fields.Field3";
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Field1"));
            graphGroup2.Name = "seriesGroup1";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Field1", Telerik.Reporting.SortDirection.Asc));
            this.barSeries1.SeriesGroup = graphGroup2;
            this.barSeries1.Y = "=Sum(Fields.Field2)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.064000129699707D), Telerik.Reporting.Drawing.Unit.Cm(0.76199996471405029D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.975000381469727D), Telerik.Reporting.Drawing.Unit.Cm(1.0160000324249268D));
            this.textBox1.Style.BorderColor.Default = System.Drawing.Color.Transparent;
            this.textBox1.Style.Color = System.Drawing.Color.Gray;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Arial";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Value = "REPORTE INVENTARIO - CODIGO POR SUCURSALES";
            // 
            // RptSucursales
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "RptSucursales";
            this.PageSettings.ContinuousPaper = false;
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Cm;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.ObjectDataSource objectDataSource1;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.CartesianCoordinateSystem cartesianCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.BarSeries barSeries1;
        private Telerik.Reporting.TextBox textBox1;
    }
}