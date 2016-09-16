using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    public class VerifyDAL
    {
        public int CheckActivationStatus(string token)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();
            int returnVal = ActivationStatusCode.initial;

            try
            {
                Guid guidToken = new Guid();
                Guid.TryParse(token, out guidToken);
                SqlCommand cmd = new SqlCommand("CheckActivationStatus", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@token", guidToken);
                sqlUtilityObject.con.Open();

                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    returnVal = ActivationStatusCode.invalidToken;                                      //0 : Token is invalid
                }
                else
                {
                    returnVal = Convert.ToInt32(result);
                    if (returnVal == 0)                                                                 //0 : false return from database
                    {
                        returnVal = ActivationStatusCode.valid;                                         //1 : Token exists, Account not activated
                    }
                    else if (returnVal == 1)                                                            //1 : true return from database
                    {
                        returnVal = ActivationStatusCode.alreadyActivated;                              //2 : Token exists, Account activated
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.CheckActivationStatusDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                ErrorLogDAL.LogError(errorLogModelObject);

                returnVal = ActivationStatusCode.error;                                                 //3 : Error            
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }
            return returnVal;
        }

        public UserModel ActivateAccount(string token)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();
            UserModel userModelReturnObject = new UserModel();

            try
            {
                SqlCommand cmd = new SqlCommand("ActivateAccount", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@token", token);
                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    userModelReturnObject.statusCode = ActivationStatusCode.invalidToken;            //statusCode = 0 : invalid token
                }
                else
                {
                    while (reader.Read())
                    {
                        userModelReturnObject.statusCode = ActivationStatusCode.valid;               //statusCode = 1 : activated
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
                userModelReturnObject.statusCode = ActivationStatusCode.error;                       //statusCode = 3 : Error
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
