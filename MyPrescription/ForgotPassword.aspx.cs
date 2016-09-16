using MyPrescription.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyPrescription.Models;
using MyPrescription.BL;
using MyPrescription.Error;

namespace MyPrescription
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendMailBtn_Click(object sender, EventArgs e)
        {
            string email = ForgotEmail.Value;

            try
            {
                ForgotPasswordBL forgotPasswordBLObject = new ForgotPasswordBL();
                UserModel userModelObject = forgotPasswordBLObject.FlagAsForgotPassword(email);

                if (userModelObject.statusCode == StatusCode.invalid)
                {
                    Response.Redirect("SignIn.aspx", false);
                }
                else if(userModelObject.statusCode == StatusCode.valid)
                {
                    string body = System.IO.File.ReadAllText(Server.MapPath(EmailTemplates.forgotPasswordHTML));    //Get HTML form EmailTemplate
                    body = body.Replace("#FULL_NAME", userModelObject.firstName + " " + userModelObject.lastName);  //replacing #TAGs in HTML
                    body = body.Replace("#TOKEN", userModelObject.token);

                    string subject = "My Prescription - Reset your password";

                    EmailUtility emailUtilityObject = new EmailUtility();
                    EmailModel emailModelObject = new EmailModel();
                    emailModelObject.receiverEmail = email;
                    emailModelObject.subject = subject;
                    emailModelObject.body = body;

                    emailUtilityObject.composeMail(emailModelObject);
                }
                else
                {
                    ErrorLog.LogError(ErrorCode.ForgotDAL, userModelObject.error);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.ForgotPAGE, ex.ToString());
            }
        }
    }
}