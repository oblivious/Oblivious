<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopularItem.ascx.cs" Inherits="TailspinSpyworks.Controls.PopularItem" %>
<%@ OutputCache Duration="3600" VaryByParam="None" %>
<div class="MostPopularHead">Our most popular items this week</div>
<div id="PanelPopularItems" style="padding: 10px;" runat="server">
    <asp:Repeater ID="RepeaterItemsList" runat="server">
       <HeaderTemplate></HeaderTemplate>
          <ItemTemplate>               
             <a class='MostPopularItemText' href='ProductDetails.aspx?productID=<%# Eval("ProductId") %>'><%# Eval("ModelName") %></a><br />              
          </ItemTemplate>
       <FooterTemplate></FooterTemplate>
    </asp:Repeater>
</div>
