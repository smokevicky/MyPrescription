using MyPrescription.DAL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Web.Http;

namespace MyPrescription.API.Controller
{
    public class HospitalController : ApiController
    {
        /// <summary>
        /// Adds a new hospital details to db
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public bool AddNewHospital(HospitalModel hospitalModelObject)
        {
            bool returnVal = false;
            string userId = User.Identity.Name;

            try {
                int hospitalId = Common.generateRandomId(FieldType.Hospital);
                hospitalModelObject.hospitalId = hospitalId;
                hospitalModelObject.userId = Convert.ToInt32(userId);

                returnVal = new HospitalDAL().AddNewHospital(hospitalModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIAddNewHospital, ex.ToString(), userId);
            }
            return returnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hospitalRequestModelObject"></param>
        /// <returns>list of hospital details, rowCount</returns>
        [HttpPost]
        [Authorize]
        public HospitalResponseModel GetHospitalDetails(HospitalRequestModel hospitalRequestModelObject)
        {            
            HospitalResponseModel hospitalResponseModelObject = new HospitalResponseModel();
            string userId = User.Identity.Name;
            hospitalRequestModelObject.userId = Convert.ToInt32(userId);

            try {
                hospitalResponseModelObject = new HospitalDAL().GetHospitalDetails(hospitalRequestModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIGetHospitalDetails, ex.ToString(), userId);
            }
            return hospitalResponseModelObject;
        }

        /// <summary>
        /// Deletes hospital from db
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public bool DeleteHospital(HospitalModel hospitalModelObject)
        {            
            bool returnVal = false;            
            string userId = User.Identity.Name;
            hospitalModelObject.userId = Convert.ToInt32(userId);

            try {
                returnVal = new HospitalDAL().DeleteHospital(hospitalModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIDeleteHospital, ex.ToString(), userId);
            }
            return returnVal;
        }

        /// <summary>
        /// Get single hospital details
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public HospitalModel GetSingleHospitalDetails(HospitalModel hospitalModelObject)
        {
            string userId = User.Identity.Name;
            hospitalModelObject.userId = Convert.ToInt32(userId);

            try {
                hospitalModelObject = new HospitalDAL().GetSingleHospitalDetails(hospitalModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIGetSingleHospitalDetails, ex.ToString(), userId);
            }
            return hospitalModelObject;           
        }

        /// <summary>
        /// update hospital details based on hospitalId
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public bool UpdateHospitalDetails(HospitalModel hospitalModelObject)
        {
            bool returnVal = false;
            string userId = User.Identity.Name;
            hospitalModelObject.userId = Convert.ToInt32(userId);

            try {
                returnVal = new HospitalDAL().UpdateHospitalDetails(hospitalModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIUpdateHospitalDetails, ex.ToString(), userId);
            }

            return returnVal;
        }
    }
}