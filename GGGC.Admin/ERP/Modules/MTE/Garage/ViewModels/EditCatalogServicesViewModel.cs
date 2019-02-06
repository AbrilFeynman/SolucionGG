using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;
using GGGC.Admin.ERP.Modules.MTE.Garage.Support;
using GGGC.Client.Contracts;
using GGGC.Client.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
namespace GGGC.Admin.ERP.Modules.MTE.Garage.ViewModels
{
    public class EditCatalogServicesViewModel : ViewModelBase
    {
        public EditCatalogServicesViewModel(IServiceFactory serviceFactory, Service obj)
        {
            _ServiceFactory = serviceFactory;

            try
            {
                //13nov 80 898
                GlobalModule.ActualStoreID = "2";
                GlobalModule.ActualUserID = 898131180;

                int intID = Math.Abs(Guid.NewGuid().GetHashCode());
                //if (intID < 0)
                //    intID = intID * -1;

                _Obj = new Service()
                {
                    EntityKey = obj.EntityKey,
                    ProductID = intID,
                    ClassID = 0,
                    GroupID = 0,
                    LineID = 0,
                    BrandID = 0,
                    UnitID = 0,
                    CodeID = "",
                    Description = "",
                    Prefix = "SERV",
                    Sufix = "",
                    TaxID = 0.16M,
                    OnStock = 1,
                    Editable = 0,
                    Comments = "",
                    UserID = GlobalModule.ActualUserID,
                    LocalIP = GlobalModule.GetLocalIP(),
                    PublicIP = GlobalModule.getPublicIP(),
                    SystemInfo = GlobalModule.GetSystemInfo(),
                    UserInfo = GlobalModule.GetUserInfo(),
                    InsertDate = DateTime.Now,//Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                    ModifiedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    StatusID = 0,
                    DeletedFlag = 0,
                    RowID = Guid.NewGuid()                  
                    //DepartmentID = obj.DepartmentID,
                    //Subject = "",//obj.Subject, Convert.ToInt32(DateTime.Now.Ticks)
                    //Body = obj.Body,
                    //StoreID = Convert.ToByte(GlobalModule.ActualStoreID),
                    //UserID = GlobalModule.ActualUserID,
                    //PriorityID = obj.PriorityID,
                    //OrderStatusID = 1,
                    //Rating = obj.Rating,
                    //SystemInfo = GlobalModule.GetSystemInfo(),
                    //UserInfo = GlobalModule.GetUserInfo(),
                    //Problem = " ",
                    //Solution = " ",
                    //Tags = "",
                    //LocalIP = GlobalModule.GetLocalIP(),
                    //PublicIP = GlobalModule.getPublicIP(),
                    //Comments = "",
                    //StatusID = 0,
                    //DeletedFlag = 0,
                    //ResourcesQty = 0,
                    //OrderDate = DateTime.Now,
                    //StartDate = DateTime.Now,
                    //EndDate = DateTime.Now,
                    //ClosedDate = DateTime.Now,
                    //ModifiedDate = DateTime.Now,
                    //InsertDate = DateTime.Now,//Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                    //LastUpdate = DateTime.Now,//Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                    //RowID = obj.RowID
                    //Guid.NewGuid()
                    //Color = car.Color,
                    //Year = car.Year,
                    //RentalPrice = car.RentalPrice
                };

                if (obj.RowID == Guid.Empty)
                    _Obj.RowID = Guid.NewGuid();
                _Obj.CleanAll();
                //00000000-0000-0000-0000-000000000000

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.ToString());
            }


            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _ServiceFactory;
        Service _Obj;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditObj;
        public event EventHandler<ServiceEventArgs> ObjUpdated;

        public Service Obj
        {
            get { return _Obj; }
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(Obj);
        }

        void OnSaveCommandExecute(object arg)
        {
            // _Car.Codigo = 33;

            ValidateModel();

            if (IsValid)
            {
                WithClient<ICatalogService>(_ServiceFactory.CreateClient<ICatalogService>(), objClient =>
                {
                    bool isNew = (_Obj.EntityKey == 0);

                    try
                    {
                        var savedCar = objClient.UpdateService(_Obj);
                        if (savedCar != null)
                        {
                            if (ObjUpdated != null)
                                ObjUpdated(this, new ServiceEventArgs(savedCar, isNew));
                        }
                    }

                    catch (FaultException ex)
                    {
                        ex.ToString();
                      
                       // throw;
                      //  _Obj.CodeID

                       //  += " El Código de Producto Ya Existe.";
                        string msg = ex.Message + "\n\n" + string.Format("El Código {0} de Producto Ya Existe.", _Obj.CodeID);
                        MostrarMensajeError(msg);
                        //if (ErrorOccured != null)
                        //    ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                    }

                    catch (Exception es)
                    {
                        es.ToString();
                        MostrarMensajeError(es.Message);
                    }


                });
            }
        }


        private async void MostrarMensajeError(string strMessage)
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                AnimateHide = true
            };
            var result = await metroWindow.ShowMessageAsync("Grupo Guadiana GC", strMessage, MessageDialogStyle.Affirmative, settings);
            //metroWindow.sh
            //metroWindow.sh

            //LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your credentials", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "MahApps", EnablePasswordPreview = true });
            //if (result == null)
            //{
            //    //User pressed cancel
            //}
            //else
            //{
            //    MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
            //}

            //  blnMostrar = true;
          
           
            //return;
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _Obj.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditObj != null)
                CancelEditObj(this, EventArgs.Empty);
        }

    }
}

