/********************************************************
** FileName:    AccountController.cs
** Author:      Jyoti Prakash Jena
** Date:        29.9.2016
** Purpose:     Handles all the Account Page Requests.
********************************************************/

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
        public ActionResult Hospitals()
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
        /// Returns the view for the Doctors page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Doctors()
        {
            try
            {
                ViewBag.UserId = Session["userId"].ToString();

                //start


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

                //end
                return View();
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}