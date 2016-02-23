<%@ Page Title="" Language="C#" MasterPageFile="~/resources/masterpages/WebsiteMainTemplate.master"
    AutoEventWireup="true" CodeFile="members.aspx.cs" Inherits="members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="place_holder_content" runat="Server">
    <h3>
        All our Members</h3>
    <div style="" class="listingbasic">
        <ul>
            <asp:Repeater ID="list_repeater_members" runat="server">
                <ItemTemplate>
                    <li>
                        <h6>
                            <a href='<%# "~/member/" + Eval("id") %>' runat="server" class="bold">
                            <%#  
                            
                            Eval("company_name") == null ? Eval("firstname") + " " + Eval("lastname") : Eval("company_name")

                            %></a></h6>
                        <a href='<%# "~/member/" + Eval("id") %>' runat="server" class="thumb">
                            <img id="Img1" width="137" runat="server" src='<%# "" + ( (Eval("image") == null) ? "~/resources/img/logo.png" : "~/resources/uploads/" + Eval("image") ) %>' alt="" /></a>
                        <p>
                            <a href='<%# "~/member/" + Eval("id") %>' runat="server" style="color: White; width: 60px; margin-left: auto; margin-right: auto;"
                                class="button blue small">View Profile</a>
                        </p>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <asp:HyperLink ID="link_prev" runat="server">HyperLink</asp:HyperLink><asp:HyperLink
        ID="link_next" runat="server">HyperLink</asp:HyperLink>
</asp:Content>
