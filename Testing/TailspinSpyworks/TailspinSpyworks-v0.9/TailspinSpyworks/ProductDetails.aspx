<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="TailspinSpyworks.ProductDetails" %>
<%@ Register src="Controls/AlsoPurchased.ascx" tagname="AlsoPurchased" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="FormView_Product" runat="server" DataKeyNames="ProductID" 
        DataSourceID="EDS_Product" 
        onpageindexchanging="FormView_Product_PageIndexChanging">
        <ItemTemplate>
        <div class="ContentHead"><%# Eval("ModelName") %></div><br />
        <table  border="0">
        <tr>
            <td style="vertical-align: top;">
             <img src='Catalog/Images/<%# Eval("ProductImage") %>'  border="0" alt='<%# Eval("ModelName") %>' />
            </td>
            <td style="vertical-align: top"><%# Eval("Description") %><br /><br /><br />
                <uc1:AlsoPurchased ID="AlsoPurchased1" runat="server" ProductId='<%# Eval("ProductID") %>' />   
            </td>
        </tr>
        </table>
        <span class="UnitCost"><b>Your Price:</b>&nbsp;<%# Eval("UnitCost", "{0:c}")%></span><br /><span class="ModelNumber"><b>Model Number:</b>&nbsp;<%# Eval("ModelNumber") %></span><br />
        <a href='AddToCart.aspx?ProductID=<%# Eval("ProductID") %>' style="border: 0 none white"><img src="~/Styles/Images/add_to_cart.gif" runat="server" alt="" style="border-width: 0" /></a><br />
        <br /><div class="SubContentHead">Reviews</div><br />
        <a id="ReviewList_AddReview" href="ReviewAdd.aspx?productID=<%# Eval("ProductID") %>">
           <img runat="server" style="vertical-align: bottom" src="~/Styles/Images/review_this_product.gif" alt="" />
        </a>       
        </ItemTemplate>
    </asp:FormView>
    <asp:EntityDataSource ID="EDS_Product" runat="server" AutoGenerateWhereClause="True"  EnableFlattening="False" 
                          ConnectionString="name=CommerceEntities" 
                          DefaultContainerName="CommerceEntities" 
                          EntitySetName="Products" 
                          EntityTypeFilter="" 
                          Select="" Where="">
                          <WhereParameters>
                                <asp:QueryStringParameter Name="ProductID" QueryStringField="productID"  Type="Int32" />
                          </WhereParameters>
    </asp:EntityDataSource>
    <asp:ListView ID="ListView_Comments" runat="server" DataKeyNames="ReviewID,ProductID,Rating" DataSourceID="EDS_CommentsList">
        <ItemTemplate>
            <tr style="background-color:#EDECB3;color: #000000;">
                <td><%# Eval("CustomerName") %></td>
                <td><img src='Styles/Images/ReviewRating_d<%# Eval("Rating") %>.gif' alt=""><br /></td>
                <td><%# Eval("Comments") %></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color:#F8F8F8;">
                <td><%# Eval("CustomerName") %></td>
                <td><img src='Styles/Images/ReviewRating_da<%# Eval("Rating") %>.gif' alt=""><br /></td>
                <td><%# Eval("Comments") %></td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr><td>There are no reviews yet for this product.</td></tr>
            </table>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                <th runat="server">Customer</th>
                                <th runat="server">Rating</th>
                                <th runat="server">Comments</th>
                            </tr>
                            <tr ID="itemPlaceholder" runat="server"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields><asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" /></Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:EntityDataSource ID="EDS_CommentsList" runat="server"  EnableFlattening="False" AutoGenerateWhereClause="True" EntityTypeFilter="" Select="" Where=""
                          ConnectionString="name=CommerceEntities" 
                          DefaultContainerName="CommerceEntities" 
                          EntitySetName="Reviews">
                          <WhereParameters>
                             <asp:QueryStringParameter Name="ProductID" QueryStringField="productID"  Type="Int32" />
                          </WhereParameters>
    </asp:EntityDataSource>

</asp:Content>
