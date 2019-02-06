using Core.Common.Core;
using GGGC.Client.Bootstrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
//using GGGC.Admin.Login.Views;

namespace GGGC.Admin
{
    public partial class App : Application
    {
        //     public static MobileServiceClient MobileServiceOK = new MobileServiceClient(
        //"https://gggc-endpoint-guadiana-002.azure-mobile.net/",
        //"qJFumeERqRoTKqOrqbzQFGNBcZHsZR14");

        private const string LogFilename = "erp.log";

        clsGlobalcs globalgato = new clsGlobalcs();

        public App()
        {
            this.Startup += this.OnApplicationStartup;
            this.LoadCompleted += this.OnLoadCompleted;
            this.Exit += this.OnApplicationExit;
            this.DispatcherUnhandledException += this.OnApplicationDispatcherUnhandledException;

            InitializeComponent();


        }

        string exeFolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");

            ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>()
            {
                new AssemblyCatalog(Assembly.GetExecutingAssembly())
            });

            // Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;




            //  new LoginWindow().ShowDialog();
            //new Shell().Show();
            //------> reporte de inventario telerik new Window1().Show();
            //new MainWindow().Show();



            // new Shell().Show();
            // new 
            //().Show();

            GlobalModule.bytSUCURSAL = Convert.ToByte(GlobalModule.GetSetting("GrupoGuadiana", "Config", "SucursalID", String.Empty));
            GlobalModule.bytEMPRESA = Convert.ToByte(GlobalModule.GetSetting("GrupoGuadiana", "Config", "EmpresaID", String.Empty));

            ShellDockDesign NewWindowB = new ShellDockDesign();
            //Current.MainWindow.WindowState = WindowState.Maximized;
            NewWindowB.Show();

           // new ShellDockDesign();
        }

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            //EqatecMonitor.TryInitializeMonitor();
            //EqatecMonitor.Instance.TrackFeatureStart(EqatecConstants.ApplicationStartupTime);
            //EqatecMonitor.Instance.TrackFeatureStart(EqatecConstants.ApplicationUptime);
        }

        private void OnLoadCompleted(object sender, NavigationEventArgs e)
        {
          //  EqatecMonitor.Instance.TrackFeatureStop(EqatecConstants.ApplicationStartupTime);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //EqatecMonitor.Instance.TrackFeatureStop(EqatecConstants.ApplicationUptime);
            //EqatecMonitor.Stop();
        }

        private void OnApplicationDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            /* More detailed error message display */
            //string exception = string.Empty;
            //Exception ex = e.Exception;
            //while (ex != null)
            //{
            //    exception += String.Format("----------------\n{0}\n", ex.Message);
            //    exception += String.Format("{0}\n", ex.StackTrace);
            //    ex = ex.InnerException;
            //}

            //string logExceptionHeader = string.Format("Exception at {0}.{1} :", DateTime.Now, DateTime.Now.Millisecond);
            //string logExceptionFooter = "End of exception.";
            //string logText = string.Join(Environment.NewLine, logExceptionHeader, exception, logExceptionFooter, Environment.NewLine);
            //File.AppendAllText(LogFilename, logText);

            //// EQATEC: cancel application startup timing if ongoing, report app crash and exception
            //EqatecMonitor.Instance.TrackFeatureCancel(EqatecConstants.ApplicationStartupTime);
            //EqatecMonitor.Instance.TrackFeatureCancel(EqatecConstants.ApplicationUptime);
            //EqatecMonitor.Instance.TrackFeature(EqatecConstants.ApplicationCrash);
            //EqatecMonitor.Instance.TrackException(e.Exception, logText);
            //EqatecMonitor.Stop();
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>() 
        //    {
        //        new AssemblyCatalog(Assembly.GetExecutingAssembly())
        //    });

        //    // HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
        //    //     try
        //    //     {
        //    //          MobileServiceClient MyMobileService = new MobileServiceClient("https://gggc-endpoint-guadiana-001.azure-mobile.net/","soOlDSYjBbUqDRPImhiKiUxAaJRyFw66");
        //    //     }

        //    //catch (Exception es) {        
        //    //}
        //    //MyMobileService = new MobileServiceClient("https://gggc-endpoint-guadiana-001.azure-mobile.net/", "soOlDSYjBbUqDRPImhiKiUxAaJRyFw66");
        //    ////D
        //    //Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        //    //string strPath = GetCurrentDirectory();
        //    //// MessageBox.Show(strPath);
        //    //string strUser = "";
        //    //// Authenticate the current user and set the default principal
        //    //GGGC.Admin.Login.Views.MainWindow auth = new MainWindow(strPath, ref strUser);
        //    //////  GGGC.Admin.Login.Views.Test auth = new Test();
        //    ////  auth.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //    //bool? dialogResult = auth.ShowDialog();
        //    //// deal with the results
        //    //if (dialogResult.HasValue && dialogResult.Value)
        //    //{
        //    //    // MessageBox.Show("User Logged In");
        //    //    //var DoubleAnimation animFadeIn = new DoubleAnimation();
        //    //    //animFadeIn.From = 0;
        //    //    //animFadeIn.To = 1;
        //    //    //animFadeIn.Duration = new Duration(TimeSpan.FromSeconds(0.5));
        //    //    //window.Show();
        //    //    //window.BeginAnimation(Window.OpacityProperty, animFadeIn);
        //    //    //lsWindows[lsWindows.Count - 2].Hide();
        //    //    Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        //    //    base.OnStartup(e);
        //    //    //Bootstrapper bs = new Bootstrapper();
        //    //    //bs.Run();

        //    //    // clsUser Usuario = new clsUser(auth.ViewModel.UserName);
        //    //    // GlobalVar.User = auth.ViewModel.UserName;

        //    //    ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>() 
        //    //{
        //    //    new AssemblyCatalog(Assembly.GetExecutingAssembly())
        //    //});
        //    //}
        //    //else
        //    //{
        //    //    // MessageBox.Show("User Clicked Cancel");
        //    //    this.Shutdown(-1);
        //    //}          

        //    Thread.CurrentThread.CurrentCulture = new CultureInfo("es");
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
        //}


        public static string GetCurrentDirectory()
        {
            string path = null;

            path = AppDomain.CurrentDomain.BaseDirectory;
            if (path.IndexOf(@"\bin") > 0)
            {
                path = path.Substring(0, path.LastIndexOf(@"\bin"));
            }

            return path;
        }
    }
}
