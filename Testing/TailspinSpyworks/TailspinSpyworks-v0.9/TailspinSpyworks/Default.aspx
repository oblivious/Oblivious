<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true"  CodeBehind="Default.aspx.cs" Inherits="TailspinSpyworks._Default" %>
<%@ Register src="Controls/PopularItem.ascx" tagname="PopularItem" tagprefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
    <asp:LoginView ID="LoginView_VisitorGreeting" runat="server">
    <AnonymousTemplate>
      Welcome to the Store !
    </AnonymousTemplate>
        <LoggedInTemplate>
        Hi <asp:LoginName ID="LoginName_Welcome" runat="server" />. Thanks for coming back. 
        </LoggedInTemplate>
    </asp:LoginView>
    </h2>
	<p><strong>TailSpin Spyworks</strong> demonstrates how extraordinarily simple it is to create powerful, scalable applications for the .NET platform. </p>
    <table>
    <tr>
    <td style="width: 50%;">               
    <h3>Some Implementation Features.</h3>
	<ul>
		<li><a href="#">CSS Based Design.</a></li>
		<li><a href="#">Data Access via Linq to Entities.</a></li>
		<li><a href="#">MasterPage driven design.</a></li>
		<li><a href="#">Modern ASP.NET Controls User.</a></li>
		<li><a href="#">Integrated Ajac Control Toolkit Editor.</a></li>
	</ul>
    </td>
    <td style="width: 50%;">
        <img src="Content/Images/SampleProductImage.gif" alt=""/></td>
    </tr>
    </table>
    
    <table style="width: 600;">
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
    <td style="width: 300px; vertical-align: top;">               
        <uc1:PopularItem ID="PopularItem1" runat="server" />
    </td>
    <td>  
    <center><h3>Ecommerce the .NET 4 Way</h3></center>
   	<blockquote>
			<p>ASP.NET offers web developers the benefit of more that a decade of innovation. This demo leverages many of the latest features of ASP.NET development to illustrate you really simply building rish web applicatios with ASP.NET can be. For more informaiton about build web applications with ASP.NET please vists the community web site at www.asp.net</p>
	</blockquote>
    </td>
    </tr>
    </table>

	<h3>Spyworks Event Calendar</h3>
    <table style="width: 740px;">
		<tr class="rowH">
			<th>Date</th>
			<th>Title</th>
			<th>Description</th>
		</tr>
		<tr class="rowA">
			<td style="width: 120px">June 01, 2011</td>
			<td style="width: 200px">Sed vestibulum blandit</td>
			<td>Come and check out demos of all the newest Tailspin Spyworks products and experience them hands on.</td>
		</tr>
		<tr class="rowB">
			<td>November 28, 2011</td>
			<td>Spyworks Product Demo</td>
			<td>Come and check out demos of all the newest Tailspin Spyworks products and experience them hands on.</td>
		</tr>
		<tr class="rowA">
			<td>November 23, 2011</td>
			<td>Spyworks Product Demo</td>
            <td>Come and check out demos of all the newest Tailspin Spyworks products and experience them hands on.</td>
		</tr>
		<tr class="rowB">
			<td>November 21, 2011</td>
			<td>Spyworks Product Demo</td>
			<td>Come and check out demos of all the newest Tailspin Spyworks products and experience them hands on.</td>
		</tr>
	</table>

</asp:Content>
