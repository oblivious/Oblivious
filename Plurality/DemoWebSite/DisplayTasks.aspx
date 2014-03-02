<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="id" DataSourceID="SqlDataSource1" 
    EmptyDataText="There are no data records to display." AllowSorting="True" 
        BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
        CellPadding="3" CellSpacing="1" GridLines="None">
    <Columns>
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
            SortExpression="id" />
        <asp:BoundField DataField="taskName" HeaderText="taskName" 
            SortExpression="taskName" />
        <asp:CheckBoxField DataField="isComplete" HeaderText="isComplete" 
            SortExpression="isComplete" />
    </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:TestDBConnectionString1 %>" 
    DeleteCommand="DELETE FROM [Tasks] WHERE [id] = @id" 
    InsertCommand="INSERT INTO [Tasks] ([taskName], [isComplete]) VALUES (@taskName, @isComplete)" 
    ProviderName="<%$ ConnectionStrings:TestDBConnectionString1.ProviderName %>" 
    SelectCommand="SELECT [id], [taskName], [isComplete] FROM [Tasks]" 
    UpdateCommand="UPDATE [Tasks] SET [taskName] = @taskName, [isComplete] = @isComplete WHERE [id] = @id">
    <DeleteParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="taskName" Type="String" />
        <asp:Parameter Name="isComplete" Type="Boolean" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="taskName" Type="String" />
        <asp:Parameter Name="isComplete" Type="Boolean" />
        <asp:Parameter Name="id" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
</asp:Content>

