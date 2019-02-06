using Core.Common.UI.Core;
using GGGC.Admin.Support.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Views
{
    /// <summary>
    /// Interaction logic for EditCatalogView.xaml
    /// </summary>
    public partial class EditCatalogServicesView : UserControlViewBase
    {
        AzureUploadSettings settings2 = new AzureUploadSettings();
        private static CloudStorageAccount _storageAccount = null;
        public ObservableCollection<BlobItem> _BlobCollection = new ObservableCollection<BlobItem>();
        public ObservableCollection<BlobItem> BlobCollection { get { return _BlobCollection; } }
        public CloudBlobClient blobClient = null;

        // Blob filters
        private int MaxBlobCountFilter = -1;    // Max number of blobs to return, or -1 for filter off
        private String BlobNameFilter = null;   // Name to wildcard match, or null for filter off
        private long MinBlobSize = -1;           // Min blob size, -1 for filter off
        private long MaxBlobSize = -1;           // Min blob size, -1 for filter off
        private int BlobTypeFilter = 0;         // 0 = All blobs, 1 = Only block blobs, 2 = Only page blobs
        private const string AZURE_DIRECTORY = "modules/gggc/tickets/";
        public EditCatalogServicesView()
        {
            InitializeComponent();
        }

        private void radComboBoxBrand_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if ()
            // MessageBox.Show(this.radComboBoxPriority.SelectedValue.ToString());
            if (this.radComboBoxBrand.SelectedValue != null)
            {
                this.txtPriority.Text = (this.radComboBoxBrand.SelectedValue.ToString());

            }
            else
            {
                if (this.txtPriority.Text == "")
                    this.txtPriority.Text = "0";
                // this.radComboBoxPriority.SelectedIndex = -1;
                //this.txtPriority.
            }
        }

        private void radComboBoxDepartment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.radComboBoxBrand.SelectedValue != null)
            {
                this.txtDepartment.Text = (this.radComboBoxBrand.SelectedValue.ToString());
            }
            else
            {
                if (this.txtDepartment.Text == "")
                    this.txtDepartment.Text = "0";
            }

        }

        private void txtPriority_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //MessageBox.Show(this.txtPriority.Text);
            this.radComboBoxBrand.SelectedValue = this.txtPriority.Text;
        }

        private void txtDepartment_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            this.radComboBoxBrand.SelectedValue = this.txtDepartment.Text;
        }

        private void Myguid_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            settings2.Path = "modules/gggc/tickets/" + ((TextBox)sender).Text;
            //cloudUpload1.Buttons.
        }

        //****************
        //*              *
        //*  LengthText  *
        //*              *
        //****************
        // Return length in text form with most appropriate units.

        public String LengthText(long length, bool showBytesText = true)
        {
            decimal n = Convert.ToDecimal(length);
            if (length == 1)
            {
                if (showBytesText)
                {
                    return "1 byte";
                }
                else
                {
                    return "1";
                }
            }
            else if (length < 1024)
            {
                if (showBytesText)
                {
                    return length.ToString() + " bytes";
                }
                else
                {
                    return length.ToString();
                }
            }
            else if (length < (1024 * 1024))
            {
                return Math.Round(n / 1024, 2).ToString() + "K";
            }
            else if (length < (1024 * 1024 * 1024))
            {
                return Math.Round(n / (1024 * 1024), 2).ToString() + "M";
            }
            else
            {
                return Math.Round(n / (1024 * 1024 * 1024), 2).ToString() + "G";
            }
        }

        private String CopyStateText(CopyState state)
        {
            if (state == null)
            {
                return String.Empty;
            }
            else
            {
                switch (state.Status)
                {
                    case CopyStatus.Pending:
                        return "Pending";
                    case CopyStatus.Success:
                        return "Success";
                    case CopyStatus.Aborted:
                        return "Aborted";
                    case CopyStatus.Failed:
                        return "Failed";
                    case CopyStatus.Invalid:
                        return "Invalid";
                    default:
                        return "Other";
                }
            }

        }


        private void UserControlViewBase_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                FocusManager.SetFocusedElement(gMain, txtCodigo);
                txtCodigo.Focusable = true;
                Keyboard.Focus(txtCodigo);
                //_storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=gggcbig;AccountKey=CQBHPdnT3EFKPxLLKcm7sWSyx6/l90Xj1FJ9q8ia69pHLRRFnahEdfLXCOGDfc+7PzE+Ck/rniwJ+OHQh+i00Q==");
                //// Create the blob client.
                //CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
                //// CloudBlobContainer container = blobClient.GetContainerReference(@"erp/modules/gggc/tickets/99b36e39-5bcc-46f3-9549-2dd6e685faec");
                //// blobClient.get
                //CloudBlobContainer container = blobClient.GetContainerReference("erp");
                ////CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(@"nestedcontainer/APictureFile.jpg");
                ////modules/gggc/tickets/99b36e39-5bcc-46f3-9549-2dd6e685faec/DIRECTORIO.docx
                ////modules/gggc/tickets/99b36e39-5bcc-46f3-9549-2dd6e685faec
                //ShowBlobContainer(container);
            }
            catch (Exception ex)
            {
                string s = ex.Message.ToString();
                //this.Cursor = Cursors.Wait;
                //ShowError("Error retrieving blob list: " + ex.Message);
            }
        }

        private void txtCodigo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //txtDescripcion.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.txtDescripcion.Focus();
                // this.cboLine.IsDropDownOpen = true;
            }
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.radComboBoxBrand.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                this.radComboBoxBrand.IsDropDownOpen = true;
            }
        }
    }
}
