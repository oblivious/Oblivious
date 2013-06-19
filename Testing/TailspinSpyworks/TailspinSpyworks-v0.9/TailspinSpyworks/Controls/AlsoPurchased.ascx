﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlsoPurchased.ascx.cs" Inherits="TailspinSpyworks.Controls.AlsoPurchased" %>
<div style="width: 300px;">
<div class="MostPopularHead">
<asp:Label ID="LabelTitle" runat="server" Text=" Customers who bought this also bought:"></asp:Label></div>
<div id="PanelAlsoBoughtItems" style="padding: 10px; background-color:#EDECB3" runat="server">
    <asp:Repeater ID="RepeaterItemsList" runat="server">
       <HeaderTemplate></HeaderTemplate>
          <ItemTemplate>               
             <a class='MostPopularItemText' href='ProductDetails.aspx?productID=<%# Eval("ProductId") %>'><%# Eval("ModelName") %></a><br />              
          </ItemTemplate>
       <FooterTemplate></FooterTemplate>
    </asp:Repeater>
</div>
</div>
