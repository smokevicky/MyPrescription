<%@ Page Title="" Language="C#" MasterPageFile="~/NonAccount.Master" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="MyPrescription.Verify" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
		<title>Email Verification</title>
	<link href="CSS/Verify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
		<div class="row verification-row">
				<!-- Article main content -->
				<article class="col-xs-12 maincontent">

					<header class="page-header">
						<h1 class="page-title">EMail Verification Successful</h1>
					</header>
				
					<div class="col-sm-12">
						<h3>Dear <span id="nameServer" runat="server" class="text-capitalize">xxx</span>,</h3>
						<p>You have successfully verified your EMail-Id i.e. <b id="emailServer" runat="server">xxx@xxx.xxx</b>.</p>
						<p>Registration details have been sent your registered Email-Id. Kindly check.</p>
						<p>Kindly note your User Identity Code : <span id="userIdServer" class="h3" runat="server">UI00XXXXXX</span> for future references.</p>
						<p>Head to the <a href="Default.aspx">LogIn</a> Page to LogIn into your account.</p>
					</div>
				
				</article>
				<!-- /Article -->
		</div>
	</div>	<!-- /container -->
</asp:Content>
