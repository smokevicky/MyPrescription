using MyPrescription.DAL;
using MyPrescription.Models;
using System;
using System.Web;

namespace MyPrescription.Error
{
    public class ErrorLog
    {
        /// <summary>
        /// LogsErrors into the database
        /// </summary>
        /// <param name="errorCode">Accepts string Error Code</param>
        /// <param name="errorMessage">Accepts string Error Message</param>
        /*public static void LogError(string errorCode, string errorMessage)
        {
            ErrorLogModel errObject = new ErrorLogModel();
            errObject.errorCode = errorCode;
            errObject.errorMessage = errorMessage;

            ErrorLogBL.LogError(errObject);
            HttpContext.Current.Response.Redirect("~/Error/Error.aspx", false);
        }*/

        /// <summary>
        /// LogsErrors into the database
        /// </summary>
        /// <param name="errorCode">Accepts string Error Code</param>
        /// <param name="errorMessage">Accepts string Error Message</param>
        /// <param name="userIdString">Accepts string UserId</param>
        public static void LogError(string errorCode, string errorMessage, string userIdString = "")
        {
            ErrorLogModel errObject = new ErrorLogModel();
            errObject.errorCode = errorCode;
            errObject.errorMessage = errorMessage;

            int userId;
            Int32.TryParse(userIdString, out userId);
            errObject.userId = userId;

            ErrorLogDAL.LogError(errObject);
            HttpContext.Current.Response.Redirect("~/Error/Error.aspx", false);
        }

        /// <summary>
        /// LogsErrors into the database
        /// </summary>
        /// <param name="errorCode">Accepts string Error Code</param>
        /// <param name="errorMessage">Accepts string Error Message</param>
        /// <param name="userIdString">Accepts int UserId</param>
        public static void LogError(string errorCode, string errorMessage, int userId)
        {
            ErrorLogModel errObject = new ErrorLogModel();
            errObject.errorCode = errorCode;
            errObject.errorMessage = errorMessage;
            errObject.userId = userId;

            ErrorLogDAL.LogError(errObject);
            HttpContext.Current.Response.Redirect("~/Error/Error.aspx", false);
        }
    }
}
