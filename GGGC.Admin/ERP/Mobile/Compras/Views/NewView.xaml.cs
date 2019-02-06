using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GGGC.Admin.ERP.Mobile.Compras.Views
{
    /// <summary>
    /// Lógica de interacción para NewView.xaml
    /// </summary>
    public partial class NewView : UserControl
    {
        public NewView()
        {
            InitializeComponent();
        }


        private void txtCodigo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtCodigo.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //if (ue.Tag != null && ue.Tag.ToString == "IgnoreEnterKeyTraversal")
                //{
                //    //ignore
                //}
                //else
                //{
                //    e.Handled = true;
                //    ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //}
            }
        }

        private void txtDescripcion_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtDescripcion.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboLine.IsDropDownOpen = true;
            }

        }

        private void cboLine_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboLine.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboMarca.IsDropDownOpen = true;
            }
        }

        private void cboMarca_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboMarca.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboImpuesto.IsDropDownOpen = true;
            }

        }

        private void cboImpuesto_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboImpuesto.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboUnidad.IsDropDownOpen = true;
            }
        }

        private void cboUnidad_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboUnidad.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.cboEstatus.IsDropDownOpen = true;
            }

        }

        private void cboLine_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (cboLine.SelectedIndex >= 0)
            //{
            //    string strLinea = cboLine.SelectedValue.ToString();
            //   // intLinea = Convert.ToInt32(strLinea);

            //  //  this.txtID.Text = intCLASE.ToString() + intGRUPO.ToString() + intLinea.ToString() + intMarca.ToString() + IDRef.ToString();
            //    //this.txtID.
            //}


        }

        private void cboMarca_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (cboMarca.SelectedIndex >= 0)
            //{
            //    string strMarca = cboMarca.SelectedValue.ToString();

            //    switch (strMarca)
            //    {
            //        case "200":
            //            strMarca = "5";
            //            break;

            //        case "104":
            //            strMarca = "8";
            //            break;

            //        case "103":
            //            strMarca = "7";
            //            break;


            //    }
               // intMarca = Convert.ToInt32(strMarca);

               // this.txtID.Text = intCLASE.ToString() + intGRUPO.ToString() + intLinea.ToString() + intMarca.ToString() + IDRef.ToString();
                //this.txtID.
           // }
        }



    }
}
