<%@ Page Title="" Language="C#" MasterPageFile="~/NonAccount.Master" AutoEventWireup="true" CodeBehind="InvalidToken.aspx.cs" Inherits="MyPrescription.InvalidToken" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>LOST ???</title>
    <link href="CSS/InvalidToken.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
		<div class="row lost-row">
			    <!-- Article main content -->
			    <article class="col-xs-12 maincontent">

				    <header class="page-header">
					    <h1 class="page-title">Looks like you are lost..!!!</h1>
				    </header>
				
                    <div class="col-sm-12">
                        <h3>
                            Don't worry. We are taking you back into the home page
                            <img src="Resources/Images/InvalidToken.gif" height="200" />
                        </h3>
                        
                    </div>
				
			    </article>
			    <!-- /Article -->
		</div>
	</div>	<!-- /container -->
</asp:Content>
