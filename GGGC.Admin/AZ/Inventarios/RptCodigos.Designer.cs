namespace GGGC.Admin.AZ.Inventarios
{
    partial class RptCodigos
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.graph1 = new Telerik.Reporting.Graph();
            this.cartesianCoordinateSystem1 = new Telerik.Reporting.CartesianCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.lineSeries1 = new Telerik.Reporting.LineSeries();
            this.lineSeries2 = new Telerik.Reporting.LineSeries();
            this.lineSeries3 = new Telerik.Reporting.LineSeries();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.6D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox4});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(22.098D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1});
            this.detail.Name = "detail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.8D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // graph1
            // 
            graphGroup1.Groupings.Add(new Telerik.Reporting.Grouping("Fields.Field2"));
            graphGroup1.Name = "categoryGroup";
            graphGroup1.Sortings.Add(new Telerik.Reporting.Sorting("Fields.Field1", Telerik.Reporting.SortDirection.Asc));
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.cartesianCoordinateSystem1);
            this.graph1.Legend.Position = Telerik.Reporting.GraphItemPosition.TopCenter;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.4D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Series.Add(this.lineSeries1);
            this.graph1.Series.Add(this.lineSeries2);
            this.graph1.Series.Add(this.lineSeries3);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.7D), Telerik.Reporting.Drawing.Unit.Inch(4.6D));
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
            this.graphAxis1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // graphAxis2
            // 
            this.graphAxis2.LabelFormat = "{0:#.}";
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.Name = "GraphAxis2";
            this.graphAxis2.Scale = numericalScale1;
            this.graphAxis2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // lineSeries1
            // 
            this.lineSeries1.CategoryGroup = graphGroup1;
            this.lineSeries1.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.lineSeries1.DataPointStyle.Visible = false;
            this.lineSeries1.LegendItem.Value = "2019";
            this.lineSeries1.LineStyle.BackgroundColor = System.Drawing.Color.Black;
            this.lineSeries1.LineStyle.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lineSeries1.LineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(3D);
            this.lineSeries1.MarkerMaxSize = Telerik.Reporting.Drawing.Unit.Pixel(50D);
            this.lineSeries1.MarkerMinSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries1.MarkerSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries1.MissingValuesLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            graphGroup2.Name = "seriesGroup";
            this.lineSeries1.SeriesGroup = graphGroup2;
            this.lineSeries1.Size = null;
            this.lineSeries1.Y = "=Fields.Field5";
            // 
            // lineSeries2
            // 
            this.lineSeries2.CategoryGroup = graphGroup1;
            this.lineSeries2.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.lineSeries2.DataPointStyle.Visible = false;
            this.lineSeries2.LegendItem.Value = "2018";
            this.lineSeries2.LineStyle.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lineSeries2.LineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(3D);
            this.lineSeries2.MarkerMaxSize = Telerik.Reporting.Drawing.Unit.Pixel(50D);
            this.lineSeries2.MarkerMinSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries2.MarkerSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries2.MissingValuesLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.lineSeries2.SeriesGroup = graphGroup2;
            this.lineSeries2.Size = null;
            this.lineSeries2.Y = "=Fields.Field6";
            // 
            // lineSeries3
            // 
            this.lineSeries3.CategoryGroup = graphGroup1;
            this.lineSeries3.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.lineSeries3.DataPointStyle.Visible = false;
            this.lineSeries3.LegendItem.Value = "2017";
            this.lineSeries3.LineStyle.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(192)))));
            this.lineSeries3.LineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(3D);
            this.lineSeries3.MarkerMaxSize = Telerik.Reporting.Drawing.Unit.Pixel(50D);
            this.lineSeries3.MarkerMinSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries3.MarkerSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries3.MissingValuesLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.lineSeries3.SeriesGroup = graphGroup2;
            this.lineSeries3.Size = null;
            this.lineSeries3.Y = "=Fields.Field7";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.532D), Telerik.Reporting.Drawing.Unit.Cm(0.762D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.435D), Telerik.Reporting.Drawing.Unit.Cm(0.566D));
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
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.532D), Telerik.Reporting.Drawing.Unit.Cm(0.196D));
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
            // objectDataSource1
            // 
            this.objectDataSource1.CalculatedFields.AddRange(new Telerik.Reporting.CalculatedField[] {
            new Telerik.Reporting.CalculatedField("Field1", typeof(int), "Fields.Mes"),
            new Telerik.Reporting.CalculatedField("Field2", typeof(string), "Fields.Mesname"),
            new Telerik.Reporting.CalculatedField("Field3", typeof(string), "Fields.Codigo"),
            new Telerik.Reporting.CalculatedField("Field4", typeof(string), "Fields.Descipcion"),
            new Telerik.Reporting.CalculatedField("Field5", typeof(decimal), "Fields.Total"),
            new Telerik.Reporting.CalculatedField("Field6", typeof(decimal), "Fields.Total2"),
            new Telerik.Reporting.CalculatedField("Field7", typeof(decimal), "Fields.Total3")});
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // RptCodigos
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "RptCodigos";
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
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.CartesianCoordinateSystem cartesianCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.LineSeries lineSeries1;
        private Telerik.Reporting.LineSeries lineSeries2;
        private Telerik.Reporting.LineSeries lineSeries3;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.ObjectDataSource objectDataSource1;
    }
}