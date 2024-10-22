using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iConstruye.Helpers;
using SAPbobsCOM;
using SAPbouiCOM;


namespace iConstruye.Methods
{
    internal class Nominas
    {
        public static SAPbouiCOM.Application SBO_Application = null;
        public static SAPbobsCOM.Company oCompany = null;

        public Nominas()
        {
            Class_Initialize();

            SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);
            SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
        }

        private void Class_Initialize()
        {
            try
            {
                Conexion.SetApplication(out SBO_Application);

                if (!(Conexion.SetConnectionContext(ref SBO_Application, out oCompany) == 0))
                {
                    LibreriaLog.AddLog("error Conexion: SetConnectionContext");
                    SBO_Application.MessageBox("Failed setting a connection to DI API", 1, "Ok", "", "");
                    System.Environment.Exit(0);
                }

                if (!(Conexion.ConnectToCompany(oCompany) == 0))
                {
                    LibreriaLog.AddLog("Error Conexion: ConnectToCompany");
                    SBO_Application.MessageBox("Failed connecting to the company's Data Base", 1, "Ok", "", "");
                    System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                LibreriaLog.AddLog("Error Class_Initialize : " + ex.Message);
            }
        }

        private void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                switch (pVal.FormTypeEx)
                {
                    case "392": // Formulario de Pagos Efectuados
                        if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED && pVal.ItemUID == "1" && pVal.BeforeAction == false)
                        {
                            RegistrarNominaDePago(FormUID); 
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LibreriaLog.AddLog("Error SBO_Application_ItemEvent: " + ex.Message);
            }
        }

        private void SBO_Application_AppEvent(BoAppEventTypes EventType)
        {
            try
            {
                switch (EventType)
                {
                    case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                        oCompany.Disconnect();
                        System.Environment.Exit(0);

                        break;

                    case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                        oCompany.Disconnect();
                        System.Environment.Exit(0);

                        break;

                    case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                        oCompany.Disconnect();
                        System.Environment.Exit(0);

                        break;

                    case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:

                        break;
                }
            }
            catch (Exception ex)
            {
                LibreriaLog.AddLog("Error SBO_Application_AppEvent: " + ex.Message);
            }

        }

        private void RegistrarNominaDePago(string FormUID)
        {
            try
            {
                SAPbouiCOM.Form oForm = SBO_Application.Forms.Item(FormUID);

                SAPbouiCOM.DBDataSource oDBDataSource = oForm.DataSources.DBDataSources.Item("OVPM");

                string proveedor = oDBDataSource.GetValue("CardCode", 0).Trim();
                string nombreProveedor = oDBDataSource.GetValue("CardName", 0).Trim();
                string montoTotal = oDBDataSource.GetValue("DocTotal", 0).Trim();
                string fechaPago = oDBDataSource.GetValue("DocDate", 0).Trim();

                string mensaje = $"Pago registrado: Proveedor: {proveedor} - {nombreProveedor}, Monto: {montoTotal}, Fecha: {fechaPago}";
            }
            catch (Exception ex)
            {
                LibreriaLog.AddLog("Error al registrar nómina de pago: " + ex.Message);
            }
        }

    }
}
