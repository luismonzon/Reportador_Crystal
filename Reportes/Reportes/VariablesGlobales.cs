using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
namespace Reportes
{

    public  static class VariablesGlobales
    {
        public static String Usuario;
        public static String Password;
        public static String Rol;
        public static void ShowReport(String url)
        {
            try
            {
                ReportDocument myReportDocument = new ReportDocument();
                myReportDocument.Load(@url);

                Reportes reportes = new Reportes();

                myReportDocument.SetDatabaseLogon(VariablesGlobales.Usuario, VariablesGlobales.Password);
                reportes.crystalReportViewer1.ReportSource = myReportDocument;
                reportes.crystalReportViewer1.DisplayToolbar = true;
                reportes.Show();
            }
             catch (LogOnException engEx)
           {
           }
           catch (DataSourceException engEx)
           {
           }
           catch (EngineException engEx)
           {
           }
        }
    }
}
