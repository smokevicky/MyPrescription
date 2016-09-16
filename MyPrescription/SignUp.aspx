<%@ Page Title="" Language="C#" MasterPageFile="~/NonAccount.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="MyPrescription.SignUp" ClientIDMode="Static"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Register</title>
    <link href="CSS/SignUp.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- container -->
	<div class="container">

		<div class="row register-row">
			
			<!-- Article main content -->
			<article class="col-xs-12 maincontent">
				<header class="page-header">
					<h1 class="page-title">Registration</h1>
				</header>
				
				<div class="col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">
					<div class="panel panel-default">                        
						<div class="panel-body">
							<h3 class="thin text-center">Register a new account</h3>
							<p class="text-center text-muted">Register on My Prescription to be able to upload your prescriptions.<br />
                                Accidentally landed here?? Click on <a href="Default.aspx">LogIn</a> to head back.</p>
							<hr />

							<form method="post" runat="server">

								<div class="top-margin">
									<label>First Name <span class="text-danger">*</span></label>
									<input type="text" id="signupFname" class="form-control" maxlength="50" placeholder="First Name" required="required" runat="server" />
								</div>

								<div class="top-margin">
									<label>Last Name</label>
									<input type="text" id="signupLname" class="form-control" maxlength="50" placeholder="Last Name (Optional)" runat="server" />
								</div>

                                <div>
                                    <div id="popover-email-content" class="hidden">
                                        <div>
                                            Please verify that the email is in proper format i.e. abc@xyz.com                           
                                        </div>
                                    </div>

                                    <div id="popover-email-title" class="hidden">
                                        <b>Incorrect EMail</b>
                                    </div>
                                </div>

                                <div>
                                    <div id="popover-emailCheck-content" class="hidden">
                                        <img height='35' src='Resources/Images/ripple.gif' /> Please wait a moment...
                                    </div>

                                    <div id="popover-emailCheck-title" class="hidden">     
                                        <b>Please wait a moment...</b>
                                    </div>
                                </div>

								<div class="top-margin">
									<label id="popover-email" data-placement="left" >Email Address <span class="text-danger">*</span></label>
                                    <label id="popover-emailCheck" data-placement="right"></label>
									<input type="email" id="signupEmail" data-placement="right" data-trigger="manual" class="form-control" maxlength="50" placeholder="Email-Id" required="required" runat="server" />
								</div>

                                <div>
                                    <div id="popover-password-content" class="hidden">
                                        <div>
                                            1. Must be of atleast 8 characters <br />
                                            2. Max 30 characters are allowed <br />
                                            3. It may contain characters or digits or special characters                            
                                        </div>
                                    </div>

                                    <div id="popover-password-title" class="hidden">
                                        <b>Password must follow these</b>
                                    </div>
                                </div>

                                <div>
                                    <div id="popover-passwordConfirm-content" class="hidden">
                                        <div>
                                            Please verify that both the password match.                           
                                        </div>
                                    </div>

                                    <div id="popover-passwordConfirm-title" class="hidden">
                                        <b>Password must match</b>
                                    </div>
                                </div>

                                <div class="row">
								    <div class="top-margin">
								    	<div class="col-sm-6">
								    		<label data-placement="left" data-trigger="manual" id="popover-password"> Password <span class="text-danger">*</span></label>
								    		<input type="password" id="signupPwd" class="form-control" maxlength="30" placeholder="Password" required="required" runat="server" />
								    	</div>
                                    </div>
                                    <div class="top-margin">
								    	<div class="col-sm-6">
								    		<label data-placement="right" data-trigger="manual" id="popover-passwordConfirm">Confirm Password <span class="text-danger">*</span></label>
								    		<input type="password" id="signupPwdc" class="form-control" maxlength="30" placeholder="Confirm Password" required="required" runat="server" />
								    	</div>
								    </div>
                                </div>

								<hr />

								<div class="row">
									<div class="col-sm-8">
										<label class="checkbox">
											<input type="checkbox" id="signupCheckbox" required="required" value="0" /> 
											I've read the <a href="termsandconditions.aspx">Terms and Conditions</a>
										</label>                        
									</div>
									<div class="col-sm-4 text-right">
										<button class="btn btn-action" id="signupFrmSubmit" type="submit">Register</button> 
                                        <asp:Button ID="signupFrmSubmitServer" runat="server" Text="Button" class="hidden" Onclick="signupFrmSubmitServer_Click" />
									</div>
								</div>                                
							</form>
						</div>
					</div>

				</div>
				
			</article>
			<!-- /Article -->

		</div>
	</div>	<!-- /container -->

    <script src="Scripts/SignUp.js"></script>
</asp:Content>
