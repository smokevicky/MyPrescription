using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPrescription.Util;
using MyPrescription.Models;
using System.Data.SqlClient;
using System.Data;

namespace MyPrescription.DAL
{
    public class SignUpDAL
    {
        public UserModel SignUp(UserModel userModelObject)
        {
            UserModel userModelReturnObject = new UserModel();
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("SignUp", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userModelObject.userId);
                cmd.Parameters.AddWithValue("@fname", userModelObject.firstName);
                cmd.Parameters.AddWithValue("@lname", userModelObject.lastName);
                cmd.Parameters.AddWithValue("@email", userModelObject.email);
                cmd.Parameters.AddWithValue("@password", userModelObject.password);
                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    userModelReturnObject.statusCode = StatusCode.invalid;                         //isValid = 0 : Not successfully logged in
                }
                else
                {
                    while (reader.Read())
                    {
                        userModelReturnObject.statusCode = StatusCode.valid;                       //isValid = 1 : User exists
                        userModelReturnObject.userId = userModelObject.userId;
                        userModelReturnObject.token = reader.GetValue(0).ToString();
                        userModelReturnObject.isActive = (bool)reader.GetValue(1);
                        userModelReturnObject.email = (string)reader.GetValue(2);
                        userModelReturnObject.status = (string)reader.GetValue(3);
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
