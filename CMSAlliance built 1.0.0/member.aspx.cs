using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class member : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int member_id = 0;
        if (Page.RouteData.Values["id"] == null) Response.Redirect("~/members.aspx");

        if (Int32.TryParse(Page.RouteData.Values["id"].ToString(), out member_id))
        {
            using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
            {
                var user_result = (from u in db.users where u.id == member_id select u);
                if (user_result.Count() > 0)
                {
                    CmsAllianceModel.user member_obj = user_result.First<CmsAllianceModel.user>();

                    //Is this a aproved user?
                    if (member_obj.approved == null || member_obj.email_confirmed == null)
                        Response.Redirect("~/members.aspx");

                    //Is the user logged-in ? Else they cannot view this profile
                    if (!member_obj.public_viewable && Session[Constants.user_session] == null)
                        Response.Redirect("~/account/login.aspx?return=" + Server.UrlEncode(Request.Url.ToString()));

                    init_page(member_obj);
                }
                else Response.Redirect("~/members");
            }
        }
        else Response.Redirect("~/members");
    }

    public void init_page(CmsAllianceModel.user member_obj)
    {
        lbl_name.Text = member_obj.firstname + " " + member_obj.lastname;
    }
}