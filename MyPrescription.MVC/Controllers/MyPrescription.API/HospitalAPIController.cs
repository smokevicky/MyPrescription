﻿/********************************************************
** FileName:    HospitalAPIController.cs
** Author:      Jyoti Prakash Jena
** Date:        3.10.2016
** Purpose:     Handles all the api call requests related to hospitals
********************************************************/

using MyPrescription.BL;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Web.Mvc;

namespace MyPrescription.MVC.Controllers.MyPrescription.API
{
    public class HospitalAPIController : Controller
    {
        /// <summary>
        /// Adds a new hospital details to db
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddNewHospital(HospitalModel hospitalModelObject)
        {
            bool returnVal = false;
            string userId = User.Identity.Name;

            try
            {
                int hospitalId = Common.generateRandomId(FieldType.Hospital);
                hospitalModelObject.hospitalId = hospitalId;
                hospitalModelObject.userId = Convert.ToInt32(userId);

                returnVal = HospitalBL.AddNewHospital(hospitalModelObject);
            }

            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.APIAddNewHospital, ex.ToString(), userId);
            }
            return Content(returnVal.ToString());
        }

        /// <summary>
        /// update hospital details based on hospitalId
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public ActionResult UpdateHospitalDetails(HospitalModel hospitalModelObject)
        {
            bool returnVal = false;
            string userId = User.Identity.Name;
            hospitalModelObject.userId = Convert.ToInt32(userId);

            try
            {
                returnVal = HospitalBL.UpdateHospitalDetails(hospitalModelObject);
            }

            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.APIUpdateHospitalDetails, ex.ToString(), userId);
            }

            return Content(returnVal.ToString());
        }

        /// <summary>
        /// Gets the list of hospitals for the current user
        /// </summary>
        /// <param name="hospitalRequestModelObject"></param>
        /// <returns>list of hospital details, rowCount</returns>
        [HttpPost]
        [Authorize]
        public JsonResult GetHospitalDetails(HospitalRequestModel hospitalRequestModelObject)
        {
            HospitalResponseModel hospitalResponseModelObject = new HospitalResponseModel();
            string userId = User.Identity.Name;
            hospitalRequestModelObject.userId = Convert.ToInt32(userId);

            try
            {
                hospitalResponseModelObject = HospitalBL.GetHospitalDetails(hospitalRequestModelObject);
            }

            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.APIGetHospitalDetails, ex.ToString(), userId);
            }
            return Json(hospitalResponseModelObject);
        }

        /// <summary>
        /// Deletes hospital from db
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public ActionResult DeleteHospital(HospitalModel hospitalModelObject)
        {
            bool returnVal = false;
            string userId = User.Identity.Name;
            hospitalModelObject.userId = Convert.ToInt32(userId);

            try
            {
                returnVal = HospitalBL.DeleteHospital(hospitalModelObject);
            }

            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.APIDeleteHospital, ex.ToString(), userId);
            }
            return Content(returnVal.ToString());
        }

        /// <summary>
        /// Get single hospital details
        /// </summary>
        /// <param name="hospitalModelObject"></param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public JsonResult GetSingleHospitalDetails(HospitalModel hospitalModelObject)
        {
            string userId = User.Identity.Name;
            hospitalModelObject.userId = Convert.ToInt32(userId);

            try
            {
                hospitalModelObject = HospitalBL.GetSingleHospitalDetails(hospitalModelObject);
            }

            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.APIGetSingleHospitalDetails, ex.ToString(), userId);
            }
            return Json(hospitalModelObject);
        }
    }
}