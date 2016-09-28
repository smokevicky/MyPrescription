using System;

namespace MyPrescription
{
    public partial class VerifyAccountActivation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userId"] == null)
            {
                Response.Redirect("SignIn.aspx", false);
            }
            else
            {
                string state = Session["isActive"].ToString();
                if(Session["isActive"].ToString().Equals("True"))
                {
                    Response.Redirect("Account/Dashboard.aspx", false);
                }
                else
                {
                    Response.Redirect("SignUpStep2.aspx", false);
                }
            }
        }
    }
}