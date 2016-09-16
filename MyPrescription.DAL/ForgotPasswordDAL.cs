using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPrescription.Models;
using MyPrescription.Util;
using System.Data.SqlClient;
using System.Data;

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
                    userModelReturnObject.statusCode = StatusCode.invalid;                       //invalid = 0 : email invalid
                }
                else
                {
                    while (reader.Read())
                    {
                        userModelReturnObject.statusCode = StatusCode.valid;                      //valid = 1 : User exists and flagged  
                        userModelReturnObject.token = reader.GetValue(0).ToString();
                        userModelReturnObject.firstName = (string)reader.GetValue(1);
                        userModelReturnObject.lastName = (string)reader.GetValue(2);                        
                    }
                }
            }
            catch (Exception ex)
            {
                userModelReturnObject.statusCode = StatusCode.error;                                //error = 2 : Error
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