using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class Data
    {
        ObservableCollection<SistemasTags> contactsList = new ObservableCollection<SistemasTags>();

        protected void EntryContactData()
        {
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Alejandra",
                LastName = "Camino"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Aria",
                LastName = "Cruz"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Alexander",
                LastName = "Feuer"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Elizabeth",
                LastName = "Lincoln"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Eddie",
                LastName = "Cochran"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Hanna",
                LastName = "Moos"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Ben",
                LastName = "Johnson"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Charlie",
                LastName = "Sheen"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Quin",
                LastName = "Sheen"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Wade",
                LastName = "Green"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Zack",
                LastName = "Johnson"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Vladimir",
                LastName = "Griffin"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Stanley",
                LastName = "Feemster"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Georgie",
                LastName = "Kinzer"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Arnetta",
                LastName = "Ratchford"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Paul",
                LastName = "Thorpe"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Cesar",
                LastName = "Carlock"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Shana",
                LastName = "Etsitty"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Alease",
                LastName = "Vanhoy"
            });
            contactsList.Add(new SistemasTags()
            {
                FirstName = "Vennie",
                LastName = "Prigge"
            });
        }

        public ObservableCollection<SistemasTags> GetContacts()
        {
            contactsList.Clear();
            EntryContactData();
            return this.contactsList;
        }
    }
}
