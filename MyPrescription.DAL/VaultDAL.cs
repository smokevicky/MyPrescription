using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    /// <summary>
    /// DAL Level class for Vault
    /// </summary>
    public class VaultDAL
    {
        /// <summary>
        /// Adds a vault to the db
        /// </summary>
        /// <param name="vaultModelObject">Accepts object of type VaultModel</param>
        /// <returns>VaultdId</returns>
        public static int AddNewVault(VaultModel vaultModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("AddNewVault", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vaultName", vaultModelObject.vaultName);
                cmd.Parameters.AddWithValue("@userId", vaultModelObject.userId);
                cmd.Parameters.AddWithValue("@hospitalId", vaultModelObject.hospitalId);
                cmd.Parameters.AddWithValue("@doctorId", vaultModelObject.doctorId);
                cmd.Parameters.AddWithValue("@date", vaultModelObject.date);
                cmd.Parameters.AddWithValue("@recordId", vaultModelObject.recordId);

                sqlUtilityObject.con.Open();

                int vaultId = (int)cmd.ExecuteScalar();

                return vaultId;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.AddNewVaultDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = vaultModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                return -1;
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }
        }

        /// <summary>
        /// Reads list of vaults from db
        /// </summary>
        /// <param name="vaultRequestModelObject">Accepts parameter of type VaultRequestModel</param>
        /// <returns>ResponseModel</returns>
        public static ResponseModel GetVaultDetails(VaultRequestModel vaultRequestModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();
            ResponseModel responseModelObject = new ResponseModel();

            try
            {
                SqlCommand cmd = new SqlCommand("GetVaultDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", vaultRequestModelObject.userId);
                cmd.Parameters.AddWithValue("@pageStart", vaultRequestModelObject.pageStart);
                cmd.Parameters.AddWithValue("@pageSize", vaultRequestModelObject.pageSize);

                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    responseModelObject.statusCode = StatusCode.invalid;
                }
                else
                {
                    responseModelObject.statusCode = StatusCode.valid;
                    var vaultList = new List<VaultModel>();

                    //to fetch list of vaults
                    while (reader.Read())
                    {
                        VaultModel vaultModelObject = new VaultModel();

                        int row;
                        Int32.TryParse(reader["row"].ToString(), out row);
                        vaultModelObject.row = row;

                        int vaultId;
                        Int32.TryParse(reader["VaultId"].ToString(), out vaultId);
                        vaultModelObject.vaultId = vaultId;

                        vaultModelObject.vaultName = reader["VName"].ToString();
                        vaultModelObject.recordType = reader["RecordType"].ToString();
                        vaultModelObject.doctorName = reader["DoctorName"].ToString();
                        vaultModelObject.hospitalName = reader["HospitalName"].ToString();
                        vaultModelObject.date = DateTime.Parse(reader["Date"].ToString()).ToLongDateString();

                        int noOfFiles;
                        Int32.TryParse(reader["NoOfFiles"].ToString(), out noOfFiles);
                        vaultModelObject.noOfFiles = noOfFiles;

                        vaultList.Add(vaultModelObject);
                    }
                    responseModelObject.list = vaultList;

                    //to fetch rowCount
                    if (reader.NextResult())
                    {
                        int rowCount;
                        while (reader.Read())
                        {
                            Int32.TryParse(reader["TotalRows"].ToString(), out rowCount);
                            responseModelObject.rowCount = rowCount;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                responseModelObject.statusCode = StatusCode.error;
                responseModelObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return responseModelObject;
        }

        /// <summary>
        /// Calls SP to delete vault
        /// </summary>
        /// <param name="vaultModelObject">Accepts parameter of type VaultModel</param>
        /// <returns>True/False</returns>
        public static bool DeleteVault(VaultModel vaultModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("DeleteVault", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vaultId", vaultModelObject.vaultId);
                cmd.Parameters.AddWithValue("@userId", vaultModelObject.userId);

                sqlUtilityObject.con.Open();

                cmd.ExecuteNonQuery();

                return true;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.DeleteVaultDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = vaultModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                return false;
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }
        }

        /// <summary>
        /// Calls SP to get Single Vault details
        /// </summary>
        /// <param name="vaultModelObject">Accepts parameters of type VaultModel</param>
        /// <returns>VaultModel Object</returns>
        public static VaultModel GetSingleVaultDetails(VaultModel vaultModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();
            VaultModel vaultModelReturnObject = new VaultModel();

            try
            {
                SqlCommand cmd = new SqlCommand("GetSingleVaultDetails", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", vaultModelObject.userId);
                cmd.Parameters.AddWithValue("@vaultId", vaultModelObject.vaultId);

                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    vaultModelReturnObject.statusCode = StatusCode.invalid;
                }
                else
                {
                    vaultModelReturnObject.statusCode = StatusCode.valid;

                    //fetching vault details
                    while (reader.Read())
                    {
                        vaultModelReturnObject.vaultName = reader["VName"].ToString();
                        vaultModelReturnObject.recordType = reader["RecordType"].ToString();
                        vaultModelReturnObject.doctorName = reader["DoctorName"].ToString();
                        vaultModelReturnObject.hospitalName = reader["HospitalName"].ToString();
                        vaultModelReturnObject.date = DateTime.Parse(reader["Date"].ToString()).ToLongDateString();
                        vaultModelReturnObject.createdDate = reader["CreatedOn"].ToString();
                    }

                    //fetching list of files
                    if (reader.NextResult())
                    {
                        List<FileModel> fileModelList = new List<FileModel>();
                        while (reader.Read())
                        {
                            FileModel fileModelObject = new FileModel();

                            int fileId;
                            Int32.TryParse(reader["FileId"].ToString(), out fileId);
                            fileModelObject.fileId = fileId;

                            fileModelObject.fileName = reader["FileName"].ToString();
                            fileModelObject.createdOn = DateTime.Parse(reader["CreatedOn"].ToString()).ToLongDateString();

                            fileModelList.Add(fileModelObject);
                        }
                        vaultModelReturnObject.filesList = fileModelList;
                    }
                }
            }

            catch (Exception ex)
            {
                vaultModelReturnObject.statusCode = StatusCode.error;
                vaultModelReturnObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return vaultModelReturnObject;
        }
    }
}
