using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GGGC.Admin.Support.Commands;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using GGGC.Admin.Modals.Model;
using System.Windows.Input;

namespace GGGC.Admin.Modals.ViewModels
{
    public class PasswordViewModel : IDataErrorInfo
    {

        private readonly Password objectPassword;
        private Dictionary<string, bool> validProperties;
        private bool allPropertiesValid = false;

        private DelegateCommand exitCommand;
        private DelegateCommand saveCommand;

        GGGC.Admin.Modals.Model.Password _newPassword;

        public PasswordViewModel()
         {
             _newPassword = new GGGC.Admin.Modals.Model.Password();
             this.objectPassword = Cars;
             this.validProperties = new Dictionary<string, bool>();
             this.validProperties.Add("ProductName", false);
             this.validProperties.Add("ProductCode", false);

         }



        #region private members

        #endregion

        #region state properties

        public string CurrentPassword
        {
            get { return objectPassword.CurrentPassword; }
            set
            {
                if (objectPassword.CurrentPassword != value)
                {
                    objectPassword.CurrentPassword = value;
                    base.OnPropertyChanged("CurrentPassword");
                }
            }
        }

        //public string ProductCode
        //{
        //    get { return currentProduct.ProductCode; }
        //    set
        //    {
        //        if (currentProduct.ProductCode != value)
        //        {
        //            currentProduct.ProductCode = value;
        //            base.OnPropertyChanged("ProductCode");
        //        }
        //    }
        //}

        //public int Width
        //{
        //    get { return currentProduct.Width; }
        //    set
        //    {
        //        if (this.currentProduct.Width != value)
        //        {
        //            this.currentProduct.Width = value;
        //            base.OnPropertyChanged("Width");
        //            base.OnPropertyChanged("Height");
        //        }
        //    }
        //}

        //public int Height
        //{
        //    get { return currentProduct.Height; }
        //    set
        //    {
        //        if (this.currentProduct.Height != value)
        //        {
        //            this.currentProduct.Height = value;
        //            base.OnPropertyChanged("Height");
        //            base.OnPropertyChanged("Width");
        //        }
        //    }
        //}

        public bool AllPropertiesValid
        {
            get { return allPropertiesValid; }
            set
            {
                if (allPropertiesValid != value)
                {
                    allPropertiesValid = value;
                    base.OnPropertyChanged("AllPropertiesValid");
                }
            }
        }

        #endregion

        #region IDataErrorInfo members

        public string Error
        {
            get { return (currentProduct as IDataErrorInfo).Error; }
        }

        public string this[string propertyName]
        {
            get
            {
                string error = (currentProduct as IDataErrorInfo)[propertyName];
                validProperties[propertyName] = String.IsNullOrEmpty(error) ? true : false;
                ValidateProperties();
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

        #endregion

        #region commands

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new DelegateCommand(Save);
                }
                return saveCommand;
            }
        }

        #endregion

        #region private helpers

        private void ValidateProperties()
        {
            foreach (bool isValid in validProperties.Values)
            {
                if (!isValid)
                {
                    this.AllPropertiesValid = false;
                    return;
                }
            }
            this.AllPropertiesValid = true;
        }

        private void Exit()
        {
            //  Application.Current.Shutdown();
            //Panel panel = default(Panel);
            //panel = (Panel)this.Parent;
            //panel.Children.RemoveAt(0);
            this.IsDirty = true;
        }

        private void Save()
        {
            objectPassword.Save(objectPassword);
        }

        #endregion


    }
}
