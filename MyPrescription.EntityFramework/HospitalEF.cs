/********************************************************
** FileName:  HospitalEF.cs
** Author:    Jyoti Prakash Jena
** Date:      19.9.2016
** Purpose:   Does all the database operations for Hospital Level
********************************************************/

using MyPrescription.EntityFramework.App_Data;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Linq;

namespace MyPrescription.EntityFramework
{
    /// <summary>
    /// Does all the database operations for Hospital Level
    /// </summary>
    public class HospitalEF
    {
        /// <summary>
        /// Adds a new hospital.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static bool AddNewHospital(HospitalModel hospitalModelObject)
        {
            try
            {
                var hospital = new HospitalMaster()
                {
                    HospitalId = hospitalModelObject.hospitalId,
                    Name = hospitalModelObject.name,
                    Address = hospitalModelObject.address,
                    Phone = hospitalModelObject.phoneNo,
                    Phone2 = hospitalModelObject.phoneNo2,
                    Email = hospitalModelObject.email,
                    UserId = hospitalModelObject.userId,
                    Status = hospitalModelObject.status,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                using (var entities = new MyPrescriptionEntities())
                {
                    entities.HospitalMasters.Add(hospital);

                    if (hospitalModelObject.isPrimary == 1)
                    {
                        var userDetail =
                            entities.UserDetails.Where(n => n.UserId == hospitalModelObject.userId)
                            .ToList()
                            .FirstOrDefault();
                        userDetail.HPrimaryMark = hospitalModelObject.hospitalId;
                    }

                    entities.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.AddNewHospitalEF, ex.ToString(), hospitalModelObject.userId);

                return false;
            }
        }

        /// <summary>
        /// Gets the list of all hospitals.
        /// </summary>
        /// <param name="hospitalRequestModelObject">The hospital request model object.</param>
        /// <returns></returns>
        public static HospitalResponseModel GetHospitalDetails(HospitalRequestModel hospitalRequestModelObject)
        {
            HospitalResponseModel hospitalResponseModelObject = new HospitalResponseModel();
            try
            {
                using (var entities = new MyPrescriptionEntities())
                {
                    var hospitalsList = from hm in entities.HospitalMasters
                                        join ud in entities.UserDetails on hm.UserId equals ud.UserId
                                        where hm.UserId == hospitalRequestModelObject.userId
                                        select
                                        new
                                        {
                                            hm.HospitalId,
                                            hm.Name,
                                            hm.Address,
                                            hm.Phone,
                                            IsPrimary = ud.HPrimaryMark == hm.HospitalId ? 1 : 0,
                                        };


                    foreach (var hospital in hospitalsList)
                    {
                        Common.Notify(hospital.Name, "info");
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.GetHospitalDetailsEF,
                    ex.ToString(), hospitalRequestModelObject.userId);

                hospitalResponseModelObject.statusCode = StatusCode.error;
                hospitalResponseModelObject.error = ex.ToString();
            }
            return hospitalResponseModelObject;
        }
    }
}
