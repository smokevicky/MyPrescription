using Ionic.Zip;
using MyPrescription.BL;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.IO;
using System.Web;

namespace MyPrescription.Account
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId;
            Int32.TryParse(Session["userId"].ToString(), out userId);

            int vaultId, fileId;

            //for downloading zip files
            if (Request.QueryString["vaultId"] != null)
            {
                Int32.TryParse(Request.QueryString["vaultId"].ToString(), out vaultId);

                string vaultPath, zipFilePath;

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;

                    vaultPath = HttpContext.Current.Server.MapPath( Common.GetUploadDirectory() + userId + "/" + vaultId + "/");
                    zipFilePath = HttpContext.Current.Server.MapPath(Common.GetUploadDirectory() + userId + "/" + vaultId + "-" + DateTime.Now.Ticks.ToString() + ".zip");

                    zip.AddDirectory(vaultPath, "Files");

                    zip.Save(zipFilePath);
                }

                string dateTimeTick = DateTime.Now.Ticks.ToString();
                string zipName = String.Format("Vault_Files-" + dateTimeTick + ".zip");

                Response.Clear();
                Response.BufferOutput = false;

                

                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName + ";");

                Response.TransmitFile(zipFilePath);

                File.Delete(zipFilePath);

                //string tabCloseScript = "<script>window.close();</script>";
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", tabCloseScript);

                Response.End();
            }

            //for downloading single files
            else if (Request.QueryString["fileId"] != null)
            {
                Int32.TryParse(Request.QueryString["fileId"].ToString(), out fileId);

                string filePath, fileName;

                //get vaultId and fileName from db by fileId
                FileModel fileModelObject = new FileModel();
                fileModelObject.userId = userId;
                fileModelObject.fileId = fileId;

                fileModelObject = FileBL.GetVaultIdFileName(fileModelObject);

                fileName = fileId + "-" + fileModelObject.fileName;

                filePath = Server.MapPath(Common.GetUploadDirectory() + userId + "/" + fileModelObject.vaultId + "/" + fileName);

                Response.Clear();
                Response.BufferOutput = false;

                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ";");

                Response.TransmitFile(filePath);
                Response.End();

                string tabCloseScript = "<script>window.close();</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", tabCloseScript);
            }
        }
    }
}