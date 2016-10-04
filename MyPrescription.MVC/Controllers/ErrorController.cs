/********************************************************
** FileName:    ErrorController.cs
** Author:      Jyoti Prakash Jena
** Date:        30.9.2016
** Purpose:     Returns view page for Errors
********************************************************/
using System.Web.Mvc;

namespace MyPrescription.MVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
    }
}