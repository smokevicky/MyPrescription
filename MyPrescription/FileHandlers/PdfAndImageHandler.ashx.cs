using MyPrescription.BL;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPrescription.FileHandlers
{
    /// <summary>
    /// Summary description for PdfHandler
    /// </summary>
    public class PdfAndImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int userId;
            Int32.TryParse(context.User.Identity.Name, out userId);

            string currentUrl = HttpContext.Current.Request.Url.AbsolutePath;
            int fileId;
            Int32.TryParse(currentUrl.Split('=', '.')[1], out fileId);

            string filePath, fileName;

            //get vaultId and fileName from db by fileId
            FileModel fileModelObject = new FileModel();
            fileModelObject.userId = userId;
            fileModelObject.fileId = fileId;

            fileModelObject = FileBL.GetVaultIdFileName(fileModelObject);

            fileName = fileId + "-" + fileModelObject.fileName;

            filePath = context.Server.MapPath(Common.GetUploadDirectory() + userId + "/" + fileModelObject.vaultId + "/" + fileName);

            context.Response.Clear();

            string contentType = MimeMapping.GetMimeMapping(filePath);
            context.Response.ContentType = contentType;
            context.Response.WriteFile(filePath);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}