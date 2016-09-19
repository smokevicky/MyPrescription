using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;

namespace MyPrescription
{
    public partial class SignUpStep2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = (string)Session["email"];
            string fname = (string)Session["fname"];
            string token = (string)Session["token"];

            EmailModel emailModelObject = new EmailModel();
            EmailUtility emailUtilityObject = new EmailUtility();

            try
            {
                //Get HTML form EmailTemplate
                string body = System.IO.File.ReadAllText(Server.MapPath(EmailTemplates.emailVerificationHTML));

                //replacing #TAGs in HTML
                body = body.Replace("#FNAME", fname);
                body = body.Replace("#TOKEN", token);

                string subject = "My Prescription - Awaiting EMail Verification";

                emailModelObject.receiverEmail = email;
                emailModelObject.subject = subject;
                emailModelObject.body = body;

                emailUtilityObject.composeMail(emailModelObject);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.SignUp2PAGE, ex.ToString());
            }
        }
    }
}