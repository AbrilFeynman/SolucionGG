﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.Guadiana.Invoice
{
    public class InvoiceItem : INotifyPropertyChanged
    {
        private int _itemID;
        private string _item;
        private int _quantity;
        private double _rate;
        private double _taxes;
        private double _totalAmount;
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        //public string ItemID
        //{
        //    get
        //    {
        //        return _itemID.ToString();
        //    }
        //    set
        //    {
        //        _itemID = Convert.ToInt32(value);
        //    }
        //}
        public string ItemName
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {

                _quantity = value;
                UpdateTotalAmount();
            }
        }
        public double Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                _rate = value;
                UpdateTotalAmount();
            }
        }
        public double Taxes
        {
            get
            {
                return _taxes;
            }
            set
            {
                _taxes = value;
                UpdateTotalAmount();
            }
        }
        public double TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
            }
        }
        #endregion

        void UpdateTotalAmount()
        {
            TotalAmount = (_quantity * _rate + _taxes);
        }
    }
}
