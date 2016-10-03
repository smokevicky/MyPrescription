using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    public class ForgotPasswordDAL
    {
        public UserModel FlagAsForgotPassword(string email)
        {
            UserModel userModelReturnObject = new UserModel();
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("FlagAsForgotPassword", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    //invalid = 0 : email invalid
                    userModelReturnObject.statusCode = StatusCode.invalid;
                }
                else
                {
                    while (reader.Read())
                    {
                        //valid = 1 : User exists and flagged  
                        userModelReturnObject.statusCode = StatusCode.valid;
                        userModelReturnObject.token = reader.GetValue(0).ToString();
                        userModelReturnObject.firstName = (string)reader.GetValue(1);
                        userModelReturnObject.lastName = (string)reader.GetValue(2);
                    }
                }
            }
            catch (Exception ex)
            {
                //error = 2 : Error
                userModelReturnObject.statusCode = StatusCode.error;
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