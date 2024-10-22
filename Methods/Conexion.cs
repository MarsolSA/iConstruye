using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iConstruye.Helpers;

namespace iConstruye.Methods
{
    internal class Conexion
    {
        SAPbobsCOM.Company oCompany = null;

        public static void SetApplication(out SAPbouiCOM.Application SBO_Application)
        {
            SAPbouiCOM.SboGuiApi SboGuiApi = null;
            string sConnectionString = null;

            try
            {
                SboGuiApi = new SAPbouiCOM.SboGuiApi();

                sConnectionString = Environment.GetCommandLineArgs()[1];
                SboGuiApi.Connect(sConnectionString);
                SBO_Application = SboGuiApi.GetApplication(-1);
            }
            catch (Exception ex)
            {
                LibreriaLog.AddLog("Error SetApplication:" + ex.Message);
                SBO_Application = null;
            }
        }

        public static int SetConnectionContext(ref SAPbouiCOM.Application SBO_Application, out SAPbobsCOM.Company oCompany)
        {
            int setConnectionContextReturn = 0;
            string sCookie = null;
            string sConnectionContext = null;

            oCompany = new SAPbobsCOM.Company();
            sCookie = oCompany.GetContextCookie();
            sConnectionContext = SBO_Application.Company.GetConnectionContext(sCookie);

            if (oCompany.Connected == true)
            {
                oCompany.Disconnect();
            }

            setConnectionContextReturn = oCompany.SetSboLoginContext(sConnectionContext);

            return setConnectionContextReturn;
        }

        public static int ConnectToCompany(SAPbobsCOM.Company oCompany)
        {
            int connectToCompanyReturn = 0;

            connectToCompanyReturn = oCompany.Connect();

            return connectToCompanyReturn;
        }
    }
}
