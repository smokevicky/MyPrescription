using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.Web;

namespace MyPrescription
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];

            if (token != null)
            {
                try
                {
                    VerifyBL verifyBLObject = new VerifyBL();
                    int activationStatusCode = verifyBLObject.CheckActivationStatus(token);


                    if (activationStatusCode == ActivationStatusCode.initial)
                    {
                        Response.Redirect("SignIn.aspx", false);
                    }
                    else if (activationStatusCode == ActivationStatusCode.invalidToken)
                    {
                        Response.Redirect("InvalidToken.aspx", false);
                    }
                    else if (activationStatusCode == ActivationStatusCode.valid)
                    {
                        UserModel userModelObject = verifyBLObject.ActivateAccount(token);

                        if (userModelObject.statusCode == ActivationStatusCode.invalidToken)
                        {
                            HttpContext.Current.Response.Redirect("SignIn.aspx", false);
                        }
                        else if (userModelObject.statusCode== ActivationStatusCode.valid)
                        {
                            
                            nameServer.InnerText = userModelObject.firstName + " " + userModelObject.lastName;
                            emailServer.InnerText = userModelObject.email;
                            userIdServer.InnerText = userModelObject.userId.ToString();

                            //Sending Registration successful mail
                            EmailUtility emailUtilityObject = new EmailUtility();

                            string body = System.IO.File.ReadAllText(Server.MapPath(EmailTemplates.successfulVerificationHTML));           //Get HTML form EmailTemplate
                            body = body.Replace("#FULL_NAME", userModelObject.firstName + " " + userModelObject.lastName);                 //replacing #TAGs in HTML       
                            body = body.Replace("#EMAIL", userModelObject.email);
                            body = body.Replace("#PASSWORD", userModelObject.password);
                            body = body.Replace("#USERID", userModelObject.userId.ToString());

                            string subject = "My Prescription - Thank you for registration";

                            EmailModel emailModelObject = new EmailModel();
                            emailModelObject.receiverEmail = userModelObject.email;
                            emailModelObject.subject = subject;
                            emailModelObject.body = body;

                            emailUtilityObject.composeMail(emailModelObject);

                        }
                        else
                        {
                            ErrorLog.LogError(ErrorCode.ActivationDAL, userModelObject.error);
                        }                        
                    }
                    else if (activationStatusCode == ActivationStatusCode.alreadyActivated)
                    {
                        Response.Redirect("AccountAlreadyActivated.aspx", false);
                    }
                    else
                    {
                        ErrorLog.LogError(ErrorCode.CheckActivationDAL, "We are currently facing some Technical Issues. Kindly cooperate.");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.LogError(ErrorCode.ActivationPAGE, ex.ToString());
                }
            }
            else
            {
                Response.Redirect("SignIn.aspx", false);
            }
        }
    }
}