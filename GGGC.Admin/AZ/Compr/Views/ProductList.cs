﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace GGGC.Admin.AZ.Compr.Views
{
   public class Product
    {

        #region Members
        private string m_name;
        private double m_rate;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the product Name
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        /// <summary>
        /// Gets or sets the rate of the product
        /// </summary>
        public double Rate
        {
            get { return m_rate; }
            set { m_rate = value; }
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
        public Product(string name, double rate)
        {
            m_name = name;
            m_rate = rate;
        }
        #endregion
    }

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
                    if (product.Name == item)
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
            //Assembly assembly = typeof(NewView).Assembly;
            //Stream xmlStream = assembly.GetManifestResourceStream("GGGC.Admin.AZ.Ncr.Views.Assets.ProdutsPriceList.xml");
            //Load XML file
            Stream xmlStream = new FileStream(@"C:\ProdutsPriceList.xml", FileMode.Open, FileAccess.Read);

            XElement xElement = XElement.Load(xmlStream);
            IEnumerable<XElement> searched = from c in xElement.Elements("Product")
                                             select c;
            //Retreive product information
            foreach (XElement pdt in searched)
            {
                Product product = new Product();
                product.Name = pdt.Element("Name").Value;
                product.Rate = Convert.ToDouble(pdt.Element("Rate").Value);
                Add(product);
            }
        }
        #endregion
    }
}
