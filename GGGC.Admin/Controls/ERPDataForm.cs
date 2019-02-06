//using ERP.Repository;
//using ERP.Repository.Service;
using System.Linq;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace GGGC.Admin
{
    public class ERPDataForm : RadDataForm
    {
        public ERPDataForm()
        {
            this.AutoGeneratingField += this.OnAutoGeneratingField;
            this.AutoEdit = true;
        }

        public bool IsCreatingNew { get; set; }

        private void OnAutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            //var order = this.CurrentItem as SalesOrderHeader;
            //if (order != null)
            //{
            //    if (e.PropertyName == "Customer")
            //    {
            //        var customers = MainRepository.TopCustomersCache;
            //        var orderCustomer = order.Customer;
            //        if (orderCustomer != null && !customers.Contains(orderCustomer))
            //        {
            //            customers.Insert(0, orderCustomer);
            //        }

            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("CustomerID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = customers,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "CustomerID"
            //        };
            //    }
            //    else if (e.PropertyName == "ShipMethod")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("ShipMethodID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.ShipMethodsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "ShipMethodID"
            //        };
            //    }

            //    return;
            //}

            //var address = this.CurrentItem as Address;
            //if (address != null)
            //{
            //    if (e.PropertyName == "StateProvinceID")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("StateProvinceID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.StatesCache,
            //            DisplayMemberPath = "FullName",
            //            SelectedValuePath = "StateProvinceID"
            //        };
            //    }

               // return;
           // }

            //var customer = this.CurrentItem as Customer;
            //if (customer != null)
            //{
            //    if (e.PropertyName == "State")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("Address.StateProvinceID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.StatesCache,
            //            DisplayMemberPath = "FullName",
            //            SelectedValuePath = "StateProvinceID"
            //        };
            //    }

            //    if (customer.IsPerson)
            //    {
            //        if (e.PropertyName == "CompanyName" || e.PropertyName == "StoreContact")
            //        {
            //            e.Cancel = true;
            //        }
            //    }
            //    else
            //    {
            //        if (e.PropertyName == "FirstName" || e.PropertyName == "LastName")
            //        {
            //            e.Cancel = true;
            //        }
            //    }

            //    return;
           // }

            //var billOfMaterial = this.CurrentItem as BillOfMaterial;
            //if (billOfMaterial != null)
            //{
            //    if (e.PropertyName == "Product")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("ComponentID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.ProductsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "ProductID"
            //        };
            //    }

            //    if (e.PropertyName == "Product1")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("ProductAssemblyID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.ProductsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "ProductID"
            //        };
            //    }

            //    if (e.PropertyName == "UnitMeasure")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("UnitMeasureCode") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.UnitMeasuresCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "UnitMeasureCode"
            //        };
            //    }

            //    return;
            //}

            //var productInventory = this.CurrentItem as ProductInventory;
            //if (productInventory != null)
            //{
            //    if (e.PropertyName == "Product")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("ProductID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.ProductsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "ProductID",
            //            IsEnabled = this.IsCreatingNew
            //        };
            //    } 
            //    else if (e.PropertyName == "Color")
            //    {
            //        e.DataField = new ColorPickerDataFormDataField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = e.DataField.DataMemberBinding
            //        };
            //    }
            //    else if (e.PropertyName == "Location")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("LocationID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.LocationsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "LocationID",
            //            IsEnabled = this.IsCreatingNew
            //        };
            //    }
            //}

            //var vendor = this.CurrentItem as Vendor;
            //if (vendor != null)
            //{
            //    if (e.PropertyName == "CreditRating")
            //    {
            //        e.DataField = new NumericUpDownDataFormDataField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = e.DataField.DataMemberBinding
            //        };
            //    }

            //    return;
            //}

            //var workOrder = this.CurrentItem as WorkOrder;
            //if (workOrder != null)
            //{
            //    if (e.PropertyName == "Product")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("ProductID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.ProductsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "ProductID"
            //        };
            //    }

            //    return;
            //}

            //var purchaseOrder = this.CurrentItem as PurchaseOrderHeader;
            //if (purchaseOrder != null)
            //{
            //    if (e.PropertyName == "ShipMethod")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("ShipMethodID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.ShipMethodsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "ShipMethodID"
            //        };
            //    }
            //    else if (e.PropertyName == "Vendor")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("VendorID") { Mode = BindingMode.TwoWay },
            //            ItemsSource = MainRepository.VendorsCache,
            //            DisplayMemberPath = "Name",
            //            SelectedValuePath = "BusinessEntityID"
            //        };
            //    }
            //    else if (e.PropertyName == "OrderStatus")
            //    {
            //        e.DataField = new DataFormComboBoxField
            //        {
            //            Label = e.DataField.Label,
            //            DataMemberBinding = new Binding("OrderStatus") { Mode = BindingMode.TwoWay },
            //            ItemsSource = purchaseOrder.OrderStatuses,
            //        };
            //    }

            //    return;
            //}

        }

        protected override void OnItemEditEnded(Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            base.OnItemEditEnded(e);

            //var savableObject = this.CurrentItem as ISavableObject;
            //if (savableObject != null)
            //{
            //    if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            //    {
            //        savableObject.Save(this.IsCreatingNew);
            //    }
            //    else
            //    {
            //        savableObject.Cancel();
            //    }
            //}
        }
    }
}
