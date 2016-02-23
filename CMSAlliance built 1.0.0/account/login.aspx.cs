using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            msg_block.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        msg_block.Visible = false;
        Boolean validated = true;
        System.Drawing.Color error_color = System.Drawing.Color.Red, default_color = System.Drawing.Color.Black;

        #region Validation

        lbl_error_email.ForeColor = (txt_email.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_email.ForeColor == default_color : false;

        if (lbl_error_email.ForeColor == default_color)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_email.Text, Constants.regex_valid_email))
            {
                lbl_error_email.ForeColor = default_color;
                lbl_error_email.Text = "Required";
            }
            else
            {
                lbl_error_email.ForeColor = error_color;
                lbl_error_email.Text = "Not Valid!";
                validated = false;
            }
        }

        lbl_error_password.ForeColor = (txt_password.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_password.ForeColor == default_color : false;

        #endregion

        if (validated)
        {
            using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
            {
                try
                {
                    CmsAllianceModel.user user_obj = (from u in db.users where u.email == txt_email.Text select u).First<CmsAllianceModel.user>();

                    if (Constants.CalculateSHA1(txt_password.Text) == user_obj.password)
                    {

                        //Ok they are logged-in, now we check to see if they have a activated account
                        //and if they have been approved by the members
                        if (!user_obj.email_confirmed.HasValue)
                        {
                            //Account is not yet activated!!?!?
                            msg_block.Visible = true;
                            msg_block.Attributes["class"] = "msg msg-error";
                            msg_block.InnerHtml = "<p>Account still needs to be Activated, check your Inbox</p>";
                        }
                        else if (!user_obj.approved.HasValue)
                        {
                            //Account is not yet activated!!?!?
                            msg_block.Visible = true;
                            msg_block.Attributes["class"] = "msg msg-error";
                            msg_block.InnerHtml = "<p>Members are still voting on your approval, please be patient!</p>";
                        }
                        else
                        {
                            //Login Succesfully
                            Session[Constants.user_session] = user_obj;

                            user_obj.lastlogin = DateTime.Now;
                            db.SaveChanges();

                            Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["return"] ?? "~/default.aspx"));
                        }

                    }
                    else
                    {
                        msg_block.Visible = true;
                        msg_block.Attributes["class"] = "msg msg-error";
                        msg_block.InnerHtml = "<p>Invalid Login Details</p>";
                    }
                }
                catch (Exception)
                {
                    msg_block.Visible = true;
                    msg_block.Attributes["class"] = "msg msg-error";
                    msg_block.InnerHtml = "<p>Invalid Login Details</p>";
                }
            }
        }
    }
}