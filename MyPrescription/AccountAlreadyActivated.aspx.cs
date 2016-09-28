using System;

namespace MyPrescription
{
    public partial class AccountAlreadyActivated : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("REFRESH", "4;URL=SignIn.aspx");
        }
    }
}