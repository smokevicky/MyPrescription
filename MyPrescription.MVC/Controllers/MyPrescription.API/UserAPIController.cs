/********************************************************
** FileName:    UserAPIController.cs
** Author:      Jyoti Prakash Jena
** Date:        28.9.2016
** Purpose:     Handles all the api call requests related to user
********************************************************/

using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MyPrescription.MVC.Controllers.MyPrescription.API
{
    public class UserAPIController : Controller
    {
        /// <summary>
        /// Determines whether the specified email is available.
        /// </summary>
        /// <param name="stringValue">The email.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IsAvailable(string stringValue)
        {
            try
            {
                string email = stringValue;
                SqlUtility sqlUtilityObject = new SqlUtility();

                SqlCommand cmd = new SqlCommand("SELECT UserId FROM UserMaster WHERE EMail = @email ");
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@email";
                param.Value = email;
                cmd.Parameters.Add(param);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlUtilityObject.con;
                sqlUtilityObject.con.Open();
                Object result = cmd.ExecuteScalar();
                sqlUtilityObject.con.Close();

                if (result != null)
                {
                    return Content(ActionResultStatusCode.False);
                }
                return Content(ActionResultStatusCode.True);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIisAvailable, ex.ToString(), User.Identity.Name);
                return Content(ActionResultStatusCode.False);
            }
        }

        /// <summary>
        /// Checks the status from token.
        /// </summary>
        /// <param name="stringValue">The token value.</param>
        /// <returns></returns>
        [HttpGet]
        public string CheckStatusFromToken(string stringValue)
        {
            try
            {
                SqlUtility sqlUtilityObject = new SqlUtility();

                Guid token = new Guid(stringValue);
                SqlCommand cmd = new SqlCommand("SELECT Status FROM UserMaster WHERE Token = @token ");
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@token";
                param.Value = token;
                cmd.Parameters.Add(param);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlUtilityObject.con;
                sqlUtilityObject.con.Open();
                Object result = cmd.ExecuteScalar();
                sqlUtilityObject.con.Close();

                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return "false";
                }
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APICheckStatusFromToken, ex.ToString(), User.Identity.Name);
            }
            return "false";
        }

        /// <summary>
        /// Checks the email from token.
        /// </summary>
        /// <param name="stringValue">The token value.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckEmailFromToken(string stringValue)
        {
            try
            {
                Guid token = new Guid(stringValue);
                SqlUtility sqlUtilityObject = new SqlUtility();

                SqlCommand cmd = new SqlCommand("SELECT EMail FROM UserMaster WHERE Token = @token ");
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@token";
                param.Value = token;
                cmd.Parameters.Add(param);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlUtilityObject.con;
                sqlUtilityObject.con.Open();
                Object result = cmd.ExecuteScalar();
                sqlUtilityObject.con.Close();

                if (result != null)
                {
                    return Content(result.ToString());
                }
                return Content(ActionResultStatusCode.False);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APICheckEmailFromToken, ex.ToString(), User.Identity.Name);
            }
            return Content(ActionResultStatusCode.False);
        }

        /// <summary>
        /// Gets the badge count.
        /// </summary>
        /// <returns></returns>s
        [Authorize]
        [HttpPost]
        public JsonResult GetBadgeCount()
        {
            int userId;
            Int32.TryParse(User.Identity.Name, out userId);

            CountModel countModelReturnObject = new CountModel();

            try
            {
                countModelReturnObject = BadgeBL.GetBadgeCount(userId);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIGetBadgeCount, ex.ToString(), User.Identity.Name);
            }
            return Json(countModelReturnObject);
        }
    }
}
