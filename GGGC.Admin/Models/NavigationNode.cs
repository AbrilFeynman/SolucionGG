using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace GGGC.Admin
{
    public class NavigationNode : ViewModelBase
    {
        private bool isExpanded;
        private bool isSelected;

        public NavigationNode()
        {
            this.IsExpanded = true;
            this.ChildNodes = new List<NavigationNode>();
        }

        public NavigationNode Parent { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IList<NavigationNode> ChildNodes { get; set; }
        public ViewModelBase ViewModel { get; set; }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged(() => this.IsSelected);
                }
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }

            set
            {
                if (this.isExpanded != value)
                {
                    this.isExpanded = value;
                    this.OnPropertyChanged(() => this.IsExpanded);
                }
            }
        }
    }
}
