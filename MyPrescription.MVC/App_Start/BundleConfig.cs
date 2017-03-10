using System.Web.Optimization;

namespace MyPrescription.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;   //enable CDN support

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Libraries/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Libraries/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Libraries/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Libraries/bootstrap.js",
                      "~/Scripts/Libraries/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/customLibraries").Include(
                    "~/Scripts/CustomLibraries/Headroom.min.js",
                    "~/Scripts/CustomLibraries/jQuery.headroom.min.js"));

            bundles.Add(new StyleBundle("~/Css/Generic").Include(
                    "~/CSS/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Css/NonAccount").Include(
                      "~/CSS/Site-NonAccount.css"
                      ));

            bundles.Add(new StyleBundle("~/Css/Account").Include(
                      "~/CSS/Site-Account.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/NonAccount/SignIn").Include(
                    "~/Scripts/NonAccount/SignIn.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/NonAccount/SignUp").Include(
                    "~/Scripts/NonAccount/SignUp.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/NonAccount/ForgotPassword").Include(
                    "~/Scripts/NonAccount/ForgotPassword.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/NonAccount/EnterNewPassword").Include(
                    "~/Scripts/NonAccount/EnterNewPassword.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Account/Master").Include(
                    "~/Scripts/Account/Master.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Account/Dashboard").Include(
                    "~/Scripts/Account/Dashboard.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Account/Hospitals").Include(
                    "~/Scripts/Account/Hospitals.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Account/Doctors").Include(
                    "~/Scripts/Account/Doctors.js"
                ));

            //EnableOptimizations:false - disable optimization for debugging purposes
            BundleTable.EnableOptimizations = false;
        }
    }
}
