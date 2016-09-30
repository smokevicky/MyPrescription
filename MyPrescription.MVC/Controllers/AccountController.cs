/********************************************************
** FileName:    AccountController.cs
** Author:      Jyoti Prakash Jena
** Date:        29.9.2016
** Purpose:     Handles all the Account Page Requests.
********************************************************/

using System.Web.Mvc;

namespace MyPrescription.MVC.Controllers
{
    /// <summary>
    /// Handles all the Account Page Requests.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Dashboard()
        {
            return Content("This is dashboard");
        }
    }
}