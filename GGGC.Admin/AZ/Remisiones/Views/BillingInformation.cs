﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.AZ.Remisiones.Views
{
   public class BillingInformation
    {
        #region Fields
        private string m_Name;
        private string m_Address;
        private DateTime m_DueDate;
        private string m_InvoiveNumber;
        private DateTime m_date;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        /// <summary>
        /// Gets or Sets the Address
        /// </summary>
        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }
        /// <summary>
        /// Gets or Sets the Date
        /// </summary>
        public DateTime Date
        {
            get { return m_date; }
            set { m_date = value; }
        }
        /// <summary>
        /// Gets or Sets the Invoice
        /// </summary>
        public string InvoiceNumber
        {
            get { return m_InvoiveNumber; }
            set { m_InvoiveNumber = value; }
        }
        /// <summary>
        /// Gets or Sets the Due Date
        /// </summary>
        public DateTime DueDate
        {
            get { return m_DueDate; }
            set { m_DueDate = value; }

        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public BillingInformation()
        {
        }
        #endregion

    }
}
