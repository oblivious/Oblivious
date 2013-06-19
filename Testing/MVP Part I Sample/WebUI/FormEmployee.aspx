<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormEmployee.aspx.cs" Inherits="WebUI.FormEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add-Employee</title>
</head>
<body>
    <form id="formEmployee" runat="server">
    <div>
        <h1>Employee Form</h1>
    
        <table>
            <tr>
                <td width="150px">
                    Firstname</td>
                <td>
                    <asp:TextBox ID="TextBoxFirstname" runat="server"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td>
                    Lastname</td>
                <td>
                    <asp:TextBox ID="TextBoxLastname" runat="server"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td>
                    Employee Type</td>
                <td>
                    <asp:DropDownList ID="DropDownListEmployeeType" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="DropDownListEmployeeType_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            
            <tr>
                <td>
                    Salary</td>
                <td>
                    <asp:DropDownList ID="DropDownListSalary" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSalary_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            
            <tr>
                <td>
                    TAX</td>
                <td>
                    <asp:TextBox ID="TextBoxTAX" runat="server" ReadOnly="true">0.10</asp:TextBox></td>
            </tr>
            
            <tr>
                <td style="height: 21px">
                    Tax Amount</td>
                <td style="height: 21px">
                    <asp:TextBox ID="TextBoxTaxAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                    <asp:Label ID="LabelErrorMessage" runat="server" ForeColor="Red" Text="[Error Message]"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
                    <input type="button" onclick="document.location='Default.aspx'" value="Cancel" /></td>
            </tr>
            
        </table>
    
    
    </div>
    </form>
</body>
</html>
