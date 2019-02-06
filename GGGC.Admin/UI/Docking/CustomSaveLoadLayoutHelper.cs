using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace GGGC.Admin.UI.Docking
{
    public class CustomSaveLoadLayoutHelper : DefaultSaveLoadLayoutHelper
    {
        protected override void ElementLoadedOverride(string serializationTag, DependencyObject element)
        {
            base.ElementLoadedOverride(serializationTag, element);
            var document = element as RadDocumentPane;
            if (document != null)
            {
                // Restore the content for the loaded document.
            }
        }
    }
}
