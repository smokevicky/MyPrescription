<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error_Error"   %>    

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta charset="utf-8" />
	<meta name="viewport"    content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="My Doctor. My Prescription." />
	<meta name="author"      content="Jyoti Prakash Jena" />
	
	<title>Error</title>

	<link rel="shortcut icon" href="assets/images/logo.png" />		
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
	<link href="../CSS/SignIn.css" rel="stylesheet" />
</head>
<body id="errorBody">
	<div class="container-fluid">
		<div class="row"><br /></div>
		<div class="row">
			<div class="col-sm-6 text-center">
				  <h1 id="oppsJumbo">Oops!</h1>
				  <h2>That was unexpected..!!!</h2>

				  -------------------------------------------------------------------------

				  <form runat="server">
				  -------------------------------------------------------------------------
				  <h4 runat="server">Error Code :  <% Response.Write(Session["errorCode"]); %></h4>
				  <h4 runat="server">Error      :  <% Response.Write(Session["error"]); %></h4>
				  -------------------------------------------------------------------------
				  </form>

				  <h3>The admin has been informed.</h3>
				  <h2>Kindly head back to the <a href="../SignIn.aspx">Home Page</a></h2>
			</div>
			<div class="col-sm-6 text-center">
				<img src="../Resources/Images/404.gif" />
			</div>
		</div>
	</div>
	

</body>
</html>
