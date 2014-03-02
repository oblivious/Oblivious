<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayTasks.aspx.cs" Inherits="DemoWAP.DisplayTasks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul>
        <asp:ListView ID="ListOfTasks" runat="server">
            <ItemTemplate>
            <li runat="server">
                <asp:Label runat="server"><%#Eval("taskName")%></asp:Label>
            </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>
</asp:Content>
