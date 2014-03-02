<%@ Control Language="C#" CodeFile="DateTime_Edit.ascx.cs" Inherits="DateTime_EditField" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=TextBox1.ClientID %>').datepicker();
    });
</script>

<asp:TextBox ID="TextBox1" runat="server" CssClass="DDTextBox" 
Text='<%# (FieldValueEditString != "") ? String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(FieldValueEditString)) : "" %>' Columns="20"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" />
<asp:CustomValidator runat="server" ID="DateValidator" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />

