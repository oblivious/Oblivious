<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EntityDataSourceDemo.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <div>
        Select a Sales Person:
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SalesPersonEDS"
            DataTextField="SalesPerson" DataValueField="SalesPerson">
        </asp:DropDownList>
        <asp:EntityDataSource ID="SalesPersonEDS" runat="server" ConnectionString="name=AdventureWorksLT2008R2Entities"
            DefaultContainerName="AdventureWorksLT2008R2Entities" EnableFlattening="False"
            EntitySetName="Customers" Select="DISTINCT it.[SalesPerson]">
        </asp:EntityDataSource>
        <br />
        <br />
        Filter:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        Customers:<br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" DisplayAfter="300" DynamicLayout="true">
                    <ProgressTemplate>
                        Data Loading... please wait...
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CustomerID"
                    DataSourceID="CustomersEDS">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                        <asp:BoundField DataField="MiddleName" HeaderText="MiddleName" SortExpression="MiddleName" />
                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                        <asp:BoundField DataField="Suffix" HeaderText="Suffix" SortExpression="Suffix" />
                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                        <asp:BoundField DataField="SalesPerson" HeaderText="SalesPerson" SortExpression="SalesPerson" />
                        <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:EntityDataSource ID="CustomersEDS" runat="server" ConnectionString="name=AdventureWorksLT2008R2Entities"
            DefaultContainerName="AdventureWorksLT2008R2Entities" EnableDelete="True" EnableFlattening="False"
            EnableInsert="True" EnableUpdate="True" EntitySetName="Customers">
        </asp:EntityDataSource>
        <asp:QueryExtender ID="CustomersQE" runat="server" TargetControlID="CustomersEDS">
            <asp:PropertyExpression>
                <asp:ControlParameter ControlID="DropDownList1" Name="SalesPerson" />
            </asp:PropertyExpression>
            <asp:SearchExpression SearchType="Contains" DataFields="CompanyName">
                <asp:ControlParameter ControlID="TextBox1" />
            </asp:SearchExpression>
            <asp:OrderByExpression DataField="LastName" Direction="Descending" />
        </asp:QueryExtender>
    </div>
    </form>
</body>
</html>
