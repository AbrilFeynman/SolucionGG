namespace GGGC.Modules.Reports.LRG.Cotizaciones
{
    partial class lrgCotizacion
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            // 
            // lrgCotizacion
            // 
            this.Name = "lrgCotizacion";
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
								this.pageHeaderSection1,
								this.detail,
								this.pageFooterSection1});
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
				new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
				new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] { styleRule1 });
        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
    }
}