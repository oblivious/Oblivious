<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Styles/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="TailspinSpyworks.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div class="ContentHead">Log in to your account</div>
<br />
    <p>
        Please enter your username and password.
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" onloggedin="LoginUser_LoggedIn">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login" style="width: 100%">
                    <p>
                    <br />
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Remember My Sign-In Across Browser Restarts</asp:Label>
                    </p>
                <p class="submitButton">
                    <asp:ImageButton ID="LoginButton" runat="server" CommandName="Login" ValidationGroup="LoginUserValidationGroup" ImageUrl="~/Styles/Images/sign_in_now.gif" />
                </p>
                </fieldset>
                <p style="padding: 20px;">
                 If you are a new user and you don't have an account, then register for one now.  <br /><br />
                 <asp:ImageButton ID="RegisterHyperLink" runat="server" EnableViewState="false" 
                        ImageUrl="../Styles/Images/register.gif" PostBackUrl="~/Account/Register.aspx"></asp:ImageButton>
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
