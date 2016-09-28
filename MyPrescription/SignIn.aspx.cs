using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Web.Security;

namespace MyPrescription
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Abandon();
            //FormsAuthentication.SignOut();
        }

        protected void logInServer_Click(object sender, EventArgs e)
        {
            string email = username.Value;
            string pwd = password.Value;

            UserModel userModelObject = new UserModel();
            SignInBL signInBLObject = new SignInBL();

            userModelObject.email = email;
            userModelObject.password = pwd;

            try
            {
                userModelObject = signInBLObject.SignIn(userModelObject);

                //Logic
                if (userModelObject.statusCode == SignInStatusCode.invalid)
                {
                    Session["statusCode"] = SignInStatusCode.invalid;
                }
                else if (userModelObject.statusCode == SignInStatusCode.valid)
                {
                    FormsAuthentication.SetAuthCookie(userModelObject.userId.ToString(), false);

                    Session["statusCode"] = SignInStatusCode.valid;
                    Session["userId"] = userModelObject.userId;
                    Session["email"] = userModelObject.email;
                    Session["isActive"] = userModelObject.isActive;
                    Session["FName"] = userModelObject.firstName;
                    Session["LName"] = userModelObject.lastName;
                    Session["Phone"] = userModelObject.phone;

                    Response.Redirect("VerifyAccountActivation.aspx", false);
                }
                else
                {
                    ErrorLog.LogError(ErrorCode.SignInDAL, userModelObject.error);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.SignInPAGE, ex.ToString());
            }
        }
    }
}