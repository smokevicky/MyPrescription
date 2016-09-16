<%@ Page Title="" Language="C#" MasterPageFile="~/NonAccount.Master" AutoEventWireup="true" CodeBehind="SignUpStep2.aspx.cs" Inherits="MyPrescription.SignUpStep2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registration Step 2</title>
    <link href="CSS/SignUpStep2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top-margin"></div>    
        <div class="container">
		    <div class="row register-row">
			
			    <!-- Article main content -->
			    <article class="col-xs-12 maincontent">

				    <header class="page-header">
					    <h1 class="page-title">Awaiting Email Verification</h1>
				    </header>
				
                    <div class="col-sm-12">
                        <p>You have successfully completed the 1st Step of <b>Registration</b>.</p>
                        <p>A mail with the verification message has been sent to your registered EMail-ID : <b><% Response.Write(Session["email"]); %></b>.</p>
                        <p>Kindly check your Email and click on the <button class="btn btn-info">VERIFY</button> button to activate your account.</p>
                    </div>
				
			    </article>
			    <!-- /Article -->
		</div>
	</div>	<!-- /container -->
</asp:Content>
