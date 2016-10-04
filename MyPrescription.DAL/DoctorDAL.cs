using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    public class DoctorDAL
    {
        /// <summary>
        /// Adds a new doctor details to db
        /// </summary>
        /// <param name="doctorModelObject">Accepts object of type DoctorModel</param>
        /// <returns>true/false</returns>
        public static bool AddNewDoctor(DoctorModel doctorModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("AddNewDoctor", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doctorId", doctorModelObject.doctorId);
                cmd.Parameters.AddWithValue("@name", doctorModelObject.name);
                cmd.Parameters.AddWithValue("@address", doctorModelObject.address);
                cmd.Parameters.AddWithValue("@phone", doctorModelObject.phoneNo);
                cmd.Parameters.AddWithValue("@phone2", doctorModelObject.phoneNo2);
                cmd.Parameters.AddWithValue("@email", doctorModelObject.email);
                cmd.Parameters.AddWithValue("@hospitalId", doctorModelObject.hospitalId);
                cmd.Parameters.AddWithValue("@userId", doctorModelObject.userId);
                cmd.Parameters.AddWithValue("@isPrimary", doctorModelObject.isPrimary);

                sqlUtilityObject.con.Open();

                cmd.ExecuteNonQuery();

                return true;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.AddNewDoctorDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = doctorModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                return false;
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }
        }

        /// <summary>
        /// Gets doctor details from db corresponding to specific user
        /// </summary>
        /// <param name="userId">Accepts UserId to populate doctors</param>
        /// <returns></returns>
        public static DoctorResponseModel GetDoctorDetails(string userId)
        {
            DoctorResponseModel doctorResponseModelObject = new DoctorResponseModel();
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("GetDoctorDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);

                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    //invalid = 0 : No Hospitals
                    doctorResponseModelObject.statusCode = StatusCode.invalid;
                }
                else
                {
                    //valid = 1 : Hospitals exist
                    doctorResponseModelObject.statusCode = StatusCode.valid;
                    var doctorList = new List<DoctorModel>();

                    //to fetch list of hospital details
                    while (reader.Read())
                    {
                        DoctorModel doctorModelObject = new DoctorModel();
                        doctorModelObject.statusCode = StatusCode.valid;
                        doctorModelObject.row = Convert.ToInt32(reader["row"].ToString());
                        doctorModelObject.doctorId = Convert.ToInt32((reader["DoctorId"]));
                        doctorModelObject.name = reader["Name"].ToString();
                        doctorModelObject.address = reader["Address"].ToString();
                        doctorModelObject.phoneNo = reader["Phone"].ToString();
                        doctorModelObject.isPrimary = Convert.ToInt32(reader["isPrimary"].ToString());

                        doctorList.Add(doctorModelObject);
                    }
                    doctorResponseModelObject.doctorModelList = doctorList;

                    //to fetch rowCount
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            doctorResponseModelObject.rowCount = Convert.ToInt32(reader["TotalRows"].ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                //invalid = 2 : Error
                doctorResponseModelObject.statusCode = StatusCode.error;
                doctorResponseModelObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return doctorResponseModelObject;
        }

        /// <summary>
        /// Deletes a doctor details form db
        /// </summary>
        /// <param name="doctorModelObject">Accepts parameter of type DoctorModel</param>
        /// <returns>true/false</returns>
        public static bool DeleteDoctor(DoctorModel doctorModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("DeleteDoctor", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doctorId", doctorModelObject.doctorId);
                cmd.Parameters.AddWithValue("@userId", doctorModelObject.userId);

                sqlUtilityObject.con.Open();

                cmd.ExecuteNonQuery();

                return true;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.DeleteDoctorDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = doctorModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                return false;
            }

            finally
            {
                sqlUtilityObject.con.Close();
            }
        }

        /// <summary>
        /// Gets single doctor details for Editing or Viewing
        /// </summary>
        /// <param name="doctorModelObject"></param>
        /// <returns>DoctorModel</returns>
        public static DoctorModel GetSingleDoctorDetails(DoctorModel doctorModelObject)
        {
            DoctorModel doctorModelReturnObject = new DoctorModel();

            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("GetSingleDoctorDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doctorId", doctorModelObject.doctorId);
                cmd.Parameters.AddWithValue("@userId", doctorModelObject.userId);

                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    doctorModelReturnObject.statusCode = StatusCode.invalid;        //invalid = 0 : No Hospitals
                }
                else
                {
                    doctorModelReturnObject.statusCode = StatusCode.valid;           //valid = 1 : Hospital exists

                    while (reader.Read())
                    {
                        doctorModelReturnObject.name = reader["Name"].ToString();
                        doctorModelReturnObject.address = reader["Address"].ToString();
                        doctorModelReturnObject.phoneNo = reader["Phone"].ToString();
                        doctorModelReturnObject.phoneNo2 = reader["Phone2"].ToString();
                        doctorModelReturnObject.email = reader["Email"].ToString();
                        doctorModelReturnObject.hospitalId = Convert.ToInt32(reader["HospitalId"].ToString());
                        doctorModelReturnObject.createdOn = reader["CreatedOn"].ToString();
                        doctorModelReturnObject.updatedOn = reader["UpdatedOn"].ToString();
                        doctorModelReturnObject.isPrimary = Convert.ToInt32(reader["isPrimary"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                doctorModelReturnObject.statusCode = StatusCode.error;                  //invalid = 2 : Error
                doctorModelReturnObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return doctorModelReturnObject;
        }

        /// <summary>
        /// Updates doctor details after editing
        /// </summary>
        /// <param name="doctorModelObject"></param>
        /// <returns>true or false</returns>
        public static bool UpdateDoctorDetails(DoctorModel doctorModelObject)
        {
            bool returnVal;

            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("UpdateDoctorDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doctorId", doctorModelObject.doctorId);
                cmd.Parameters.AddWithValue("@name", doctorModelObject.name);
                cmd.Parameters.AddWithValue("@address", doctorModelObject.address);
                cmd.Parameters.AddWithValue("@phoneNo", doctorModelObject.phoneNo);
                cmd.Parameters.AddWithValue("@phoneNo2", doctorModelObject.phoneNo2);
                cmd.Parameters.AddWithValue("@email", doctorModelObject.email);
                cmd.Parameters.AddWithValue("@isPrimary", doctorModelObject.isPrimary);
                cmd.Parameters.AddWithValue("@hospitalId", doctorModelObject.hospitalId);
                cmd.Parameters.AddWithValue("@userId", doctorModelObject.userId);

                sqlUtilityObject.con.Open();

                cmd.ExecuteNonQuery();

                returnVal = true;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.UpdateDoctorDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = doctorModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                returnVal = false;
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }
            return returnVal;
        }
    }
}
