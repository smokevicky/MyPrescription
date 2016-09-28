/********************************************************
** FileName:    FileDAL.cs
** Author:      Jyoti Prakash Jena
** Date:        --
** Purpose:     Handle db operations for files
********************************************************/

using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    /// <summary>
    /// Handle db operations for files
    /// </summary>
    public class FileDAL
    {
        /// <summary>
        /// Calls SP to Add a new file
        /// </summary>
        /// <param name="fileModelObject">Accepts parameter of type FileModel</param>
        /// <returns>FileId</returns>
        public static int AddNewFile(FileModel fileModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("AddNewFile", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fileName", fileModelObject.fileName);
                cmd.Parameters.AddWithValue("@userId", fileModelObject.userId);
                cmd.Parameters.AddWithValue("@vaultId", fileModelObject.vaultId);

                sqlUtilityObject.con.Open();

                int fileId = (int)cmd.ExecuteScalar();

                return fileId;
            }

            catch (Exception ex)
            {
                ErrorLogModel errorLogModelObject = new ErrorLogModel();
                errorLogModelObject.errorCode = ErrorCode.AddNewFileDAL;
                errorLogModelObject.errorMessage = ex.ToString();
                errorLogModelObject.userId = fileModelObject.userId;
                ErrorLogDAL.LogError(errorLogModelObject);

                return -1;
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }
        }

        /// <summary>
        /// Gets VaultId and FileName By FileId
        /// </summary>
        /// <param name="fileModelObject">Accepts parameter of type FileModel</param>
        /// <returns>FileModelObject</returns>
        public static FileModel GetVaultIdFileName(FileModel fileModelObject)
        {
            SqlUtility sqlUtilityObject = new SqlUtility();
            FileModel fileModelReturnObject = new FileModel();

            try
            {
                SqlCommand cmd = new SqlCommand("GetVaultIdFileName", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", fileModelObject.userId);
                cmd.Parameters.AddWithValue("@fileId", fileModelObject.fileId);

                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    fileModelReturnObject.statusCode = StatusCode.invalid;
                }
                else
                {
                    fileModelReturnObject.statusCode = StatusCode.valid;

                    //fetching vault details
                    while (reader.Read())
                    {
                        int vaultId;
                        Int32.TryParse(reader["VaultId"].ToString(), out vaultId);
                        fileModelReturnObject.vaultId = vaultId;

                        fileModelReturnObject.fileName = reader["FileName"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                fileModelReturnObject.statusCode = StatusCode.error;
                fileModelReturnObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return fileModelReturnObject;
        }
    }
}
