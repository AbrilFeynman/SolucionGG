using System.Windows;
using System.Windows.Controls;

namespace GGGC.Admin
{
    public class TreeViewItemConteinerStyleSelector : StyleSelector
    {
        public Style TopLevelStyle { get; set; }
        public Style MidLevelStyle { get; set; }
        public Style BottomLevelStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            var levelCounter = 1;
            var navigationNode = item as NavigationNode;

            if (navigationNode != null)
            {
                while (navigationNode.Parent != null)
                {
                    levelCounter++;
                    navigationNode = navigationNode.Parent;
                }

                switch (levelCounter)
                {
                    case 1:
                        return this.TopLevelStyle; 
                    case 2:
                        return this.MidLevelStyle; 
                    case 3:
                        return this.BottomLevelStyle; 
                }
            }


            return base.SelectStyle(item, container);
        }
    }
}
