<%@ Page Title="" Language="C#" MasterPageFile="~/resources/masterpages/WebsiteMainTemplate.master"
    AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="account_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="place_holder_content" runat="Server">
    <div id="main">
        <h4>
            Welcome back Hendrik Tredoux from Amakozi Technologies</h4>
            <p>Logged-In</p>
    </div>
    <div id="right">
        <br />
        <h3>
            Newest Members</h3>
        <asp:Repeater ID="repeat_list_newest_members_profile_page" runat="server">

            <ItemTemplate>
            
                <div class="quote">
            <div class="quotehead">
            </div>
            <div class="quotecontent">
                <a runat="server" href='<%# "~/member/" + Eval("id") %>'><b><%# (Eval("company_name") == null) ? Eval("firstname") + " " + Eval("lastname") : Eval("company_name") %></b></a><br />
                <img runat="server" src='<%# "" + ( (Eval("image") == null) ? "~/resources/img/logo.png" : "~/resources/uploads/" + Eval("image") ) %>' style="width: 120px; margin-left: 0px;" /><br />
                <label class="small"><%# Eval("email") %></label>
            </div>
            <div class="quotebottom">
            </div>
        </div>
        <br />

            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
