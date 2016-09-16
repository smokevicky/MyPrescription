<%@ Page Title="" Language="C#" MasterPageFile="~/Account/Account.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs"
    Inherits="MyPrescription.Account.Profile" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Profile</title>
    <link href="../CSS/Account/Profile.css" rel="stylesheet" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper">
        
        <div class="wrapper-heading">
            <h3>Profile Details</h3>

            <ul class="nav nav-tabs" id="tabs">
              <li class="active"><a data-toggle="tab" href="#home" id="tabHome">Basic Details</a></li>
              <li><a data-toggle="tab" href="#profilePic" id="tabProfilePicture">Profile Picture</a></li>
              <li><a data-toggle="tab" href="#changePassword" id="tabChangePassword">Change Password</a></li>
            </ul>
        </div>        

        <div class="wrapper-body">                       

            <div class="tab-content">
              <div id="home" class="tab-pane fade in active">
                <h3>Basic Details</h3>
                <p>basic details here</p>
              </div>
              <div id="profilePic" class="tab-pane fade">
                  <div class="col-sm-6">
                    <h3>Current Profile Picture</h3>
                    <p>
                        <img id="profilePicVeryLarge" class="img-responsive img-circle" src="../Resources/ProfilePictures/default.jpg" runat="server" />
                    </p>
                  </div>
                  <div class="col-sm-6">
                      <h3>Change Profile Pic</h3>                      
                      <form id="profilePicForm" runat="server">
                          <div class="form-group">
                            <asp:FileUpload ID="changeProfilePic" runat="server" accept="image/*" required="required" />
                          </div>

                          <div class="form-group note-unmarked">
                              <div class="col-sm-2">
                                  Instructions:
                              </div>
                              <div class="col-sm-10">
                                  We will crop the image for you if the Image is not in the Ratio 1:1.
                                  <br />
                                  For best results, kindly try to upload a square size picture.
                                  <br />
                                  Max File Size : 4MB.
                                  <br />
                                  Only .jpg, .jpeg and .png files are allowed.
                              </div>                
                          </div>

                          <div class="form-group">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save" CssClass="btn btn-action" />
                              <input type="reset" value="Reset" class="btn btn-warning" />
                          </div>
                      </form>
                  </div>
              </div>
              <div id="changePassword" class="tab-pane fade">
                <h3>Change Password</h3>
                <p>pwd change here</p>
              </div>
            </div>

        </div>

        <div class="wrapper-footer">
            asdasd
        </div>

    </div>

    <script src="../Scripts/Account/Profile.js"></script>    
</asp:Content>
