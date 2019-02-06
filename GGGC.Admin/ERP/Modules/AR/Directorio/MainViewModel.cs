using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.AR.Directorio
{
    public class MainViewModel
    {
        private ObservableCollection<Person> items;

        public MainViewModel()
        {
            List<Person> itemsSource = new List<Person>();
            for (int i = 0; i < 50; i++)
            {
                itemsSource.Add(new Person()
                {
                    Image = this.images[i % 12],
                    Name = this.names[i % 12],
                    Email = this.emails[i % 12],
                    Number = i.ToString()
                });
            }

            this.items = new ObservableCollection<Person>(itemsSource);
        }

        public ObservableCollection<Person> Items
        {
            get
            {
                return this.items;
            }
        }

        private List<string> images = new List<string>
        {
#if SILVERLIGHT
			"../../Images/TileView/People/pic1.png",
			"../../Images/TileView/People/pic2.png",
			"../../Images/TileView/People/pic3.png",
			"../../Images/TileView/People/pic4.png",
			"../../Images/TileView/People/pic5.png",
			"../../Images/TileView/People/pic6.png",
			"../../Images/TileView/People/pic7.png",
			"../../Images/TileView/People/pic8.png",
			"../../Images/TileView/People/pic9.png",
			"../../Images/TileView/People/pic10.png",
			"../../Images/TileView/People/pic11.png",
			"../../Images/TileView/People/pic12.png"
#else
			"/GGGC.Admin;component/Resources/People/pic1.png",
            "/GGGC.Admin;component/Resources/People/pic2.png",
            "/GGGC.Admin;component/Resources/People/pic3.png",
            "/GGGC.Admin;component/Resources/People/pic4.png",
            "/GGGC.Admin;component/Resources/People/pic5.png",
            "/GGGC.Admin;component/Resources/People/pic6.png",
            "/GGGC.Admin;component/Resources/People/pic7.png",
            "/GGGC.Admin;component/Resources/People/pic8.png",
            "/GGGC.Admin;component/Resources/People/pic9.png",
            "/GGGC.Admin;component/Resources/People/pic10.png",
            "/GGGC.Admin;component/Resources/People/pic11.png",
            "/GGGC.Admin;component/Resources/People/pic12.png"
#endif
		};

        private List<string> names = new List<string>
        {
            "Juan Montes",
            "Abril Rodriguez",
            "Guillermo Uribe",
            "Patricia Aguilar",
            "Miguel Salazar",
            "Erika Flores",
            "Yadira",
            "Martha Navarro",
            "Jorge Campos",
            "Laura Campos",
            "Liliana Campos",
            "Ann Devon"
        };

        private List<string> emails = new List<string>
        {
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com",
            "user@llantasyrinesdleguadiana.com"
        };
    }
}
