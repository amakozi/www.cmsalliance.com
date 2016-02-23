<%@ Page Title="" Language="C#" MasterPageFile="~/resources/masterpages/WebsiteMainTemplate.master"
    AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="account_forgotpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="place_holder_content" runat="Server">
    <div id="main">
        <br />
        <br />
        <h1>Forgot Password?</h1>
        <div runat="server" id="msg_block" class="msg msg-error">
            <p></p>
        </div>
        <fieldset>
            <legend>Let us generate a random password for you!</legend>
            <br />
            <label for="txt_email">
                E-Mail: <span class="small">
                    <asp:Label ID="lbl_error_email" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox runat="server" ID="txt_email" ValidationGroup="forgotpassword_local_form" />
            <br />
            <span style="margin-left:
                80px;">
            <asp:Button ID="Button1" runat="server" ValidationGroup="forgotpassword_local_form" CssClass="button medium gray" Width="180"
                Text="Send me My Password" onclick="Button1_Click"   />
                </span>
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
