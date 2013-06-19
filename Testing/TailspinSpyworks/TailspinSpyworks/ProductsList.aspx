<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="ProductsList.aspx.cs" Inherits="TailspinSpyworks.ProductsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="ListView_Products" runat="server" 
                  DataKeynames="ProductID"
                  DataSourceID="EDS_ProductsByCategory"
                  GroupItemCount="2">
        <EmptyDataTemplate>
            <table runat="server">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td runat="server" />
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td runat="server">
                <table border="0" width="300">
                    <tr>
                        <td style="width: 25px;">&nbsp;</td>
                        <td style="vertical-align: middle; text-align: right;">
                            <a href='ProductDetails.aspx?productID=<%# Eval("ProductID") %>'>
                                <image src='Catalog/Images/Thumbs/<%# Eval("ProductImage") %>'
                                       width="100" height="75" border="0"></image>
                            </a>&nbsp;&nbsp;
                        </td>
                        <td style="width: 250px; vertical-align: middle;">
                            <a href='ProductDetails.aspx?productID=<%# Eval("ProductID") %>'><span
                                class="ProductListHead"><%# Eval("ModelName") %></span><br/>
                            </a>
                            <span class="ProductListItem">
                                <b>Special Price: </b><%# Eval("UnitCost", "{0:c}") %>
                            </span><br />
                            <a href='AddToCart.aspx?prductID=<%# Eval("ProductID") %>'>
                                <span class="ProductListItem"><b>Add To Cart</b></span>
                            </a>
                        </td>
                    </tr>
                </table>
            </td>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="groupPlaceholderContainer" runat="server">
                            <tr id="groupPlaceholder" runat="server"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server"><td runat="server"></td></tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:EntityDataSource ID="EDS_ProductsBycategory" runat="server" EnableFlattening="False" AutoGenerateWhereClause="True"
                          ConnectionString="name=CommerceEntities" DefaultcontainerName="CommerceEntities" EntitySetName="Products">
                          <WhereParameters>
                            <asp:QueryStringParameter Name="CategoryID" QueryStringField="CategoryId" Type="Int32" />
                          </WhereParameters>
    </asp:EntityDataSource>
</asp:Content>
