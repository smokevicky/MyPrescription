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
using System.Collections.Generic;
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
                //creating add query
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

                using (var context = new MyPrescriptionEntities())
                {
                    context.HospitalMasters.Add(hospital);

                    //if the hospital is primary
                    //then HPrimaryMark is being updated in UserDetails to HospitalId
                    if (hospitalModelObject.isPrimary == 1)
                    {
                        var userDetail =
                            context.UserDetails.Where(n => n.UserId == hospitalModelObject.userId)
                            .ToList()
                            .FirstOrDefault();
                        userDetail.HPrimaryMark = hospitalModelObject.hospitalId;
                    }

                    context.SaveChanges();
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
                using (var context = new MyPrescriptionEntities())
                {
                    //creating select query
                    var hospitalsList = from hm in context.HospitalMasters
                                        join ud in context.UserDetails
                                        on hm.UserId equals ud.UserId
                                        where hm.UserId == hospitalRequestModelObject.userId
                                        select
                                        new
                                        {
                                            hm.HospitalId,
                                            hm.Name,
                                            hm.Address,
                                            hm.Phone,
                                            IsPrimary = ud.HPrimaryMark == hm.HospitalId ? 1 : 0
                                        };


                    if (hospitalsList.Any())
                    {
                        hospitalResponseModelObject.statusCode = StatusCode.valid;

                        //rows count
                        hospitalResponseModelObject.rowCount = hospitalsList.Count();

                        //sorting logic
                        switch (hospitalRequestModelObject.sortBy)
                        {
                            case "serial-up":
                                hospitalsList = hospitalsList.OrderBy(h => h.HospitalId);
                                break;

                            case "primary-up":
                                hospitalsList = hospitalsList.OrderBy(h => h.IsPrimary);
                                break;
                            case "primary-down":
                                hospitalsList = hospitalsList.OrderByDescending(h => h.IsPrimary);
                                break;
                            case "name-up":
                                hospitalsList = hospitalsList.OrderBy(h => h.Name);
                                break;
                            case "name-down":
                                hospitalsList = hospitalsList.OrderByDescending(h => h.Name);
                                break;
                            case "address-up":
                                hospitalsList = hospitalsList.OrderBy(h => h.Address);
                                break;
                            case "address-down":
                                hospitalsList = hospitalsList.OrderByDescending(h => h.Address);
                                break;
                            case "ph-up":
                                hospitalsList = hospitalsList.OrderBy(h => h.Phone);
                                break;
                            case "ph-down":
                                hospitalsList = hospitalsList.OrderByDescending(h => h.Phone);
                                break;
                            default:
                                hospitalsList = hospitalsList.OrderBy(h => h.HospitalId);
                                break;
                        }

                        //paging logic
                        hospitalsList =
                            hospitalsList.Skip(hospitalRequestModelObject.pageStart - 1)
                                .Take(hospitalRequestModelObject.pageSize);

                        List<HospitalModel> tempListOfHospitals = new List<HospitalModel>();

                        //adding hospitals to tempListOfHospitals
                        foreach (var hospital in hospitalsList)
                        {
                            var hospitalModelobject = new HospitalModel();

                            hospitalModelobject.hospitalId = hospital.HospitalId;
                            hospitalModelobject.name = hospital.Name;
                            hospitalModelobject.address = hospital.Address;
                            hospitalModelobject.phoneNo = hospital.Phone;
                            hospitalModelobject.isPrimary = hospital.IsPrimary;

                            tempListOfHospitals.Add(hospitalModelobject);
                        }

                        //adding temp list of all hospitals to hospitalModelList
                        hospitalResponseModelObject.hospitalModelList = tempListOfHospitals;
                    }
                    else
                    {
                        hospitalResponseModelObject.statusCode = StatusCode.invalid;
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

            hospitalResponseModelObject.pageSize = hospitalRequestModelObject.pageSize;
            hospitalResponseModelObject.pageStart = hospitalRequestModelObject.pageStart;
            hospitalResponseModelObject.sortBy = hospitalRequestModelObject.sortBy;

            return hospitalResponseModelObject;
        }

        /// <summary>
        /// Deletes the hospital.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static bool DeleteHospital(HospitalModel hospitalModelObject)
        {
            try
            {
                using (var context = new MyPrescriptionEntities())
                {
                    //deleting files from related vaults
                    var fileList = from file in context.FileMasters
                                   join vault in context.VaultMasters
                                   on file.VaultId equals vault.VaultId
                                   where vault.HospitalId == hospitalModelObject.hospitalId
                                         && vault.UserId == hospitalModelObject.userId
                                   select file;
                    context.FileMasters.RemoveRange(fileList.ToList());

                    //deleting vaults
                    var vaultList = from file in context.VaultMasters
                                    where file.HospitalId == hospitalModelObject.hospitalId
                                          & file.UserId == hospitalModelObject.userId
                                    select file;
                    context.VaultMasters.RemoveRange(vaultList.ToList());

                    //checking if doctor is primary marked
                    var doctorPrimaryMarkQuery = from user in context.UserDetails
                                                 join doctor in context.DoctorMasters
                                                 on user.DPrimaryMark equals doctor.DoctorId
                                                 where doctor.HospitalId == hospitalModelObject.hospitalId
                                                       && doctor.UserId == hospitalModelObject.userId
                                                 select user;

                    var userDetail = doctorPrimaryMarkQuery.ToList().FirstOrDefault();

                    //if doctor is primary then DPrimaryMark is being set to null
                    if (userDetail != null)
                    {
                        userDetail.DPrimaryMark = null;
                    }

                    //deleting doctors
                    var doctorsList = from doctor in context.DoctorMasters
                                      where doctor.HospitalId == hospitalModelObject.hospitalId
                                            && doctor.UserId == hospitalModelObject.userId
                                      select doctor;
                    context.DoctorMasters.RemoveRange(doctorsList.ToList());

                    //checking if hospital is primary marked
                    var hospitalPrimaryMarkQuery = from user in context.UserDetails
                                                   where user.HPrimaryMark == hospitalModelObject.hospitalId
                                                   select user;
                    userDetail = hospitalPrimaryMarkQuery.ToList().FirstOrDefault();

                    //if hospital is primary then HPrimaryMark is being set to null
                    if (userDetail != null)
                    {
                        userDetail.HPrimaryMark = null;
                    }

                    //deleting hospitals
                    var hospitalsList = from hospital in context.HospitalMasters
                                        where hospital.HospitalId == hospitalModelObject.hospitalId
                                              && hospital.UserId == hospitalModelObject.userId
                                        select hospital;
                    context.HospitalMasters.RemoveRange(hospitalsList.ToList());

                    context.SaveChanges();
                };

                return true;
            }
            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.DeleteHospitalEF, ex.ToString(), hospitalModelObject.userId);

                return false;
            }
        }

        /// <summary>
        /// Gets single hospital details.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static HospitalModel GetSingleHospitalDetails(HospitalModel hospitalModelObject)
        {
            HospitalModel hospitalModelReturnObject = new HospitalModel();

            try
            {
                using (var context = new MyPrescriptionEntities())
                {
                    //creating select query
                    var singleHospitalDetails = (from hospital in context.HospitalMasters
                                                 join UserDetail in context.UserDetails
                                                 on hospital.UserId equals UserDetail.UserId
                                                 where hospital.HospitalId == hospitalModelObject.hospitalId
                                                       && hospital.UserId == hospitalModelObject.userId
                                                 select new
                                                 {
                                                     hospital.HospitalId,
                                                     hospital.Name,
                                                     hospital.Address,
                                                     hospital.Phone,
                                                     hospital.Phone2,
                                                     hospital.Email,
                                                     hospital.CreatedOn,
                                                     hospital.UpdatedOn,
                                                     IsPrimary = hospital.HospitalId == UserDetail.HPrimaryMark ? 1 : 0
                                                 }).FirstOrDefault();
                    if (singleHospitalDetails != null)
                    {
                        hospitalModelReturnObject.statusCode = StatusCode.valid;

                        //assigning respective properties from context
                        hospitalModelReturnObject.hospitalId = singleHospitalDetails.HospitalId;
                        hospitalModelReturnObject.name = singleHospitalDetails.Name;
                        hospitalModelReturnObject.address = singleHospitalDetails.Address;
                        hospitalModelReturnObject.phoneNo = singleHospitalDetails.Phone;
                        hospitalModelReturnObject.phoneNo2 = singleHospitalDetails.Phone2;
                        hospitalModelReturnObject.email = singleHospitalDetails.Email;
                        hospitalModelReturnObject.isPrimary = singleHospitalDetails.IsPrimary;
                        hospitalModelReturnObject.createdOn = singleHospitalDetails.CreatedOn.ToLongDateString();
                        hospitalModelReturnObject.updatedOn = singleHospitalDetails.UpdatedOn.ToLongDateString();
                    }
                    else
                    {
                        hospitalModelReturnObject.statusCode = StatusCode.invalid;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.GetSingleHospitalDetailsEF,
                    ex.ToString(), hospitalModelObject.userId);

                hospitalModelReturnObject.statusCode = StatusCode.error;
                hospitalModelReturnObject.error = ex.ToString();
            }
            return hospitalModelReturnObject;
        }

        /// <summary>
        /// Updates the hospital details.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static bool UpdateHospitalDetails(HospitalModel hospitalModelObject)
        {
            try
            {
                using (var context = new MyPrescriptionEntities())
                {
                    var hospitalMaster = (from hospital in context.HospitalMasters
                                          where hospital.HospitalId == hospitalModelObject.hospitalId
                                                && hospital.UserId == hospitalModelObject.userId
                                          select hospital).FirstOrDefault();
                    if (hospitalMaster != null)
                    {
                        //updating all the values if found
                        hospitalMaster.Name = hospitalModelObject.name;
                        hospitalMaster.Address = hospitalModelObject.address;
                        hospitalMaster.Phone = hospitalModelObject.phoneNo;
                        hospitalMaster.Phone2 = hospitalModelObject.phoneNo2;
                        hospitalMaster.Email = hospitalModelObject.email;
                        hospitalMaster.UpdatedOn = DateTime.Now;

                        //if the hospital is primary marked
                        //then updating HPrimaryMark with hospitalId
                        if (hospitalModelObject.isPrimary == 1)
                        {
                            var userDetail = (from user in context.UserDetails
                                              where user.UserId == hospitalModelObject.userId
                                              select user).FirstOrDefault();
                            if (userDetail != null)
                            {
                                userDetail.HPrimaryMark = hospitalModelObject.hospitalId;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                };

                return true;
            }
            catch (Exception ex)
            {
                Error.ErrorLog.LogError(ErrorCode.UpdateHospitalEF, ex.ToString(), hospitalModelObject.userId);

                return false;
            }
        }
    }
}
