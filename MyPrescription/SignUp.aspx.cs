using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;

namespace MyPrescription
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signupFrmSubmitServer_Click(object sender, EventArgs e)
        {
            string fname = signupFname.Value;
            string lname = signupLname.Value;
            string email = signupEmail.Value;
            string password = signupPwd.Value;

            SignUpBL signUpBLObject = new SignUpBL();
            UserModel userModelObject = new UserModel();
            userModelObject.userId = Common.generateRandomId(FieldType.User);
            userModelObject.firstName = fname;
            userModelObject.lastName = lname;
            userModelObject.email = email;
            userModelObject.password = password;

            try
            {
                userModelObject = signUpBLObject.SignUp(userModelObject);

                if (userModelObject.statusCode == StatusCode.invalid)
                {
                    Session["statusCode"] = StatusCode.invalid;
                }
                else if (userModelObject.statusCode == StatusCode.valid)
                {
                    Session["statusCode"] = StatusCode.valid;
                    Session["userId"] = userModelObject.userId;
                    Session["token"] = userModelObject.token;
                    Session["isActive"] = userModelObject.isActive;
                    Session["status"] = userModelObject.status;
                    Session["email"] = userModelObject.email;

                    //used for sending mail in signupStep2
                    Session["fname"] = fname;

                    Response.Redirect("SignUpStep2.aspx", false);
                }
                else
                {
                    ErrorLog.LogError(ErrorCode.SignUpDAL, userModelObject.error);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.SignUpPAGE, ex.ToString());
            }
        }
    }
}