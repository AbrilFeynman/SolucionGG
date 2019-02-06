using GGGC.Admin.Modals.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.Modals.Model
{
    class Contrasenia:IDataErrorInfo
    {
             public string Current_Password { get; set; }              
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordActual { get; set; }
        //public int Width { get; set; }
        //public int Height { get; set; }

        public void Save(Contrasenia p)
        {

            try
            {
                //await todoTable.InsertAsync(todoItem).ConfigureAwait(false);
                //items.Add(todoItem);
                //todoTable.InsertAsync()

                // InsertTodoItem(p);
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
                    case "CurrentPassword":
                        validationResult = ValidatePassword();
                        break;
                    //case "ProductCode":
                    //    validationResult = ValidateCode();
                    //    break;

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

        private string ValidatePassword()
        {
            if (String.IsNullOrEmpty(this.Current_Password))
                return "El Product Name es obligatorio";
            else if (this.Current_Password.Length < 5)
                return "Debe contener más de 5 carácteres";
            else
                return String.Empty;
        }


        //private string ValidateCode()
        //{
        //    if (String.IsNullOrEmpty(this.ProductCode))
        //        return "El Codigo es obligatorio";
        //    else if (this.ProductCode.Length < 5)
        //        return "Codigo Debe contener más de 5 carácteres";
        //    else
        //        return String.Empty;
        //}

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
