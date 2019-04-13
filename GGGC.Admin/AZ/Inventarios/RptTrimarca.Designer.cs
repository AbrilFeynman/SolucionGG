namespace GGGC.Admin.AZ.Inventarios
{
    partial class RptTrimarca
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
            this.polarCoordinateSystem1 = new Telerik.Reporting.PolarCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.barSeries1 = new Telerik.Reporting.BarSeries();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(4.5999608039855957D);
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
            new Telerik.Reporting.CalculatedField("Field1", typeof(string), "Fields.Marca"),
            new Telerik.Reporting.CalculatedField("Field2", typeof(int), "Fields.Cantidad"),
            new Telerik.Reporting.CalculatedField("Field3", typeof(string), "Fields.Descripcion")});
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // graph1
            // 
            graphGroup1.Name = "categoryGroup1";
            this.graph1.CategoryGroups.Add(graphGroup1);
            colorPalette1.Colors.Add(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))));
            colorPalette1.Colors.Add(System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))));
            colorPalette1.Colors.Add(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))));
            colorPalette1.Colors.Add(System.Drawing.Color.Turquoise);
            this.graph1.ColorPalette = colorPalette1;
            this.graph1.CoordinateSystems.Add(this.polarCoordinateSystem1);
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5240001678466797D), Telerik.Reporting.Drawing.Unit.Cm(1.2699999809265137D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Series.Add(this.barSeries1);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.989999771118164D), Telerik.Reporting.Drawing.Unit.Cm(5.8400001525878906D));
            // 
            // polarCoordinateSystem1
            // 
            this.polarCoordinateSystem1.AngularAxis = this.graphAxis2;
            this.polarCoordinateSystem1.Name = "polarCoordinateSystem1";
            this.polarCoordinateSystem1.RadialAxis = this.graphAxis1;
            // 
            // graphAxis1
            // 
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "GraphAxis1";
            categoryScale1.SpacingSlotCount = 0D;
            this.graphAxis1.Scale = categoryScale1;
            this.graphAxis1.Style.Visible = false;
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
            this.graphAxis2.Style.Visible = false;
            // 
            // barSeries1
            // 
            this.barSeries1.ArrangeMode = Telerik.Reporting.GraphSeriesArrangeMode.Stacked100;
            this.barSeries1.CategoryGroup = graphGroup1;
            this.barSeries1.CoordinateSystem = this.polarCoordinateSystem1;
            this.barSeries1.DataPointLabel = "=CDbl(Fields.Field2) / CDbl(Exec(\'graph1\', Sum(Fields.Field2)))";
            this.barSeries1.DataPointLabelConnectorStyle.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.barSeries1.DataPointLabelConnectorStyle.Padding.Top = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.barSeries1.DataPointLabelFormat = "{0:0.00%}";
            this.barSeries1.LegendItem.Value = "=Fields.Field3";
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Field3"));
            graphGroup2.Name = "seriesGroup1";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=sum(Fields.Cantidad)", Telerik.Reporting.SortDirection.Asc));
            this.barSeries1.SeriesGroup = graphGroup2;
            this.barSeries1.X = "=Sum(Fields.Field2)";
            // 
            // RptTrimarca
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "RptTrimarca";
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
        private Telerik.Reporting.PolarCoordinateSystem polarCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.BarSeries barSeries1;
    }
}