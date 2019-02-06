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

namespace GGGC.Admin.ERP.Designs
{
    /// <summary>
    /// Interaction logic for SucursalesLRG.xaml
    /// </summary>
    public partial class SucursalesLRG : UserControl
    {
        public SucursalesLRG()
        {
            InitializeComponent();
            LoadImage();
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((ContentControl)this.Parent).Content = null;

        }

        private void LoadImage()
        {
            //BitmapImage src = new BitmapImage();
            //src.BeginInit();
            //src.UriSource = new Uri(@"Resources/Images/App/Backgrounds/BG1.jpg", UriKind.Relative);
            //src.CacheOption = BitmapCacheOption.OnLoad;
            //src.EndInit();
            //Uri(@"C:\Images\original.jpg")));

            LayoutRoot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/App/Backgrounds/suc1.png")));
           // LayoutRoot2.Background = new ImageBrush(new BitmapImage(new Uri(@"C:\Images\gomez.png")));


            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(@"C:\Images\gomez.png"));
                     brush.Stretch = Stretch.None;
            LayoutRoot2.Background = brush;


            ImageBrush brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri(@"C:\Images\torreon.png"));
            brush3.Stretch = Stretch.None;
            LayoutRoot3.Background = brush3;


            ImageBrush brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri(@"C:\Images\chihuahua.png"));
            brush4.Stretch = Stretch.None;
            LayoutRoot4.Background = brush4;
           // brush.ViewportUnits = BrushMappingMode.Absolute;
            //brush.Viewport = new Rect(0, 0, brush.ImageSource.Width, brush.ImageSource.Height);
           // LayoutRoot2.Background.s

            //pack://application:,,,/Resources/Australia.png"
            //preview.Source = src;
            //preview.Stretch = Stretch.Uniform;
        }
    }
}
