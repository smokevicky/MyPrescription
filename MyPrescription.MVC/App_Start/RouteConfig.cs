﻿using System.Web.Mvc;
using System.Web.Routing;

namespace MyPrescription.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "VerifyApi",
                url: "{controller}/{action}/{token}",
                defaults: new { controller = "Home", action = "Index", token = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SignIn/SignUp",
                url: "{controller}/{action}/{userModelObject}",
                defaults: new { controller = "Home", action = "Index", userModelObject = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserApi",
                url: "{controller}/{action}/{stringValue}",
                defaults: new { controller = "Home", action = "Index", stringValue = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HospitalEditApi",
                url: "{controller}/{action}/{hospitalId}",
                defaults: new { controller = "Account", action = "Index", hospitalId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HospitalApi",
                url: "{controller}/{action}/{hospitalRequestModelObject}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
