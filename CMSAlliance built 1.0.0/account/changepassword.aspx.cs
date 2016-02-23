using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_changepassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constants.user_session] == null) Response.Redirect("~/account/login.aspx?return=" + Server.UrlEncode(Request.Url.ToString()));
        else
        {
            if (!Page.IsPostBack)
            {
                msg_block.Visible = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        msg_block.Visible = false;
        Boolean validated = true;
        System.Drawing.Color error_color = System.Drawing.Color.Red, default_color = System.Drawing.Color.Black;

        #region Validation

        lbl_error_password.ForeColor = (txt_password.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_password.ForeColor == default_color : false;

        lbl_error_confirm_password.ForeColor = (txt_confirm_password.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_confirm_password.ForeColor == default_color : false;

        if (txt_password.Text.Length > 0 && txt_confirm_password.Text.Length > 0)
        {
            if (txt_password.Text != txt_confirm_password.Text)
            {
                msg_block.Visible = true;
                msg_block.Attributes["class"] = "msg msg-error";
                msg_block.InnerHtml = "<p>Passwords do not Match!</p>";
                validated = false;
            }
        }

        #endregion

        if (validated)
        {
            using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
            {
                CmsAllianceModel.user current_user_obj = (CmsAllianceModel.user)Session[Constants.user_session];
                var users = (from u in db.users where u.id == current_user_obj.id select u);
                if (users.Count() > 0)
                {
                    CmsAllianceModel.user user_obj = users.First<CmsAllianceModel.user>();
                    user_obj.password = Constants.CalculateSHA1(txt_password.Text);
                    db.SaveChanges();

                    msg_block.Visible = true;
                    msg_block.Attributes["class"] = "msg msg-ok";
                    msg_block.InnerHtml = "<p>Your Password has been Changed Successfully!</p>";

                    try
                    {
                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["from.email"]);
                        msg.ReplyToList.Add(new System.Net.Mail.MailAddress("admin@cmsalliance.com"));
                        msg.To.Add(user_obj.email);
                        msg.Subject = "Password Change";
                        msg.Body = "Just being pro-active and information you that your account`s password was changed, if this was not you contact us Immediatly!";

                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Send(msg);
                    }
                    catch (Exception) { }
                }
                else
                {
                    //Ummmm... Something went very wrong.
                    //Send them to logout
                    Response.Redirect("~/account/logout.aspx");
                }
            }
        }
    }
}