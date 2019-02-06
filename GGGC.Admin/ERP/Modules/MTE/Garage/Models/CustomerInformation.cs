using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Models
{
    public class CustomerInformation
    {

        #region Fields
        private string m_Name;
        private string m_Address;
        private DateTime m_DueDate;
        private string m_InvoiveNumber;
        private DateTime m_date;
        private string m_CustomerID;
        private string m_CompanyID;
        private string m_SucursalID;
        private string m_VehiculoID;
        private string m_Vehiculo;
        private string m_Placas;
        private string m_kilometraje;
        private string m_observaciones;
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
        
        public string CustomerID
        {
            get { return m_CustomerID; }
            set { m_CustomerID = value; }
        }

        public string CompanyID
        {
            get { return m_CompanyID; }
            set { m_CompanyID = value; }
        }

        public string SucursalID
        {
            get { return m_SucursalID; }
            set { m_SucursalID = value; }
        }

        public string VehiculoID
        {
            get { return m_VehiculoID; }
            set { m_VehiculoID = value; }
        }

        public string Vehiculo
        {
            get { return m_Vehiculo; }
            set { m_Vehiculo = value; }
        }

        public string Placas
        {
            get { return m_Placas; }
            set { m_Placas = value; }
        }

        public string Kilometraje
        {
            get { return m_kilometraje; }
            set { m_kilometraje = value; }
        }

        public string Observaciones
        {
            get { return m_observaciones; }
            set { m_observaciones = value; }
        }


        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public CustomerInformation()
        {
        }
        #endregion
    }
}
