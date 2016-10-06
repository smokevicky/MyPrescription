﻿/********************************************************
** FileName:    AccountController.cs
** Author:      Jyoti Prakash Jena
** Date:        29.9.2016
** Purpose:     Handles all the Account Page Requests.
********************************************************/

using MyPrescription.BL;
using MyPrescription.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPrescription.MVC.Controllers
{
    /// <summary>
    /// Handles all the Account Page Requests.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AccountController : Controller
    {
        /// <summary>
        /// Returns the view for the Dashboard page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {
            try
            {
                ViewBag.UserId = Session["userId"].ToString();
                return View();
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Signs the user out and destroys the session and form-authentication cookie.
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Returns the view for the Hospitals page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Hospitals(int? page, string sortBy, int? pageSize)
        {
            try
            {
                ViewBag.UserId = Session["userId"].ToString();
                var defaultPageSize = 5;

                page = (page == null) ? 1 : page;
                pageSize = (pageSize == null) ? defaultPageSize : pageSize;

                var requestModelObject = new HospitalRequestModel()
                {
                    //setting all the default values
                    pageStart = (int)((page - 1) * pageSize) + 1,
                    pageSize = (int)pageSize,
                    sortBy = sortBy,
                    userId = Convert.ToInt32(ViewBag.UserId)
                };

                var listOfHospitals = HospitalBL.GetHospitalDetails(requestModelObject);
                listOfHospitals.page = (int)page;

                return View(listOfHospitals);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Returns the view for the Hospitals page and renders grid based on the HospitalRequestModel
        /// </summary>
        /// <param name="hospitalRequestModelObject">The hospital request model object.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Hospitals(HospitalRequestModel hospitalRequestModelObject)
        {
            try
            {
                ViewBag.UserId = Session["userId"].ToString();

                var requestModelObject = new HospitalRequestModel()
                {
                    //setting all the default values
                    pageStart = hospitalRequestModelObject.pageStart,
                    pageSize = hospitalRequestModelObject.pageSize,
                    sortBy = hospitalRequestModelObject.sortBy,
                    userId = Convert.ToInt32(ViewBag.UserId)
                };

                var listOfHospitals = HospitalBL.GetHospitalDetails(requestModelObject);

                return View(listOfHospitals);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Returns the view for the Doctors page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Doctors()
        {
            try
            {
                ViewBag.UserId = Session["userId"].ToString();

                string constr = ConfigurationManager.ConnectionStrings["MyPrescriptionConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT HospitalId, Name FROM HospitalMaster WHERE " +
                                                           "UserId = '" + Session["userId"] + "' ORDER BY Name"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        var reader = cmd.ExecuteReader();

                        var hospitalsList = new List<HospitalDropDownModel>();

                        int hospitalId;

                        while (reader.Read())
                        {
                            var hospitalDropDownModelObject = new HospitalDropDownModel();

                            int.TryParse(reader["HospitalId"].ToString(), out hospitalId);
                            hospitalDropDownModelObject.hospitalId = hospitalId;

                            hospitalDropDownModelObject.hospitalName = reader["Name"].ToString();

                            //adding modelObjects to the list
                            hospitalsList.Add(hospitalDropDownModelObject);
                        }

                        ViewBag.ListOfHospitals = hospitalsList;

                        con.Close();
                    }
                }
                //hospitalsList.Items.Insert(0, new ListItem("--Select the Hospital the Doctor belongs to--", "0"));

                return View();
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        /// <summary>
        /// Retuns view for editing of hospital details
        /// </summary>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditHospital(int? hospitalId)
        {
            try
            {
                int userId;
                int.TryParse(Session["userId"].ToString(), out userId);
                ViewBag.UserId = userId;

                var hospitalDetails =
                    HospitalBL.GetSingleHospitalDetails(new HospitalModel() { hospitalId = (int)hospitalId, userId = ViewBag.UserId });

                return View(hospitalDetails);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ActionName("EditHospital")]
        public ActionResult EditHospital_Post(FormCollection formCollection)
        {
            try
            {
                return View();
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}