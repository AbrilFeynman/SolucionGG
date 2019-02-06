using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace GGGC.UI
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

    }
}
