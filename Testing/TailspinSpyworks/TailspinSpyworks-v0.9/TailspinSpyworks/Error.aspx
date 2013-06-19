<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TailspinSpyworks.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<center>
        <div class="ContentHead">ERROR</div><br /><br />
        <asp:Label ID="Label_ErrorFrom" runat="server" Text="Label"></asp:Label><br /><br />
        <asp:Label ID="Label_ErrorMessage" runat="server" Text="Label"></asp:Label><br /><br />
</center>
</asp:Content>
