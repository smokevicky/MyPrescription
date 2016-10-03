<%@ Page Title="" Language="C#" MasterPageFile="~/NonAccount.Master" AutoEventWireup="true" CodeBehind="EnterNewPassword.aspx.cs" 
	Inherits="MyPrescription.EnterNewPassword" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Enter New Password</title>
	<link href="CSS/EnterNewPassword.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
		<div class="row enterNew-row">
				<!-- Article main content -->
				<article class="col-xs-12 maincontent">

					<header class="page-header">
						<h1 class="page-title" id="HeaderText" runat="server">Enter New Password</h1>
					</header>			                    
										
					<div class="row">        
						<div class="col-sm-12">
							<h4 id="LargeText" runat="server">
							To confirm your identity, kindly enter your registered Email-Id.    
							</h4>
							<h4>
								<input type="hidden" id="token" value="<% Response.Write(Session["token"]); %>" />
							</h4>
						</div>
					</div>

					<div class="row" id="confirmIdentity" runat="server">
						<form>
							<div class="col-sm-8">         
								<div class="top-margin"></div>
								<input type="email" class="form-control" id="RegisteredEmail" required="required" maxlength="50" 
									placeholder="Enter registered Email-Id"/>  
								<div id="EmailOutputDiv"></div>
							</div>  
							<div class="col-sm-4">
								<div class="top-margin"></div>
								<input type="submit" value="Confirm" id="ConfirmBtn" class="btn btn-info" />
							</div>
						</form>
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

					<div class="row hidden" id="enterNewPassword-row">
						<form runat="server">
						<div class="col-sm-12">
							<div class="top-margin"></div>
							<input type="password" class="form-control" data-placement="auto top" data-trigger="manual" id="Password" 
								maxlength="30" required="required" placeholder="Enter New Password" runat="server" />
							<div class="top-margin"></div>
							<input type="password" class="form-control" data-placement="auto bottom" data-trigger="manual" id="confirmPassword" 
								maxlength="30" required="required" placeholder="Confirm New Password" runat="server" />
						</div>
						<div class="col-sm-12">
							<div class="top-margin"></div>
							<input type="submit" class="btn btn-action" id="ResetBtn" value="Reset" />
							<asp:Button ID="ResetBtnServer" runat="server" OnClick="ResetBtnServer_Click" CssClass="hidden" />                            
						</div>
						</form>
					</div>

					<div id="FinalOutputDiv" runat="server"></div>

				</article>
				<!-- /Article -->
		</div>
	</div>	<!-- /container -->
	<script src="Scripts/EnterNewPassword.js"></script>
</asp:Content>
