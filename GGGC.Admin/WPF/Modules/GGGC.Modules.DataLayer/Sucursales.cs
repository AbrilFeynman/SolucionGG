using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GGGC.Modules.DataLayer
{
    public class Sucursales
    {
        public List<Sucursal> EmployeeList { get; set; }
        //public static

        public Sucursal GetEmployee(int employeeId)
        {
            // EXEC GetEmployeeDetails 1
            Sucursal e = new Sucursal();
            

            using (SqlConnection conn = DB.GetSqlConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"app_GetSucursal";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter p1 = new SqlParameter("businessEntityId", System.Data.SqlDbType.Int);
                    p1.Value = employeeId;

                    cmd.Parameters.Add(p1);

                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    if (reader.Read())
                    {
                        e.Load(reader);
                    }

                }
            }

            return e;
        }

        /// <summary>
        /// Returns an employee using Inline SQL
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Sucursal GetEmployeeDoNOTCALL(int employeeId)
        {
            Sucursal e = new Sucursal();

            using (SqlConnection conn = DB.GetSqlConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from HumanResources.Employee E
			JOIN Person.Person P ON E.BusinessEntityId = P.BusinessEntityId AND P.PersonType = 'EM'
			JOIN HumanResources.EmployeeDepartmentHistory EH ON E.BusinessEntityId = EH.BusinessEntityId
			JOIN HumanResources.Department D ON D.DepartmentId = EH.DepartmentId
where
	E.BusinessEntityId = {0}";

                    cmd.CommandText = string.Format(cmd.CommandText, employeeId.ToString());
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    if (reader.Read())
                    {
                        e.Load(reader);
                    }
                }
            }

            return e;
        }

        /// <summary>
        /// Update the name of a department given its ID
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="newName"></param>
        public void UpdateDepartmentName(int departmentId, string newName, string oldName)
        {
            using (SqlConnection conn = DB.GetSqlConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UpdateDepartmentName";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter p1 = new SqlParameter("id", System.Data.SqlDbType.Int);
                    p1.Value = departmentId;
                    cmd.Parameters.Add(p1);

                    SqlParameter p2 = new SqlParameter("name", System.Data.SqlDbType.NVarChar, 100);
                    p2.Value = newName;
                    cmd.Parameters.Add(p2);

                    // used for concurrency check (manual)
                    SqlParameter p3 = new SqlParameter("oldname", System.Data.SqlDbType.NVarChar, 100);
                    p3.Value = oldName;
                    cmd.Parameters.Add(p3);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    
    }

    public class Sucursal
    {

        public int SucursalId { get; set; }
        public string IP { get; set; }
        //public string LastName { get; set; }
        //public int DepartmentId { get; set; }
        //public string DepartmentName { get; set; }

        public void Load(SqlDataReader reader)
        {
            SucursalId = Int32.Parse(reader["SucursalId"].ToString());
            IP = reader["IP"].ToString();
            //LastName = reader["LastName"].ToString();
            //DepartmentId = Int32.Parse(reader["DepartmentId"].ToString());
            //DepartmentName = reader["Name"].ToString();
        }



   


    }
}
