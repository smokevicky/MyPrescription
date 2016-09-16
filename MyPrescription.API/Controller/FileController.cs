using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Web.Http;

namespace MyPrescription.API.Controller
{
    public class FileController : ApiController
    {
        /// <summary>
        /// Handles API request to Add a new file
        /// </summary>
        /// <param name="fileModelObject"></param>
        /// <returns>FileId</returns>
        [HttpPost]
        [Authorize]
        public int AddNewFile(FileModel fileModelObject)
        {
            int returnVal = -1;

            int userId;
            Int32.TryParse(User.Identity.Name, out userId);
            fileModelObject.userId = userId;

            try
            {
                returnVal = FileBL.AddNewFile(fileModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIAddNewFile, ex.ToString(), userId);
            }

            return returnVal;
        }
    }
}
