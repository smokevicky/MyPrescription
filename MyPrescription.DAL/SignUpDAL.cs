/********************************************************
** FileName: SignUpDAL.cs
** Author:   Jyoti Prakash Jena
** Date:     -
** Purpose:  Adds a New User to the db
********************************************************/

using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    /// <summary>
    /// Signs up a new user
    /// </summary>
    public class SignUpDAL
    {
        /// <summary>
        /// Signs up a new user.
        /// </summary>
        /// <param name="userModelObject">The user model object.</param>
        /// <returns></returns>
        public UserModel SignUp(UserModel userModelObject)
        {
            var userModelReturnObject = new UserModel();
            var sqlUtilityObject = new SqlUtility();

            try
            {
                var cmd = new SqlCommand("SignUp", sqlUtilityObject.con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userId", userModelObject.userId);
                cmd.Parameters.AddWithValue("@fname", userModelObject.firstName);
                cmd.Parameters.AddWithValue("@lname", userModelObject.lastName);
                cmd.Parameters.AddWithValue("@email", userModelObject.email);
                cmd.Parameters.AddWithValue("@password", userModelObject.password);
                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    //isValid = 0 : Not successfully logged in
                    userModelReturnObject.statusCode = StatusCode.invalid;
                }
                else
                {
                    while (reader.Read())
                    {
                        //isValid = 1 : User exists
                        userModelReturnObject.statusCode = StatusCode.valid;
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
                //isValid = 2 : Error
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
