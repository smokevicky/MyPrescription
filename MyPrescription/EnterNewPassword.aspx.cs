using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;

namespace MyPrescription
{
    public partial class EnterNewPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if(Session["token"] == null)
            {
                Response.Redirect("InvalidToken.aspx", false);
            }
        }

        protected void ResetBtnServer_Click(object sender, EventArgs e)
        {
            string password = Password.Value;
            string cPassword = confirmPassword.Value;

            UserModel userModelObject = new UserModel();
            userModelObject.password = password;
            userModelObject.token = Session["token"].ToString();

            if (password == cPassword)
            {
                try
                {
                    UserModel userModelReturnObject = new UserModel();
                    EnterNewPasswordBL enterNewPasswordBLObject = new EnterNewPasswordBL();
                    userModelReturnObject = enterNewPasswordBLObject.ResetPassword(userModelObject);
                    
                    if(userModelReturnObject.statusCode == StatusCode.invalid)
                    {
                        LargeText.InnerHtml = "<span style='color:red'>Password reset Unsuccessful</span>";
                        FinalOutputDiv.InnerHtml = "";
                        Response.Redirect("InvalidToken.aspx", false);
                    }
                    else if(userModelReturnObject.statusCode == StatusCode.valid)
                    {
                        //Send Email
                        EmailModel emailModelObject = new EmailModel();
                        EmailUtility emailUtilityObject = new EmailUtility();
                       
                        string body = System.IO.File.ReadAllText(Server.MapPath(EmailTemplates.successfulPasswordResetHTML));//Get HTML form EmailTemplate
                        body = body.Replace("#FULL_NAME", userModelReturnObject.firstName + " " + userModelReturnObject.lastName);                                                                     //replacing #TAGs in HTML
                        body = body.Replace("#EMAIL", userModelReturnObject.email);
                        body = body.Replace("#PASSWORD", userModelReturnObject.password);
                        body = body.Replace("#USERID", userModelReturnObject.userId.ToString());

                        string subject = "My Prescription - PASSWORD reset successful";

                        emailModelObject.receiverEmail = userModelReturnObject.email;
                        emailModelObject.subject = subject;
                        emailModelObject.body = body;

                        emailUtilityObject.composeMail(emailModelObject);

                        HeaderText.InnerHtml = "<span style='color:green'>Password reset Successful</span>";
                        //--Send Email

                        LargeText.Visible = false;
                        FinalOutputDiv.Visible = true;
                        FinalOutputDiv.InnerHtml = "Everything's done here. Kindly head to the <a href='SignIn.aspx'>HOME</a> page and sign in using your new credentials<br />A confirmation mail containing your new credentials has been sent to your registered Email-Id : <b>" + userModelReturnObject.email + "</b>.";
                        confirmIdentity.Visible = false;
                        Response.AddHeader("REFRESH", "10;URL=SignIn.aspx");
                    }
                    else
                    {
                        ErrorLog.LogError(ErrorCode.ResetDAL, userModelObject.error);
                    }

                }
                catch(Exception ex)
                {
                    ErrorLog.LogError(ErrorCode.ResetPAGE, ex.ToString());
                }
            }
            else
            {
                Response.Redirect("InvalidToken.aspx", false);
            }
        }
    }
}