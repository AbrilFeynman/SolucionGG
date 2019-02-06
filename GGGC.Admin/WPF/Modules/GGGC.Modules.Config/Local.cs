using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
#if !SILVERLIGHT
namespace GGGC.Modules.Config
{

   [Serializable()]
    public class Local
    {
        //private string sPathConfiguracion = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Sistema\\" + "Configuracion.xml";
        public const string appName = "GrupoGuadiana";
        public const string section = "Config";

        [XmlElement()]
        public cFacturas Facturas;
        

        [XmlElement()]
        public cBaseDatos BaseDatos;

        public Local()
        {
            BaseDatos = new cBaseDatos();
            Facturas = new cFacturas();
        }

        public Local leerConfiguracion()
        {
            try
            {
                Local Config = new Local();

                string tipoBD = funGlobal.GetSetting(appName, section, "TipoBaseDatos", "-1");
                if (tipoBD.Equals("-1"))
                {
                    throw new Exception("Debe configurar la base de datos");
                }

                //MSAccess = 0,
                //MSSQLServer = 1,
                //MySQL = 2,
                //DBase = 3,
                //Firebird = 4

                if (tipoBD.Equals("0"))
                {
                    Config.BaseDatos.TipoBaseDatos = TipoBD.MSAccess;
                }
                else if (tipoBD.Equals("1"))
                {
                    Config.BaseDatos.TipoBaseDatos = TipoBD.MSSQLServer;
                }
                else if (tipoBD.Equals("2"))
                {
                    Config.BaseDatos.TipoBaseDatos = TipoBD.MySQL;
                }
                else if (tipoBD.Equals("3"))
                {
                    Config.BaseDatos.TipoBaseDatos = TipoBD.DBase;
                }
                else if (tipoBD.Equals("4"))
                {
                    Config.BaseDatos.TipoBaseDatos = TipoBD.Firebird;
                }

                Config.BaseDatos.RutaNombreBD = funGlobal.GetSetting(appName, section, "RutaNombreBD", String.Empty);
                Config.BaseDatos.DB = funGlobal.GetSetting(appName, section, "DB", String.Empty);
                Config.BaseDatos.Usuario = "sa";//ggConfig.funGlobal.Decrypt(funGlobal.GetSetting(appName, section, "Usuario", String.Empty));
                Config.BaseDatos.Password = "dgo2007";//ggConfig.funGlobal.Decrypt(funGlobal.GetSetting(appName, section, "Password", String.Empty));
                Config.BaseDatos.Servidor = funGlobal.GetSetting(appName, section, "Servidor", String.Empty);

                Config.BaseDatos.ServidorA = funGlobal.GetSetting(appName, section, "ServidorA", String.Empty);
                Config.BaseDatos.DatabaseA = funGlobal.GetSetting(appName, section, "DatabaseA", String.Empty);
                Config.BaseDatos.UsuarioA = "sa"; //ggConfig.funGlobal.Decrypt(funGlobal.GetSetting(appName, section, "UsuarioA", String.Empty));
                Config.BaseDatos.PasswordA = "dgo2007";// ggConfig.funGlobal.Decrypt(funGlobal.GetSetting(appName, section, "PasswordA", String.Empty));

                Config.BaseDatos.ServidorCentral = funGlobal.GetSetting(appName, section, "CentralServidor", String.Empty);
                Config.BaseDatos.DatabaseCentral = funGlobal.GetSetting(appName, section, "CentralDatabase", String.Empty);
                Config.BaseDatos.UsuarioCentral = "sa"; //ggConfig.funGlobal.Decrypt(funGlobal.GetSetting(appName, section, "CentralUsuario", String.Empty));
               // Config.BaseDatos.PasswordCentral = "GRUPO.gu@di@n@.SEP-2012";//"GRUPO.gu@di@n@.2012-MEMO";//ggConfig.funGlobal.Decrypt(funGlobal.GetSetting(appName, section, "CentralPassword", String.Empty));
                Config.BaseDatos.PasswordCentral = funGlobal.GetSetting(appName, section, "CentralPassword", String.Empty);
                    //"GRUPO.gu@di@n@.2012-2012";//ggConfig.funGlobal.Decrypt(funGlobal.GetSetting(appName, section, "CentralPassword", String.Empty));
                //Application.Run(new Form2());
                //Config.BaseDatos.UsuarioCentral = ggConfig.funGlobal.Decrypt(Config.BaseDatos.UsuarioCentral);
                //Config.BaseDatos.PasswordCentral = ggConfig.funGlobal.Decrypt(Config.BaseDatos.PasswordCentral);


                Config.BaseDatos.TipoBaseDatos = TipoBD.MSSQLServer;
                Config.BaseDatos.RutaNombreBD = "rdse";
                Config.BaseDatos.Usuario = "user_si_e";
                Config.BaseDatos.Password = "GRUPO.gu@di@n@.GC.2099";
                Config.BaseDatos.Servidor = "w4o51vpush.database.windows.net";

                return Config;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer configuración: " + ex.Message);
            }
        }

        public void guardarConfiguracion(GGGC.Modules.Config.Local Configuracion)
        {
            try
            {
                if (Configuracion.BaseDatos.TipoBaseDatos == TipoBD.MSAccess)
                {
                    funGlobal.SaveSetting(appName, section, "TipoBaseDatos", "0");
                }
                else if (Configuracion.BaseDatos.TipoBaseDatos == TipoBD.MSSQLServer)
                {
                    funGlobal.SaveSetting(appName, section, "TipoBaseDatos", "1");
                }
                else if (Configuracion.BaseDatos.TipoBaseDatos == TipoBD.MySQL)
                {
                    funGlobal.SaveSetting(appName, section, "TipoBaseDatos", "2");
                }
                else if (Configuracion.BaseDatos.TipoBaseDatos == TipoBD.DBase)
                {
                    funGlobal.SaveSetting(appName, section, "TipoBaseDatos", "3");
                }
                else if (Configuracion.BaseDatos.TipoBaseDatos == TipoBD.Firebird)
                {
                    funGlobal.SaveSetting(appName, section, "TipoBaseDatos", "4");
                }

                funGlobal.SaveSetting(appName, section, "RutaNombreBD", Configuracion.BaseDatos.RutaNombreBD);
                funGlobal.SaveSetting(appName, section, "DB", Configuracion.BaseDatos.DB);
                funGlobal.SaveSetting(appName, section, "Usuario", Configuracion.BaseDatos.Usuario);
                funGlobal.SaveSetting(appName, section, "Password", Configuracion.BaseDatos.Password);
                funGlobal.SaveSetting(appName, section, "Servidor", Configuracion.BaseDatos.Servidor);

                funGlobal.SaveSetting(appName, section, "ServidorA", Configuracion.BaseDatos.ServidorA);
                funGlobal.SaveSetting(appName, section, "DatabaseA", Configuracion.BaseDatos.DatabaseA);
                funGlobal.SaveSetting(appName, section, "UsuarioA", Configuracion.BaseDatos.UsuarioA);
                funGlobal.SaveSetting(appName, section, "PasswordA", Configuracion.BaseDatos.PasswordA);

                funGlobal.SaveSetting(appName, section, "CentralServidor", Configuracion.BaseDatos.ServidorCentral);
                funGlobal.SaveSetting(appName, section, "CentralDatabase", Configuracion.BaseDatos.DatabaseCentral);
                funGlobal.SaveSetting(appName, section, "CentralUsuario", Configuracion.BaseDatos.UsuarioCentral);
                funGlobal.SaveSetting(appName, section, "CentralPassword", Configuracion.BaseDatos.PasswordCentral);




            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar configuración, Error: " + ex.Message);
            }
        }

        public class cBaseDatos
        {
            [XmlAttribute()]
            public TipoBD TipoBaseDatos = TipoBD.MSAccess;

            [XmlAttribute()]
            public string RutaNombreBD = "";

            [XmlAttribute()]
            public string DB = "";

            [XmlAttribute()]
            public string Usuario = "";
            [XmlAttribute()]

            public string Password = "";

            [XmlAttribute()]
            public string Servidor = "";

            [XmlAttribute()]
            public string ServidorA = "";

            [XmlAttribute()]
            public string DatabaseA = "";

            [XmlAttribute()]
            public string UsuarioA = "";

            [XmlAttribute()]
            public string PasswordA = "";

            [XmlAttribute()]
            public string ServidorCentral = "";

            [XmlAttribute()]
            public string DatabaseCentral = "";

            [XmlAttribute()]
            public string UsuarioCentral = "";

            [XmlAttribute()]
            public string PasswordCentral = "";

            [XmlAttribute()]
            public string EmpresaID = "";

            [XmlAttribute()]
            public string SucursalID = "";

        }

        public class cFacturas
        {
            [XmlAttribute()]
            public string RutaXML = "";
            [XmlAttribute()]
            public string RutaXML_OUT = "C:\\Ektelesis.Net\\CFDI\\DATOS\\OUT";
            [XmlAttribute()]
            public string RutaXML_IN = "C:\\Ektelesis.Net\\CFDI\\DATOS\\IN";
            [XmlAttribute()]

            public string RutaPDF = "";
            [XmlAttribute()]
            public string RutaCer = "";
            [XmlAttribute()]
            public string Certificado = "";
            [XmlAttribute()]
            public string ValidoDesde = "";
            [XmlAttribute()]
            public string ValidoHasta = "";
            [XmlAttribute()]

            public string noCertificado = "";
            [XmlAttribute()]
            public string RutaKey = "";
            [XmlAttribute()]
            public string ClaveKey = "";
            [XmlAttribute()]
            public string Serie = "";
            [XmlAttribute()]
            public string noAprobacion = "";
            [XmlAttribute()]
            public string anoAprobacion = "";
            [XmlAttribute()]
            public string FolioInicial = "";
            [XmlAttribute()]
            public string FolioFinal = "";
            [XmlAttribute()]
            public string FolioActual = "";
        }

        public enum TipoBD : int
        {
            MSAccess = 0,
            MSSQLServer = 1,
            MySQL = 2,
            DBase = 3,
            Firebird = 4
        }
    }

}
#endif
