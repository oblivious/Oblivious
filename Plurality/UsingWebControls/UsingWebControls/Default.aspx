<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UsingWebControls.Default" %>

<%@ Register src="UserControls/Header.ascx" tagname="Header" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style3
        {
            width: 68px;
        }
    </style>
</head>
<body>
    <form id="form1" DefaultFocus="FirstNameTextBox" DefaultButton="SubmitButton" runat="server">
    <div>
        
        <table class="style1">
            <tr>
                <td class="style2" colspan="2">
                    <uc1:Header ID="Header1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    First Name</td>
                <td>
                    <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="FirstNameTextBox" ErrorMessage="Please enter first name">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Last Name</td>
                <td>
                    <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="LastNameTextBox" ErrorMessage="Please enter last name">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Birthday</td>
                <td>
                    <asp:TextBox ID="BirthdayTextBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="BirthdayTextBox" ErrorMessage="Please enter a birthday">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="BirthdayTextBox" ErrorMessage="Please enter a valid date" 
                        Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Email</td>
                <td>
                    <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="EmailTextBox" ErrorMessage="Enter a valid email" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    County</td>
                <td>
                    <asp:DropDownList ID="CountyDropDown" runat="server" Width="130px">
                        <asp:ListItem Value="">Select One</asp:ListItem>
                        <asp:ListItem>Clare</asp:ListItem>
                        <asp:ListItem>Cork</asp:ListItem>
                        <asp:ListItem>Dublin</asp:ListItem>
                        <asp:ListItem>Galway</asp:ListItem>
                        <asp:ListItem>Laois</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="CountyDropDown" ErrorMessage="Please select a county">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                    <br />
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" 
                        onclick="SubmitButton_Click" />
                    <br />
                    <br />
                    <asp:Label ID="OutputLabel" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
