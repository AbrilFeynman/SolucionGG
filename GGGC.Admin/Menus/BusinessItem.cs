using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GGGC.Admin.Menus
{
    public class BusinessItem : INotifyPropertyChanged
    {
        public BusinessItem(BusinessItem parent)
        {
            this.Items = new ObservableCollection<BusinessItem>();
            this.Parent = parent;
        }

        public ObservableCollection<BusinessItem> Items { get; set; }
        public BusinessItem Parent { get; private set; }

        public string Name { get; set; }
        public string Tag { get; set; }

        public string GetPath()
        {
            string path = this.Name;
            BusinessItem nextParent = this.Parent;

            while (nextParent != null)
            {
                path = nextParent.Name + @"\" + path;
                nextParent = nextParent.Parent;
            }

            return path;
        }


        private bool selected;
        public bool IsSelected
        {
            get
            {
                return selected;
            }
            set
            {
                if (value != selected)
                {
                    selected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private bool enabled;
        public bool IsEnabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (value != enabled)
                {
                    enabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        private int visibility;
        public int Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                if (value != visibility)
                {
                    visibility = value;
                    OnPropertyChanged("Visibility");
                }
            }
        }


        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if (value != imagePath)
                {
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
