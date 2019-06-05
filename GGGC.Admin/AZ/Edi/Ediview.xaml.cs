using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Globalization;

using Microsoft.Win32;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace GGGC.Admin.AZ.Edi
{
    /// <summary>
    /// Interaction logic for Ediview.xaml
    /// </summary>
    public partial class Ediview : UserControl
    {
        public string ruta;
        public string carpeta;
       
        DataTable tabla1 = new DataTable();
        public Ediview()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.csv;*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                StreamReader sr = File.OpenText(openFileDialog.FileName);


                folioid.Text = openFileDialog.FileName.ToString();
                ruta = openFileDialog.FileName.ToString();
                carpeta = System.IO.Path.GetDirectoryName(ruta);

            }





        }

        private void SaveDetail()
        {

            string connectionStringer = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";
            SqlConnection sqlcon = new SqlConnection(connectionStringer);
            sqlcon.Open();

            foreach (DataRow Registro in tabla1.Rows)
            {

              
               

                DateTime fechaxxxx = Convert.ToDateTime(Registro[0]);
                string fecha = fechaxxxx.ToString("dd/MM/yyyy");

                string tiempo = Registro[1].ToString();
                string messid = Registro[2].ToString();
                string mesnam = Registro[3].ToString();
                string mcust = Registro[4].ToString();
                string country = Registro[5].ToString();
                string seccio = Registro[6].ToString();

                string mspn = Registro[7].ToString();
                string destore = Registro[8].ToString();
                string decust = Registro[9].ToString();
                string uom = Registro[10].ToString();
                string qty = Registro[11].ToString();


                DateTime sell = Convert.ToDateTime(Registro[12]);
                string sello = sell.ToString("dd/MM/yyyy");


                string depart = Registro[13].ToString();
                if (String.IsNullOrEmpty(depart))
                {
                    depart = "0";
                }
                string sales = Registro[14].ToString();
                if (String.IsNullOrEmpty(sales))
                {
                    sales = "0";
                }

                string mich = Registro[15].ToString();
                if (String.IsNullOrEmpty(mich))
                {
                    mich = "0";
                }
                string brand = Registro[16].ToString();
                if (String.IsNullOrEmpty(brand))
                {
                    brand = "0";
                }


                try
                {
                    string consulta = "INSERT INTO [dbo].[Edi] ([curDate],[CurTime],[messageFromId],[messageFromName],[MichelinCustomerNbr],[countryCode],[seccion]" +
                        " ,[mspn],[dealerStoreNbr],[dealerCustNbr],[uom],[qty],[sellOutDate],[dealerPartNbr],[salesPrice],[michAAN],[brandCd])" +
                        " VALUES(Convert(smalldatetime,'"+fecha+"',104),'"+tiempo+"','"+messid+"','"+mesnam+"',"+mcust+",'"+country+"','"+seccio+"','"+mspn+"'," +
                        " "+destore+","+decust+",'"+uom+"',"+qty+",Convert(smalldatetime,'"+sello+"',104),'"+depart+"',"+sales+","+mich+",'"+brand+"')";
                    SqlCommand agregar = new SqlCommand(consulta, sqlcon);
                    agregar.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("error al conectar " + ex.ToString());
                }


            }

            sqlcon.Close();
            //MessageBox.Show("se guardo Correctamente ");


        }




        private void Guardar_Click(object sender, RoutedEventArgs e)
        {



            string archiveDirectory2 = carpeta;





            if (Directory.Exists(archiveDirectory2) && Directory.GetFileSystemEntries(archiveDirectory2).Count() > 0) //comprobar que exista la carpeta y tenga archivos

            {

                System.IO.DirectoryInfo di = new DirectoryInfo(archiveDirectory2);





                foreach (FileInfo file in di.GetFiles())

                {

                    //Log("filename::" + file.Name);


                    //aqui tu metodo

                    converandsave(file.Name);
                    tabla1 = new DataTable();
                  


                }
                MessageBox.Show("Se han insertado exitosamente.");


            }

        }
        private void converandsave(string file)
        {


            tabla1 = ConvertCSVtoDataTable(carpeta+@"\"+file);


            if (tabla1.Rows.Count > 0)
            {
                // seleccionar el primer registro y revisar que no esta en la tbl de la bd
                DateTime fecha = Convert.ToDateTime(tabla1.Rows[1][0]);
                string fecha2 = fecha.ToString("dd/MM/yyyy");
                //insertar  primero como prueba de registros

                int renglones = tabla1.Rows.Count;


                bool exist = revisarfecha(fecha2);
                if (exist == true)
                {
                    MessageBox.Show("la fecha ya se encuentra registrada");
                }
                else
                {
                    SaveDetail();

                }

            }
           

        }

        public static bool revisarfecha(string fecha)
        {
            bool answ = true;

            string conect = "SERVER = gggctserver.database.windows.net; DATABASE = devArellantas; USER ID = sysadmin_gg_gc_sa_dgo_testing; PASSWORD = GRUPO.gu@di@n@.Grupo.Campos_#Staging_Test.2099 ";

            SqlConnection sqlconn = new SqlConnection(conect);
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR DE CONEXION" + ex.Message);
            }




          
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Edi where curDate = Convert(smalldatetime,'" + fecha + "',104)", sqlconn);
            DataSet dsPubs = new DataSet("Pubs");
            adapter.Fill(dsPubs, "Edi");
            DataTable dtbl = new DataTable();

            dtbl = dsPubs.Tables["Edi"];
            sqlconn.Close();

            if (dtbl.Rows.Count < 1)
            {
                answ = false;
            }




            





            return answ;

        }


        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }






    }
}
