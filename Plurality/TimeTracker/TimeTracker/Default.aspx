<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TimeTracker.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="ObjectDataSource1" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                    SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" 
                    SortExpression="LastName" />
                <asp:BoundField DataField="Department" HeaderText="Department" 
                    SortExpression="Department" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetEmployees" TypeName="TimeTracker.Models.TimeTrackerRepository">
        </asp:ObjectDataSource>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataSourceID="ObjectDataSource2" 
            onselectedindexchanged="GridView2_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="SubmissionDate" HeaderText="Date" 
                    SortExpression="SubmissionDate" />
                <asp:BoundField DataField="MondayHours" HeaderText="Monday" 
                    SortExpression="MondayHours" />
                <asp:BoundField DataField="TuesdayHours" HeaderText="Tuesday" 
                    SortExpression="TuesdayHours" />
                <asp:BoundField DataField="WednesdayHours" HeaderText="Wednesday" 
                    SortExpression="WednesdayHours" />
                <asp:BoundField DataField="ThursdayHours" HeaderText="Thursday" 
                    SortExpression="ThursdayHours" />
                <asp:BoundField DataField="FridayHours" HeaderText="Friday" 
                    SortExpression="FridayHours" />
                <asp:BoundField DataField="SaturdayHours" HeaderText="Saturday" 
                    SortExpression="SaturdayHours" />
                <asp:BoundField DataField="SundayHours" HeaderText="Sunday" 
                    SortExpression="SundayHours" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
            SelectMethod="GetEmployeeTimeCards" 
            TypeName="TimeTracker.Models.TimeTrackerRepository">
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" DefaultValue="0" Name="id" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
