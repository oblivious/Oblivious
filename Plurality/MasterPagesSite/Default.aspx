<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" 
		 Inherits="_Default" MasterPageFile="SiteTemplate.master" Trace="true"
		 Title="This is my home page" %>
<%@ MasterType VirtualPath="~/SiteTemplate.master" %>
<asp:Content runat="server" ID="mainContent" ContentPlaceHolderID="mainContentPlaceholder">
	<h1>This is the main page.</h1>
	<asp:TextBox runat="server" ID="_tb" />
	<br/>
	<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
	<br/>
	<asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
</asp:Content>
