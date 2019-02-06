using System;
using System.Data.SqlClient;
using System.Diagnostics;
#if !SILVERLIGHT
using System.Data;
using System.Threading;

namespace GGGC.Modules.Data.MSSQL
{
    public class Datos : GGGC.Modules.Data.aDatos
    {
        public Datos() : base()
        {
            // connectionString = "Data Source=" + Config.BaseDatos.Servidor + ";Initial Catalog=" + Config.BaseDatos.RutaNombreBD + ";Persist Security Info=True;User ID=" + Config.BaseDatos.Usuario + ";Password=" + Config.BaseDatos.Password + ";";
            // connectionString = "Data Source=" + Config.BaseDatos.Servidor + ";Initial Catalog=" + Config.BaseDatos.RutaNombreBD + ";Persist Security Info=True;User ID=" + Config.BaseDatos.Usuario + ";Password=" + ggConfig.funGlobal.Decrypt(Config.BaseDatos.Password) + ";";
        }
        public Datos(int i)
            : base(i)
        {
            switch (i)
            {
                //case 0:
                //    connectionString = "Data Source=" + Config.BaseDatos.ServidorCentral + ";Initial Catalog=" + Config.BaseDatos.DatabaseCentral + ";Persist Security Info=True;User ID=" + Config.BaseDatos.UsuarioCentral + ";Password=" + Config.BaseDatos.PasswordCentral + ";";
                //    break;
                //case 1:
                //    connectionString = "Data Source=" + Config.BaseDatos.Servidor + ";Initial Catalog=" + Config.BaseDatos.DB + ";Persist Security Info=True;User ID=" + Config.BaseDatos.Usuario + ";Password=" + Config.BaseDatos.Password + ";";
                //    break;
                //case 2:
                //    connectionString = "Data Source=" + Config.BaseDatos.ServidorA + ";Initial Catalog=" + Config.BaseDatos.DatabaseA + ";Persist Security Info=True;User ID=" + Config.BaseDatos.UsuarioA + ";Password=" + Config.BaseDatos.PasswordA + ";";
                //    break;
                //case 4:
                //    connectionString = "Data Source=" + "192.168.200.10" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                //    break;
                case 5:
                    connectionString = "Data Source=" + "tcp:w4o51vpush.database.windows.net,1433" + ";Initial Catalog=" + "gggcPublic" + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;User ID=" + "admin_gg_gc@w4o51vpush" + ";Password=" + "GRUPO.gu@di@n@.GC.2099" + ";";
                    break;
                case 6:
                    //connectionString = "Data Source=" + "tcp:h59980dm7x.database.windows.net,1433" + ";Initial Catalog=" + "rdbsgggc_Guadiana_01" + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;User ID=" + "admin_gg_gc_sadgo@h59980dm7x" + ";Password=" + "GRUPO.gu@di@n@.GC._2014#Durango#Torreon#Chihuahua.2099" + ";";
                    //connectionString = "Data Source=" + "DEV1\\GMO" + ";Initial Catalog=" + "Catalogos" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "memomemin.2099" + ";";
                    connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "Catalogos" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    //connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "Catalogos" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    break;
                case 7:
                    connectionString = "Data Source=" + "tcp:h59980dm7x.database.windows.net,1433" + ";Initial Catalog=" + "rdbsgggc_Guadiana_01" + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;User ID=" + "admin_gg_gc_sadgo@h59980dm7x" + ";Password=" + "GRUPO.gu@di@n@.GC._2014#Durango#Torreon#Chihuahua.2099" + ";";
                    break;

                case 8:
                    //connectionString = "Data Source=" + "tcp:h59980dm7x.database.windows.net,1433" + ";Initial Catalog=" + "rdbsgggc_Guadiana_01" + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;User ID=" + "admin_gg_gc_sadgo@h59980dm7x" + ";Password=" + "GRUPO.gu@di@n@.GC._2014#Durango#Torreon#Chihuahua.2099" + ";";
                    connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "Catalogos" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    //connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "Catalogos" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    break;

                case 9:
                    connectionString = "Data Source=" + "192.168.200.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 99:
                    connectionString = "Data Source=" + "192.168.200.20" + ";Initial Catalog=" + "GF" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 10:
                    connectionString = "Data Source=" + "192.168.200.2" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 101:
                    connectionString = "Data Source=" + "192.168.2.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";Connect Timeout=5;";
                    break;
                case 102:
                    connectionString = "Data Source=" + "192.168.6.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;
                case 103:
                    connectionString = "Data Source=" + "192.168.14.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;
                case 104:
                    connectionString = "Data Source=" + "192.168.200.10" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;
                case 105:
                    connectionString = "Data Source=" + "192.168.3.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;
                case 106:
                    connectionString = "Data Source=" + "192.168.8.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;
                case 107:
                    connectionString = "Data Source=" + "192.168.200.20" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 108:
                    connectionString = "Data Source=" + "192.168.15.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 109:
                    connectionString = "Data Source=" + "192.168.4.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;
                case 110:
                    connectionString = "Data Source=" + "192.168.7.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 111:
                    connectionString = "Data Source=" + "192.168.11.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 112:
                    connectionString = "Data Source=" + "192.168.11.140" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 113:
                    connectionString = "Data Source=" + "192.168.5.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 114:
                    connectionString = "Data Source=" + "192.168.10.1" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 115:
                    connectionString = "Data Source=" + "192.168.20.5" + ";Initial Catalog=" + "Punto_De_Venta" + ";Persist Security Info=True;User ID=" + "sa" + ";Password=" + "dgo2007" + ";";
                    break;

                case 120:
                    connectionString = "Data Source=" + "tcp:jb72xorvf4.database.windows.net,1433" + ";Initial Catalog=" + "gggc_Migration" + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;User ID=" + "admin_gg_gc_sa.dgo-dbmigration@jb72xorvf4" + ";Password=" + "GRUPO.gu@di@n@.GC._2014#Durango#Torreon#Chihuahua.2099" + ";";
                    break;

                case 83:
                    //connectionString = "Data Source=" + "tcp:h59980dm7x.database.windows.net,1433" + ";Initial Catalog=" + "rdbsgggc_Guadiana_01" + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;User ID=" + "admin_gg_gc_sadgo@h59980dm7x" + ";Password=" + "GRUPO.gu@di@n@.GC._2014#Durango#Torreon#Chihuahua.2099" + ";";
                    connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "mB14" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    //connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "Catalogos" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    break;

                case 50:
                    //connectionString = "Data Source=" + "tcp:h59980dm7x.database.windows.net,1433" + ";Initial Catalog=" + "rdbsgggc_Guadiana_01" + ";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;User ID=" + "admin_gg_gc_sadgo@h59980dm7x" + ";Password=" + "GRUPO.gu@di@n@.GC._2014#Durango#Torreon#Chihuahua.2099" + ";";
                    connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "Guadiana" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    //connectionString = "Data Source=" + "192.168.200.5\\GGSQL" + ";Initial Catalog=" + "Catalogos" + ";Persist Security Info=False;User ID=" + "sa" + ";Password=" + "GRUPO.gu@di@n@.GC.durango-ERP" + ";";
                    break;
            }
        }

        public override System.Data.DataTable Consulta(string SQL)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(SQL, Cnn))
                    {
                        try
                        {
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = SQL;
                            Cmd.CommandTimeout = 360;
                            System.Data.SqlClient.SqlDataAdapter Datos = new System.Data.SqlClient.SqlDataAdapter(Cmd);
                            System.Data.DataTable Tabla = new System.Data.DataTable();
                            Cnn.Open();
                            Datos.Fill(Tabla);
                            return Tabla;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (Cnn.State == ConnectionState.Open)
                            {
                                Cnn.Close();
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

        //public override System.Data.DataTable Consulta(string SQL, int i)
        //{
        //    try
        //    {
        //        switch (i)
        //        {
        //            case 0:
        //                connectionString = "Data Source=" + Config.BaseDatos.ServidorCentral + ";Initial Catalog=" + Config.BaseDatos.DatabaseCentral + ";Persist Security Info=True;User ID=" + Config.BaseDatos.UsuarioCentral + ";Password=" + Config.BaseDatos.PasswordCentral + ";";
        //                break;
        //            case 1:
        //                connectionString = "Data Source=" + Config.BaseDatos.Servidor + ";Initial Catalog=" + Config.BaseDatos.DB + ";Persist Security Info=True;User ID=" + Config.BaseDatos.Usuario + ";Password=" + Config.BaseDatos.Password + ";";
        //                break;

        //            case 2:
        //                connectionString = "Data Source=" + Config.BaseDatos.ServidorA + ";Initial Catalog=" + Config.BaseDatos.DatabaseA + ";Persist Security Info=True;User ID=" + Config.BaseDatos.UsuarioA + ";Password=" + Config.BaseDatos.PasswordA + ";";
        //                break;
        //        }

        //        using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(connectionString))
        //        {
        //            using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(SQL, Cnn))
        //            {
        //                try
        //                {
        //                    Cmd.CommandType = CommandType.Text;
        //                    Cmd.CommandText = SQL;
        //                    System.Data.SqlClient.SqlDataAdapter Datos = new System.Data.SqlClient.SqlDataAdapter(Cmd);
        //                    System.Data.DataTable Tabla = new System.Data.DataTable();
        //                    Cnn.Open();
        //                    Datos.Fill(Tabla);
        //                    return Tabla;
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
        //                }
        //                finally
        //                {
        //                    if (Cnn.State == ConnectionState.Open)
        //                    {
        //                        Cnn.Close();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
        //    }
        //}

        public override string Cnn()
        {
            return connectionString.ToString();
        }

        public override object ConsultaDato(string SQL)
        {
            //Datos.connectionString.
            try
            {
                using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(SQL, Cnn))
                    {
                        try
                        {
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = SQL;
                            Cnn.Open();
                            return Cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (Cnn.State == ConnectionState.Open)
                            {
                                Cnn.Close();
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


        public override System.Data.DataSet ConsultaDataset(string SQL)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (System.Data.SqlClient.SqlDataAdapter Datos = new System.Data.SqlClient.SqlDataAdapter(SQL, Cnn))
                    {
                        try
                        {
                            //Cmd.CommandType = CommandType.StoredProcedure;
                            //Cmd.CommandText = SQL;
                            // System.Data.SqlClient.SqlDataAdapter Datos = new System.Data.SqlClient.SqlDataAdapter(Cmd);
                            Datos.SelectCommand.CommandType = CommandType.StoredProcedure;
                            System.Data.DataSet Tabla = new System.Data.DataSet();
                            Cnn.Open();
                            Datos.Fill(Tabla);
                            return Tabla;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error de acceso a datos, Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (Cnn.State == ConnectionState.Open)
                            {
                                Cnn.Close();
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

        public override void Ejecuta(string SQL, System.Data.SqlClient.SqlParameter[] sqlParameter)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(SQL, Cnn))
                    {
                        try
                        {
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = SQL;
                            Cmd.CommandTimeout = 30;
                            //Cnn.Open();


                            foreach (System.Data.SqlClient.SqlParameter par in sqlParameter)
                            {
                                Cmd.Parameters.AddWithValue(par.ParameterName, par.Value);
                            }

                            switch (Cnn.DataSource)
                            {
                                case "192.168.200.5\\GGSQL":
                                    Cnn.Open();
                                    Cmd.ExecuteNonQuery();
                                    break;

                                case "192.168.200.1":
                                    Cnn.Open();
                                    Cmd.ExecuteNonQuery();
                                    break;

                                case "192.168.200.2":
                                    Cnn.Open();
                                    Cmd.ExecuteNonQuery();
                                    break;

                                default:
                                    if (QuickOpen(Cnn, 5000) == true)
                                    {
                                        Cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        throw new Exception("Error de acceso a datos");
                                    }

                                    break;


                            }



                            //Cnn.Open();

                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error al insertar a datos, Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (Cnn.State == ConnectionState.Open)
                            {
                                Cnn.Close();
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

        public override void ExecuteSqlTransaction(string strCodigo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    //command.CommandText = "DELETE FROM Stocks_Ideales WHERE Codigo_De_Articulo = " + strCodigo;
                    //command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Stocks_Ideales SELECT Numero_Corto_De_Sucursal, " + strCodigo + " AS Codigo_De_Articulo, 0 AS Stock_Ideal,1 AS Modificacion_Directa, GETDATE() AS Fecha_Y_Hora_De_Ultima_Actualizacion FROM Sucursales WHERE (Numero_Corto_De_Sucursal NOT IN (0, 90)) AND (Estatus_De_Sucursal = 1)";
                    command.ExecuteNonQuery();

                    // command.CommandText = "DELETE FROM Existencias_Iniciales WHERE Codigo_De_Articulo = " + strCodigo;
                    //command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Existencias_Iniciales SELECT Numero_Corto_De_Sucursal, " + strCodigo + " AS Codigo_De_Articulo, 0 AS Cantidad, CONVERT(smalldatetime, CONVERT(varchar(10), GETDATE(), 101)) AS Fecha_De_Existencia, GETDATE() AS Fecha_Y_Hora_De_Ultima_Actualizacion FROM Sucursales WHERE (Numero_Corto_De_Sucursal NOT IN (0, 90)) AND (Estatus_De_Sucursal = 1)";
                    command.ExecuteNonQuery();

                    //ommand.CommandText = "DELETE FROM Costo_Promedio_Inicial WHERE Codigo_De_Articulo = " + strCodigo;
                    //command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Costo_Promedio_Inicial SELECT " + strCodigo + " AS Codigo_De_Articulo, 0 AS Costo_Promedio_Unitario, 0 AS Existencia, MAX(Fecha_De_Costo_Promedio) AS Fecha_De_Costo_Promedio, GETDATE() AS Fecha_Y_Hora_De_Ultima_Actualizacion FROM Costo_Promedio_Inicial";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Precios(Codigo_De_Articulo,Nivel_De_Precios,Precio,Moneda,Fecha_Y_Hora_De_Ultima_Actualizacion) VALUES(" + strCodigo + ",'PL',0," + 1 + ",GETDATE())";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Precios(Codigo_De_Articulo,Nivel_De_Precios,Precio,Moneda,Fecha_Y_Hora_De_Ultima_Actualizacion) VALUES(" + strCodigo + ",'SP',0," + 1 + ",GETDATE())";
                    command.ExecuteNonQuery();



                    //strInstruccion = "DELETE FROM Existencias_Iniciales WHERE Codigo_De_Articulo = " & strCodigoDeArticulo

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    // Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    // Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    //Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        //Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        //Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
            }
        }

        public bool QuickOpen(SqlConnection conn, int timeout)
        {
            // We'll use a Stopwatch here for simplicity. A comparison to a stored DateTime.Now value could also be used
            Stopwatch sw = new Stopwatch();
            bool connectSuccess = false;

            // Try to open the connection, if anything goes wrong, make sure we set connectSuccess = false
            Thread t = new Thread(delegate ()
            {
                try
                {
                    sw.Start();
                    conn.Open();
                    connectSuccess = true;
                    //return true;

                }
                catch { }
            });



            // Make sure it's marked as a background thread so it'll get cleaned up automatically
            t.IsBackground = true;
            t.Start();

            if (connectSuccess)
                return true;


            // Keep trying to join the thread until we either succeed or the timeout value has been exceeded
            while (timeout > sw.ElapsedMilliseconds)
                if (t.Join(1))
                    break;

            //if (connectSuccess)
            //    return true;

            // If we didn't connect successfully, throw an exception
            if (!connectSuccess)
                return false;
            else
            {
                return true;

            }
            //throw new Exception("Timed out while trying to connect.");

        }

        public override int SelectMaxID(string llave, string tabla)
        {
            try
            {
                string sql = "SELECT (Max([" + llave + "]) + 1) AS MaxID FROM [" + tabla + "]";
                object obj = ConsultaDato(sql);
                if (obj != null)
                {
                    if (Convert.IsDBNull(obj))
                    {
                        return 1;
                    }
                    else
                    {
                        return int.Parse(obj.ToString());
                    }
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error SelectMaxID: " + ex.Message);
            }
        }


        public override void Ejecuta(string SQL)
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    using (System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(SQL, Cnn))
                    {
                        try
                        {
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = SQL;
                            Cnn.Open();
                            Cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error al insertar a datos, Error: " + ex.ToString());
                        }
                        finally
                        {
                            if (Cnn.State == ConnectionState.Open)
                            {
                                Cnn.Close();
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
    }
}

#endif
