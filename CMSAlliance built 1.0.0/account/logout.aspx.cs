using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[Constants.user_session] = null;
        Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["return"] ?? "~/account/login.aspx"));   
    }
}