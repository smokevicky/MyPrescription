using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyPrescription.Util;
using MyPrescription.Models;
using MyPrescription.Error;

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
                string body = System.IO.File.ReadAllText(Server.MapPath(EmailTemplates.emailVerificationHTML));//Get HTML form EmailTemplate
                body = body.Replace("#FNAME", fname);                                                                     //replacing #TAGs in HTML
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