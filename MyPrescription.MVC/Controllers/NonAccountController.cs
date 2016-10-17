/********************************************************
** FileName:    NonAccountController.cs
** Author:      Jyoti Prakash Jena
** Date:        28.9.2016
** Purpose:     Handles all the NonAccount Page Requests.
********************************************************/

using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.MVC.Controllers.MyPrescription.API;
using MyPrescription.Util;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPrescription.MVC.Controllers
{
    /// <summary>
    /// Handles all the NonAccount Page Requests.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class NonAccountController : Controller
    {
        /// <summary>
        /// Signs the user in.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignIn(UserModel userModelObject)
        {
            SignInBL signInBLObject = new SignInBL();

            try
            {
                userModelObject = signInBLObject.SignIn(userModelObject);

                //Logic
                if (userModelObject.statusCode == SignInStatusCode.invalid)
                {
                    Session["statusCode"] = SignInStatusCode.invalid;

                    TempData["statusCode"] = SignInStatusCode.invalid;

                    return RedirectToAction("Index", "Home");
                }
                if (userModelObject.statusCode == SignInStatusCode.valid)
                {
                    FormsAuthentication.SetAuthCookie(userModelObject.userId.ToString(), false);

                    Session["statusCode"] = SignInStatusCode.valid;
                    Session["userId"] = userModelObject.userId;
                    Session["email"] = userModelObject.email;
                    Session["isActive"] = userModelObject.isActive;
                    Session["FName"] = userModelObject.firstName;
                    Session["LName"] = userModelObject.lastName;
                    Session["Phone"] = userModelObject.phone;
                    Session["token"] = userModelObject.token;

                    return RedirectToAction("VerifyAccountActivation", "NonAccount");
                }

                //Error has occured
                ErrorLog.LogError(ErrorCode.SignInDAL, userModelObject.error);
                return View("Error");

                //return userModelObject.statusCode;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.SignInPAGE, ex.ToString());
                return View("Error");
                //return StatusCode.error;
            }
        }

        /// <summary>
        /// Returns view of the SignUp page.
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="userModelObject">The user model object.</param>
        /// <returns></returns>
        public ActionResult AddNewUser(UserModel userModelObject)
        {
            //used for sending mail in signupStep2
            Session["fname"] = userModelObject.firstName;

            SignUpBL signUpBLObject = new SignUpBL();

            userModelObject.userId = Common.generateRandomId(FieldType.User);

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

                    return Content(ActionResultStatusCode.True);
                }
                else
                {
                    ErrorLog.LogError(ErrorCode.SignUpDAL, userModelObject.error);
                }
                return Content(ActionResultStatusCode.False);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.SignUpPAGE, ex.ToString());
                return Content(ActionResultStatusCode.False);
            }
        }

        /// <summary>
        /// Verifies the account activation.
        /// If activated then redirects to the Dashboard else redirect to the SignUpStep2
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyAccountActivation()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("index", "home");
            }

            if (Session["isActive"].ToString().Equals("True"))
            {
                return RedirectToAction("Dashboard", "Account");
            }

            return RedirectToAction("SignUpStep2", "NonAccount");

        }

        /// <summary>
        /// Sends Email to the registered email for Step2 of Signup process.
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpStep2()
        {
            string email = (string)Session["email"];
            string fname = (string)Session["fname"];
            string token = (string)Session["token"];

            EmailModel emailModelObject = new EmailModel();
            EmailUtility emailUtilityObject = new EmailUtility();

            try
            {
                //Get HTML from EmailTemplate
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
            return View();
        }

        /// <summary>
        /// Verifies the specified token.
        /// </summary>
        /// <param name="token">The token(guid).</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Verify(string token)
        {
            if (token != null)
            {
                try
                {
                    VerifyBL verifyBLObject = new VerifyBL();
                    int activationStatusCode = verifyBLObject.CheckActivationStatus(token);


                    if (activationStatusCode == ActivationStatusCode.initial)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (activationStatusCode == ActivationStatusCode.invalidToken)
                    {
                        return View("InvalidToken");
                    }
                    else if (activationStatusCode == ActivationStatusCode.valid)
                    {
                        UserModel userModelObject = verifyBLObject.ActivateAccount(token);

                        if (userModelObject.statusCode == ActivationStatusCode.invalidToken)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (userModelObject.statusCode == ActivationStatusCode.valid)
                        {

                            ViewBag.name = userModelObject.firstName + " " + userModelObject.lastName;
                            ViewBag.email = userModelObject.email;
                            ViewBag.userId = userModelObject.userId.ToString();

                            //Sending Registration successful mail
                            EmailUtility emailUtilityObject = new EmailUtility();

                            string body =
                                    System.IO.File.ReadAllText(Server.MapPath(EmailTemplates.successfulVerificationHTML));
                            //Get HTML form EmailTemplate
                            body = body.Replace("#FULL_NAME", userModelObject.firstName + " " + userModelObject.lastName);
                            //replacing #TAGs in HTML       
                            body = body.Replace("#EMAIL", userModelObject.email);
                            body = body.Replace("#PASSWORD", userModelObject.password);
                            body = body.Replace("#USERID", userModelObject.userId.ToString());

                            string subject = "My Prescription - Thank you for registration";

                            EmailModel emailModelObject = new EmailModel();
                            emailModelObject.receiverEmail = userModelObject.email;
                            emailModelObject.subject = subject;
                            emailModelObject.body = body;

                            emailUtilityObject.composeMail(emailModelObject);

                            return View();
                        }
                        else
                        {
                            ErrorLog.LogError(ErrorCode.ActivationDAL, userModelObject.error);
                            return View("Error");
                        }
                    }
                    else if (activationStatusCode == ActivationStatusCode.alreadyActivated)
                    {
                        return View("AlreadyActivated");
                    }
                    else
                    {
                        ErrorLog.LogError(ErrorCode.CheckActivationDAL,
                            "We are currently facing some Technical Issues. Kindly cooperate.");
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.LogError(ErrorCode.ActivationPAGE, ex.ToString());
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Handles the Forgot Password Request.
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Sends the forgot password email and flags the account.
        /// </summary>
        /// <param name="stringValue">The email of the account.</param>
        [HttpGet]
        public void SendForgotPasswordEmailAndFlag(string stringValue)
        {
            string email = stringValue;

            try
            {
                ForgotPasswordBL forgotPasswordBLObject = new ForgotPasswordBL();
                UserModel userModelObject = forgotPasswordBLObject.FlagAsForgotPassword(email);

                if (userModelObject.statusCode == StatusCode.invalid)
                {
                    Response.Redirect("SignIn.aspx", false);
                }
                else if (userModelObject.statusCode == StatusCode.valid)
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

        /// <summary>
        /// Returns view of the Terms and conditions page.
        /// </summary>
        /// <returns></returns>
        public ActionResult TermsAndConditions()
        {
            return View();
        }

        [HttpGet]
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public ActionResult ResetPassword(string token)
        {
            string status = "";

            status = new UserAPIController().CheckStatusFromToken(token);
            //status = response.Content;

            if (status.Equals("ForgotPassword"))
            {
                Session["token"] = token;
                return RedirectToAction("EnterNewPassword", "NonAccount");
            }
            else
            {
                return View("InvalidToken");
            }
        }

        /// <summary>
        /// Returns view for the EnterNewPassword page
        /// </summary>
        /// <returns></returns>
        public ActionResult EnterNewPassword()
        {
            return View();
        }

        [HttpGet]
        /// <summary>
        /// Finals the reset password.
        /// </summary>
        /// <param name="stringValue">The password</param>
        /// <returns></returns>
        public ActionResult FinalResetPassword(string stringValue)
        {
            string password = stringValue;

            UserModel userModelObject = new UserModel();
            userModelObject.password = password;
            userModelObject.token = Session["token"].ToString();

            try
            {
                UserModel userModelReturnObject = new UserModel();
                EnterNewPasswordBL enterNewPasswordBLObject = new EnterNewPasswordBL();
                userModelReturnObject = enterNewPasswordBLObject.ResetPassword(userModelObject);

                if (userModelReturnObject.statusCode == StatusCode.invalid)
                {
                    //return Content(StatusCode.invalid.ToString());
                    return View("PasswordResetUnsuccessful");
                }
                if (userModelReturnObject.statusCode == StatusCode.valid)
                {
                    //Send Email
                    EmailModel emailModelObject = new EmailModel();
                    EmailUtility emailUtilityObject = new EmailUtility();

                    //Get HTML form EmailTemplate
                    string body = System.IO.File.ReadAllText(Server.MapPath(EmailTemplates.successfulPasswordResetHTML));

                    //replacing #TAGs in HTML
                    body = body.Replace("#FULL_NAME", userModelReturnObject.firstName + " " + userModelReturnObject.lastName);
                    body = body.Replace("#EMAIL", userModelReturnObject.email);
                    body = body.Replace("#PASSWORD", userModelReturnObject.password);
                    body = body.Replace("#USERID", userModelReturnObject.userId.ToString());

                    string subject = "My Prescription - PASSWORD reset successful";

                    emailModelObject.receiverEmail = userModelReturnObject.email;
                    emailModelObject.subject = subject;
                    emailModelObject.body = body;

                    emailUtilityObject.composeMail(emailModelObject);

                    Response.AddHeader("REFRESH", "10;URL=/Home");
                    //return Content(StatusCode.valid.ToString());
                    return View("PasswordResetSuccessful");
                }
                else
                {
                    ErrorLog.LogError(ErrorCode.ResetDAL, userModelObject.error);
                    //return Content(StatusCode.error.ToString());
                    return View("Error");
                }

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.ResetPAGE, ex.ToString());
                //return Content(StatusCode.error.ToString());
                return View("Error");
            }
        }
    }
}