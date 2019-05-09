namespace GGGC.Admin.AZ.Inventarios
{
    partial class Rptmeta
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphGroup graphGroup4 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphTitle graphTitle1 = new Telerik.Reporting.GraphTitle();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Drawing.ColorPalette colorPalette1 = new Telerik.Reporting.Drawing.ColorPalette();
            Telerik.Reporting.GraphGroup graphGroup3 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.graph1 = new Telerik.Reporting.Graph();
            this.cartesianCoordinateSystem1 = new Telerik.Reporting.CartesianCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.barSeries1 = new Telerik.Reporting.BarSeries();
            this.lineSeries4 = new Telerik.Reporting.LineSeries();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.4D);
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(9.15D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1,
            this.textBox1,
            this.textBox4});
            this.detail.Name = "detail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.55D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.35D), Telerik.Reporting.Drawing.Unit.Cm(0.566D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.435D), Telerik.Reporting.Drawing.Unit.Cm(0.704D));
            this.textBox4.Style.BorderColor.Default = System.Drawing.Color.Transparent;
            this.textBox4.Style.Color = System.Drawing.Color.Gray;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Arial";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Value = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.35D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.435D), Telerik.Reporting.Drawing.Unit.Cm(0.566D));
            this.textBox1.Style.BorderColor.Default = System.Drawing.Color.Transparent;
            this.textBox1.Style.Color = System.Drawing.Color.Gray;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Arial";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "REPORTE VENTAS";
            // 
            // graph1
            // 
            graphGroup1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.Date"));
            graphGroup1.Name = "itemGroup";
            graphGroup1.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.Date", Telerik.Reporting.SortDirection.Asc));
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.cartesianCoordinateSystem1);
            this.graph1.DataSource = null;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.7D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Series.Add(this.barSeries1);
            this.graph1.Series.Add(this.lineSeries4);
            graphGroup4.Name = "seriesGroup";
            this.graph1.SeriesGroups.Add(graphGroup4);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.071D), Telerik.Reporting.Drawing.Unit.Inch(4.3D));
            this.graph1.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            graphTitle1.Position = Telerik.Reporting.GraphItemPosition.TopCenter;
            graphTitle1.Style.LineColor = System.Drawing.Color.LightGray;
            graphTitle1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            graphTitle1.Text = "";
            this.graph1.Titles.Add(graphTitle1);
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
            this.graphAxis2.LabelFormat = "{0:C2}";
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.Name = "GraphAxis2";
            this.graphAxis2.Scale = numericalScale1;
            this.graphAxis2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            // 
            // barSeries1
            // 
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.Date"));
            graphGroup2.Name = "itemGroup";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.Date", Telerik.Reporting.SortDirection.Asc));
            this.barSeries1.CategoryGroup = graphGroup2;
            colorPalette1.Colors.Add(System.Drawing.Color.LimeGreen);
            this.barSeries1.ColorPalette = colorPalette1;
            this.barSeries1.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.barSeries1.DataPointLabelConnectorStyle.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.barSeries1.DataPointLabelConnectorStyle.Padding.Top = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.barSeries1.DataPointLabelConnectorStyle.Visible = false;
            this.barSeries1.DataPointLabelStyle.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.barSeries1.LegendItem.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.barSeries1.LegendItem.Value = "Ventas";
            graphGroup3.Name = "seriesGroup";
            this.barSeries1.SeriesGroup = graphGroup3;
            this.barSeries1.Y = "= Fields.Total";
            // 
            // lineSeries4
            // 
            this.lineSeries4.CategoryGroup = graphGroup2;
            this.lineSeries4.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.lineSeries4.DataPointLabelFormat = "{0:C2}";
            this.lineSeries4.DataPointLabelStyle.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.lineSeries4.DataPointLabelStyle.Visible = true;
            this.lineSeries4.DataPointStyle.Visible = true;
            this.lineSeries4.LegendItem.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.lineSeries4.LegendItem.Value = "Meta";
            this.lineSeries4.LineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.lineSeries4.MarkerMaxSize = Telerik.Reporting.Drawing.Unit.Pixel(50D);
            this.lineSeries4.MarkerMinSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries4.MarkerSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries4.MissingValuesLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.lineSeries4.MissingValuesLineStyle.Visible = false;
            this.lineSeries4.Name = "lineSeries2";
            this.lineSeries4.SeriesGroup = graphGroup3;
            this.lineSeries4.Size = null;
            this.lineSeries4.Y = "=100000.00";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.CalculatedFields.AddRange(new Telerik.Reporting.CalculatedField[] {
            new Telerik.Reporting.CalculatedField("Field1", typeof(int), "Fields.Date"),
            new Telerik.Reporting.CalculatedField("Field2", typeof(decimal), "Fields.Total")});
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // Rptmeta
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "Rptmeta";
            this.PageSettings.ContinuousPaper = false;
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.LetterRotated;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Cm;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(8.071D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.CartesianCoordinateSystem cartesianCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.BarSeries barSeries1;
        private Telerik.Reporting.LineSeries lineSeries4;
        private Telerik.Reporting.ObjectDataSource objectDataSource1;
    }
}