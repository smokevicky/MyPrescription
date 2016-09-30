using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    public class SignInDAL
    {

        public UserModel SignIn(UserModel userModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();

            UserModel userModelReturnObject = new UserModel();

            //if (userModelObject.email.Equals("admin@myprescription.com") && userModelObject.password.Equals("mindfire"))
            //{
            //    userModelReturnObject.statusCode = SignInStatusCode.valid;
            //    userModelReturnObject.userId = 000000000;
            //    userModelReturnObject.email = "admin@myprescription.com";
            //    userModelReturnObject.isActive = true;

            //    return userModelReturnObject;
            //}

            try
            {
                SqlCommand cmd = new SqlCommand("LogIn", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", userModelObject.email);
                cmd.Parameters.AddWithValue("@password", userModelObject.password);
                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    userModelReturnObject.statusCode = SignInStatusCode.invalid;                       //invalid = 0 : wrong credentials
                }
                else
                {
                    while (reader.Read())
                    {
                        userModelReturnObject.statusCode = SignInStatusCode.valid;                      //valid = 1 : User exists
                        userModelReturnObject.userId = Convert.ToInt32((reader["UserId"].ToString()));
                        userModelReturnObject.email = reader["EMail"].ToString();
                        userModelReturnObject.isActive = (bool)reader["isActive"];
                        userModelReturnObject.firstName = reader["FName"].ToString();
                        userModelReturnObject.lastName = reader["LName"].ToString();
                        userModelReturnObject.phone = reader["Phone"].ToString();
                        userModelReturnObject.token = reader["Token"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                userModelReturnObject.statusCode = SignInStatusCode.error;                                //error = 2 : Error
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
