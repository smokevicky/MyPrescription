using MyPrescription.Error;
using MyPrescription.Util;
using System;

using System.IO;
using System.Linq;

namespace MyPrescription.Account
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string profilePicPath = "../Resources/ProfilePictures/" + Session["userId"].ToString() + ".jpeg";

            if (File.Exists(Server.MapPath(profilePicPath)))
            {
                profilePicVeryLarge.Src = profilePicPath;
            }
        }

        /// <summary>
        /// Event fired after clicking on save button on change Profile Picture tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.changeProfilePic.HasFile)
                {
                    string fileExtension = Path.GetExtension(changeProfilePic.PostedFile.FileName).Substring(1);
                    
                    if (Constant.allowedFileTypes.Any(fileExtension.ToLower().Contains))
                    {
                        int fileSize = changeProfilePic.PostedFile.ContentLength;

                        if(fileSize < 2097152)
                        {
                            string path = Server.MapPath("../Resources/ProfilePictures/" + Session["userId"] + ".jpeg");
                            var tempFile = System.IO.Path.GetTempFileName();

                            changeProfilePic.SaveAs(tempFile);

                            //System.IO.File.Copy(path, tempFile, true);

                            System.Drawing.Image profilePic = System.Drawing.Image.FromFile(tempFile);

                            System.Drawing.Image newProfilePic = Common.FixedSize(profilePic, 300, 300, true);
                            profilePic.Dispose();

                            newProfilePic.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                            newProfilePic.Dispose();
                            
                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            File.Delete(tempFile);
                                                        
                            Common.Notify("Profile Picture updated successfully", "success");
                        }
                        else
                        {
                            Common.Notify("Max File Size is 2MB", "warning");                            
                            return;
                        }                        
                    }
                    else
                    {
                        Common.Notify("Wrong file type selected. Check Instructions", "danger");                                                
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ErrorCode.ProfilePAGE, ex.ToString(), Session["userId"].ToString());
            }
        }

    }
}