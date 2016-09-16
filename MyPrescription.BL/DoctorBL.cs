/********************************************************
**FileName:
**Author:
**Date:
**Purpose:
********************************************************/

using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class DoctorBL
    {
        /// <summary>
        /// Adds a new doctor to db
        /// </summary>
        /// <param name="doctorModelObject">Accepts objects of type DoctorModel</param>
        /// <returns></returns>
        public static bool AddNewDoctor(DoctorModel doctorModelObject)
        {
            bool returnVal = false;

            returnVal = DoctorDAL.AddNewDoctor(doctorModelObject);

            return returnVal;
        }

        /// <summary>
        /// Gets list of all doctors for specific user
        /// </summary>
        /// <param name="userId">Accepts userId</param>
        /// <returns></returns>
        public static DoctorResponseModel GetDoctorDetails(string userId)
        {
            DoctorResponseModel doctorResponseModelObject = new DoctorResponseModel();

            doctorResponseModelObject = DoctorDAL.GetDoctorDetails(userId);

            return doctorResponseModelObject;
        }

        /// <summary>
        /// Deletes a doctor details from db
        /// </summary>
        /// <param name="doctorModelObject">Accepts parameter of type DoctorModel</param>
        /// <returns>true/false</returns>
        public static bool DeleteHospital(DoctorModel doctorModelObject)
        {
            bool returnVal = false;

            returnVal = DoctorDAL.DeleteDoctor(doctorModelObject);

            return returnVal;
        }

        /// <summary>
        /// Gets single doctor details for editing
        /// </summary>
        /// <param name="doctorModelObject"></param>
        /// <returns>DoctorModel</returns>
        public static DoctorModel GetSingleDoctorDetails(DoctorModel doctorModelObject)
        {
            return DoctorDAL.GetSingleDoctorDetails(doctorModelObject);
        }

        /// <summary>
        /// Update doctor details after editing
        /// </summary>
        /// <param name="doctorModelObject">Accepts parameter of type doctorModelObject</param>
        /// <returns>true/false</returns>
        public static bool UpdateUpdateDoctorDetails(DoctorModel doctorModelObject)
        {
            return DoctorDAL.UpdateDoctorDetails(doctorModelObject);
        }
    }
}
