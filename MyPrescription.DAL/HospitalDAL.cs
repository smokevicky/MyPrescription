using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    public class HospitalDAL
    {
        /// <summary>
        /// Adds one new hospital details to the db
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>bool</returns>
        public bool AddNewHospital(HospitalModel hospitalModelObject)
        {            
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("AddNewHospital", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hospitalId", hospitalModelObject.hospitalId);
                cmd.Parameters.AddWithValue("@name", hospitalModelObject.name);
                cmd.Parameters.AddWithValue("@address", hospitalModelObject.address);
                cmd.Parameters.AddWithValue("@phone", hospitalModelObject.phoneNo);
                cmd.Parameters.AddWithValue("@phone2", hospitalModelObject.phoneNo2);
                cmd.Parameters.AddWithValue("@email", hospitalModelObject.email);
                cmd.Parameters.AddWithValue("@userId", hospitalModelObject.userId);
                cmd.Parameters.AddWithValue("@status", hospitalModelObject.status);
                cmd.Parameters.AddWithValue("@isPrimary", hospitalModelObject.isPrimary);

                sqlUtilityObject.con.Open();

                cmd.ExecuteNonQuery();

                return true;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.AddNewHospitalDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = hospitalModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                return false;
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }
        }        

        /// <summary>
        /// Gets hospital details from db corresponding to specific user
        /// </summary>
        /// <param name="hospitalRequestModelObject">Passing page start, page size</param>
        /// <returns></returns>
        public HospitalResponseModel GetHospitalDetails(HospitalRequestModel hospitalRequestModelObject)
        {
            HospitalResponseModel hospitalResponseModelObject = new HospitalResponseModel();
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("GetHospitalDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pageStart", hospitalRequestModelObject.pageStart);
                cmd.Parameters.AddWithValue("@pageSize", hospitalRequestModelObject.pageSize);
                cmd.Parameters.AddWithValue("@userId", hospitalRequestModelObject.userId);
                cmd.Parameters.AddWithValue("@orderBy", hospitalRequestModelObject.sortBy);
                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    hospitalResponseModelObject.statusCode = StatusCode.invalid;        //invalid = 0 : No Hospitals
                }
                else
                {
                    hospitalResponseModelObject.statusCode = SignInStatusCode.valid;                      //valid = 1 : Hospitals exist
                    var hospitalList = new List<HospitalModel>();
                    
                    //to fetch list of hospital details
                    while (reader.Read())
                    {
                        HospitalModel hospitalModelObject = new HospitalModel();
                        hospitalModelObject.row = Convert.ToInt32(reader["row"].ToString());
                        hospitalModelObject.hospitalId = Convert.ToInt32((reader["HospitalId"].ToString()));
                        hospitalModelObject.name = reader["Name"].ToString();
                        hospitalModelObject.address = reader["Address"].ToString();
                        hospitalModelObject.phoneNo = reader["Phone"].ToString();
                        hospitalModelObject.isPrimary = Convert.ToInt32(reader["isPrimary"].ToString());

                        hospitalList.Add(hospitalModelObject);
                    }
                    hospitalResponseModelObject.hospitalModelList = hospitalList;

                    //to fetch rowCount
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            hospitalResponseModelObject.rowCount = Convert.ToInt32(reader["TotalRows"].ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                hospitalResponseModelObject.statusCode = StatusCode.error;        //invalid = 2 : Error
                hospitalResponseModelObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return hospitalResponseModelObject;
        }

        /// <summary>
        /// Deletes hospital record by searching hospitalId
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <returns>True or False</returns>
        public bool DeleteHospital(HospitalModel hospitalModelObject)
        {
            bool returnVal;

            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("DeleteHospital", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hospitalId", hospitalModelObject.hospitalId);
                cmd.Parameters.AddWithValue("@userId", hospitalModelObject.userId);

                sqlUtilityObject.con.Open();

                cmd.ExecuteNonQuery();

                returnVal = true;   
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.DeleteHospitalDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = hospitalModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                returnVal = false;
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return returnVal;
        }

        /// <summary>
        /// Gets single hospital details for Editing or Viewing
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <returns>HospitalModel Object</returns>
        public HospitalModel GetSingleHospitalDetails(HospitalModel hospitalModelObject)
        {
            HospitalModel hospitalReturnModelObject = new HospitalModel();

            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("GetSingleHospitalDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hospitalId", hospitalModelObject.hospitalId);
                cmd.Parameters.AddWithValue("@userId", hospitalModelObject.userId);

                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    hospitalReturnModelObject.statusCode = StatusCode.invalid;        //invalid = 0 : No Hospitals
                }
                else
                {
                    hospitalReturnModelObject.statusCode = StatusCode.valid;           //valid = 1 : Hospital exists

                    while (reader.Read())
                    {                                                                        
                        hospitalReturnModelObject.name = reader["Name"].ToString();
                        hospitalReturnModelObject.address = reader["Address"].ToString();
                        hospitalReturnModelObject.phoneNo = reader["Phone"].ToString();
                        hospitalReturnModelObject.phoneNo2 = reader["Phone2"].ToString();
                        hospitalReturnModelObject.email = reader["Email"].ToString();
                        hospitalReturnModelObject.createdOn = DateTime.Parse(reader["CreatedOn"].ToString()).ToLongDateString();
                        hospitalReturnModelObject.updatedOn = DateTime.Parse(reader["UpdatedOn"].ToString()).ToLongDateString();
                        hospitalReturnModelObject.isPrimary = Convert.ToInt32(reader["isPrimary"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                hospitalReturnModelObject.statusCode = StatusCode.error;                  //invalid = 2 : Error
                hospitalReturnModelObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return hospitalReturnModelObject;
        }

        /// <summary>
        /// Updates hospital details after editing
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true or false</returns>
        public bool UpdateHospitalDetails(HospitalModel hospitalModelObject)
        {
            bool returnVal;

            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("UpdateHospitalDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hospitalId", hospitalModelObject.hospitalId);
                cmd.Parameters.AddWithValue("@name", hospitalModelObject.name);
                cmd.Parameters.AddWithValue("@address", hospitalModelObject.address);
                cmd.Parameters.AddWithValue("@phoneNo", hospitalModelObject.phoneNo);
                cmd.Parameters.AddWithValue("@phoneNo2", hospitalModelObject.phoneNo2);
                cmd.Parameters.AddWithValue("@email", hospitalModelObject.email);
                cmd.Parameters.AddWithValue("@isPrimary", hospitalModelObject.isPrimary);
                cmd.Parameters.AddWithValue("@userId", hospitalModelObject.userId);

                sqlUtilityObject.con.Open();

                cmd.ExecuteNonQuery();

                returnVal = true;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.UpdateHospitalDetailsDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = hospitalModelObject.userId;
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
