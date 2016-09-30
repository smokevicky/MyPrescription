<%@ Page Title="" Language="C#" MasterPageFile="~/NonAccount.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="MyPrescription.SignIn" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>My Prescription</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%    
	if (!IsPostBack) { Session["statusCode"] = MyPrescription.Util.SignInStatusCode.initial; }
%>

<!-- Header -->
	<div id="head">
		<div class="container-fluid">
			<div class="row">

				<div class="col-sm-7">
					<h1 class="lead">MY PRESCRIPTION</h1>
					<p class="tagline">my doctor my prescription</p>
					<p><a class="btn btn-default btn-lg" role="button" href="about.aspx#WhyAreYouHere">WHY ARE YOU HERE</a> <a class="btn btn-action btn-lg" role="button" href="signup.aspx">SIGN UP</a></p>
				</div>
				
				<div>
					<div id="popover-content" class="hidden">
						<div>
							1. Write email in the proper format i.e. abc@xyz.com <br />
							2. Password must be of atleast 8 characters                            
						</div>
					</div>

					<div id="popover-title" class="hidden">
						<b>Please correct the following</b>
					</div>
				</div>

				<% if((int)Session["statusCode"] == MyPrescription.Util.SignInStatusCode.invalid)
					{
						invalidText.Attributes["class"] = invalidText.Attributes["class"].Replace("hidden", "");
					}
				%>                
				
				<div class="col-sm-5">       

					<div id="invalidText" runat="server" class="hidden">
					<b> &#10007; Kindly double check your credentials<br/></b>
					</div>           
					 
					<div class="form-container">
					  <div class="form style-2">
						<div class="header">
						  <h1 class="active">Sign In</h1>   
						</div>

						<div id="frmDiv">
							<form class="active" method="post" runat="server">                              
							  <div class="form-group">                                
								<input id="username" type="text" placeholder="Email-id" required="required" maxlength="50" runat="server"  />
								<label for="username" data-placement="top" id="popover">Email-id</label>
								<div class="line"></div>
							  </div>
							  <div class="form-group">                                
								<input id="password" type="password" placeholder="Password" required="required" maxlength="30" runat="server"  />
								<label for="password">Password</label>                                
								<div class="line"></div>
							  </div>
							  <button id="logIn" class="button">Login</button>
							  <asp:Button ID="logInServer" class="logInServerClass" runat="server" Text="LogIn" OnClick="logInServer_Click" />
							 <div id="temp" runat="server" class="footer"><a href="ForgotPassword.aspx"><b>Forgot your password?</b></a></div>
							</form>  
						</div>

						<div id="loadingDiv"></div>             
					  </div>                      
					</div>

				</div>

			</div>
		</div>
	</div>
	<!-- /Header -->
	<script src="Scripts/SignIn.js"></script>
</asp:Content>
