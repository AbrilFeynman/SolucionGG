using System.Windows.Controls;
using System.Windows;

namespace GGGC.Admin
{
    public class CurrentViewTemplateSelector: DataTemplateSelector
    {
        public DataTemplate GridViewTemplate { get; set; }
        public DataTemplate DocumentTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            //if (item is IDataViewModel)
            //{
            //    return this.GridViewTemplate;
            //}

            //if (item is IDocumentViewModel)
            //{
            //    return this.DocumentTemplate;
            //}

            return base.SelectTemplate(item, container);
        }
    }
}
