/********************************************************
** FileName:    HomeController.cs
** Author:      Jyoti Prakash Jena
** Date:        28.9.2016
** Purpose:     Handles Index, About and Contact pages
********************************************************/

using System.Web.Mvc;

namespace MyPrescription.MVC.Controllers
{
    /// <summary>
    /// Handles Index, About and Contact pages
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        // GET: Home
        /// <summary>
        /// Handles the Index page request.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles the About page request.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "You message here";
            return View();
        }

        /// <summary>
        /// Handles the Contact page request.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }
    }
}