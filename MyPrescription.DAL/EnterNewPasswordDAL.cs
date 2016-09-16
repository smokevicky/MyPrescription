using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    public class EnterNewPasswordDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModelObject"></param>
        /// <returns></returns>
        public UserModel ResetPassword(UserModel userModelObject)
        {
            UserModel userModelReturnObject = new UserModel();
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("ResetPassword", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@token", userModelObject.token);
                cmd.Parameters.AddWithValue("@password", userModelObject.password);
                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    userModelReturnObject.statusCode = StatusCode.invalid;                         //0 : Not Resetted
                }
                else
                {
                    while (reader.Read())
                    {
                        userModelReturnObject.statusCode = StatusCode.valid;                       //1 : Reset Successful
                        userModelReturnObject.userId = Convert.ToInt32(reader.GetValue(0));
                        userModelReturnObject.email = (string)reader.GetValue(1);
                        userModelReturnObject.password = (string)reader.GetValue(2);
                        userModelReturnObject.firstName = (string)reader.GetValue(3);
                        userModelReturnObject.lastName = (string)reader.GetValue(4);
                    }
                }
            }

            catch (Exception ex)
            {
                userModelReturnObject.statusCode = StatusCode.error;                                 //isValid = 2 : Error
                userModelReturnObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return userModelReturnObject;
        }
    }
}
