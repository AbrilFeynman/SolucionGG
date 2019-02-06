using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public class PersonViewModel
    {
        private ObservableCollection<Person> items;

        public PersonViewModel()
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
			"/TileView;component/Images/TileView/People/pic1.png",
			"/TileView;component/Images/TileView/People/pic2.png",
			"/TileView;component/Images/TileView/People/pic3.png",
			"/TileView;component/Images/TileView/People/pic4.png",
			"/TileView;component/Images/TileView/People/pic5.png",
			"/TileView;component/Images/TileView/People/pic6.png",
			"/TileView;component/Images/TileView/People/pic7.png",
			"/TileView;component/Images/TileView/People/pic8.png",
			"/TileView;component/Images/TileView/People/pic9.png",
			"/TileView;component/Images/TileView/People/pic10.png",
			"/TileView;component/Images/TileView/People/pic11.png",
			"/TileView;component/Images/TileView/People/pic12.png"
#endif
		};

        private List<string> names = new List<string>
		{
			"Andrew Fuller",
			"Martin Sommer",
			"Anne Dogsworth",
			"Steven Buchanan",
			"Janet Leverling",
			"Michael Suyama",
			"Margaret Peacock",	
			"Robert King",
			"John Steel",
			"Laura Gallahan",
			"Nancy Davolio",
			"Ann Devon"
		};

        private List<string> emails = new List<string>
		{
			"Andrew@somemail.com",
			"Martin@somemail.com",
			"Anne@somemail.com",
			"Steven@somemail.com",
			"Janet@somemail.com",
			"Michael@somemail.com",
			"Margaret@somemail.com",	
			"Robert@somemail.com",
			"John@somemail.com",
			"Laura@somemail.com",
			"Nancy@somemail.com",
			"Ann@somemail.com"
		};
    }
}
