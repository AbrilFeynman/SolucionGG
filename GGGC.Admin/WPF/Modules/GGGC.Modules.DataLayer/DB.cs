using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
using Microsoft.Practices.TransientFaultHandling;

namespace GGGC.Modules.DataLayer
{
    public class DB
    {
        public static string ConnectionString
        {
            get
            {
                string connStr = ConfigurationManager.ConnectionStrings["AWConnection"].ToString();

                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connStr);
              //  sb.ApplicationName = ApplicationName ?? sb.ApplicationName;
               // sb.ConnectTimeout = (ConnectionTimeout > 0) ? ConnectionTimeout : sb.ConnectTimeout;

                return sb.ToString();
            }
        }
        public static SqlConnection GETSQL()
        {
            SqlConnection conn = new SqlConnection("");
            return conn;

        }

        public static SqlConnection GetSqlConnection()
        {
            RetryPolicy rp = new RetryPolicy(new ConnectionExceptionDetectionStrategy(), 3);

            rp.Retrying += (sender, args) =>
            {
                // This code runs when a retry is taking place...
                System.Diagnostics.Debug.WriteLine("Retrying... count=" + args.CurrentRetryCount.ToString());
            };

            // Open the database connection with retries
            SqlConnection conn = new SqlConnection(ConnectionString);
            //conn.OpenWithRetry(rp);

            rp.ExecuteAction(() =>
            {
                // The action to perform with retry
                conn.Open();
            });


            //conn.StatisticsEnabled = EnableStatistics;
            //conn.StateChange += conn_StateChange;
            return conn;
        }

        public static DataTable GetSucursales()
        {
            DataTable table = new DataTable("ApplicationLog");
            SqlDataAdapter da = null;

            using (SqlConnection conn = DB.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblSucursales", conn);
                //cmd.Parameters.Add(new SqlParameter("appname", System.Data.SqlDbType.NVarChar, 100));
                //cmd.Parameters["appname"].Value = appName;

                da = new SqlDataAdapter(cmd);

                int res = da.Fill(table);
            }

            return table;
        }

        public static object ConsultaDato(string SQL)
        {
            //Datos.connectionString.
            try
            {

                using (SqlConnection conn = DB.GetSqlConnection())
                {
                    using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(SQL, conn))
                    {
                        try
                        {
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = SQL;
                           // conn.Open();
                            return Cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
            }
        }


        public class ConnectionExceptionDetectionStrategy: ITransientErrorDetectionStrategy
        {
            private bool isTransient = false;

            public bool IsTransient(Exception ex)
            {
                // Detect a network connection error
                bool isTransient = (ex is SqlException
                    && ((SqlException)ex).Class == 20
                    && ((SqlException)ex).Number == 53);

                if (!isTransient)
                {
                //   var retryMgr = EnterpriseLibraryContainer.Current.GetInstance<RetryManager>();
                }               
             return isTransient;
            }

        
           // private bool isTransient = false;
            //public bool IsTransient(Exception ex)
            //{
            //    // Detect a network connection error
            //    bool isTransient = (ex is SqlException
            //        && ((SqlException)ex).Class == 20
            //        && ((SqlException)ex).Number == 53);

            //    // If error is different implement the default strategy
            //    if (!isTransient)
            //    {
            //        // Retrieve the retry mgr of the configuration
            //        var retryMgr = EnterpriseLibraryContainer.Current.GetInstance<RetryManager>();
            //        RetryPolicy rp = retryMgr.GetDefaultSqlConnectionRetryPolicy();
            //        isTransient = rp.ErrorDetectionStrategy.IsTransient(ex);
            //    }
           //   return isTransient;
            //}

       //  return isTransient;
        }

        
    }
}
