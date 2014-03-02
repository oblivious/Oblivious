<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" %>

<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = TextBox1.Text + " added.";
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Task</h2>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Add..." onclick="Button1_Click" />
    <hr />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>

