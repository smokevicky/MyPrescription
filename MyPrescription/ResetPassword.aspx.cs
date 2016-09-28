using System;
using System.Net.Http;

namespace MyPrescription
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        string status;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckToken();            
        }

        private async void CheckToken()
        {
            string token = Request.QueryString["token"];

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/user/CheckStatusFromToken/" + token);
            if (response.IsSuccessStatusCode)
            {
                status = await response.Content.ReadAsStringAsync();
            }

            if (status.Equals("\"ForgotPassword\""))
            {
                Session["token"] = token;
                Response.Redirect("EnterNewPassword.aspx", false);
            }
            else
            {
                Response.Redirect("InvalidToken.aspx", false);
            }
        } 
    }
}