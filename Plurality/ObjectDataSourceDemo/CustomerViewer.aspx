<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerViewer.aspx.cs"
    Inherits="ObjectDataSourceDemo.CustomerViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Select a Country:<asp:DropDownList ID="DropDownList1" runat="server" 
            AutoPostBack="True" />
    </div>
    
        <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    
        <br />

    </form>
</body>
</html>
