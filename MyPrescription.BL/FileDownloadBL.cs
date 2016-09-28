using System;
using System.Web;

namespace MyPrescription.BL
{
    public class FileDownloadBL
    {
        public static bool DownloadFileByPath(string filePath)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                string zipName = String.Format("Vault_Files.zip");
                HttpContext.Current.Response.ContentType = "application/zip";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName + ";");

                HttpContext.Current.Response.TransmitFile(filePath);
                HttpContext.Current.Response.End();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
