using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace GGGC.Admin.ERP.Modules.MTE.Garage.Support
{
    public static class Locations
    {
        public static List<Departamento> Departamentos;
        public static List<Empresa> Empresas;

        public static List<DepartamentoLRG> DepartamentosLRG;
        public static List<DepartamentoCLT> DepartamentosCLT;
        public static List<DepartamentoConsejo> DepartamentosConsejo;

        static Locations()
        {
            Departamentos = new List<Departamento>();
            Empresas = new List<Empresa>();
            DepartamentosLRG = new List<DepartamentoLRG>();
            DepartamentosCLT = new List<DepartamentoCLT>();
            DepartamentosConsejo = new List<DepartamentoConsejo>();

            #region  generated code
            //Departamentos.Add(new Departamento() { ID = 1, EmpresaCode = "1", DepartamentName = "BASE 1" });
            //Departamentos.Add(new Departamento() { ID = 2, EmpresaCode = "1", DepartamentName = "BASE 2" });
            //Departamentos.Add(new Departamento() { ID = 3, EmpresaCode = "1", DepartamentName = "BASE 3" });
            //Departamentos.Add(new Departamento() { ID = 4, EmpresaCode = "1", DepartamentName = "BASE 4" });
            //Departamentos.Add(new Departamento() { ID = 5, EmpresaCode = "1", DepartamentName = "BASE 5" });
            //Departamentos.Add(new Departamento() { ID = 7, EmpresaCode = "1", DepartamentName = "BASE 7" });
            //Departamentos.Add(new Departamento() { ID = 8, EmpresaCode = "1", DepartamentName = "BASE 8" });
            //Departamentos.Add(new Departamento() { ID = 9, EmpresaCode = "1", DepartamentName = "BASE 9" });
            //Departamentos.Add(new Departamento() { ID = 10, EmpresaCode = "1", DepartamentName = "BASE 10" });
            //Departamentos.Add(new Departamento() { ID = 11, EmpresaCode = "1", DepartamentName = "BASE 11" });
            //Departamentos.Add(new Departamento() { ID = 12, EmpresaCode = "1", DepartamentName = "BASE 12" });
            //Departamentos.Add(new Departamento() { ID = 13, EmpresaCode = "1", DepartamentName = "BASE 13" });
            //Departamentos.Add(new Departamento() { ID = 15, EmpresaCode = "1", DepartamentName = "BASE 15" });

            //Departamentos.Add(new Departamento() { ID = 6, EmpresaCode = "2", DepartamentName = "BASE 6" });
            //Departamentos.Add(new Departamento() { ID = 14, EmpresaCode = "2", DepartamentName = "BASE 14" });

            DepartamentosConsejo.Add(new DepartamentoConsejo() { ID = 20, DepartamentName = "SR CAMPOS" });
            DepartamentosConsejo.Add(new DepartamentoConsejo() { ID = 21, DepartamentName = "SRA NORMA CAMPOS" });
            DepartamentosConsejo.Add(new DepartamentoConsejo() { ID = 22,  DepartamentName = "LIC. LAURA" });
            DepartamentosConsejo.Add(new DepartamentoConsejo() { ID = 23,  DepartamentName = "LIC. MONICA" });
            DepartamentosConsejo.Add(new DepartamentoConsejo() { ID = 24,  DepartamentName = "DRA LILIANA" });
            DepartamentosConsejo.Add(new DepartamentoConsejo() { ID = 25,  DepartamentName = "LIC. JORGE" });
            DepartamentosConsejo.Add(new DepartamentoConsejo() { ID = 26,  DepartamentName = "SR GERARDO" });

            Departamentos.Add(new Departamento() { ID = 30,  DepartamentName = "AUDITORIA" });
            Departamentos.Add(new Departamento() { ID = 31,  DepartamentName = "PATRIMONIO" });
            Departamentos.Add(new Departamento() { ID = 32,  DepartamentName = "SISTEMAS" });
            Departamentos.Add(new Departamento() { ID = 33, DepartamentName = "CALIDAD" });
            Departamentos.Add(new Departamento() { ID = 34,  DepartamentName = "COORDINADOR SUC/MAYOREO" });





            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 1,  DepartamentName = "BASE 1" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 2, DepartamentName = "BASE 2" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 3, DepartamentName = "BASE 3" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 4,  DepartamentName = "BASE 4" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 5, DepartamentName = "BASE 5" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 7,  DepartamentName = "BASE 7" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 8, DepartamentName = "BASE 8" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 9, DepartamentName = "BASE 9" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 10,  DepartamentName = "BASE 10" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 11, DepartamentName = "BASE 11" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 12, DepartamentName = "BASE 12" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 13,  DepartamentName = "BASE 13" });
            DepartamentosLRG.Add(new DepartamentoLRG() { ID = 15,  DepartamentName = "BASE 15" });


            DepartamentosCLT.Add(new DepartamentoCLT() { ID = 6, DepartamentName = "BASE 6" });
            DepartamentosCLT.Add(new DepartamentoCLT() { ID = 14, DepartamentName = "BASE 14" });




            #endregion
            #region  generated code
            Empresas.Add(new Empresa() { Code = "1", Name = "LRG" });
            Empresas.Add(new Empresa() { Code = "2", Name = "CLT" });
            Empresas.Add(new Empresa() { Code = "3", Name = "CONSEJO" });
            Empresas.Add(new Empresa() { Code = "4", Name = "DEPARTAMENTOS" });
          
            #endregion
        }
    }

    public class Empresa
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Departamento
    {
        public int ID { get; set; }
        public string DepartamentName { get; set; }
        //public string EmpresaCode { get; set; }
    }


    public class DepartamentoLRG
    {
        public int ID { get; set; }
        public string DepartamentName { get; set; }
       
    }

    public class DepartamentoCLT
    {
        public int ID { get; set; }
        public string DepartamentName { get; set; }

    }

    public class DepartamentoConsejo
    {
        public int ID { get; set; }
        public string DepartamentName { get; set; }

    }
}
