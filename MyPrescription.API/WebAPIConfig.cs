using MyPrescription.Models;
using System.Web.Http;

namespace MyPrescription.API
{
    public class WebAPIConfig
    {
        HospitalModel hospitalModelObject = new HospitalModel();

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "userApi",
                routeTemplate: "api/{controller}/{action}/{stringValue}",
                defaults: new { stringValue = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "hospitalApi",
                routeTemplate: "api/{controller}/{action}/{hospitalModelObject}",
                defaults: new { hospitalModelObject = RouteParameter.Optional }                
            );

            config.Routes.MapHttpRoute(
                name: "doctorApi",
                routeTemplate: "api/{controller}/{action}/{doctorModelObject}",
                defaults: new { doctorModelObject = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "vaultApi",
                routeTemplate: "api/{controller}/{action}/{vaultModelObject}",
                defaults: new { vaultModelObject = RouteParameter.Optional }
            );
            
            config.Routes.MapHttpRoute(
                name: "fileApi",
                routeTemplate: "api/{controller}/{action}/{fileModelObject}",
                defaults: new { fileModelObject = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "formDataApi",
                routeTemplate: "api/{controller}/{action}/{data}",
                defaults: new { data = RouteParameter.Optional }
            );
        }
    }
}
