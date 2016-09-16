using System;

namespace MyPrescription.Account
{
    public partial class Hospitals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userIdSessionVariable.Value = Session["userId"].ToString();
            }
            catch (NullReferenceException)
            {
                Response.Redirect("../SignIn.aspx", false);
            }
        }
    }
}