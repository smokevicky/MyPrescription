using MyPrescription.Models;
using System.Net;
using System.Net.Mail;

namespace MyPrescription.Util
{
    public class EmailUtility
    {
        /// <summary>
        /// Composes mail and sends it using senderEmail, subject and Body
        /// </summary>
        /// <param name="emailModelObject"></param>
        public void composeMail(EmailModel emailModelObject)
        {
            string senderEmail = System.Configuration.ConfigurationManager.AppSettings.Get("senderEmail");              //data fetch from Web.config
            string senderPassword = System.Configuration.ConfigurationManager.AppSettings.Get("senderPassword");

            using (MailMessage mm = new MailMessage(senderEmail, emailModelObject.receiverEmail))
            {
                mm.Subject = emailModelObject.subject;
                mm.Body = emailModelObject.body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(senderEmail, senderPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

    }

    /// <summary>
    /// Keeps physical locations of EmailTemplate HTMLs
    /// </summary>
    public struct EmailTemplates
    {
        public const string emailVerificationHTML = "EmailTemplates/VerificationEmail.html";
        public const string successfulVerificationHTML = "EmailTemplates/SuccessfulVerification.html";
        public const string forgotPasswordHTML = "EmailTemplates/ForgotPasswordEmail.html";
        public const string successfulPasswordResetHTML = "EmailTemplates/SuccessfulPasswordReset.html";
    }
}
