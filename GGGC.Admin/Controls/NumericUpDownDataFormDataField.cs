using Telerik.Windows.Controls;

namespace GGGC.Admin
{
    public class NumericUpDownDataFormDataField : DataFormDataField
    {
        protected override System.Windows.DependencyProperty GetControlBindingProperty()
        {
            return RadNumericUpDown.ValueProperty;
        }

        protected override System.Windows.Controls.Control GetControl()
        {
            var numeric = new RadNumericUpDown()
            { 
                Minimum = 1, 
                Maximum = 5,
                IsInteger = true
            };
            
            if (this.DataMemberBinding != null)
            {
                numeric.SetBinding(this.GetControlBindingProperty(), this.DataMemberBinding);
            }

            return numeric;
        }
    }
}
