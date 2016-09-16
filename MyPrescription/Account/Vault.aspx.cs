using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPrescription.Account
{
    public partial class Vault : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["MyPrescriptionConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userIdSessionVariable.Value = Session["userId"].ToString();

                UploadFile(sender, e);

                if (!IsPostBack)
                {
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
                    hospitalsList.Items.Insert(0, new ListItem("--Select the related Hospital--", "0"));

                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT DoctorId, Name FROM DoctorMaster WHERE UserId = '" + Session["userId"] + "' ORDER BY Name"))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            con.Open();
                            doctorsList.DataSource = cmd.ExecuteReader();
                            doctorsList.DataTextField = "Name";
                            doctorsList.DataValueField = "DoctorId";
                            doctorsList.DataBind();
                            con.Close();
                        }
                    }
                    doctorsList.Items.Insert(0, new ListItem("--Select the related Doctor--", "0"));

                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT RecordId, Name FROM RecordTypeMaster ORDER BY Name"))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            con.Open();
                            typesList.DataSource = cmd.ExecuteReader();
                            typesList.DataTextField = "Name";
                            typesList.DataValueField = "RecordId";
                            typesList.DataBind();
                            con.Close();
                        }
                    }
                    typesList.Items.Insert(0, new ListItem("--Select the applicable Type of Prescription--", "0"));
                }
            }
            catch (NullReferenceException)
            {
                Response.Redirect("../SignIn.aspx", false);
            }
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            
        }
    }
}