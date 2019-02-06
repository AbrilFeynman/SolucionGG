using System;
using System.Collections.Generic;
using System.Text;

#if !SILVERLIGHT
using System.Data.SqlClient;

namespace GGGC.Modules.Data
{
    public abstract class aDatos
    {
        //protected static GGGC.Modules.Config.Local Config;
        protected  string connectionString;

        public aDatos()
        {
            try
            {
                //Config = new GGGC.Modules.Config.Local();
                //Config = Config.leerConfiguracion();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo leer el archivo de configuración: " + ex.Message);
            }
        }

        public aDatos(int i)
        {
            try
            {
                //Config = new GGGC.Modules.Config.Local();
                //Config = Config.leerConfiguracion();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo leer el archivo de configuración: " + ex.Message);
            }
        }

        public abstract System.Data.DataTable Consulta(string SQL);
        public abstract System.Data.DataSet ConsultaDataset(string SQL);
        public abstract object ConsultaDato(string SQL);
        public abstract void Ejecuta(string SQL, SqlParameter[] sqlParameter);
        public abstract void Ejecuta(string SQL);
        public abstract void ExecuteSqlTransaction(string SQL);
        public abstract int SelectMaxID(string llave, string tabla);
        public abstract string Cnn();
    }
}

#endif