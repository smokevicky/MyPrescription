using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace MyPrescription.DAL
{
    public class ErrorLogDAL
    {
        /// <summary>
        /// Logs errors into db
        /// </summary>
        /// <param name="errorLogModelObject">Accepts parameters of type ErrorLogModel</param>
        public static void LogError(ErrorLogModel errorLogModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();

            using (SqlConnection connection = new SqlUtility().con)
            {
                try
                {
                    SqlCommand command = new SqlCommand("LogError", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@userId", errorLogModelObject.userId));
                    command.Parameters.Add(new SqlParameter("@errorCode", errorLogModelObject.errorCode));
                    command.Parameters.Add(new SqlParameter("@errorMessage", errorLogModelObject.errorMessage));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    string path = HttpContext.Current.Server.MapPath("~/ErrorLog/ErrorLog.txt");
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/ErrorLog"));
                    string errorMessage = DateTime.Now.ToString() + "--- Unable to connect to MyPrescription Database---Message : " + ex.ToString();
                    using (StreamWriter writer = File.AppendText(path))
                    {
                        writer.WriteLine(errorMessage);
                        writer.Close();
                    }
                }
            }
        }
    }
}
