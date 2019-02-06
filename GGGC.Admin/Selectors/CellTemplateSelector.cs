using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace GGGC.Admin
{
    public class CellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ColorTemplate { get; set; }
        public DataTemplate CreditRatingTemplate { get; set; }
        public DataTemplate ActiveStatusTemplate { get; set; }
        public DataTemplate PreferenceTemplate { get; set; }
        public DataTemplate OnlineOrderStatusTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var cell = container as GridViewCell;
            if (cell != null)
            {
                var columnHeader = cell.Column.Header.ToString();
                switch (columnHeader)
                {
                    case "Color":
                        return this.ColorTemplate;
                    case "Credit Rating":
                        return this.CreditRatingTemplate;
                    case "Active Status":
                        return this.ActiveStatusTemplate;
                    case "Preference":
                        return this.PreferenceTemplate;
                    case "Is Online Order":
                        return this.OnlineOrderStatusTemplate;
                    default:
                        break;
                }

            }

            return base.SelectTemplate(item, container);
        }
    }
}
