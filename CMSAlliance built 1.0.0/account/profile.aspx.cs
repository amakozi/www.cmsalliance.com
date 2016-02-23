using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
        {
            CmsAllianceModel.user user_obj = (CmsAllianceModel.user)Session[Constants.user_session];
            repeat_list_newest_members_profile_page.DataSource = (from u in db.users where u.approved != null && u.email_confirmed != null && u.id != user_obj.id orderby u.approved descending select u).Take(4);
            repeat_list_newest_members_profile_page.DataBind();
        }
    }
}