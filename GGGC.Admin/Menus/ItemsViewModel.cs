using System;
using System.Collections.ObjectModel;

namespace GGGC.Admin.Menus
{
    public class ItemsViewModel : ObservableCollection<BusinessItem>
    {
        private enum MachineState : int
        {
            PowerOff = 0,
            Running = 5,
            Sleeping = 10,
            Hibernating = Sleeping + 5
        }

        public enum Menu : int
        {
            SALES = 5,
            EKTELESIS = 2,
            CATALOGOS = 3,
            SHOPS = 4,
            INVENTORY = 1,
            CXC = 6,
            CXP = 7,
            AUD = 8,
            SISTEMAS = 9,
            GG = 500,
            HOME = 13,
            LRG = 15,
            CLT = 16,
            MTO = 20,
            BUENFIN = 2015

        }

        enum Months : byte { Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec };

        public ItemsViewModel(Menu intMenu)
        {
            Menu myMenu = 0;

            myMenu = intMenu;

            switch (myMenu)
            {
                case Menu.CATALOGOS:
                    /*

                    //Root
                    BusinessItem mnuInventory = new BusinessItem(null) { Name = "Inventario", Tag = "100", IsSelected = false, IsEnabled = true };
                    this.Add(mnuInventory);
                    //Classes
                    BusinessItem mnuInvClass1 = new BusinessItem(mnuInventory) { Name = "Llantas, Cámaras y Corbatas    ", Tag = "101", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass2 = new BusinessItem(mnuInventory) { Name = "Refacciones", Tag = "102", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass3 = new BusinessItem(mnuInventory) { Name = "Rines", Tag = "103", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass4 = new BusinessItem(mnuInventory) { Name = "Servicios", Tag = "104", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass5 = new BusinessItem(mnuInventory) { Name = "Miscelaneos", Tag = "105", IsSelected = false, IsEnabled = true };
                    mnuInventory.Items.Add(mnuInvClass1);
                    mnuInventory.Items.Add(mnuInvClass2);
                    mnuInventory.Items.Add(mnuInvClass3);
                    mnuInventory.Items.Add(mnuInvClass4);
                    mnuInventory.Items.Add(mnuInvClass5);
                    //mnuInventory.i                   
                    //Fixed Groups
                    //class 1
                    BusinessItem mnuInvGroup01 = new BusinessItem(mnuInvClass1) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass1Lines = new BusinessItem(mnuInvClass1) { Name = "Lineas", Tag = "109", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass1Brands = new BusinessItem(mnuInvClass1) { Name = "Marcas", Tag = "119", IsSelected = false };
                    mnuInvClass1.Items.Add(mnuInvGroup01);

                    BusinessItem mnuInvGroup1 = new BusinessItem(mnuInvGroup01) { Name = "Auto", Tag = "70", IsSelected = false };
                    BusinessItem mnuInvGroup2 = new BusinessItem(mnuInvGroup01) { Name = "Camioneta", Tag = "153", IsSelected = false };
                    BusinessItem mnuInvGroup3 = new BusinessItem(mnuInvGroup01) { Name = "Camion", Tag = "154", IsSelected = false };

                    mnuInvGroup01.Items.Add(mnuInvGroup1);
                    mnuInvGroup01.Items.Add(mnuInvGroup2);
                    mnuInvGroup01.Items.Add(mnuInvGroup3);

                    //mnuInvGroup02.Items.Add(mnuInv2Group3);
                    //mnuInvClass1.Items.Add(mnuClass1Lines);
                    //mnuInvClass1.Items.Add(mnuClass1Brands);
                    //class 2
                    BusinessItem mnuInvGroup02 = new BusinessItem(mnuInvClass2) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass2Lines = new BusinessItem(mnuInvClass2) { Name = "Lineas", Tag = "110", IsSelected = false };
                    BusinessItem mnuClass2Brands = new BusinessItem(mnuInvClass2) { Name = "Marcas", Tag = "120", IsSelected = false };
                    mnuInvClass2.Items.Add(mnuInvGroup02);
                    //mnuInvClass2.Items.Add(mnuClass2Lines);
                    //mnuInvClass2.Items.Add(mnuClass2Brands);

                    //class 3
                    BusinessItem mnuInvGroup03 = new BusinessItem(mnuInvClass3) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass3Lines = new BusinessItem(mnuInvClass3) { Name = "Lineas", Tag = "111", IsSelected = false };
                    BusinessItem mnuInvClass3Brands = new BusinessItem(mnuInvClass3) { Name = "Marcas", Tag = "121", IsSelected = false };
                    mnuInvClass3.Items.Add(mnuInvGroup03);
                    //mnuInvClass3.Items.Add(mnuInvClass3Lines);
                    //mnuInvClass3.Items.Add(mnuInvClass3Brands);

                    //class 4
                    BusinessItem mnuInvGroup04 = new BusinessItem(mnuInvClass4) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass4Lines = new BusinessItem(mnuInvClass4) { Name = "Lineas", Tag = "112", IsSelected = false };
                    BusinessItem mnuInvClass4Brands = new BusinessItem(mnuInvClass4) { Name = "Marcas", Tag = "122", IsSelected = false };
                    mnuInvClass4.Items.Add(mnuInvGroup04);
                    //mnuInvClass4.Items.Add(mnuInvClass4Lines);
                    //mnuInvClass4.Items.Add(mnuInvClass4Brands);

                    BusinessItem mnuInv4Group1 = new BusinessItem(mnuInvGroup04) { Name = "Servicios", Tag = "80", IsSelected = false };
                    BusinessItem mnuInv4Group2 = new BusinessItem(mnuInvGroup04) { Name = "Insumos", Tag = "85", IsSelected = false };
                    //  BusinessItem mnuInvGroup3 = new BusinessItem(mnuInvGroup04) { Name = "Camion", Tag = "154", IsSelected = false };

                    mnuInvGroup04.Items.Add(mnuInv4Group1);
                    mnuInvGroup04.Items.Add(mnuInv4Group2);
                    //mnuInvGroup01.Items.Add(mnuInvGroup3);

                    //class 5
                    BusinessItem mnuInvGroup05 = new BusinessItem(mnuInvClass5) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass5Lines = new BusinessItem(mnuInvClass5) { Name = "Lineas", Tag = "113", IsSelected = false };
                    BusinessItem mnuClass5Brands = new BusinessItem(mnuInvClass5) { Name = "Marcas", Tag = "123", IsSelected = false };
                    mnuInvClass5.Items.Add(mnuInvGroup05);
                    //mnuInvClass5.Items.Add(mnuClass5Lines);
                    //mnuInvClass5.Items.Add(mnuClass5Brands);

                    //Variable Groups
                    //Class 2 Groups
                    BusinessItem mnuInv2Group4 = new BusinessItem(mnuInvGroup02) { Name = "Acumuladores", Tag = "151", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInv2Group1 = new BusinessItem(mnuInvGroup02) { Name = "Amortiguadores", Tag = "152", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInv2Group2 = new BusinessItem(mnuInvGroup02) { Name = "Miscelaneos", Tag = "153", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInv2Group3 = new BusinessItem(mnuInvGroup02) { Name = "Refacciones", Tag = "154", IsSelected = false, IsEnabled = true };
                    //BusinessItem mnuInv2Group33 = new BusinessItem(mnuInvGroup02) { Name = "4-Refacciones", Tag = "155", IsSelected = false };
                    //  BusinessItem mnuInv2Group3 = new BusinessItem(mnuInvGroup02) { Name = "Refacciones", Tag = "154", IsSelected = false };


                    mnuInvGroup02.Items.Add(mnuInv2Group4);
                    mnuInvGroup02.Items.Add(mnuInv2Group1);
                    mnuInvGroup02.Items.Add(mnuInv2Group2);
                    mnuInvGroup02.Items.Add(mnuInv2Group3);
                    // mnuInvGroup02.Items.Add(mnuInv2Group33);

                    //Class 3 Groups
                    BusinessItem mnuInv3Group1 = new BusinessItem(mnuInvGroup02) { Name = "Rines", Tag = "161", IsSelected = false };
                    mnuInvGroup03.Items.Add(mnuInv3Group1);

                    BusinessItem mnuTest = new BusinessItem(null) { Name = "Precios", Tag = "2098", IsSelected = false };
                    // this.Add(mnuTest);
                    //Classes
                    BusinessItem mnutestClass1 = new BusinessItem(mnuTest) { Name = "TEST", Tag = "2099", IsSelected = false };
                    BusinessItem mnutestClass2 = new BusinessItem(mnuTest) { Name = "TEST 02", Tag = "3000", IsSelected = false };
                    BusinessItem mnutestClass3 = new BusinessItem(mnuTest) { Name = "Precios Refacciones", Tag = "3001", IsSelected = false };

                    BusinessItem mnutestClass4 = new BusinessItem(mnuTest) { Name = "Prices", Tag = "90", IsSelected = false };

                    BusinessItem mnutestClass5 = new BusinessItem(mnuTest) { Name = "Mobile", Tag = "931", IsSelected = false };


                    BusinessItem mnutestClass6 = new BusinessItem(mnuTest) { Name = "Rines", Tag = "352", IsSelected = false };


                    BusinessItem mnutestClass7 = new BusinessItem(mnuTest) { Name = "Compras", Tag = "850", IsSelected = false };

                    //BusinessItem mnuInvClass2 = new BusinessItem(mnuTest) { Name = "Refacciones", Tag = "102", IsSelected = false };
                    //BusinessItem mnuInvClass3 = new BusinessItem(mnuTest) { Name = "Rines", Tag = "103", IsSelected = false };
                    //BusinessItem mnuInvClass4 = new BusinessItem(mnuTest) { Name = "Servicios", Tag = "104", IsSelected = false };
                    //BusinessItem mnuInvClass5 = new BusinessItem(mnuTest) { Name = "Miscelaneos", Tag = "105", IsSelected = false };
                    mnuTest.Items.Add(mnutestClass1);
                    mnuTest.Items.Add(mnutestClass2);
                    mnuTest.Items.Add(mnutestClass3);
                    mnuTest.Items.Add(mnutestClass4);
                    mnuTest.Items.Add(mnutestClass5);
                    mnuTest.Items.Add(mnutestClass6);
                    mnuTest.Items.Add(mnutestClass7);


                    // BusinessItem mnuTest = new BusinessItem(null) { Name = "Test", Tag = "2099", IsSelected = false };
                    //this.Add(mnuTest);
                    ////Classes
                    //BusinessItem mnutestClass1 = new BusinessItem(mnuTest) { Name = "TEST", Tag = "2099", IsSelected = false };
                    //BusinessItem mnutestClass2 = new BusinessItem(mnuTest) { Name = "TEST 02", Tag = "3000", IsSelected = false };

                    ////BusinessItem mnuInvClass2 = new BusinessItem(mnuTest) { Name = "Refacciones", Tag = "102", IsSelected = false };
                    ////BusinessItem mnuInvClass3 = new BusinessItem(mnuTest) { Name = "Rines", Tag = "103", IsSelected = false };
                    ////BusinessItem mnuInvClass4 = new BusinessItem(mnuTest) { Name = "Servicios", Tag = "104", IsSelected = false };
                    ////BusinessItem mnuInvClass5 = new BusinessItem(mnuTest) { Name = "Miscelaneos", Tag = "105", IsSelected = false };
                    //mnuTest.Items.Add(mnutestClass1);
                    //mnuTest.Items.Add(mnutestClass2);

                    BusinessItem menuClientes = new BusinessItem(null)
                    {
                        Name = "Clientes",
                        IsSelected = false

                    };

                    BusinessItem menuProveedores = new BusinessItem(null)
                    {
                        Name = "Provedores",
                        IsSelected = false

                    };

                    BusinessItem menuVentas = new BusinessItem(null)
                    {
                        Name = "Ventas",
                        IsSelected = false

                    };


                    //BusinessItem menuInventariosClases = new BusinessItem(menuInventarios)
                    //{
                    //    Name = "Clases",
                    //    IsSelected = false

                    //};

                    BusinessItem menuClientesClases = new BusinessItem(menuClientes)
                    {
                        Name = "Clases",
                        IsSelected = false

                    };

                    BusinessItem menuProveedoresClases = new BusinessItem(menuProveedores)
                    {
                        Name = "Clases",
                        IsSelected = false

                    };

                    BusinessItem menuVentasClases = new BusinessItem(menuVentas)
                    {
                        Name = "Clases",
                        IsSelected = false

                    };


                    //    BusinessItem menuLlantas = new BusinessItem(menuProductos)
                    //    {
                    //        Name = "Llantas",
                    //        IsSelected = false

                    //    };

                    //    BusinessItem menuCamaras = new BusinessItem(menuProductos)
                    //    {
                    //        Name = "Cámaras",
                    //        IsSelected = false

                    //    };

                    //    BusinessItem menuCorbatas = new BusinessItem(menuProductos)
                    //    {
                    //        Name = "Corbatas",
                    //        IsSelected = false

                    //    };

                    //BusinessItem menu2 = new BusinessItem(menuInventarios) //Name = menu1.Name + "." + "Requisiciones",
                    //        {
                    //            Name ="1. Cargar Requisicion",
                    //            IsSelected = false
                    //        };

                    ////menuInventarios.Items.Add(menuProductos);
                    //menuInventarios.Items.Add(menuInventariosClases);
                    //menuClientes.Items.Add(menuClientesClases);
                    //menuProveedores.Items.Add(menuProveedoresClases);
                    //menuVentas.Items.Add(menuVentasClases);


                    //menuInventariosClases.Items.Add(menuProductos);
                    //menuInventariosClases.Items.Add(menuServicios);
                    //menuInventariosClases.Items.Add(menuRefacciones);
                    //menuInventariosClases.Items.Add(menuRines);
                    //menuInventariosClases.Items.Add(menuMiscelaneos);


                    //menuInventarios.Items.Add(menuMarcas);
                    //menuInventarios.Items.Add(menuLineas);
                    //menuInventarios.Items.Add(menuGrupos);
                    //menuProductos.Items.Add(menuLlantas);
                    //menuProductos.Items.Add(menuCamaras);
                    //menuProductos.Items.Add(menuCorbatas);
                    //   // menuInventarios.Items.Add(menu2);
                    //    //this.Add(menuInventarios);
                    //    this.Add(menuInventarios);
                    //    this.Add(menuClientes);
                    //    this.Add(menuProveedores);
                    //    this.Add(menuVentas);

                    break;

                case Menu.EKTELESIS:

                    BusinessItem mnuEktelesis = new BusinessItem(null) { Name = "Módulos", Tag = "4098", IsSelected = false };
                    this.Add(mnuEktelesis);
                    //Classes
                    BusinessItem mnuekteClass1 = new BusinessItem(mnuEktelesis) { Name = "LRG", Tag = "4099", IsSelected = false };
                    BusinessItem mnuekteClass2 = new BusinessItem(mnuEktelesis) { Name = "CLT", Tag = "4000", IsSelected = false };
                    //BusinessItem mnuekteClass3 = new BusinessItem(mnuEktelesis) { Name = "Precios Refacciones", Tag = "4001", IsSelected = false };

                    //BusinessItem mnuInvClass2 = new BusinessItem(mnuTest) { Name = "Refacciones", Tag = "102", IsSelected = false };
                    //BusinessItem mnuInvClass3 = new BusinessItem(mnuTest) { Name = "Rines", Tag = "103", IsSelected = false };
                    //BusinessItem mnuInvClass4 = new BusinessItem(mnuTest) { Name = "Servicios", Tag = "104", IsSelected = false };
                    //BusinessItem mnuInvClass5 = new BusinessItem(mnuTest) { Name = "Miscelaneos", Tag = "105", IsSelected = false };
                    mnuEktelesis.Items.Add(mnuekteClass1);
                    //    mnuEktelesis.Items.Add(mnuekteClass2);
                    //  mnuEktelesis.Items.Add(mnuekteClass3);


                    //Fixed Groups
                    //class 1
                    BusinessItem mnuEkteGroup01 = new BusinessItem(mnuekteClass1) { Name = "Como Vamos", Tag = "7000", IsSelected = false };
                    BusinessItem mnuEkteGroup02 = new BusinessItem(mnuekteClass1) { Name = "Historial", Tag = "7002", IsSelected = false };
                  //  BusinessItem mnuEkteGroup03 = new BusinessItem(mnuekteClass1) { Name = "Recibos de Caja", Tag = "7003", IsSelected = false, IsEnabled = false };
                    // BusinessItem mnuClass1Lines = new BusinessItem(mnuInvClass1) { Name = "Lineas", Tag = "109", IsSelected = false };
                    //BusinessItem mnuClass1Brands = new BusinessItem(mnuInvClass1) { Name = "Marcas", Tag = "119", IsSelected = false };
                    // mnuekteClass1.Items.Add(mnuEkteGroup01);
                    mnuekteClass1.Items.Add(mnuEkteGroup02);
                   // mnuekteClass1.Items.Add(mnuEkteGroup03);


                    BusinessItem mnuClassAuto = new BusinessItem(mnuekteClass1) { Name = "Auto", Tag = "8000", IsSelected = false };
                    mnuEkteGroup01.Items.Add(mnuClassAuto);

                    BusinessItem mnuClassCta = new BusinessItem(mnuekteClass1) { Name = "Camioneta", Tag = "8710", IsSelected = false };
                    // mnuEkteGroup01.Items.Add(mnuClassCta);

                    BusinessItem mnuClassCamion = new BusinessItem(mnuekteClass1) { Name = "Camión", Tag = "8020", IsSelected = false };
                    //mnuEkteGroup01.Items.Add(mnuClassCamion);
                    break;

                //case 10:
                //{
                //    BusinessItem mnuInventory3 = new BusinessItem(null) { Name = "Módulos", Tag = "4098", IsSelected = false };
                //    this.Add(mnuInventory3);
                //}                    
                //    break;

                case Menu.INVENTORY:

                    BusinessItem mnuInv = new BusinessItem(null) { Name = "Entradas", Tag = "5098", IsSelected = false };
                    this.Add(mnuInv);
                    //Classes
                    BusinessItem mnuCompras = new BusinessItem(mnuInv) { Name = "Compras", Tag = "5099", IsSelected = false };
                    BusinessItem mnuComprasMichelin = new BusinessItem(mnuInv) { Name = "Compras Michelin", Tag = "5100", IsSelected = false };

                    BusinessItem mnuTransferencias = new BusinessItem(mnuInv) { Name = "Transferencias", Tag = "401", IsSelected = false };



                    BusinessItem mnuAjustes = new BusinessItem(mnuInv) { Name = "Ajustes", Tag = "301", IsSelected = false };
                    // BusinessItem mnuekteClass2 = new BusinessItem(mnuEktelesis) { Name = "CLT", Tag = "4000", IsSelected = false };
                    mnuInv.Items.Add(mnuCompras);
                    mnuInv.Items.Add(mnuComprasMichelin);
                    mnuInv.Items.Add(mnuTransferencias);

                    mnuInv.Items.Add(mnuAjustes);
                    //BusinessItem mnuekteClass3 = new BusinessItem(mnuEktelesis) { Name = "Precios Refacciones", Tag = "4001", IsSelected = false };

                    BusinessItem mnuInv2 = new BusinessItem(null) { Name = "Salidas", Tag = "5098", IsSelected = false };
                    BusinessItem mnuTransferencias2 = new BusinessItem(mnuInv2) { Name = "Transferencias", Tag = "5099", IsSelected = false };
                    BusinessItem mnuFacturas = new BusinessItem(mnuInv2) { Name = "Facturas", Tag = "5099", IsSelected = false };

                    BusinessItem mnuDevoluciones = new BusinessItem(mnuInv) { Name = "Devoluciones", Tag = "6400", IsSelected = false };
                    BusinessItem mnuDevolucionesOI = new BusinessItem(mnuInv) { Name = "Devoluciones Otros Ingresos", Tag = "6401", IsSelected = false };


                    this.Add(mnuInv2);
                    mnuInv2.Items.Add(mnuTransferencias2);
                    mnuInv2.Items.Add(mnuDevoluciones);
                    mnuInv2.Items.Add(mnuDevolucionesOI);
                    //mnuInv2.Items.Add(mnuFacturas);
                    break;

                case Menu.CXC:

                    BusinessItem mnuCxC1 = new BusinessItem(null) { Name = "Módulos", Tag = "6001", IsSelected = false };
                    this.Add(mnuCxC1);

                    BusinessItem mnuCxC2 = new BusinessItem(null) { Name = "Ektelesis", Tag = "6002", IsSelected = false };
                    this.Add(mnuCxC2);

                    //Classes
                    //BusinessItem mnuCompras = new BusinessItem(mnuInv) { Name = "Compras", Tag = "5099", IsSelected = false };
                    //BusinessItem mnuComprasMichelin = new BusinessItem(mnuInv) { Name = "Compras Michelin", Tag = "5100", IsSelected = false };
                    //BusinessItem mnuTransferencias = new BusinessItem(mnuInv) { Name = "Transferencias", Tag = "401", IsSelected = false };
                    //  BusinessItem mnuCxC1_1 = new BusinessItem(mnuCxC1) { Name = "Facturas", Tag = "601", IsSelected = false };

                    BusinessItem mnuCxC2_1 = new BusinessItem(mnuCxC2) { Name = "Ventas con NCR", Tag = "601", IsSelected = false };
                    BusinessItem mnuCxC2_2 = new BusinessItem(mnuCxC2) { Name = "Comisiones CXC", Tag = "602", IsSelected = false };
                    BusinessItem mnuCxC2_3 = new BusinessItem(mnuCxC2) { Name = "Clientes", Tag = "8010", IsSelected = false };
                    //    BusinessItem mnuFactOI = new BusinessItem(mnuCxC) { Name = "Facturas Otros Ingresos", Tag = "602", IsSelected = false };

                    //BusinessItem mnuCoti = new BusinessItem(mnuCxC) { Name = "Cotizacion", Tag = "680", IsSelected = false };
                    //BusinessItem mnupedido = new BusinessItem(mnuCxC) { Name = "Pedidos", Tag = "682", IsSelected = false };
                    //BusinessItem mnuordenes = new BusinessItem(mnuCxC) { Name = "Orden de Trabajo", Tag = "684", IsSelected = false };
                    //BusinessItem mnuexis = new BusinessItem(mnuCxC) { Name = "Existencias", Tag = "686", IsSelected = false };
                    //BusinessItem mnuAjustes = new BusinessItem(mnuInv) { Name = "Ajustes", Tag = "301", IsSelected = false };
                    //// BusinessItem mnuekteClass2 = new BusinessItem(mnuEktelesis) { Name = "CLT", Tag = "4000", IsSelected = false };
                    //mnuSales.Items.Add(mnuCompras);
                    //mnuSales.Items.Add(mnuComprasMichelin);
                    //mnuSales.Items.Add(mnuTransferencias);
                    mnuCxC2.Items.Add(mnuCxC2_1);
                    mnuCxC2.Items.Add(mnuCxC2_2);
                    mnuCxC2.Items.Add(mnuCxC2_3);
                    //mnuCxC.Items.Add(mnuFactOI);
                    //mnuCxC.Items.Add(mnuCoti);

                    //mnuCxC.Items.Add(mnupedido);
                    //mnuCxC.Items.Add(mnuordenes);
                    //mnuCxC.Items.Add(mnuexis);

                    //mnuSales.Items.Add(mnuAjustes);
                    ////BusinessItem mnuekteClass3 = new BusinessItem(mnuEktelesis) { Name = "Precios Refacciones", Tag = "4001", IsSelected = false };

                    //BusinessItem mnuSales2 = new BusinessItem(null) { Name = "Salidas", Tag = "5098", IsSelected = false };
                    //BusinessItem mnuTransferencias2 = new BusinessItem(mnuInv2) { Name = "Transferencias", Tag = "5099", IsSelected = false };
                    //BusinessItem mnuFacturas = new BusinessItem(mnuInv2) { Name = "Facturas", Tag = "5099", IsSelected = false };
                    //this.Add(mnuSales2);
                    //mnuSales2.Items.Add(mnuTransferencias2);
                    //mnuSales2.Items.Add(mnuFacturas);

                    break;

                case Menu.AUD:

                    BusinessItem mnuaudi1 = new BusinessItem(null) { Name = "CFDIS", Tag = "6001", IsSelected = false };
                    this.Add(mnuaudi1);
                    //Classes
                    //BusinessItem mnuCompras = new BusinessItem(mnuInv) { Name = "Compras", Tag = "5099", IsSelected = false };
                    //BusinessItem mnuComprasMichelin = new BusinessItem(mnuInv) { Name = "Compras Michelin", Tag = "5100", IsSelected = false };
                    //BusinessItem mnuTransferencias = new BusinessItem(mnuInv) { Name = "Transferencias", Tag = "401", IsSelected = false };
                    BusinessItem mnuaudi_1 = new BusinessItem(mnuaudi1) { Name = "Facturas", Tag = "41", IsSelected = false };

                    //BusinessItem mnuCxC2_1 = new BusinessItem(mnuCxC2) { Name = "Ventas con NCR", Tag = "601", IsSelected = false };
                    //BusinessItem mnuCxC2_2 = new BusinessItem(mnuCxC2) { Name = "Comisiones Antiguedad", Tag = "600", IsSelected = false };
                    //    BusinessItem mnuFactOI = new BusinessItem(mnuCxC) { Name = "Facturas Otros Ingresos", Tag = "602", IsSelected = false };

                    //BusinessItem mnuCoti = new BusinessItem(mnuCxC) { Name = "Cotizacion", Tag = "680", IsSelected = false };
                    //BusinessItem mnupedido = new BusinessItem(mnuCxC) { Name = "Pedidos", Tag = "682", IsSelected = false };
                    //BusinessItem mnuordenes = new BusinessItem(mnuCxC) { Name = "Orden de Trabajo", Tag = "684", IsSelected = false };
                    //BusinessItem mnuexis = new BusinessItem(mnuCxC) { Name = "Existencias", Tag = "686", IsSelected = false };
                    //BusinessItem mnuAjustes = new BusinessItem(mnuInv) { Name = "Ajustes", Tag = "301", IsSelected = false };
                    //// BusinessItem mnuekteClass2 = new BusinessItem(mnuEktelesis) { Name = "CLT", Tag = "4000", IsSelected = false };
                    //mnuSales.Items.Add(mnuCompras);
                    //mnuSales.Items.Add(mnuComprasMichelin);
                    //mnuSales.Items.Add(mnuTransferencias);
                    mnuaudi1.Items.Add(mnuaudi_1);
                    //mnuCxC2.Items.Add(mnuCxC2_1);

    */
                    break;


                case Menu.SALES:

                    BusinessItem mnuSales = new BusinessItem(null) { Name = "Modulos", Tag = "598", IsSelected = false };
                    this.Add(mnuSales);
                    //Classes
                    //BusinessItem mnuCompras = new BusinessItem(mnuInv) { Name = "Compras", Tag = "5099", IsSelected = false };
                    //BusinessItem mnuComprasMichelin = new BusinessItem(mnuInv) { Name = "Compras Michelin", Tag = "5100", IsSelected = false };

                    //BusinessItem mnuTransferencias = new BusinessItem(mnuInv) { Name = "Transferencias", Tag = "401", IsSelected = false };
                    BusinessItem mnuFact = new BusinessItem(mnuSales) { Name = "Facturas", Tag = "6500", IsSelected = false };
                    BusinessItem mnuFactOI = new BusinessItem(mnuSales) { Name = "Facturas Otros Ingresos", Tag = "6501", IsSelected = false };
                    BusinessItem mnuCoti = new BusinessItem(mnuSales) { Name = "Cotizacion", Tag = "680", IsSelected = false };
                    BusinessItem mnupedido = new BusinessItem(mnuSales) { Name = "Pedidos", Tag = "682", IsSelected = false };
                    BusinessItem mnuordenes = new BusinessItem(mnuSales) { Name = "Orden de Trabajo", Tag = "684", IsSelected = false };
                    BusinessItem mnuexis = new BusinessItem(mnuSales) { Name = "Existencias", Tag = "686", IsSelected = false };


                    //BusinessItem mnuAjustes = new BusinessItem(mnuInv) { Name = "Ajustes", Tag = "301", IsSelected = false };
                    //// BusinessItem mnuekteClass2 = new BusinessItem(mnuEktelesis) { Name = "CLT", Tag = "4000", IsSelected = false };
                    //mnuSales.Items.Add(mnuCompras);
                    //mnuSales.Items.Add(mnuComprasMichelin);
                    //mnuSales.Items.Add(mnuTransferencias);
                    mnuSales.Items.Add(mnuFact);
                    mnuSales.Items.Add(mnuFactOI);
                    mnuSales.Items.Add(mnuCoti);

                    mnuSales.Items.Add(mnupedido);
                    mnuSales.Items.Add(mnuordenes);
                    mnuSales.Items.Add(mnuexis);

                    //mnuSales.Items.Add(mnuAjustes);
                    ////BusinessItem mnuekteClass3 = new BusinessItem(mnuEktelesis) { Name = "Precios Refacciones", Tag = "4001", IsSelected = false };

                    //BusinessItem mnuSales2 = new BusinessItem(null) { Name = "Salidas", Tag = "5098", IsSelected = false };
                    //BusinessItem mnuTransferencias2 = new BusinessItem(mnuInv2) { Name = "Transferencias", Tag = "5099", IsSelected = false };
                    //BusinessItem mnuFacturas = new BusinessItem(mnuInv2) { Name = "Facturas", Tag = "5099", IsSelected = false };
                    //this.Add(mnuSales2);
                    //mnuSales2.Items.Add(mnuTransferencias2);
                    //mnuSales2.Items.Add(mnuFacturas);
                    break;


                case Menu.SISTEMAS:


                    //Root
                    BusinessItem mnuSistemas = new BusinessItem(null) { Name = "Sistemas", Tag = "100", IsSelected = false };
                    this.Add(mnuSistemas);
                    //Classes
                    BusinessItem mnuDepto1 = new BusinessItem(mnuSistemas) { Name = "Desarrollo    ", Tag = "101", IsSelected = false };
                    BusinessItem mnuDepto2 = new BusinessItem(mnuSistemas) { Name = "Redes y Telecomunicaciones", Tag = "102", IsSelected = false };

                    mnuSistemas.Items.Add(mnuDepto1);
                    mnuSistemas.Items.Add(mnuDepto2);

                    //Fixed Groups
                    //class 1
                    BusinessItem mnuSubDepto1_1 = new BusinessItem(mnuDepto1) { Name = "1", IsSelected = false };
                    BusinessItem mnuSubDepto1_2 = new BusinessItem(mnuDepto1) { Name = "2", Tag = "109", IsSelected = false };
                    mnuDepto1.Items.Add(mnuSubDepto1_1);
                    mnuDepto1.Items.Add(mnuSubDepto1_2);


                    BusinessItem mnuSubDepto2_1 = new BusinessItem(mnuDepto2) { Name = "3", IsSelected = false };
                    BusinessItem mnuSubDepto2_2 = new BusinessItem(mnuDepto2) { Name = "4", Tag = "109", IsSelected = false };
                    mnuDepto2.Items.Add(mnuSubDepto2_1);
                    mnuDepto2.Items.Add(mnuSubDepto2_2);
                    break;

                case Menu.GG:
                    //Root
                    BusinessItem mnuGG = new BusinessItem(null) { Name = "Inicio", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG);

                    //Classes
                    BusinessItem mnuGGDepto1 = new BusinessItem(mnuGG) { Name = "Empleados    ", Tag = "101", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Hidden };
                    BusinessItem mnuGGDepto2 = new BusinessItem(mnuGG) { Name = "Empresas", Tag = "102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Hidden };
                    BusinessItem mnuGGDepto3 = new BusinessItem(mnuGG) { Name = "Módulos", Tag = "800", IsSelected = false, IsEnabled = true };

                    mnuGG.Items.Add(mnuGGDepto1);
                    mnuGG.Items.Add(mnuGGDepto2);
                    mnuGG.Items.Add(mnuGGDepto3);


                    //Root
                    BusinessItem mnuGG_Tablero = new BusinessItem(null) { Name = "Tablero", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Tablero);

                    BusinessItem mnuGG_Orden = new BusinessItem(null) { Name = "Facturacion", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Orden);


                    BusinessItem mnuGG_Remision = new BusinessItem(null) { Name = "Vales", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Remision);


                    BusinessItem mnuGG_Modules_TableroNivel1_1 = new BusinessItem(mnuGG) { Name = "Compras", Tag = "011788", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Tablero.Items.Add(mnuGG_Modules_TableroNivel1_1);
                    BusinessItem mnuGG_Modules_TableroNivel1_2 = new BusinessItem(mnuGG) { Name = "Ajustes", Tag = "04422", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Tablero.Items.Add(mnuGG_Modules_TableroNivel1_2);
                    BusinessItem mnuGG_Modules_TableroNivel1_3 = new BusinessItem(mnuGG) { Name = "Transferencias", Tag = "04423", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Tablero.Items.Add(mnuGG_Modules_TableroNivel1_3);
                    BusinessItem mnuGG_Modules_TableroNivel1_4 = new BusinessItem(mnuGG) { Name = "Reportes Inventarios", Tag = "04447", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Tablero.Items.Add(mnuGG_Modules_TableroNivel1_4);
                    BusinessItem mnuGG_Modules_TableroNivel1_5 = new BusinessItem(mnuGG) { Name = "Cotizaciones", Tag = "0717", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Tablero.Items.Add(mnuGG_Modules_TableroNivel1_5);


                    BusinessItem mnuGG_Modules_Nivel1_1 = new BusinessItem(mnuGG) { Name = "OrdenPatio", Tag = "0117", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_1);
                    BusinessItem mnuGG_Modules_Nivel1_5 = new BusinessItem(mnuGG) { Name = "Precios", Tag = "01117", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_5);


                    BusinessItem mnuGG_Modules_Nivel1_12 = new BusinessItem(mnuGG) { Name = "OrdenMaterial", Tag = "01118", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_12);


                    BusinessItem mnuGG_Modules_Nivel1_11 = new BusinessItem(mnuGG) { Name = "PL Exi", Tag = "01336", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_11);
                    BusinessItem mnuGG_Modules_Nivel1_6 = new BusinessItem(mnuGG) { Name = "Ventas General", Tag = "07117", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_6);

                    BusinessItem mnuGG_Modules_Nivel1_13 = new BusinessItem(mnuGG) { Name = "PagareCentro", Tag = "071173", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_13);

                    BusinessItem mnuGG_Modules_Nivel1_14 = new BusinessItem(mnuGG) { Name = "Remisiones", Tag = "0711739", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_14);

                    BusinessItem mnuGG_Modules_Nivel1_7 = new BusinessItem(mnuGG) { Name = "Clientes Agregados", Tag = "08117", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_7);
                    BusinessItem mnuGG_Modules_Nivel1_8 = new BusinessItem(mnuGG) { Name = "Servicios", Tag = "9991", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_8);
                    BusinessItem mnuGG_Modules_Nivel1_9 = new BusinessItem(mnuGG) { Name = "Operarios", Tag = "0665", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_9);
                    BusinessItem mnuGG_Modules_Nivel1_10 = new BusinessItem(mnuGG) { Name = "PagareArellantas", Tag = "04444", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Orden.Items.Add(mnuGG_Modules_Nivel1_10);
               

                    BusinessItem mnuGG_Modules_Nivel1_2 = new BusinessItem(mnuGG) { Name = "Remision", Tag = "0119", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Remision.Items.Add(mnuGG_Modules_Nivel1_2);
                    BusinessItem mnuGG_Modules_Nivel1_3 = new BusinessItem(mnuGG) { Name = "Otros Ingresos", Tag = "0888", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Remision.Items.Add(mnuGG_Modules_Nivel1_3);
                    BusinessItem mnuGG_Modules_Nivel1_4 = new BusinessItem(mnuGG) { Name = "Pagares", Tag = "08888", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Remision.Items.Add(mnuGG_Modules_Nivel1_4);


                    //Root
                    BusinessItem mnuGG_Administracion = new BusinessItem(null) { Name = "Administración", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Administracion);

                    BusinessItem mnuGG_Catalogos_Nivel1 = new BusinessItem(mnuGG) { Name = "Catálogos (Inventariados)", Tag = "0101", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Administracion.Items.Add(mnuGG_Catalogos_Nivel1);


                    BusinessItem mnuGG_Catalogos_Nivel2 = new BusinessItem(mnuGGDepto1) { Name = "Llantas", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_Nivel1.Items.Add(mnuGG_Catalogos_Nivel2);

                    BusinessItem mnuGG_Catalogos_Nivel3 = new BusinessItem(mnuGGDepto1) { Name = "Rines", Tag = "951", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_Nivel1.Items.Add(mnuGG_Catalogos_Nivel3);

                    BusinessItem mnuGG_Catalogos_Nivel5 = new BusinessItem(mnuGGDepto1) { Name = "Cámaras y Corbatas", Tag = "952", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_Nivel1.Items.Add(mnuGG_Catalogos_Nivel5);

                    BusinessItem mnuGG_Catalogos_Nivel4 = new BusinessItem(mnuGGDepto1) { Name = "Refacciones", Tag = "952", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_Nivel1.Items.Add(mnuGG_Catalogos_Nivel4);

                    BusinessItem mnuGG_Catalogos_Nivel6 = new BusinessItem(mnuGGDepto1) { Name = "Insumos", Tag = "952", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_Nivel1.Items.Add(mnuGG_Catalogos_Nivel6);

                    BusinessItem mnuGG_Catalogos_Nivel8 = new BusinessItem(mnuGGDepto1) { Name = "Accesorios", Tag = "952", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_Nivel1.Items.Add(mnuGG_Catalogos_Nivel8);

                    BusinessItem mnuGG_Catalogos_Nivel9 = new BusinessItem(mnuGGDepto1) { Name = "Publicidad", Tag = "952", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_Nivel1.Items.Add(mnuGG_Catalogos_Nivel9);


                    BusinessItem mnuGG_Catalogos_NoInventariados = new BusinessItem(mnuGG) { Name = "Catálogos (No Inventariados)", Tag = "0101", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Administracion.Items.Add(mnuGG_Catalogos_NoInventariados);

                    BusinessItem mnuGG_Catalogos_Serv = new BusinessItem(mnuGGDepto1) { Name = "Servicios", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_NoInventariados.Items.Add(mnuGG_Catalogos_Serv);

                    BusinessItem mnuGG_Catalogos_Paq = new BusinessItem(mnuGGDepto1) { Name = "Paquetes", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_NoInventariados.Items.Add(mnuGG_Catalogos_Paq);

                    BusinessItem mnuGG_Catalogos_Bon = new BusinessItem(mnuGGDepto1) { Name = "Bonificaciones/Descuentos", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Catalogos_NoInventariados.Items.Add(mnuGG_Catalogos_Bon);

                    //Root
                    BusinessItem mnuGG_CxC = new BusinessItem(null) { Name = "Cuentas por Cobrar (CxC)", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_CxC);

                    BusinessItem mnuGG_CxC_NCR = new BusinessItem(mnuGG) { Name = "Notas de Crédito", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_CxC.Items.Add(mnuGG_CxC_NCR);


                    //Root
                    BusinessItem mnuGG_Finanzas = new BusinessItem(null) { Name = "Finanzas", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Finanzas);

                    //Root
                    BusinessItem mnuGG_Caja = new BusinessItem(null) { Name = "Caja", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Caja);


                    //Root
                    BusinessItem mnuGG_Ventas = new BusinessItem(null) { Name = "Ventas", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Ventas);

                    //Root
                    BusinessItem mnuGG_Compras = new BusinessItem(null) { Name = "Compras", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Compras);

                    BusinessItem mnuGG_Modulos_Nivel1_1 = new BusinessItem(mnuGG) { Name = "Compras", Tag = "0103", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Compras.Items.Add(mnuGG_Modulos_Nivel1_1);

                    BusinessItem mnuGG_Modulos_Nivel1_2 = new BusinessItem(mnuGG) { Name = "Transferencias", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Compras.Items.Add(mnuGG_Modulos_Nivel1_2);

                    BusinessItem mnuGG_Modulos_Nivel1_3 = new BusinessItem(mnuGG) { Name = "Ajustes", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Compras.Items.Add(mnuGG_Modulos_Nivel1_3);


                    //Root
                    BusinessItem mnuGG_Almacen = new BusinessItem(null) { Name = "Almacen", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Almacen);

                    //Root
                    BusinessItem mnuGG_Ektelesis = new BusinessItem(null) { Name = "Ektelesis", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Ektelesis);

                    //Root
                    BusinessItem mnuGG_OtrosIngresos = new BusinessItem(null) { Name = "Otros Ingresos", Tag = "00", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_OtrosIngresos);

                    BusinessItem mnuGG_Modulos_Nivel4_1 = new BusinessItem(mnuGG) { Name = "Facturas O.I", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_OtrosIngresos.Items.Add(mnuGG_Modulos_Nivel4_1);

                    BusinessItem mnuGG_Modulos_Nivel4_2 = new BusinessItem(mnuGG) { Name = "Devoluciones O.I", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_OtrosIngresos.Items.Add(mnuGG_Modulos_Nivel4_2);

                    //BusinessItem mnuGG_Catalogos = new BusinessItem(null) { Name = "Catálogos", Tag = "0100", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    //this.Add(mnuGG_Catalogos);



                    //NIVEL MENU 0
                    BusinessItem mnuGG_Moduloss = new BusinessItem(null) { Name = "Módulos", Tag = "0200", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    this.Add(mnuGG_Moduloss);

                    //NIVEL 1
                    BusinessItem mnuGG_Modulos_Nivel1 = new BusinessItem(mnuGG) { Name = "Inventarios", Tag = "0201", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Moduloss.Items.Add(mnuGG_Modulos_Nivel1);



                    //NIVEL 2
                    BusinessItem mnuGG_Modulos_Nivel2 = new BusinessItem(mnuGG) { Name = "Ventas", Tag = "0301", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Moduloss.Items.Add(mnuGG_Modulos_Nivel2);



                    //Classes
                    BusinessItem mnuMTODeptot = new BusinessItem(mnuGG) { Name = "Taller Guadiana    ", Tag = "951", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Moduloss.Items.Add(mnuMTODeptot);



                    //NIVEL 3
                    BusinessItem mnuGG_Modulos_Nivel3 = new BusinessItem(mnuGG) { Name = "Crédito y Cobranza", Tag = "0401", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Moduloss.Items.Add(mnuGG_Modulos_Nivel3);

                    BusinessItem mnuGG_Modulos_Nivel3_1 = new BusinessItem(mnuGG) { Name = "Notas de Crédito", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Modulos_Nivel3.Items.Add(mnuGG_Modulos_Nivel3_1);

                    //NIVEL 2
                    BusinessItem mnuGG_Modulos_Nivel4 = new BusinessItem(mnuGG) { Name = "Otros Ingresos", Tag = "0301", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Moduloss.Items.Add(mnuGG_Modulos_Nivel4);

                    //NIVEL 3
                    BusinessItem mnuGG_Modulos_NivelEdi = new BusinessItem(mnuGG) { Name = "EDI - Michelin", Tag = "0401", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Moduloss.Items.Add(mnuGG_Modulos_NivelEdi);

                    BusinessItem mnuGG_Modulos_Nivel3_EdiCat = new BusinessItem(mnuGG) { Name = "Catálogo de Clientes", Tag = "8012", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    mnuGG_Modulos_NivelEdi.Items.Add(mnuGG_Modulos_Nivel3_EdiCat);

                    ////NIVEL 2
                    //BusinessItem mnuGG_Modulos_Nivel4 = new BusinessItem(mnuGG) { Name = "Otros Ingresos", Tag = "0301", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    //mnuGG_Moduloss.Items.Add(mnuGG_Modulos_Nivel4);

                    //BusinessItem mnuGG_Modulos_Nivel1_1 = new BusinessItem(mnuGG) { Name = "Compras", Tag = "0102", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    //mnuGG_Modulos_Nivel1.Items.Add(mnuGG_Modulos_Nivel1_1);

                    //Fixed Groups
                    //class 1
                    BusinessItem mnuGGSubDepto1_1 = new BusinessItem(mnuGGDepto1) { Name = "Directorio", Tag = "206", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    BusinessItem mnuGGSubDepto1_2 = new BusinessItem(mnuGGDepto1) { Name = "2", Tag = "109", IsSelected = false };
                    BusinessItem mnuGGSubDepto1_3 = new BusinessItem(mnuGGDepto3) { Name = "Tickets", Tag = "802", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuGGSubDepto1_4 = new BusinessItem(mnuGGDepto1) { Name = "Tickets Administracion", Tag = "212", IsSelected = false };
                    BusinessItem mnuGGSubDepto1_5 = new BusinessItem(mnuGGDepto1) { Name = "Facturas", Tag = "805", IsSelected = false, IsEnabled = false };
                    BusinessItem mnuGGSubDepto1_6 = new BusinessItem(mnuGGDepto1) { Name = "Clientes", Tag = "8010", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuGGSubDepto1_7 = new BusinessItem(mnuGGDepto1) { Name = "Historiales", Tag = "7002", IsSelected = false, IsEnabled = true };

                    BusinessItem mnuGGSubDepto1_8 = new BusinessItem(mnuGGDepto1) { Name = "Refacciones", Tag = "7004", IsSelected = false, IsEnabled = true };

                    BusinessItem mnuGGSubDepto1_9 = new BusinessItem(null) { Name = "Catálogos", Tag = "100", IsSelected = false, IsEnabled = true };
                    //this.Add(mnuInventory);

                    mnuGGDepto1.Items.Add(mnuGGSubDepto1_1);
                    //mnuGGDepto1.Items.Add(mnuGGSubDepto1_2);
                    mnuGGDepto3.Items.Add(mnuGGSubDepto1_3);
                    mnuGGDepto3.Items.Add(mnuGGSubDepto1_5);
                    //  mnuGGDepto1.Items.Add(mnuGGSubDepto1_4);
                    mnuGGDepto3.Items.Add(mnuGGSubDepto1_6);
                    mnuGGDepto3.Items.Add(mnuGGSubDepto1_7);
                    // mnuGGDepto3.Items.Add(mnuGGSubDepto1_8);
                    mnuGGDepto3.Items.Add(mnuGGSubDepto1_9);


                    BusinessItem mnuInvClass1 = new BusinessItem(mnuGGSubDepto1_9) { Name = "Llantas, Cámaras y Corbatas    ", Tag = "101", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass2 = new BusinessItem(mnuGGSubDepto1_9) { Name = "Refacciones", Tag = "102", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass3 = new BusinessItem(mnuGGSubDepto1_9) { Name = "Rines", Tag = "103", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass4 = new BusinessItem(mnuGGSubDepto1_9) { Name = "Servicios", Tag = "104", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass5 = new BusinessItem(mnuGGSubDepto1_9) { Name = "Miscelaneos", Tag = "105", IsSelected = false, IsEnabled = true };
                    // mnuGGSubDepto1_9.Items.Add(mnuInvClass1);
                    mnuGGSubDepto1_9.Items.Add(mnuInvClass2);
                    //mnuGGSubDepto1_9.Items.Add(mnuInvClass3);
                    //mnuGGSubDepto1_9.Items.Add(mnuInvClass4);
                    //mnuGGSubDepto1_9.Items.Add(mnuInvClass5);
                    //mnuGGSubDepto1_9.i                   
                    //Fixed Groups
                    //class 1
                    BusinessItem mnuInvGroup01 = new BusinessItem(mnuInvClass1) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass1Lines = new BusinessItem(mnuInvClass1) { Name = "Lineas", Tag = "109", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass1Brands = new BusinessItem(mnuInvClass1) { Name = "Marcas", Tag = "119", IsSelected = false };
                    mnuInvClass1.Items.Add(mnuInvGroup01);

                    BusinessItem mnuInvGroup1 = new BusinessItem(mnuInvGroup01) { Name = "Auto", Tag = "70", IsSelected = false };
                    BusinessItem mnuInvGroup2 = new BusinessItem(mnuInvGroup01) { Name = "Camioneta", Tag = "153", IsSelected = false };
                    BusinessItem mnuInvGroup3 = new BusinessItem(mnuInvGroup01) { Name = "Camion", Tag = "154", IsSelected = false };

                    mnuInvGroup01.Items.Add(mnuInvGroup1);
                    mnuInvGroup01.Items.Add(mnuInvGroup2);
                    mnuInvGroup01.Items.Add(mnuInvGroup3);

                    //mnuInvGroup02.Items.Add(mnuInv2Group3);
                    //mnuInvClass1.Items.Add(mnuClass1Lines);
                    //mnuInvClass1.Items.Add(mnuClass1Brands);
                    //class 2
                    BusinessItem mnuInvGroup02 = new BusinessItem(mnuInvClass2) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass2Lines = new BusinessItem(mnuInvClass2) { Name = "Lineas", Tag = "110", IsSelected = false };
                    BusinessItem mnuClass2Brands = new BusinessItem(mnuInvClass2) { Name = "Marcas", Tag = "120", IsSelected = false };
                    mnuInvClass2.Items.Add(mnuInvGroup02);
                    //mnuInvClass2.Items.Add(mnuClass2Lines);
                    //mnuInvClass2.Items.Add(mnuClass2Brands);

                    //class 3
                    BusinessItem mnuInvGroup03 = new BusinessItem(mnuInvClass3) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass3Lines = new BusinessItem(mnuInvClass3) { Name = "Lineas", Tag = "111", IsSelected = false };
                    BusinessItem mnuInvClass3Brands = new BusinessItem(mnuInvClass3) { Name = "Marcas", Tag = "121", IsSelected = false };
                    mnuInvClass3.Items.Add(mnuInvGroup03);
                    //mnuInvClass3.Items.Add(mnuInvClass3Lines);
                    //mnuInvClass3.Items.Add(mnuInvClass3Brands);

                    //class 4
                    BusinessItem mnuInvGroup04 = new BusinessItem(mnuInvClass4) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInvClass4Lines = new BusinessItem(mnuInvClass4) { Name = "Lineas", Tag = "112", IsSelected = false };
                    BusinessItem mnuInvClass4Brands = new BusinessItem(mnuInvClass4) { Name = "Marcas", Tag = "122", IsSelected = false };
                    mnuInvClass4.Items.Add(mnuInvGroup04);
                    //mnuInvClass4.Items.Add(mnuInvClass4Lines);
                    //mnuInvClass4.Items.Add(mnuInvClass4Brands);

                    BusinessItem mnuInv4Group1 = new BusinessItem(mnuInvGroup04) { Name = "Servicios", Tag = "80", IsSelected = false };
                    BusinessItem mnuInv4Group2 = new BusinessItem(mnuInvGroup04) { Name = "Insumos", Tag = "85", IsSelected = false };
                    //  BusinessItem mnuInvGroup3 = new BusinessItem(mnuInvGroup04) { Name = "Camion", Tag = "154", IsSelected = false };

                    mnuInvGroup04.Items.Add(mnuInv4Group1);
                    mnuInvGroup04.Items.Add(mnuInv4Group2);
                    //mnuInvGroup01.Items.Add(mnuInvGroup3);

                    //class 5
                    BusinessItem mnuInvGroup05 = new BusinessItem(mnuInvClass5) { Name = "Grupos", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuClass5Lines = new BusinessItem(mnuInvClass5) { Name = "Lineas", Tag = "113", IsSelected = false };
                    BusinessItem mnuClass5Brands = new BusinessItem(mnuInvClass5) { Name = "Marcas", Tag = "123", IsSelected = false };
                    mnuInvClass5.Items.Add(mnuInvGroup05);
                    //mnuInvClass5.Items.Add(mnuClass5Lines);
                    //mnuInvClass5.Items.Add(mnuClass5Brands);

                    //Variable Groups
                    //Class 2 Groups
                    BusinessItem mnuInv2Group4 = new BusinessItem(mnuInvGroup02) { Name = "Acumuladores", Tag = "7005", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInv2Group1 = new BusinessItem(mnuInvGroup02) { Name = "Amortiguadores", Tag = "7006", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInv2Group2 = new BusinessItem(mnuInvGroup02) { Name = "Miscelaneos", Tag = "7007", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuInv2Group3 = new BusinessItem(mnuInvGroup02) { Name = "Refacciones", Tag = "7008", IsSelected = false, IsEnabled = true };
                    //BusinessItem mnuInv2Group33 = new BusinessItem(mnuInvGroup02) { Name = "4-Refacciones", Tag = "155", IsSelected = false };
                    //  BusinessItem mnuInv2Group3 = new BusinessItem(mnuInvGroup02) { Name = "Refacciones", Tag = "154", IsSelected = false };


                    mnuInvGroup02.Items.Add(mnuInv2Group4);
                    mnuInvGroup02.Items.Add(mnuInv2Group1);
                    mnuInvGroup02.Items.Add(mnuInv2Group2);
                    mnuInvGroup02.Items.Add(mnuInv2Group3);
                    // mnuInvGroup02.Items.Add(mnuInv2Grou








                    BusinessItem mnuGGSubDepto2_1 = new BusinessItem(mnuGGDepto2) { Name = "LRG - Llantas y Rines", Tag = "204", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    BusinessItem mnuGGSubDepto2_2 = new BusinessItem(mnuGGDepto2) { Name = "CLT - Centro Llantero", Tag = "109", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Collapsed };
                    mnuGGDepto2.Items.Add(mnuGGSubDepto2_1);
                    mnuGGDepto2.Items.Add(mnuGGSubDepto2_2);


                    BusinessItem mnuLRG = new BusinessItem(null) { Name = "LRG - Ll y R G", Tag = "2000", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };



                    //  this.Add(mnuLRG);



                    //ARELLANTAS

                    BusinessItem mnuB20 = new BusinessItem(null) { Name = "ARELLANTAS - B20", Tag = "20", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };
                    //  this.Add(mnuB20);

                    BusinessItem mnuB20_Modules = new BusinessItem(mnuGG) { Name = "Ventas", Tag = "200", IsSelected = false, IsEnabled = true };
                    mnuB20.Items.Add(mnuB20_Modules);

                    BusinessItem mnuB20_Sales = new BusinessItem(mnuB20_Modules) { Name = "Orden Servicio", Tag = "201", IsSelected = false, IsEnabled = true };
                    mnuB20_Modules.Items.Add(mnuB20_Sales);

                    break;



                case Menu.LRG:
                    //Root
                    //BusinessItem mnuLRG = new BusinessItem(null) { Name = "DataBooks", Tag = "100", IsSelected = false, IsEnabled = true };

                    //  BusinessItem mnuLRG = new BusinessItem(null) { Name = "LRG - Ll y R G", Tag = "2000", IsSelected = false, IsEnabled = true, ImagePath = "/GGGC.Admin;component/Resources/Images/folder.png" };



                    //  this.Add(mnuLRG);
                    //Classes
                    // BusinessItem mnuLRGDepto1 = new BusinessItem(mnuLRG) { Name = "TCAR", Tag = "1501", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    // BusinessItem mnuLRGDepto2 = new BusinessItem(mnuLRG) { Name = "Telemarketing", Tag = "1502", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    // //BusinessItem mnuLRGDepto2 = new BusinessItem(mnuLRG) { Name = "Empresas", Tag = "102", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Hidden };
                    // //BusinessItem mnuLRGDepto3 = new BusinessItem(mnuLRG) { Name = "Módulos", Tag = "800", IsSelected = false, IsEnabled = true };

                    // mnuLRG.Items.Add(mnuLRGDepto1);
                    //mnuLRG.Items.Add(mnuLRGDepto2);
                    //mnuLRG.Items.Add(mnuLRGDepto3);

                    //Fixed Groups
                    //class 1
                    //BusinessItem mnuLRGSubDepto1_1 = new BusinessItem(mnuLRGDepto1) { Name = "Directorio", Tag = "206", IsSelected = false, Visibility = (int)System.Windows.Visibility.Hidden };
                    //BusinessItem mnuLRGSubDepto1_2 = new BusinessItem(mnuLRGDepto1) { Name = "2", Tag = "109", IsSelected = false };
                    //BusinessItem mnuLRGSubDepto1_3 = new BusinessItem(mnuLRGDepto3) { Name = "Tickets", Tag = "801", IsSelected = false, IsEnabled = true };
                    //BusinessItem mnuLRGSubDepto1_4 = new BusinessItem(mnuLRGDepto1) { Name = "Tickets Administracion", Tag = "212", IsSelected = false };
                    //mnuLRGDepto1.Items.Add(mnuLRGSubDepto1_1);
                    ////mnuLRGDepto1.Items.Add(mnuLRGSubDepto1_2);
                    //mnuLRGDepto3.Items.Add(mnuLRGSubDepto1_3);
                    ////  mnuLRGDepto1.Items.Add(mnuLRGSubDepto1_4);                  
                    //BusinessItem mnuLRGSubDepto2_1 = new BusinessItem(mnuLRGDepto2) { Name = "LRG - Llantas y Rines", Tag = "204", IsSelected = false, IsEnabled = true };
                    //BusinessItem mnuLRGSubDepto2_2 = new BusinessItem(mnuLRGDepto2) { Name = "CLT - Centro Llantero", Tag = "109", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Collapsed };
                    //mnuLRGDepto2.Items.Add(mnuLRGSubDepto2_1);
                    //mnuLRGDepto2.Items.Add(mnuLRGSubDepto2_2);
                    break;


                case Menu.CLT:
                    //Root
                    //BusinessItem mnuLRG = new BusinessItem(null) { Name = "DataBooks", Tag = "100", IsSelected = false, IsEnabled = true };

                    BusinessItem mnuCLT = new BusinessItem(null) { Name = "CLT - C LL T", Tag = "200", IsSelected = false, IsEnabled = true };



                    this.Add(mnuCLT);
                    //Classes
                    //BusinessItem mnuLRGDepto1 = new BusinessItem(mnuLRG) { Name = "TCAR", Tag = "1501", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    //BusinessItem mnuLRGDepto2 = new BusinessItem(mnuLRG) { Name = "Telemarketing", Tag = "1502", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    ////BusinessItem mnuLRGDepto2 = new BusinessItem(mnuLRG) { Name = "Empresas", Tag = "102", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Hidden };
                    ////BusinessItem mnuLRGDepto3 = new BusinessItem(mnuLRG) { Name = "Módulos", Tag = "800", IsSelected = false, IsEnabled = true };

                    //mnuLRG.Items.Add(mnuLRGDepto1);
                    //mnuLRG.Items.Add(mnuLRGDepto2);
                    //mnuLRG.Items.Add(mnuLRGDepto3);

                    //Fixed Groups
                    //class 1
                    //BusinessItem mnuLRGSubDepto1_1 = new BusinessItem(mnuLRGDepto1) { Name = "Directorio", Tag = "206", IsSelected = false, Visibility = (int)System.Windows.Visibility.Hidden };
                    //BusinessItem mnuLRGSubDepto1_2 = new BusinessItem(mnuLRGDepto1) { Name = "2", Tag = "109", IsSelected = false };
                    //BusinessItem mnuLRGSubDepto1_3 = new BusinessItem(mnuLRGDepto3) { Name = "Tickets", Tag = "801", IsSelected = false, IsEnabled = true };
                    //BusinessItem mnuLRGSubDepto1_4 = new BusinessItem(mnuLRGDepto1) { Name = "Tickets Administracion", Tag = "212", IsSelected = false };
                    //mnuLRGDepto1.Items.Add(mnuLRGSubDepto1_1);
                    ////mnuLRGDepto1.Items.Add(mnuLRGSubDepto1_2);
                    //mnuLRGDepto3.Items.Add(mnuLRGSubDepto1_3);
                    ////  mnuLRGDepto1.Items.Add(mnuLRGSubDepto1_4);                  
                    //BusinessItem mnuLRGSubDepto2_1 = new BusinessItem(mnuLRGDepto2) { Name = "LRG - Llantas y Rines", Tag = "204", IsSelected = false, IsEnabled = true };
                    //BusinessItem mnuLRGSubDepto2_2 = new BusinessItem(mnuLRGDepto2) { Name = "CLT - Centro Llantero", Tag = "109", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Collapsed };
                    //mnuLRGDepto2.Items.Add(mnuLRGSubDepto2_1);
                    //mnuLRGDepto2.Items.Add(mnuLRGSubDepto2_2);

                    break;

                case Menu.HOME:
                    //Root
                    BusinessItem mnuHOME = new BusinessItem(null) { Name = "Guillermo", Tag = "100", IsSelected = false };
                    //this.Add(mnuHOME);
                    //Classes
                    BusinessItem mnuHOMEDepto1 = new BusinessItem(mnuHOME) { Name = "Configuracion    ", Tag = "201", IsSelected = false };
                    BusinessItem mnuHOMEDepto2 = new BusinessItem(mnuHOME) { Name = "Empresas", Tag = "102", IsSelected = false };
                    //  mnuHOME.Items.Add(mnuHOMEDepto1);
                    //  mnuHOME.Items.Add(mnuHOMEDepto2);
                    //Fixed Groups
                    //class 1
                    BusinessItem mnuHOMESubDepto1_1 = new BusinessItem(mnuHOMEDepto1) { Name = "Contactos", IsSelected = false };
                    BusinessItem mnuHOMESubDepto1_2 = new BusinessItem(mnuHOMEDepto1) { Name = "2", Tag = "109", IsSelected = false };
                    //mnuHOMEDepto1.Items.Add(mnuHOMESubDepto1_1);
                    //mnuGGDepto1.Items.Add(mnuGGSubDepto1_2);
                    BusinessItem mnuHOMESubDepto2_1 = new BusinessItem(mnuHOMEDepto2) { Name = "LRG - Llantas y Rines", IsSelected = false };
                    BusinessItem mnuHOMESubDepto2_2 = new BusinessItem(mnuHOMEDepto2) { Name = "CLT - Centro Llantero", Tag = "109", IsSelected = false };
                    mnuHOMEDepto2.Items.Add(mnuHOMESubDepto2_1);
                    mnuHOMEDepto2.Items.Add(mnuHOMESubDepto2_2);
                    break;


                case Menu.MTO:


                    //Root
                    BusinessItem mnuMTO = new BusinessItem(null) { Name = "Módulos", Tag = "100", IsSelected = false, IsEnabled = true };
                    this.Add(mnuMTO);
                    //Classes
                    BusinessItem mnuMTODepto1 = new BusinessItem(mnuMTO) { Name = "Taller Guadiana    ", Tag = "951", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Visible };
                    BusinessItem mnuMTODepto2 = new BusinessItem(mnuMTO) { Name = "Salidas", Tag = "52", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Visible };
                    BusinessItem mnuMTODepto3 = new BusinessItem(mnuMTO) { Name = "Consultas", Tag = "53", IsSelected = false, IsEnabled = true };

                    mnuMTO.Items.Add(mnuMTODepto1);
                    //mnuMTO.Items.Add(mnuMTODepto2);
                    //mnuMTO.Items.Add(mnuMTODepto3);

                    //Fixed Groups
                    //class 1
                    BusinessItem mnuMTOSubDepto1_1 = new BusinessItem(mnuMTODepto1) { Name = " Guadiana", Tag = "2951", IsSelected = false, Visibility = (int)System.Windows.Visibility.Hidden };
                    //  BusinessItem mnuMTOSubDepto1_2 = new BusinessItem(mnuMTODepto1) { Name = "2", Tag = "109", IsSelected = false };
                    // BusinessItem mnuMTOSubDepto1_3 = new BusinessItem(mnuMTODepto2) { Name = "Órdenes de Servicio", Tag = "801", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuMTOSubDepto1_4 = new BusinessItem(mnuMTODepto3) { Name = "Existencias", Tag = "212", IsSelected = false };
                    //BusinessItem mnuMTOSubDepto1_5 = new BusinessItem(mnuMTODepto1) { Name = "Invoice", Tag = "805", IsSelected = false, IsEnabled = true };
                    //  mnuMTODepto1.Items.Add(mnuMTOSubDepto1_1);
                    //mnuMTODepto1.Items.Add(mnuMTOSubDepto1_2);
                    // mnuMTODepto3.Items.Add(mnuMTOSubDepto1_3);
                    //  mnuMTODepto3.Items.Add(mnuMTOSubDepto1_4);
                    //  mnuMTODepto1.Items.Add(mnuMTOSubDepto1_4);

                    BusinessItem mnuMTOSubDepto2_1 = new BusinessItem(mnuMTODepto2) { Name = "Órdenes de Servicio", Tag = "204", IsSelected = false, IsEnabled = true };
                    // BusinessItem mnuMTOSubDepto2_2 = new BusinessItem(mnuMTODepto2) { Name = "CLT - Centro Llantero", Tag = "109", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Collapsed };
                    // mnuMTODepto2.Items.Add(mnuMTOSubDepto2_1);
                    //mnuMTODepto2.Items.Add(mnuMTOSubDepto2_2);
                    break;


                case Menu.BUENFIN:


                    //Root
                    BusinessItem mnuBUENFIN = new BusinessItem(null) { Name = "Módulos", Tag = "100", IsSelected = false, IsEnabled = true };
                    this.Add(mnuBUENFIN);
                    //Classes
                    BusinessItem mnuBUENFINDepto1 = new BusinessItem(mnuBUENFIN) { Name = "Vales de Entrega    ", Tag = "1951", IsSelected = false, IsEnabled = true, Visibility = (int)System.Windows.Visibility.Hidden };
                    BusinessItem mnuBUENFINDepto2 = new BusinessItem(mnuBUENFIN) { Name = "Salidas", Tag = "52", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Hidden };
                    BusinessItem mnuBUENFINDepto3 = new BusinessItem(mnuBUENFIN) { Name = "Consultas", Tag = "53", IsSelected = false, IsEnabled = true };

                    mnuBUENFIN.Items.Add(mnuBUENFINDepto1);
                    //mnuBUENFIN.Items.Add(mnuBUENFINDepto2);
                    //mnuBUENFIN.Items.Add(mnuBUENFINDepto3);

                    //Fixed Groups
                    //class 1
                    BusinessItem mnuBUENFINSubDepto1_1 = new BusinessItem(mnuBUENFINDepto1) { Name = " Guadiana", Tag = "2951", IsSelected = false, Visibility = (int)System.Windows.Visibility.Hidden };
                    //  BusinessItem mnuBUENFINSubDepto1_2 = new BusinessItem(mnuBUENFINDepto1) { Name = "2", Tag = "109", IsSelected = false };
                    // BusinessItem mnuBUENFINSubDepto1_3 = new BusinessItem(mnuBUENFINDepto2) { Name = "Órdenes de Servicio", Tag = "801", IsSelected = false, IsEnabled = true };
                    BusinessItem mnuBUENFINSubDepto1_4 = new BusinessItem(mnuBUENFINDepto3) { Name = "Existencias", Tag = "212", IsSelected = false };
                    //BusinessItem mnuBUENFINSubDepto1_5 = new BusinessItem(mnuBUENFINDepto1) { Name = "Invoice", Tag = "805", IsSelected = false, IsEnabled = true };
                    //  mnuBUENFINDepto1.Items.Add(mnuBUENFINSubDepto1_1);
                    //mnuBUENFINDepto1.Items.Add(mnuBUENFINSubDepto1_2);
                    // mnuBUENFINDepto3.Items.Add(mnuBUENFINSubDepto1_3);
                    //  mnuBUENFINDepto3.Items.Add(mnuBUENFINSubDepto1_4);
                    //  mnuBUENFINDepto1.Items.Add(mnuBUENFINSubDepto1_4);

                    BusinessItem mnuBUENFINSubDepto2_1 = new BusinessItem(mnuBUENFINDepto2) { Name = "Órdenes de Servicio", Tag = "204", IsSelected = false, IsEnabled = true };
                    // BusinessItem mnuMTOSubDepto2_2 = new BusinessItem(mnuMTODepto2) { Name = "CLT - Centro Llantero", Tag = "109", IsSelected = false, IsEnabled = false, Visibility = (int)System.Windows.Visibility.Collapsed };
                    // mnuMTODepto2.Items.Add(mnuMTOSubDepto2_1);
                    //mnuMTODepto2.Items.Add(mnuMTOSubDepto2_2);
                    break;

            }


            //for (int i = 0; i < 100; i++)
            //{
            //    BusinessItem bi1 = new BusinessItem(null)
            //        {
            //            Name = i.ToString(),
            //            IsSelected = false
            //        };

            //    for (int j = 0; j < 5; j++)
            //    {
            //        BusinessItem bi2 = new BusinessItem(bi1)
            //            {
            //                Name = bi1.Name + "." + j,
            //                IsSelected = false
            //            };

            //        for (int k = 0; k < 5; k++)
            //        {
            //            BusinessItem bi3 = new BusinessItem(bi2)
            //                {
            //                    Name = bi2.Name + "." + k,
            //                    IsSelected = false
            //                };
            //            bi2.Items.Add(bi3);

            //            for (int l = 0; l < 5; l++)
            //            {
            //                BusinessItem bi4 = new BusinessItem(bi3)
            //                    {
            //                        Name = bi3.Name + "." + l,
            //                        IsSelected = false
            //                    };
            //                bi3.Items.Add(bi4);
            //            }
            //        }

            //        bi1.Items.Add(bi2);
            //    }
            //    this.Add(bi1);
            //}
        }


        public BusinessItem GetItemByName(string name)
        {
            BusinessItem item = null;
            string[] parts = name == null ? new string[] { "0" } : name.Split('.');
            foreach (string s in parts)
            {
                try
                {
                    int index = Int32.Parse(s);

                    if (item == null)
                    {
                        item = (index >= 0 && index < this.Items.Count) ? this[index] : null;
                    }
                    else
                    {
                        item = (index >= 0 && index < item.Items.Count) ? item.Items[index] : null;
                    }
                }
                catch
                {
                    item = null;
                    break;
                }
            }

            return item;
        }


    }
}
