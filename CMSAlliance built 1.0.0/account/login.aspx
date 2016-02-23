<%@ Page Title="" Language="C#" MasterPageFile="~/resources/masterpages/WebsiteMainTemplate.master"
    AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="account_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="place_holder_content" runat="Server">
    <div id="main">
        <br />
        <br />
        <h1>Login to your Account</h1>
        <div runat="server" id="msg_block" class="msg msg-error">
            <p>
                No Such Code Registered?!?!</p>
        </div>
        <fieldset>
            <legend>Login with your Details</legend>
            <br />
            <label for="txt_email">
                E-Mail: <span class="small">
                    <asp:Label ID="lbl_error_email" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox runat="server" ID="txt_email" ValidationGroup="login_local_form" />
            <br />
            <label for="txt_password">
                Password: <span class="small">
                    <asp:Label ID="lbl_error_password" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox TextMode="Password" runat="server" ID="txt_password" 
                ValidationGroup="login_local_form" />
            <br />
            <label></label>
            
            <asp:Button ID="Button1" runat="server" CssClass="button medium gray" 
                Width="100" Text="Login Now" onclick="Button1_Click" 
                ValidationGroup="login_local_form" />
        </fieldset>
        
    </div>
    <div id="right">
        <br />
        <br />
        <h4>
            What happens next?</h4>
        <p>
            After confirming the validity of your email address there will be a voting process.
            Once your membership request is approved, you will receive a email helping you complete
            the registration process.
        </p>
    </div>
</asp:Content>
