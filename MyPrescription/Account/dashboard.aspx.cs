using System;

namespace MyPrescription.Account
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            nameSessionVariable.Value = Session["FName"].ToString() + " " + Session["LName"].ToString();
            nameSessionVariable.Value = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(nameSessionVariable.Value);
        }
    }
}