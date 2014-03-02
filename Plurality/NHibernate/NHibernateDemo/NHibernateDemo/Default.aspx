<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="NHibernateDemo._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:BulletedList ID="CustomersList" runat="server">
    </asp:BulletedList>
    <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
    <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
    <asp:Button ID="AddButton" runat="server" Text="Add" 
        onclick="AddButton_Click" />
    <br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1"
        runat="server" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataSourceID="ObjectDataSource1" ForeColor="Black" GridLines="Horizontal">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" 
                SortExpression="LastName" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="NHibernateDemo.Code.Customer" DeleteMethod="DeleteCustomer" 
        InsertMethod="AddCustomer" SelectMethod="GetCustomersList" 
        TypeName="NHibernateDemo.Code.Customers" >
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
            <asp:Parameter Name="firstName" Type="String" />
            <asp:Parameter Name="lastName" Type="String" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
