<%@ Page Title="" Language="C#" MasterPageFile="~/Account/Account.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" 
    Inherits="MyPrescription.Account.Dashboard" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dashboard</title>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    Dashboard    
    
    <input type="hidden" runat="server" id="nameSessionVariable" class="text-capitalize" />    

    <script src="../Scripts/Account/Dashboard.js"></script>
</asp:Content>
