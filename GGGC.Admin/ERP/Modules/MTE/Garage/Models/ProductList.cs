using GGGC.Modules.Data;
using System;
using System.Collections.Generic;
using System.Data;


namespace GGGC.Admin.ERP.Modules.MTE.Garage.Models
{
    public class Product
    {
        #region Members
        private string m_name;
        private decimal _price;
        private string _codeId;
        private int _productId;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the product Name
        /// </summary>
        /// 

        public int ProductID
        {
            get { return _productId; }
            set { _productId = value; }
        }
        public string CodeID
        {
            get { return _codeId; }
            set { _codeId = value; }
        }
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        /// <summary>
        /// Gets or sets the rate of the product
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Product()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="rate">Rate</param>
        public Product(string name, decimal rate, int productId, string codeId)
        {
            m_name = name;
            _price = rate;
            _codeId = codeId;
            _productId = productId;
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class ProductList : List<Product>
    {
        #region Properties
        /// <summary>
        /// Returns the product based on the index
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Product this[string item]
        {
            get
            {
                foreach (Product product in this)
                    if (product.CodeID == item) // if (product.Name == item)
                        return product;
                return null;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load product information from Embedded XML file
        /// </summary>
        public void LoadFromXml()
        {
            //Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //Stream xmlStream = assembly.GetManifestResourceStream("SampleBrowser.DocIO.Invoice.Assets.ProdutsPriceList.xml");


            AccesoDatos sCen = new AccesoDatos(6);
            //System.Data.DataSet ds = new System.Data.DataSet();
            string sSQL = "SELECT ProductID, CodeID, Description FROM  Articulos ORDER BY CodeID  ";

            DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
            int i = 0;
            foreach (var row in tbl.Rows)
            {

                Product product = new Product();
                product.Name = tbl.Rows[i]["Description"].ToString();
                product.Price = 0; //Convert.ToDouble(tbl.Rows[i]["ProductID"]);
                product.CodeID = tbl.Rows[i]["CodeID"].ToString();
                product.ProductID = Convert.ToInt32(tbl.Rows[i]["ProductID"]);
                Add(product);



                // objects.Add(new Product(Convert.ToInt32(tbl.Rows[i]["ProductID"]), tbl.Rows[i]["CodeID"].ToString(), , " "));
                i++;
            }


            //Stream xmlStream = new FileStream(@"C:\ProdutsPriceList.xml", FileMode.Open, FileAccess.Read);
            ////Load XML file
            //XElement xElement = XElement.Load(xmlStream);
            //IEnumerable<XElement> searched = from c in xElement.Elements("Product")
            //                                 select c;
            ////Retreive product information
            //foreach (XElement pdt in searched)
            //{
            //    Product product = new Product();
            //    product.Name = pdt.Element("Name").Value;
            //    product.Rate = Convert.ToDouble(pdt.Element("Rate").Value);
            //    Add(product);
            //}
        }

        public void LoadServicesFromXml()
        {
            //Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //Stream xmlStream = assembly.GetManifestResourceStream("SampleBrowser.DocIO.Invoice.Assets.ProdutsPriceList.xml");


            AccesoDatos sCen = new AccesoDatos(6);
            //System.Data.DataSet ds = new System.Data.DataSet();
            string sSQL = "SELECT ProductID, CodeID, Description FROM  mto_Servicios ORDER BY CodeID  ";

            DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
            int i = 0;
            foreach (var row in tbl.Rows)
            {

                Product product = new Product();
                product.Name = tbl.Rows[i]["Description"].ToString();
                product.Price = 0; //Convert.ToDouble(tbl.Rows[i]["ProductID"]);
                product.CodeID = tbl.Rows[i]["CodeID"].ToString();
                product.ProductID = Convert.ToInt32(tbl.Rows[i]["ProductID"]);
                Add(product);



                // objects.Add(new Product(Convert.ToInt32(tbl.Rows[i]["ProductID"]), tbl.Rows[i]["CodeID"].ToString(), , " "));
                i++;
            }


            //Stream xmlStream = new FileStream(@"C:\ProdutsPriceList.xml", FileMode.Open, FileAccess.Read);
            ////Load XML file
            //XElement xElement = XElement.Load(xmlStream);
            //IEnumerable<XElement> searched = from c in xElement.Elements("Product")
            //                                 select c;
            ////Retreive product information
            //foreach (XElement pdt in searched)
            //{
            //    Product product = new Product();
            //    product.Name = pdt.Element("Name").Value;
            //    product.Rate = Convert.ToDouble(pdt.Element("Rate").Value);
            //    Add(product);
            //}
        }

        private void cargarProv()
        {
            try
            {
                AccesoDatos sCen = new AccesoDatos(6);
                //System.Data.DataSet ds = new System.Data.DataSet();
                string sSQL = "SELECT SupplierID, RFC, TradeName FROM  mto_Suppliers WHERE Visibility = 1  ";

                DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
                int i = 0;
                foreach (var row in tbl.Rows)
                {
                    // objects.Add(new Supplier(Convert.ToInt32(tbl.Rows[i]["SupplierID"]), tbl.Rows[i]["RFC"].ToString(), tbl.Rows[i]["TradeName"].ToString(), " "));
                    i++;
                }




                //foreach(var row in tbl.Rows)
                //{
                //    var obj = new Marca()
                //    {
                //        id_test = (int)tbl.Rows[0]["id_test"];
                //        name = (string)tbl.Rows[0]["name"];
                //    };
                //    //test.Add(obj);
                //  objects.Add(new Brand("1", "Alta"));
                //}

            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error en cargarDatos: " + ex.Message);
            }

        }


        //private void cargarProducts()
        //{
        //    try
        //    {
        //        AccesoDatos sCen = new AccesoDatos(6);
        //        //System.Data.DataSet ds = new System.Data.DataSet();
        //        string sSQL = "SELECT ProductID, CodeID, Description FROM  Articulos ORDER BY CodeID  ";

        //        DataTable tbl = sCen.BaseDatos.Consulta(sSQL);
        //        int i = 0;
        //        foreach (var row in tbl.Rows)
        //        {
        //            objects.Add(new Product(Convert.ToInt32(tbl.Rows[i]["ProductID"]), tbl.Rows[i]["CodeID"].ToString(), tbl.Rows[i]["Description"].ToString(), " "));
        //            i++;
        //        }




        //        //foreach(var row in tbl.Rows)
        //        //{
        //        //    var obj = new Marca()
        //        //    {
        //        //        id_test = (int)tbl.Rows[0]["id_test"];
        //        //        name = (string)tbl.Rows[0]["name"];
        //        //    };
        //        //    //test.Add(obj);
        //        //  objects.Add(new Brand("1", "Alta"));
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        //  MessageBox.Show("Error en cargarDatos: " + ex.Message);
        //    }

        //}
        #endregion
    }
}
