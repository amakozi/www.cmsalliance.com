using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
        {
            Boolean is_logged_in = Session[Constants.user_session] != null;
            var members = (from u in db.users where u.approved != null && u.email_confirmed != null && (u.public_viewable || is_logged_in) orderby u.approved descending select u);
            int page = 0, perpage = 10;

            if (Page.RouteData.Values["page_number"] != null)
            {
                if (!Int32.TryParse(Page.RouteData.Values["page_number"].ToString(), out page)) Response.Redirect("~/members");
            }

            link_next.NavigateUrl = "~/members/" + (page + 1);
            link_next.Text = "Next";

            link_prev.NavigateUrl = "~/members/" + ((page - 1) > 0 ? page : 0);
            link_prev.Text = "Previous";

            list_repeater_members.DataSource = members.Skip(perpage * page).Take(perpage);
            list_repeater_members.DataBind();
        }
    }
}