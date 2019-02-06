using GGGC.Admin.Support.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using GGGC.Admin.Modals.Model;

namespace GGGC.Admin.Modals.ViewModels
{
    class NewPasswordViewModel : ViewModelBaseTMP, IDataErrorInfo
    {
        #region private members

        //private readonly Password Password;
        private readonly Contrasenia currentObject;
        private Dictionary<string, bool> validProperties;
        private bool allPropertiesValid = false;

        private DelegateCommand exitCommand;
        private DelegateCommand saveCommand;

        #endregion

        #region constructors

        public NewPasswordViewModel(Contrasenia newPassword)
        {
            this.currentObject = newPassword;
            this.validProperties = new Dictionary<string, bool>();
            this.validProperties.Add("CurrentPassword", false);
            //this.validProperties.Add("Height", false);
            //this.validProperties.Add("Width", false);

            //this.currentObject.
          //  Password.ActualWidth = 5;
            //newPassword.
            //Password.
        }

        #endregion

        #region state properties


        //public string PasswordActual
        //{
        //    get { return currentObject.p; }
        //}

        //public string CurrentPassword
        //  {
        //        get { return currentObject.CurrentPassword; }
        //        set
        //        {
        //            if (currentObject.CurrentPassword != value)
        //             {
        //                 currentObject.CurrentPassword = value;
        //                 base.OnPropertyChanged("CurrentPassword");
        //             }
        //        }
        //  }

        public string CurrentPassword
        {
            get { return currentObject.Current_Password; }
            set
            {
                if (currentObject.Current_Password != value)
                {
                    currentObject.Current_Password = value;
                    base.OnPropertyChanged("CurrentPassword");
                }
            }
        }





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
            get { return (currentObject as IDataErrorInfo).Error; }
        }

        public string this[string propertyName]
        {
            get
            {
                string error = (currentObject as IDataErrorInfo)[propertyName];
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
            //Application.Current.Shutdown();
        }

        private void Save()
        {
            //currentProduct.Save();
        }

        #endregion
    }

}
