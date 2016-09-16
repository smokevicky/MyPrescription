

using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace MyPrescription.API.Controller
{
    /// <summary>
    /// Handles api requests for user controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UserController : ApiController
    {
        /// <summary>
        /// Determines whether the specified email is available.
        /// </summary>
        /// <param name="stringValue">The Email-Id.</param>
        /// <returns>
        ///   <c>true</c> if the specified string value is available; otherwise, <c>false</c>.
        /// </returns>
        [HttpGet]
        public bool isAvailable(string stringValue)
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
                    return false;
                }
                else
                {
                    return true;
                }
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIisAvailable, ex.ToString(), User.Identity.Name);
            }
            return false;
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
        public string CheckEmailFromToken(string stringValue)
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
                    return result.ToString();
                }
                else
                {
                    return "false";
                }
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APICheckEmailFromToken, ex.ToString(), User.Identity.Name);
            }
            return "false";
        }

        /// <summary>
        /// Gets the badge count.
        /// </summary>
        /// <returns></returns>s
        [Authorize]
        [HttpPost]
        public CountModel GetBadgeCount()
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
            return countModelReturnObject;
        }
    }
}
