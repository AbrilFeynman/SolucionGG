using System;
using System.Collections.Generic;
using System.Text;

#if !SILVERLIGHT
namespace GGGC.Modules.Data
{
    public class AccesoDatos
    {
        //private static GGGC.Modules.Config.Local Config;
        public aDatos BaseDatos;
        
        public AccesoDatos()
        {
            try
            {
                //Config = new GGGC.Modules.Config.Local();
                //Config = Config.leerConfiguracion();

                BaseDatos = generarInstanciaDatos();
            }
            catch (Exception ex)
            {
                throw new Exception("Nel se pudo leer el archivo de configuración: " + ex.Message);
            }
        }

        public AccesoDatos(int i)
        {
            try
            {
                //Config = new GGGC.Modules.Config.Local();
                //Config = Config.leerConfiguracion();

                BaseDatos = generarInstanciaDatos(i);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo leer el archivo de configuración: " + ex.Message);
            }
        }

        private GGGC.Modules.Data.aDatos generarInstanciaDatos()
        {
            try
            {
                //if (Config.BaseDatos.TipoBaseDatos == GGGC.Modules.Config.Local.TipoBD.MSAccess)
                //{
                //    return new GGGC.Modules.Data.MSSQL.Datos();
                //}
                //else if (Config.BaseDatos.TipoBaseDatos == GGGC.Modules.Config.Local.TipoBD.MSSQLServer)
                //{
                  return new GGGC.Modules.Data.MSSQL.Datos();
                //}
                //else if (Config.BaseDatos.TipoBaseDatos == GGGC.Modules.Config.Local.TipoBD.DBase)
                //{
                //    return new GGGC.Modules.Data.MSSQL.Datos();
                //}
                //else
                //{
                //    throw new Exception("El acceso a datos no esta configurado");
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar nueva instancia de datos, Error: " + ex.Message);
            }
        }

        private GGGC.Modules.Data.aDatos generarInstanciaDatos(int i)
        {
            return new GGGC.Modules.Data.MSSQL.Datos(i);
            //try
            //{
            //    if (Config.BaseDatos.TipoBaseDatos == GGGC.Modules.Config.Local.TipoBD.MSAccess)
            //    {
            //        return new GGGC.Modules.Data.MSSQL.Datos(i);
            //    }
            //    else if (Config.BaseDatos.TipoBaseDatos == GGGC.Modules.Config.Local.TipoBD.MSSQLServer)
            //    {
            //        return new GGGC.Modules.Data.MSSQL.Datos(i);
            //    }
            //    //else if (Config.BaseDatos.TipoBaseDatos == uConfig.Local.TipoBD.MySQL)
            //    //{
            //    //return new uDatos.MySQL.Datos();
            //    //}
            //    else if (Config.BaseDatos.TipoBaseDatos == GGGC.Modules.Config.Local.TipoBD.DBase)
            //    {
            //        return new GGGC.Modules.Data.MSSQL.Datos(i);
            //    }
            //    else
            //    {
            //        throw new Exception("El acceso a datos no esta configurado");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error al generar nueva instancia de datos, Error: " + ex.Message);
            //}
        }
    }
}

#endif