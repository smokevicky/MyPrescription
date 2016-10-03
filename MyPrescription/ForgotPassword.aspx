<%@ Page Title="" Language="C#" MasterPageFile="~/NonAccount.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="MyPrescription.ForgotPassword" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Forgot Password</title>
	<link href="CSS/ForgotPassword.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
		<div class="row forgot-row">
				<!-- Article main content -->
				<article class="col-xs-12 maincontent">

					<header class="page-header">
						<h1 class="page-title">Forgot your password ???</h1>
					</header>
				
					<div class="col-sm-12">
						<form runat="server">
							<h4 id="LargeDiv">
								Don't worry.<br />Enter your registered Email-Id to recover your password.
							</h4>                        
							<div class="top-margin"></div>
							<p>
								<input type="email" class="form-control" id="ForgotEmail" required="required" runat="server"/>
								<div id="OutputDiv"></div>
							</p>        
							<p>
								<input type="submit" value="Recover" id="RecoverBtn" class="btn btn-action" />
								<asp:Button ID="SendMailBtn" OnClick="SendMailBtn_Click" runat="server" CssClass="hidden"/>
							</p>  
						</form>            
					</div>  
				
				</article>
				<!-- /Article -->
		</div>
	</div>	<!-- /container -->
	<script src="Scripts/ForgotPassword.js"></script>
</asp:Content>
