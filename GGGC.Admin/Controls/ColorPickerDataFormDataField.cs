using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace GGGC.Admin
{
    public class ColorPickerDataFormDataField : DataFormDataField
    {
        protected override DependencyProperty GetControlBindingProperty()
        {
            return RadColorPicker.SelectedColorProperty;
        }

        protected override Control GetControl()
        {
            var colorPicker = new RadColorPicker();
            if (this.DataMemberBinding != null)
            {
                colorPicker.SetBinding(this.GetControlBindingProperty(), this.DataMemberBinding);
            }

            colorPicker.SetBinding(
                RadColorPicker.SelectedColorProperty, 
                new Binding("Color") 
                { 
                    Source = this.Item, 
                    Mode = BindingMode.TwoWay,
                    TargetNullValue = Colors.Transparent
                });

            return colorPicker;
        }
    }
}
