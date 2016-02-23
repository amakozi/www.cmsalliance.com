<%@ Page Title="" Language="C#" MasterPageFile="~/resources/masterpages/WebsiteMainTemplate.master"
    AutoEventWireup="true" CodeFile="confirm.aspx.cs" Inherits="confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="place_holder_content" runat="Server">
    <div id="main">
        <br />
        <br />
        <h1>
            Step 2: Confirm you email address</h1>
        <p>
            We send you a email containing a code to your personal email address. Please enter
            the code below to confirm your email address.
        </p>

        <br />

        <div runat="server" id="msg_block" class="msg msg-error"><p>No Such Code Registered?!?!</p></div>

        Enter email confirmation code:
        <asp:TextBox ValidationGroup="email_confirm_group" ID="txt_confirmation_code" runat="server"></asp:TextBox>
        &nbsp;
        <span style="padding-left: 190px;">
            <asp:Button ValidationGroup="email_confirm_group" Width="100" ID="Button1" runat="server" CssClass="button medium gray" 
            Text="Confirm" onclick="Button1_Click" />
        </span>
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
