<%@ Page Title="" Language="C#" MasterPageFile="~/resources/masterpages/WebsiteMainTemplate.master"
    AutoEventWireup="true" CodeFile="vote.aspx.cs" Inherits="vote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="place_holder_content" runat="Server">
    <br />
    <div style="width: 100%">
        <img runat="server" id="new_member_logo" src="#" style="width: 120px; float: right;" />
        <h2>
            <asp:Label ID="lbl_header_name" runat="server" Text="Label"></asp:Label>
            has requested membership to the alliance.</h2>
        <div style="width: 400px; float: left;">
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td style="text-align: right">
                        Name :&nbsp;
                    </td>
                    <td>
                        <i>
                            <asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label></i>
                    </td>
                </tr>
                <span runat="server" id="company_block">
                    <tr>
                        <td style="text-align: right">
                            Company :&nbsp;
                        </td>
                        <td>
                            <asp:HyperLink ID="link_company_name" runat="server">HyperLink</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Company Website :&nbsp;
                        </td>
                        <td>
                            <asp:HyperLink ID="link_website" runat="server">HyperLink</asp:HyperLink>
                        </td>
                    </tr>
                </span>
                <tr>
                    <td style="text-align: right">
                        Email Address :&nbsp;
                    </td>
                    <td>
                        <asp:HyperLink ID="link_email" runat="server">HyperLink</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        Created :&nbsp;
                    </td>
                    <td>
                        <i>
                            <asp:Label ID="lbl_created" runat="server" Text="Label"></asp:Label></i>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 400px; float: right;">
            Doing a automatic search we found the following possible references for you to look
            at. Note that these might not be "Ron Grisnich".
            <br />
            <ul>
                <li>Facebook : http://www.facebook.com/ron.grisnich</li>
                <li>Linked In : http://www.linkedin.com/ron.grisnich</li>
            </ul>
        </div>
    </div>
    <div style="width: 100%; clear: both; text-align: center">
        <br />
        <asp:Button Width="180" CssClass="button bigrounded green" ID="Button1" runat="server"
            Text="Yes, my vote is yes" OnClick="Button1_Click" /><asp:Button CssClass="button bigrounded red"
                Width="180" ID="Button2" runat="server" Text="No! Never! No!" OnClick="Button2_Click" />
    </div>
</asp:Content>
