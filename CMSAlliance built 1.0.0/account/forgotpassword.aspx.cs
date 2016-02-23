using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_forgotpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constants.user_session] != null) Response.Redirect("~/account/changepassword.aspx");
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

        #endregion

        if (validated)
        {
            using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
            {
                var users = (from u in db.users where u.email.ToLower() == txt_email.Text.ToLower() select u);
                if (users.Count() > 0)
                {
                    CmsAllianceModel.user user_obj = users.First<CmsAllianceModel.user>();

                    //Generate a Random Password
                    String new_random_password = Constants.RandomString(5);

                    //Send it out in the e-mail
                    user_obj.password = Constants.CalculateSHA1(new_random_password);
                    db.SaveChanges();

                    msg_block.Visible = true;
                    msg_block.Attributes["class"] = "msg msg-ok";
                    msg_block.InnerHtml = "<p>Your new password is on it's way! Check your INBOX</p>";

                    try
                    {
                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["from.email"]);
                        msg.ReplyToList.Add(new System.Net.Mail.MailAddress("admin@cmsalliance.com"));
                        msg.To.Add(user_obj.email);
                        msg.Subject = "E-Mail Confirmation";
                        msg.Body = "Your password has been reset to [" + new_random_password + "] without the brackets";

                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Send(msg);
                    }
                    catch (Exception) { }
                }
                else
                {
                    msg_block.Visible = true;
                    msg_block.Attributes["class"] = "msg msg-error";
                    msg_block.InnerHtml = "<p>No such Registered E-Mail!</p>";
                }
            }
        }
    }
}