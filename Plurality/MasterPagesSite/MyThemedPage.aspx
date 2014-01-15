<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyThemedPage.aspx.cs" Inherits="MyThemedPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
		<br/>
		<asp:TextBox ID="TextBox2" SkinID="barTextBox" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
