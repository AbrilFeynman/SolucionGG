using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GGGC.Client.Entities;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class PriorityStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is Ticket)
            {
                Ticket club = item as Ticket;
                if (club.PriorityID == 1)
                    return AltaStyle;

                if (club.PriorityID == 2)
                    return MediaStyle;

                if (club.PriorityID == 3)
                    return BajaStyle;
                
                
               
            }
            return null;
        }
        public Style AltaStyle { get; set; }
        public Style BajaStyle { get; set; }
        public Style MediaStyle { get; set; }
    }
}
