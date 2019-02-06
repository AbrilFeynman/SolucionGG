using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using GGGC.Modules.Data;
using System;


namespace GGGC.Admin.Modals.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private bool blnMostrar = false;
        public SettingsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("OK");
            MostrarMensaje();
            // myMetodo();
        }


        private async void MostrarMensaje()
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;

            //GGGC.Admin.Shell.sho
            //ShowAppBar
            //var settings = new MetroDialogSettings()
            //{
            //    AffirmativeButtonText = "Aceptar",
            //    AnimateHide = true
            //    //ColorScheme = MetroDialogColorScheme.Theme // : MetroDialogColorScheme.Theme
            //};
            //var result = await metroWindow.ShowMessageAsync("Grupo Guadiana GC", "Contraseña Guardada Exitosamente", MessageDialogStyle.Affirmative, settings);
            //if (result == null) //user pressed cancel
            //    return;

            //if (result == MessageDialogResult.Affirmative)
            //{
            //    //Matches.Remove(selectedMatch);
            //    //SelectedMatch = null;
            //    //  this.dtFechaInicial.Focus();
            //}

            // MessageBox.Show("Hello", "Hello " + result + "!");
            //Window parentWindow = Window.GetWindow(this);
            //object obj = parentWindow.FindName("mainFlyout");
            //Flyout flyout = (Flyout)obj;
            //flyout.Content = new SomeFlyOutUserControl();
            //flyout.IsOpen = !flyout.IsOpen;

            //Shell parentWindow = (Shell)Window.GetWindow(this);
            //parentWindow.fConfig.IsOpen = false;
          //  parentWindow.fConfig.


        }



        private async void MostrarMensajeError(string strMessage, byte bytBox)
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                AnimateHide = true
            };
            var result = await metroWindow.ShowMessageAsync("Grupo Guadiana GC", strMessage, MessageDialogStyle.Affirmative, settings);
            metroWindow.Show();
            //metroWindow.sh

            //LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your credentials", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "MahApps", EnablePasswordPreview = true });
            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
               // MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
            }

            blnMostrar = true;
           // result.
            if (result != null) //user pressed cancel
            {
                if (bytBox == 1)
                    txtPasswordActual.Focus();

                if (bytBox == 2)
                    txtPasswordNuevo.Focus();

                if (bytBox == 3)
                    txtPasswordRepetir.Focus();

                if (bytBox == 4)
                    this.btnGuardar.Focus();
            }
            return;
        }

        public async Task<MessageDialogResult> ShowMessage(string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            //  metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;

            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                AnimateHide = true
            };

            return await metroWindow.ShowMessageAsync("Grupo Guadiana GC", message, MessageDialogStyle.Affirmative, settings);

            //MessageDialogResult result = await _dialog.ShowMessage("This is just a test", MessageDialogStyle.AffirmativeAndNegative).ConfigureAwait(false);

            //if (result == MessageDialogResult.Affirmative)
            //    _dialog.ShowView<MyView>();
        }
        public void myMetodo()
        {
            //Shell parentWindow = (Shell)Window.GetWindow(this);
            //parentWindow.ShowAppBar2();
        }

        private void txtPasswordActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPasswordActual.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                // txtPasswordNuevo.Focus();
                //this.cboMarca.IsDropDownOpen = true;
            }
        }

        private void txtPasswordNuevo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPasswordNuevo.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                //this.cboMarca.IsDropDownOpen = true;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (fncValidar())
            {
                fncGuardar();
                MostrarMensaje();
            }
            else
            {

            }

            //Validar();
            //MostrarMensajeError("wefgr");
            //myMetodo();
        }

        private bool fncGuardar()
        {

            string sSQL = "";

            //if (fncValidar() == false)
            //    return false;

            List<SqlParameter> pars = new List<SqlParameter>();

            try
            {

                string strNuevoPassword = this.txtPasswordNuevo.Password.Trim().ToUpper();


                pars.Clear();
                pars.Add(new SqlParameter("@UserID", GlobalModule.ActualUserID));
                pars.Add(new SqlParameter("@Password", strNuevoPassword));


                AccesoDatos sCen = new AccesoDatos(6);
                sCen.BaseDatos.Ejecuta("UPDATE tblTesting SET Password = @Password WHERE UserID = @UserID", pars.ToArray());
                GlobalModule.ActualUserPassword = strNuevoPassword;
                this.txtPasswordActual.Password = "";
                this.txtPasswordNuevo.Password = "";
                this.txtPasswordRepetir.Password = "";
                pars.Clear();







                // string strMail = "<p>Alta de Artículo</p> <p>Codigo_De_Articulo:<b> " + productID + "</b></p>" + "<p>Descripción: " + strDesc + "</p> <font color=\"#08088A\"> Fecha:" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString() + "</font> <p>...</p>";





                //strMail_Cadena = strMail + "\n" + strResult;
                //strMail_Code = productID.ToString();

                //  sendEmal(strMail + "\n" + strResult, "" + productID.ToString());




                //  pars2.Clear();



                //RadWindow.Alert(new DialogParameters()
                //{
                //    Content = "Producto Guardado \n\n o Exitosamente del sistema",
                //    Header = "GGGC"
                //    // IconContent = "";
                //});
                return true;
            }

            catch (Exception ex)
            {
                throw new Exception("Error al Guardar: " + ex.Message);


                //RadWindow.Alert(new DialogParameters()
                //{
                //    Content = "La requisicion puede estar duplicada \n\n o esta pendiente habilitar mas funciones del sistema",
                //    Header = "GGGC"
                //    // IconContent = "";
                //});

                // MessageBox.Show("Mensaje error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        private void txtPasswordRepetir_KeyDown(object sender, KeyEventArgs e)
        {
            // txtPasswordNuevo.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            if (e.Key == Key.Enter)
            {
                Button_Click_1(sender, e);
            }

        }

        private bool fncValidar()
        {

            if (txtPasswordNuevo.Password.All(char.IsLetterOrDigit) == false)
            {
                //just letters and digits.
                MostrarMensajeError("La Nueva Contraseña solo puede contener Letras y Números", 2);
                return false;
            }

            if (this.txtPasswordActual.Password.Trim().Length == 0)
            {
                MostrarMensajeError("Debe Ingresar su Contraseña Actual", 1);
                // Thread.Sleep(1000);
                //  MessageDialogResult result = await _dialog.ShowMessage("This is just a test", MessageDialogStyle.AffirmativeAndNegative).ConfigureAwait(false);
                // await((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("PasswordBox Button was clicked!");
                //throw new Exception("Debe Ingresar la Descripción del Producto.");
                //Shell parentWindow = (Shell)Window.GetWindow(this);
                //parentWindow.Focus();
                //while (blnMostrar == false)
                //{
                //}
                //  var result = await this.ShowMessageAsync("Quit application?",
                //"Sure you want to quit application?",
                //MessageDialogStyle.AffirmativeAndNegative, mySettings);
                //txtPasswordActual.Focus();
                //blnMostrar = true;
                return false;
            }

            if (this.txtPasswordNuevo.Password.Trim().Length == 0)
            {
                MostrarMensajeError("Debe Ingresar la Nueva Contraseña", 2);
                return false;
            }

            if (this.txtPasswordRepetir.Password.Trim().Length == 0)
            {
                MostrarMensajeError("Debe Confirmar la Nueva Contraseña", 3);
                return false;
            }

            if (this.txtPasswordRepetir.Password.Trim().ToUpper() != this.txtPasswordNuevo.Password.Trim().ToUpper())
            {
                MostrarMensajeError("La Nueva Contraseña y la Confirmación deben ser iguales, Verifique e Intente Nuevamente", 3);
                return false;
            }


            if (this.txtPasswordActual.Password.Trim().ToUpper() != GlobalModule.ActualUserPassword)
            {
                MostrarMensajeError("La Contraseña Actual no es correcta, Intente Nuevamente", 1);
                return false;
            }
            //bool isLetterOrDigit = char.IsLetterOrDigit(txtPasswordNuevo.Password);


            if (txtPasswordNuevo.Password.Length < 1)
            {
                //just letters and digits.
                MostrarMensajeError("La Contraseña debe tener como mínimo 6 caracteres", 2);
                return false;
            }



            //GlobalModule.ActualUserID = validatedUser.UserID;
            //GlobalModule.ActualUserPassword = validatedUser.Password;


            return true;

        }

        private async void Validar()
        {


            if (this.txtPasswordActual.Password.Trim().Length == 0)
            {
                MessageDialogResult result = await ShowMessage("This is just a test", MessageDialogStyle.AffirmativeAndNegative).ConfigureAwait(false);
                txtPasswordActual.Focus();
            }

            //if (result == MessageDialogResult.Affirmative)
            //_dialog.ShowView<MyView>();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("user control loaded");
        }

        private void UserControl_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           // MessageBox.Show("keyboard focus");
        }





        //private void ShowDynamicFlyout(object sender, RoutedEventArgs e)
        //{
        //    var flyout = new DynamicFlyout
        //    {
        //        Header = "Dynamic flyout"
        //    };

        //    // when the flyout is closed, remove it from the hosting FlyoutsControl
        //    RoutedEventHandler closingFinishedHandler = null;
        //    closingFinishedHandler = (o, args) =>
        //    {
        //        flyout.ClosingFinished -= closingFinishedHandler;
        //        flyoutsControl.Items.Remove(flyout);
        //    };
        //    flyout.ClosingFinished += closingFinishedHandler;

        //    flyoutsControl.Items.Add(flyout);

        //    flyout.IsOpen = true;
        //}





    }
}
