using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using GGGC.Client.Contracts;
using GGGC.Client.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CatalogViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public CatalogViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            EditObjectCommand = new DelegateCommand<Article>(OnEditObjectCommand);
            NewObjectCommand = new DelegateCommand<object>(OnNewObjectCommand);
          //  DetailObjectCommand = new DelegateCommand<Article>(OnDetailObjectCommand);
          //this.buttonNewIsEnabled = true;
           // this.EntitySetIsLoading = true;
        }
        IServiceFactory _ServiceFactory;

        public override string ViewTitle
        {
            get { return "Catalogo de Productos"; }
        }

        public override bool Busy
        {
            get { return true; }
        }

        ObservableCollection<Article> _OC;
        EditCatalogViewModel _CurrentObjectViewModel;
        private bool entitySetIsLoading = true;
        private bool buttonNewIsEnabled = false;
        //private bool blnFirstLoading = true;


        public DelegateCommand<Article> EditObjectCommand { get; private set; }

        public DelegateCommand<Article> DetailObjectCommand { get; private set; }
        public DelegateCommand<object> NewObjectCommand { get; private set; }
        public DelegateCommand<Article> DeleteCarCommand { get; private set; }

        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public bool EntitySetIsLoading
        {
            get
            {
                return this.entitySetIsLoading;
            }
            set
            {
                this.entitySetIsLoading = value;
                this.OnPropertyChanged("EntitySetIsLoading");
            }
        }


        public bool ButtonNewIsEnabled
        {
            get
            {
                return this.buttonNewIsEnabled;
            }
            set
            {
                this.buttonNewIsEnabled = value;
                this.OnPropertyChanged("ButtonNewIsEnabled");
            }
        }

        public ObservableCollection<Article> Articles
        {
            get { return _OC; }
            set
            {
                if (_OC != value)
                {
                    _OC = value;
                    OnPropertyChanged(() => Articles, false);
                }
            }
        }

        public EditCatalogViewModel CurrentObjectViewModel
        {
            get { return _CurrentObjectViewModel; }
            set
            {
                if (_CurrentObjectViewModel != value)
                {
                    _CurrentObjectViewModel = value;
                    OnPropertyChanged(() => CurrentObjectViewModel, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            // can check properties for null here if not want to re-get every time view shows
            try
            {
                Task<string> taskResult = Task.Factory.StartNew<string>(() =>
                {
                    _OC = new ObservableCollection<Article>();
                    this.EntitySetIsLoading = true;
                    // this.buttonNewIsEnabled = false;

                    WithClient<ICatalogService>(_ServiceFactory.CreateClient<ICatalogService>(), inventoryClient =>
                    {
                        Article[] collection = inventoryClient.GetAllArticles();
                        Thread.Sleep(500);

                        if (collection != null)
                        {
                            // Refresh de grid updating the IsBusy property
                            System.Windows.Application.Current.Dispatcher.Invoke(
                            System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
                            {
                                foreach (Article obj in collection)
                                    _OC.Add(obj);

                                this.EntitySetIsLoading = false;
                                this.buttonNewIsEnabled = false;

                            });
                        }
                    });

                    return "";

                }
            );

                this.EntitySetIsLoading = false;
                this.buttonNewIsEnabled = true;
            }
            catch (Exception e)
            {
                string s = e.Message.ToString();
                // throw;
            }

        }

        void OnNewObjectCommand(object arg)
        {

            // System.Windows.MessageBox.Show("false");
            this.buttonNewIsEnabled = false;
            Article obj = new Article();
            CurrentObjectViewModel = new EditCatalogViewModel(_ServiceFactory, obj);
            CurrentObjectViewModel.ObjUpdated += CurrentObjViewModel_ObjUpdated;
            CurrentObjectViewModel.CancelEditObj += CurrentCarViewModel_CancelEditCar;


            // MessageBox.Show("OnNewObjectCommand");
        }

        void OnBusyObjectCommand(object arg)
        {

            //   System.Windows.MessageBox.Show("OnNewObjectCommand");
        }

        void OnEditObjectCommand(Article obj)
        {
            if (obj != null)
            {
                CurrentObjectViewModel = new EditCatalogViewModel(_ServiceFactory, obj);
                CurrentObjectViewModel.ObjUpdated += CurrentObjViewModel_ObjUpdated;
                CurrentObjectViewModel.CancelEditObj += CurrentCarViewModel_CancelEditCar;
                // MessageBox.Show("OnEditObjectCommand");
            }
        }

        void OnDetailObjectCommand(Article obj)
        {
            if (obj != null)
            {
                CurrentObjectViewModel = new EditCatalogViewModel(_ServiceFactory, obj);
                CurrentObjectViewModel.ObjUpdated += CurrentObjViewModel_ObjUpdated;
                CurrentObjectViewModel.CancelEditObj += CurrentCarViewModel_CancelEditCar;
                // MessageBox.Show("OnEditObjectCommand");
            }
        }

        void CurrentObjViewModel_ObjUpdated(object sender, Support.ArticleEventArgs e)
        {
            if (!e.IsNew)
            {
                Article obj = _OC.Where(item => item.EntityKey == e.Articles.EntityKey).FirstOrDefault();
                if (obj != null)
                {
                    //car.Code = e.Car.Code;
                    //obj.Subject = e.Tickets.Subject;
                    //obj.Body = e.Tickets.Body;
                    //obj.DepartmentID = e.Tickets.DepartmentID;
                    //obj.PriorityID = e.Tickets.PriorityID;
                    //obj.RowID = e.Tickets.RowID;
                    //obj.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    //obj.LastUpdate = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    //obj.RowID = Guid.NewGuid();
                    //DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //car.Color = e.Car.Color;
                    //car.Year = e.Car.Year;
                    //car.RentalPrice = e.Car.RentalPrice;
                    //this.tb
                    //   MessageBox.Show("CurrentObjViewModel_ObjUpdated if");

                }
            }
            else
                _OC.Add(e.Articles);
            CurrentObjectViewModel = null;
            //  MessageBox.Show("CurrentObjViewModel_ObjUpdated else");
            // OnViewLoaded();
        }

        void CurrentCarViewModel_CancelEditCar(object sender, EventArgs e)
        {
            CurrentObjectViewModel = null;
            this.buttonNewIsEnabled = true;
        }

        //Ticket[] _Tickets;
        //// CustomerRentalData[] _CurrentlyRented;

        //public Ticket[] Tickets
        //{
        //    get { return _Tickets; }
        //    set
        //    {
        //        if (_Tickets != value)
        //        {
        //            _Tickets = value;
        //            OnPropertyChanged(() => Tickets, false);
        //        }
        //    }
        //}
        //private void CalculateStats()
        //{

        //    // var result = Tickets.GroupBy(g => g.PriorityID);

        //    int res1 = Tickets.AsEnumerable().Where(o => (int)o.PriorityID == 1).Count();
        //    int res2 = Tickets.AsEnumerable().Where(o => (int)o.PriorityID == 2).Count();
        //    int res3 = Tickets.AsEnumerable().Where(o => (int)o.PriorityID == 3).Count();

        //    // ok  int res3 = Tickets.AsEnumerable().Where(o => (int)o.PriorityID == 3).Sum(o => o.PriorityID);


        //   // totalIncome = myList.Where(x => x.RecType == 1).Sum(x => x.Income);

        //    //int res4 = _OC.Where(x => x.PriorityID == 2).Sum(x => x.PriorityID);
        //    //int res5 = _OC.Where(x => x.PriorityID == 3).Sum(x => x.PriorityID);

        //    //var vres4 = Tickets.Where(x => x.PriorityID == 2).Sum(x => x.PriorityID);
        //    //var vres5 = Tickets.Where(x => x.PriorityID == 3).Sum(x => x.PriorityID);

        //    // var vres6 = Tickets.Where(x => x.PriorityID == 3).ToList();

        //    // int vres7 = Tickets.Where(x => x.PriorityID == 3).GroupBy;

        //     //decimal count = (from p in Tickets
        //     //                 where p.PriorityID == 2
        //     //                 select p.PriorityID).Sum();

        //   // int totalIncome = Tickets.Where(x => (int)x.PriorityID == 2).Select(x => (int)x.PriorityID).Sum(x => x.);

        //   // var totalIncome = Tickets.Sum(c => (c.PriorityID == 2 ? c.PriorityID : 0));

        //    string s = "";
        //     //var PrioritySum = from emp in Tickets
        //     //                group emp by emp.PriorityID into empg
        //     //                select new { NAME = empg.Key, SALARY = empg.Sum(x => x.PriorityID) }; 
        //         //(n => n.PriorityID);
        //         //.Select(t => new { t.Key, MySum = t.Sum(n => n.PriorityID).ToString() });

        //    //var result = from m in collection.MetalCollection
        //    //             from g in m.GoldCollection
        //    //             group g by g.Category
        //    //                 into gg
        //    //                 select new
        //    //                 {
        //    //                     Category = gg.Key,
        //    //                     Sum = gg.Sum(x => x.Weight)
        //    //                 };

        //    //var result = from m in Tickets
        //    //             group g by g.
        //    //                 into gg
        //    //                 select new
        //    //                 {
        //    //                     Category = gg.Key,
        //    //                     Sum = gg.Sum(x => x.Weight)
        //    //                 };

        //    OverallStats = new List<TicketStat>
        //    {
        //        new TicketStat {Name = "Alta", Count = res1},
        //        new TicketStat {Name = "Media", Count = res2},
        //        new TicketStat {Name = "Baja", Count = res3},
        //    };
        //}

        //public List<TicketStat> OverallStats { get; private set; }

    }

    //public class TicketStat
    //{
    //    public string Name { get; set; }
    //    public int Count { get; set; }
    //}
}

