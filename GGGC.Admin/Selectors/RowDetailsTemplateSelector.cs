using System.Windows.Controls;
using System.Windows;
using Telerik.Windows.Controls.GridView;
//using ERP.Repository.Service;

namespace GGGC.Admin
{
    public class RowDetailsTemplateSelector: DataTemplateSelector
    {
        public DataTemplate OrderRowDetailsTemplate { get; set; }
        public DataTemplate PurchaseOrderRowDetailsTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var row = container as GridViewRow;

            //if (row.Item is SalesOrderHeader)
            //{
            //    return this.OrderRowDetailsTemplate;
            //}

            //if (row.Item is PurchaseOrderHeader)
            //{
            //    return this.PurchaseOrderRowDetailsTemplate;
            //}

            return base.SelectTemplate(item, container);
        }
    }
}
