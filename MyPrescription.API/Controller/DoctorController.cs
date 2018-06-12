using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Web.Http;

namespace MyPrescription.API.Controller
{
    public class DoctorController : ApiController
    {
        /// <summary>
        /// Adds a new doctor to the db
        /// </summary>
        /// <param name="doctorModelObject">Accepts objects to type DoctorModel</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public bool AddNewDoctor(DoctorModel doctorModelObject)
        {
            bool returnVal = false;
            string userId = User.Identity.Name;

            try
            {
                int doctorId = Common.generateRandomId(FieldType.Doctor);
                doctorModelObject.doctorId = doctorId;
                doctorModelObject.userId = Convert.ToInt32(userId);
                returnVal = DoctorBL.AddNewDoctor(doctorModelObject);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIAddNewDoctor, ex.ToString(), userId);
            }
            return returnVal;
        }

        [HttpGet]
        [Authorize]
        public DoctorResponseModel GetDoctorDetails()
        {
            DoctorResponseModel doctorResponseModelObject = new DoctorResponseModel();
            string userId = User.Identity.Name;

            try
            {
                doctorResponseModelObject = DoctorBL.GetDoctorDetails(userId);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIGetDoctorDetails, ex.ToString(), userId);
            }

            return doctorResponseModelObject;
        }

        /// <summary>
        /// Deletes a Doctor details from db
        /// </summary>
        /// <param name="doctorModelObject">Accepts parameter of type DoctorModel</param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public bool DeleteDoctor(DoctorModel doctorModelObject)
        {
            bool returnVal = false;
            string userId = User.Identity.Name;

            try
            {
                doctorModelObject.userId = Convert.ToInt32(userId);
                returnVal = DoctorBL.DeleteHospital(doctorModelObject);

            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIDeleteDoctor, ex.ToString(), userId);
            }
            return returnVal;
        }

        /// <summary>
        /// Get single doctor details
        /// </summary>
        /// <param name="doctorModelObject">Accepts parameter of type DoctorModel</param>
        /// <returns>DoctorModel</returns>
        [HttpPost]
        [Authorize]
        public DoctorModel GetSingleDoctorDetails(DoctorModel doctorModelObject)
        {
            string userId = User.Identity.Name;

            try
            {
                doctorModelObject.userId = Convert.ToInt32(userId);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIGetSingleDoctorDetails, ex.ToString(), userId);
            }
            return DoctorBL.GetSingleDoctorDetails(doctorModelObject);
        }

        /// <summary>
        /// Update doctor details after editing
        /// </summary>
        /// <param name="doctorModelObject">Accepts parameter of type doctorModelObject</param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public bool UpdateDoctorDetails(DoctorModel doctorModelObject)
        {
            try
            {
                doctorModelObject.userId = Convert.ToInt32(User.Identity.Name);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIUpdateDoctorDetails, ex.ToString(), User.Identity.Name);
            }

            return DoctorBL.UpdateUpdateDoctorDetails(doctorModelObject);
        }
    }
}
