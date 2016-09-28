using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace MyPrescription.Account
{
    public partial class Doctors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userIdSessionVariable.Value = Session["userId"].ToString();
                if (!this.IsPostBack)
                {
                    string constr = ConfigurationManager.ConnectionStrings["MyPrescriptionConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT HospitalId, Name FROM HospitalMaster WHERE UserId = '" + Session["userId"] + "' ORDER BY Name"))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            con.Open();
                            hospitalsList.DataSource = cmd.ExecuteReader();
                            hospitalsList.DataTextField = "Name";
                            hospitalsList.DataValueField = "HospitalId";
                            hospitalsList.DataBind();
                            con.Close();
                        }
                    }
                    hospitalsList.Items.Insert(0, new ListItem("--Select the Hospital the Doctor belongs to--", "0"));
                }
            }
            catch (NullReferenceException)
            {
                Response.Redirect("../SignIn.aspx", false);
            }
        }
    }
}