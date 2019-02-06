using System;
using System.Collections.ObjectModel;

using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
#if !SILVERLIGHT
using System.Data;
using System.Data.SqlClient;
namespace GGGC.Modules.Data.MSSQL
{
    public class SQLRepository
    {

        //private GGGC.Modules.Data.AccesoDatos sNew = new GGGC.Modules.Data.AccesoDatos(0);

        //public ObservableCollection<Makes> GetMakes()
        //{
        //    ObservableCollection<Makes> users = null;
        //    using (SqlConnection connection = new SqlConnection(sNew.BaseDatos.Cnn()))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = "GetMakes";
        //        using (IDataReader reader = command.ExecuteReader())
        //        {
        //            users = MapUsers(reader);
        //        }
        //    }
        //    return users;
        //}


        //private ObservableCollection<Makes> MapUsers(IDataReader reader)
        //{
        //    ObservableCollection<Makes> users = new ObservableCollection<Makes>();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            Makes user = new Makes();
        //            user.MakeID = Convert.ToInt32(reader["MakeID"]);
        //            user.Make = reader["Make"].ToString();
        //            user.LastUpdate = Convert.ToDateTime(reader["LastUpdate"].ToString());
        //            //user.FirstName = reader["First_name"].ToString();
        //            //user.LastName = reader["Last_name"].ToString();
        //            users.Add(user);
        //        }
        //    }
        //    return users;
        //}

    }
}
#endif