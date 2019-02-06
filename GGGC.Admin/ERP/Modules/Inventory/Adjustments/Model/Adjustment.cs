using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
//using Windows.UI.Popups;

namespace GGGC.Admin.ERP.Modules.Inventory.Adjustments.Model
{
    public class Adjustment : IDataErrorInfo
    {
       //private MobileServiceCollection<Product, Product> items;
       //private IMobileServiceTable<Product> todoTable = App.MobileServiceOK.GetTable<Product>();

        #region state properties

        public string Id { get; set; }

         [JsonProperty(PropertyName = "productname")]
         public string ProductName { get; set; }

         [JsonProperty(PropertyName = "productcode")]
        public string ProductCode { get; set; }


         public string Folio { get; set; }
         public DateTime Fecha { get; set; }
        
        //public int Width { get; set; }
        //public int Height { get; set; }

        #endregion

        public Adjustment()
        {
            this.Fecha = DateTime.Now.Date;

        }
         public void Save(Adjustment p)
        {

            try
            {
                //await todoTable.InsertAsync(todoItem).ConfigureAwait(false);
                //items.Add(todoItem);
                //todoTable.InsertAsync()

              //  InsertTodoItem(p);
            }
            catch (Exception e)
            {
                e.ToString();
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw;
            }
            
            //var todoItem = new Product { ProductCode = "1", ProductName = "UNO"};
            //InsertTodoItem(todoItem);
            //Insert code to save new Product to database etc 

        //      MobileServiceCollection<TodoItem, TodoItem> items;
        //IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<Inventory_Article>();
        }


        //public async static Task<string> asyncMethod()
        //{

        // //     MobileServiceCollection<Product, Product> items;
        // //IMobileServiceTable<Product> todoTable = App.MyMobileService.GetTable<Product>();
        
        // //   //IMobileServiceTable<TodoItem> todoTable = Program.MobileService.GetTable<TodoItem>();

        // //   todoTable = Program.MobileService.GetTable<TodoItem>();
        // //   await todoTable.InsertAsync(new TodoItem() { Text = "WINFORMS44", Complete = false });
        // //   return "finished";
        //}


        //private async void InsertTodoItem(Product todoItem)
        //{
        //     //This code inserts a new TodoItem into the database. When the operation completes
        //     //and Mobile Services has assigned an Id, the item is added to the CollectionView
        //    //await todoTable.InsertAsync(todoItem);
        //    //items.Add(todoItem);


        //    try
        //    {
        //        //await todoTable.InsertAsync(todoItem).ConfigureAwait(false);
        //        //items.Add(todoItem);
        //        //todoTable.InsertAsync()
               
        //        todoItem.Id = Guid.NewGuid().ToString();
                              
        //        //todoItem.Width = 0;
        //        //todoItem.Height = 0;

        //        //await todoTable.InsertAsync(todoItem);
        //        //items.Add(todoItem);

        //        await todoTable.InsertAsync(todoItem);
        //       // items.Add(todoItem);
        //    }
        //    catch (MobileServiceInvalidOperationException e)
        //    {
        //        string errormsg = e.Message + " - " + e.Response.ReasonPhrase + " " +  e.Response.StatusCode.ToString();
        //        //var ignoreAsyncOpResult = errormsg.ShowAsync();
        //    }

        //    catch (Exception e)
        //    {
        //        e.ToString();
        //        System.Diagnostics.Debug.WriteLine(e.StackTrace);
        //        throw;
        //    }

            
        //}

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string propertyName]
        {
            get
            {
                string validationResult = null;
                switch (propertyName)
                {
                    case "ProductName":
                        validationResult = ValidateName();
                        break;
                    case "ProductCode":
                        validationResult = ValidateCode();
                        break;

                    case "Folio":
                        validationResult = ValidarFolio();
                        break;

                    case "Fecha":
                        validationResult = ValidarFecha();
                        break;
                    //case "Height":
                    //    validationResult = ValidateHeight();
                    //    break;
                    //case "Width":
                    //    validationResult = ValidateWidth();
                    //    break;
                    default:
                        throw new ApplicationException("Unknown Property being validated on Product.");
                }
                return validationResult;
            }
        }



        private string ValidateName()
        {
            if (String.IsNullOrEmpty(this.ProductName))
                return "El Product Name es obligatorio";
            else if (this.ProductName.Length < 5)
                return "Debe contener más de 5 carácteres";
            else
                return String.Empty;
        }


        private string ValidateCode()
        {
            if (String.IsNullOrEmpty(this.ProductCode))
                return "El Codigo es obligatorio";
            else if (this.ProductCode.Length < 5)
                return "Codigo Debe contener más de 5 carácteres";
            else
                return String.Empty;
        }


        private string ValidarFolio()
        {
            if (String.IsNullOrEmpty(this.Folio))
                return "Debe ingresar un folio";
            else if (this.Folio.Length < 4)
                return "El folio más de 4 carácteres";
            else
            return String.Empty;
        }

        private string ValidarFecha()
        {
          
            if (this.Fecha.Date == DateTime.Now.Date)
                return "Height should be greater than 0";
         else
                return String.Empty;

            //if (this.Height > this.Width)
            //    return "Height should be less than Width.";
        }

        private string ValidateHeight()
        {
            //if (this.Height <= 0)
            //    return "Height should be greater than 0";
            //if (this.Height > this.Width)
            //    return "Height should be less than Width.";
            //else
                return String.Empty;
        }

        private string ValidateWidth()
        {
            //if (this.Width <= 0)
            //    return "Width should be greater than 0";
            //if (this.Width < this.Height)
            //    return "Width should be greater than Height.";
            //else
                return String.Empty;
        }
    }
}
