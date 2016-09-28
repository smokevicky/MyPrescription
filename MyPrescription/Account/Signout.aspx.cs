using System;
using System.Web.Security;

namespace MyPrescription.Account
{
    public partial class Signout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("../SignIn.aspx", false);
        }
    }
}