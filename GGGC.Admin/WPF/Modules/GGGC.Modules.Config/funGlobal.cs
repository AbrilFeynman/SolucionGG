using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

#if !SILVERLIGHT
namespace GGGC.Modules.Config
{
    public class funGlobal
    {
        public static string Encrypt(string strText, string strEncrKey = "&%#@?,:*")
        {
            byte[] IV = {
			0x12,
			0x34,
			0x56,
			0x78,
			0x90,
			0xab,
			0xcd,
			0xef
		};
            try
            {
                byte[] bykey = System.Text.Encoding.UTF8.GetBytes(strEncrKey);
                byte[] InputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write);
                cs.Write(InputByteArray, 0, InputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al encriptar, Error: " + ex.Message);
            }
        }

        public static string Decrypt(string strText, string sDecrKey = "&%#@?,:*")
        {
            byte[] IV = {
			0x12,
			0x34,
			0x56,
			0x78,
			0x90,
			0xab,
			0xcd,
			0xef
		};
            byte[] inputByteArray = new byte[strText.Length + 1];
            try
            {
                byte[] byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desencriptar, Error: " + ex.Message);
            }
        }

        public static bool ComprobarCampo(object Campo)
        {
            bool bRes = true;
            if (Campo != null)
            {
                if (Convert.IsDBNull(Campo))
                {
                    bRes = false;
                }
                else
                {
                    if (Campo.ToString().Trim().Length == 0)
                    {
                        bRes = false;
                    }
                }
            }
            else
            {
                bRes = false;
            }
            return bRes;
        }

        public static string LimpiarRFC(string RFC)
        {
            string RFCGenerico = "XAXX010101000";
            string sRFC = RFC.Trim();

            if (!string.IsNullOrEmpty(RFC))
            {
                sRFC = Regex.Replace(sRFC, " ", string.Empty);
                sRFC = Regex.Replace(sRFC, "-", string.Empty);
                sRFC = sRFC.Trim();

                if (sRFC.Length == 0)
                {
                    sRFC = RFCGenerico;
                }
                else if (sRFC.Length < 12)
                {
                    sRFC = RFCGenerico;
                }
                else if (sRFC.Length > 13)
                {
                    sRFC = RFCGenerico;
                }
            }
            else
            {
                sRFC = RFCGenerico;
            }

            return sRFC;
        }

        //public static bool validaTablaFactura(System.Data.DataTable tabla)
        //{
        //    try
        //    {
        //        if (tabla != null)
        //        {
        //            List<string> Campos = new List<string>();
        //            Campos.Add("serie");
        //            Campos.Add("folio");
        //            Campos.Add("fecha");
        //            Campos.Add("formaDePago");
        //            Campos.Add("condicionesDePago");
        //            Campos.Add("subTotal");
        //            Campos.Add("descuento");
        //            Campos.Add("motivoDescuento");
        //            Campos.Add("total");
        //            Campos.Add("metodoDePago");
        //            Campos.Add("rfc");
        //            Campos.Add("nombre");
        //            Campos.Add("calle");
        //            Campos.Add("noExterior");
        //            Campos.Add("noInterior");
        //            Campos.Add("colonia");
        //            Campos.Add("localidad");
        //            Campos.Add("referencia");
        //            Campos.Add("municipio");
        //            Campos.Add("estado");
        //            Campos.Add("pais");
        //            Campos.Add("codigoPostal");
        //            Campos.Add("totalImpuestosRetenidos");
        //            Campos.Add("totalImpuestosTrasladados");
        //            Campos.Add("R_importe_ISR");
        //            Campos.Add("R_importe_IVA");
        //            Campos.Add("T_tasa_IVA");
        //            Campos.Add("T_importe_IVA");
        //            Campos.Add("T_tasa_IEPS");
        //            Campos.Add("T_importe_IEPS");
        //            Campos.Add("Moneda");
        //            Campos.Add("TipoCambio");

        //            for (int i = 0; i < Campos.Count; i++)
        //            {
        //                if (tabla.Columns.IndexOf(Campos[i].ToString()) == -1)
        //                {
        //                    throw new Exception("La columna: " + Campos[i].ToString() + " no existe en la consulta");
        //                }
        //            }

        //            return true;
        //        }
        //        else
        //        {
        //            throw new Exception("La consulta de la tabla no es válida");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al validar consulta Tabla Factura, Error: " + ex.Message);
        //    }
        //}

        //public static bool validaTablaDetalle(System.Data.DataTable tabla)
        //{
        //    try
        //    {
        //        if (tabla != null)
        //        {
        //            List<string> Campos = new List<string>();
        //            Campos.Add("serie");
        //            Campos.Add("folio");
        //            Campos.Add("cantidad");
        //            Campos.Add("unidad");
        //            Campos.Add("noIdentificacion");
        //            Campos.Add("descripcion");
        //            Campos.Add("valorUnitario");
        //            Campos.Add("importe");
        //            Campos.Add("IA_Numero");
        //            Campos.Add("IA_Fecha");
        //            Campos.Add("IA_Aduana");
        //            Campos.Add("CP_numero");

        //            for (int i = 0; i < Campos.Count; i++)
        //            {
        //                if (tabla.Columns.IndexOf("") == -1)
        //                {
        //                    throw new Exception("La columna: " + Campos[i].ToString() + " no existe en la consulta");
        //                }
        //            }

        //            return true;
        //        }
        //        else
        //        {
        //            throw new Exception("La consulta de la tabla no es válida");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al validar consulta Tabla Factura, Error: " + ex.Message);
        //    }
        //}


      //  #if !SILVERLIGHT
         public static string GetSetting(string appName, string section, string key, string sDefault)
        {
            // Los datos de VB se guardan en:
            // HKEY_CURRENT_USER\Software\VB and VBA Program Settings
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\VB and VBA Program Settings\" +
                                                              appName + "\\" + section);
            string s = sDefault;
            if (rk != null)
            {
                s = (string)rk.GetValue(key);
            }
            return s;
        }

        public static void SaveSetting(string appName, string section, string key, string setting)
        {
            // Los datos de VB se guardan en:
            // HKEY_CURRENT_USER\Software\VB and VBA Program Settings
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\VB and VBA Program Settings\" +
                                                                appName + "\\" + section);
            rk.SetValue(key, setting);
        }

//#endif


        public static string App_Path_Sistema_SAT()
        {
            string sPath = System.Environment.CurrentDirectory;
            if (sPath.EndsWith("\\"))
            {
                return "C:\\Ektelesis.Net\\CFDI\\EXE\\Sistema\\SAT\\";
            }
            
         
            else 
            {
                return  "C:\\Ektelesis.Net\\CFDI\\EXE\\Sistema\\SAT\\";
            }
        }
    }
}

#endif
