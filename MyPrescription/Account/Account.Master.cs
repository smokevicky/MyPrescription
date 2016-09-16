using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.IO;

namespace MyPrescription.Account
{
    public partial class Account : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("../SignIn.aspx", false);
            }
            else if (Session["isActive"].ToString() != "True")
            {
                Response.Redirect("../SignUpStep2.aspx", false);
            }
            else
            {               
                spanName.InnerText = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Session["FName"].ToString() + " " + Session["LName"].ToString());
                spanEmail.InnerText = Session["EMail"].ToString();
                if(Session["Phone"].ToString() != "")
                {
                    spanPhone.InnerText = Session["Phone"].ToString();
                }

                string profilePicPath = "../Resources/ProfilePictures/" + Session["userId"].ToString() + ".jpeg";

                if (File.Exists(Server.MapPath(profilePicPath)))
                {
                    ProfilePic.Src = profilePicPath;
                    ProfilePicLarge.Src = profilePicPath;
                }
            }
        }
    }
}