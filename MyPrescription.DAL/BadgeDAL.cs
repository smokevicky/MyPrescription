using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyPrescription.DAL
{
    public class BadgeDAL
    {
        /// <summary>
        /// Gets the badge count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static CountModel GetBadgeCount(int userId)
        {
            CountModel countModelObject = new CountModel();
            SqlUtility sqlUtilityObject = new SqlUtility();

            try
            {
                SqlCommand cmd = new SqlCommand("GetBadgeCount", sqlUtilityObject.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);

                sqlUtilityObject.con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    //invalid = 0 : Invalid userId
                    countModelObject.statusCode = StatusCode.invalid;
                }
                else
                {
                    //valid = 1 : details found
                    countModelObject.statusCode = StatusCode.valid;

                    //to fetch list of hospital details
                    while (reader.Read())
                    {
                        int hospitalCount, vaultCount, doctorCount;

                        Int32.TryParse(reader["HospitalCount"].ToString(), out hospitalCount);
                        countModelObject.hospitalCount = hospitalCount;

                        Int32.TryParse(reader["DoctorCount"].ToString(), out doctorCount);
                        countModelObject.doctorCount = doctorCount;

                        Int32.TryParse(reader["VaultCount"].ToString(), out vaultCount);
                        countModelObject.vaultCount = vaultCount;
                    }
                }
            }

            catch (Exception ex)
            {
                //invalid = 2 : Error
                countModelObject.statusCode = StatusCode.error;
                countModelObject.error = ex.ToString();
            }
            finally
            {
                sqlUtilityObject.con.Close();
            }

            return countModelObject;
        }

    }
}
