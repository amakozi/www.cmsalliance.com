<%@ Page Title="" Language="C#" MasterPageFile="~/resources/masterpages/WebsiteMainTemplate.master"
    AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="place_holder_content" runat="Server">
    <div id="main">
        <script type="text/javascript">
            $(window).load(function () {
                $('#slider').nivoSlider();
            });
        </script>
        <br />
        <br />
        <br />
        <div style="z-index: -1;" id="slider" class="nivoSlider">
            <a href="members.htm">
                <img runat="server" src="~/resources/img/banner1.png" /></a> <a href="register.htm">
                    <img runat="server" src="~/resources/img/banner2.png" /></a> <a href="register.htm">
                        <img runat="server" src="~/resources/img/banner3.png" /></a>
        </div>
    </div>
    <div id="right">
        <h2>
            Become a member
            <span class="citing">Already a member? <a id="A1"
                runat="server" href="~/account/login.aspx">Login Here</a></span>
            
            </h2>
            
        <hr />
        <fieldset>
            <legend>Your Details</legend>
            <br />
            <label for="frontpage_register_details_firstname">
                Firstname: <span class="small"><asp:Label ID="lbl_error_user_firstname" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox ValidationGroup="frontpage_register_form" runat="server" ID="txt_user_firstname" />
            <label for="frontpage_register_details_lastname">
                Last Name: <span class="small"><asp:Label ID="lbl_error_user_lastname" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox ValidationGroup="frontpage_register_form" runat="server" ID="txt_user_lastname" />
            <label for="frontpage_register_details_email">
                E-Mail: <span class="small">
                    <asp:Label ID="lbl_error_user_email" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox ValidationGroup="frontpage_register_form" runat="server" ID="txt_user_email" />
            <label for="frontpage_register_employement">
                Employement: <span class="small"><asp:Label ID="lbl_error_user_employment" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:DropDownList ValidationGroup="frontpage_register_form" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ID="dropdown_employment" />
            <label for="frontpage_register_details_password">
                Password: <span class="small"><asp:Label ID="lbl_error_user_password" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox ValidationGroup="frontpage_register_form" runat="server" TextMode="Password" ID="txt_password" />
            <label for="frontpage_register_details_confirm_password">
                Confirm Password: <span class="small"><asp:Label ID="lbl_error_user_confirm_password" runat="server" Text="Label">Required</asp:Label>
            </span>
            &nbsp;</label><asp:TextBox ValidationGroup="frontpage_register_form"  runat="server" TextMode="Password" ID="txt_confirm_password" />
        </fieldset>
        <fieldset runat="server" id="fieldset_company_details">
            <legend>Company Details</legend>
            <br />
            <label for="frontpage_register_company_name">
                Company Name: <span class="small"><asp:Label ID="lbl_error_company_name" runat="server" Text="Label">Required</asp:Label></span>
            </label>
            <asp:TextBox ValidationGroup="frontpage_register_form" ID="txt_new_company_name" runat="server"></asp:TextBox>
            <label for="frontpage_register_company_website"></label>

            <label for="frontpage_register_company_name">
                    Company Website: <span class="small"><asp:Label ID="lbl_error_company_website" runat="server" Text="Label">Required</asp:Label></span>
                </label>
                <asp:TextBox ValidationGroup="frontpage_register_form" ID="txt_company_website" runat="server"></asp:TextBox>
            
                <label for="frontpage_register_company_name">
                    Company Size: <span class="small"><asp:Label ID="lbl_error_company_size" runat="server" Text="Label">Required</asp:Label></span>
                </label>
                <asp:DropDownList ValidationGroup="frontpage_register_form" runat="server" ID="dropdown_company_size" />
        </fieldset>
        <asp:Button runat="server" ValidationGroup="frontpage_register_form" CssClass="button medium gray" Text="Register" 
            Width="100" onclick="Unnamed1_Click" />
        <hr />
    </div>
</asp:Content>
