/********************************************************
** FileName:   FileBL.cs
** Author:     Jyoti Prakash Jena
** Date:       --
** Purpose:    Handle BL calls for files
********************************************************/

using MyPrescription.DAL;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.IO;
using System.Web;

namespace MyPrescription.BL
{
    /// <summary>
    /// Handle BL calls for files
    /// </summary>
    public class FileBL
    {
        /// <summary>
        /// Calls DAL AddNewFile
        /// </summary>
        /// <param name="fileModelObject"></param>
        /// <returns>FileId</returns>
        public static int AddNewFile(FileModel fileModelObject)
        {
            return FileDAL.AddNewFile(fileModelObject);
        }

        /// <summary>
        /// Calls FileDAL to Get VaultId and FileName By fileId
        /// </summary>
        /// <param name="fileModelObject">Accepts parameters of type FileModel</param>
        /// <returns>FileModel Object</returns>
        public static FileModel GetVaultIdFileName(FileModel fileModelObject)
        {
            return FileDAL.GetVaultIdFileName(fileModelObject);
        }

        /// <summary>
        /// Saves files Physically
        /// </summary>
        /// <param name="files">Accepts List of files to be saved</param>
        /// <param name="userId">Accepts userId as Int</param>
        /// <param name="vaultId">Accepts vaultId as Int</param>
        /// <returns></returns>
        public static bool SaveFilePhysically(HttpFileCollection files, int userId, int vaultId)
        {
            try
            {
                string fileName, folderPath, vaultPath, fileNamePath;
                int fileId, fileCountFlag = 0;
                HttpPostedFile file;
                FileModel fileModelObject;

                for (int i = 0; i < files.Count; i++)
                {
                    file = files[i];
                    fileName = System.IO.Path.GetFileName(file.FileName);

                    fileModelObject = new FileModel();
                    fileModelObject.fileName = fileName;
                    fileModelObject.userId = userId;
                    fileModelObject.vaultId = vaultId;

                    //calling AddNewFile BL and inserting file details into db
                    fileId = FileBL.AddNewFile(fileModelObject);

                    folderPath = HttpContext.Current.Server.MapPath(Common.GetUploadDirectory() + userId);
                    vaultPath = HttpContext.Current.Server.MapPath(Common.GetUploadDirectory() + userId + "/" + vaultId);
                    fileNamePath = HttpContext.Current.Server.MapPath(Common.GetUploadDirectory() + userId + "/" + vaultId + "/" + fileId + "-" + fileName);

                    //if directory doesn't exists, it creates the directory
                    Directory.CreateDirectory(folderPath);
                    Directory.CreateDirectory(vaultPath);

                    file.SaveAs(fileNamePath);

                    fileCountFlag++;
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.SaveFilePhysicallyBL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = userId;
                ErrorLogBL.LogError(errorLogModelObject);
            }
            return false;
        }
    }
}
