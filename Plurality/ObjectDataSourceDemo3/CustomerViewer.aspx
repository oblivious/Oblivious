<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerViewer.aspx.cs" Inherits="CustomerViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Customer Viewer<asp:ScriptManager ID="sm" runat="server">
            </asp:ScriptManager>
        </h2>
        Select a Country:
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
            DataSourceID="CountriesODS" AppendDataBoundItems="True">
            <asp:ListItem Value="" Text="Select a Country" />
        </asp:DropDownList>
        <asp:ObjectDataSource ID="CountriesODS" runat="server" 
            SelectMethod="GetCountries" TypeName="BAL"></asp:ObjectDataSource>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="CustomersGridView" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="CustomerID" 
                    DataSourceID="CustomersODS" ForeColor="#333333" GridLines="None" 
                    onselectedindexchanged="CustomersGridView_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                            SortExpression="CustomerID" />
                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" 
                            SortExpression="CompanyName" />
                        <asp:BoundField DataField="ContactName" HeaderText="ContactName" 
                            SortExpression="ContactName" />
                        <asp:BoundField DataField="ContactTitle" HeaderText="ContactTitle" 
                            SortExpression="ContactTitle" />
                        <asp:BoundField DataField="Address" HeaderText="Address" 
                            SortExpression="Address" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList1" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="CustomersODS" runat="server" 
            SelectMethod="GetCustomersByCountry" TypeName="BAL">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" DefaultValue="NULL" 
                    Name="country" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="300" 
                    DynamicLayout="true" >
                    <ProgressTemplate>
                        <img src="Images/ajax-loader.gif" alt="Loading..."/>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                    CellPadding="4" DataSourceID="CustomerODS" ForeColor="#333333" GridLines="None" 
                    Height="50px" Width="125px" onprerender="DetailsView1_PreRender">
                    <AlternatingRowStyle BackColor="White" />
                    <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                    <Fields>
                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                            SortExpression="CustomerID" />
                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" 
                            SortExpression="CompanyName" />
                        <asp:BoundField DataField="ContactName" HeaderText="ContactName" 
                            SortExpression="ContactName" />
                        <asp:BoundField DataField="ContactTitle" HeaderText="ContactTitle" 
                            SortExpression="ContactTitle" />
                        <asp:BoundField DataField="Address" HeaderText="Address" 
                            SortExpression="Address" />
                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                        <asp:BoundField DataField="Region" HeaderText="Region" 
                            SortExpression="Region" />
                        <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" 
                            SortExpression="PostalCode" />
                        <asp:BoundField DataField="Country" HeaderText="Country" 
                            SortExpression="Country" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                        <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
                    </Fields>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                </asp:DetailsView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CustomersGridView" 
                    EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    
    </div>
        <asp:ObjectDataSource ID="CustomerODS" runat="server" 
            SelectMethod="GetCustomer" TypeName="BAL">
            <SelectParameters>
                <asp:ControlParameter ControlID="CustomersGridView" DefaultValue="NULL" 
                    Name="custID" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </form>
</body>
</html>
