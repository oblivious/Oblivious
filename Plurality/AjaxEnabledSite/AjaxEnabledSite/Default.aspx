<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AjaxEnabledSite.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="sm" runat="server">
<%--            <Scripts>
                <asp:ScriptReference Path="~/Scripts/Custom.js" />
            </Scripts>--%>
        </asp:ScriptManager>

        <span id="OutputSpan"></span>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="TimeLabel" runat="server" />

            <br />

            <asp:Button ID="SubmitButton" runat="server" onclick="SubmitButton_Click" 
                Text="Get Time" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
