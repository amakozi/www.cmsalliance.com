using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class confirm : System.Web.UI.Page
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
        if (txt_confirmation_code.Text.Trim().Length == 0)
        {
            msg_block.Visible = true;
            msg_block.Attributes["class"] = "msg msg-error";
            msg_block.InnerHtml = "<p>Confirmation Code Required</p>";
        }
        else
        {
            using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
            {
                try
                {
                    CmsAllianceModel.user user_obj = (from u in db.users where u.email_confirmation_code == txt_confirmation_code.Text select u).First<CmsAllianceModel.user>();

                    user_obj.email_confirmation_code = null;
                    user_obj.email_confirmed = DateTime.Now;

                    db.SaveChanges();

                    #region Send out all the voting e-mails to members!

                    foreach (CmsAllianceModel.user member_obj in (from u in db.users where u.approved != null && u.email_confirmed != null select u))
                    {
                        //Send the User their Confirmation E-Mail
                        try
                        {
                            System.Net.Mail.MailMessage msg = new MailMessage();
                            msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["from.email"]);
                            msg.ReplyToList.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["from.email"]));
                            msg.To.Add(member_obj.email);
                            msg.Subject = "Please Vote for " + member_obj.firstname + " " + member_obj.lastlogin;
                            msg.Body = "Please Vote for " + member_obj.firstname + " " + member_obj.lastlogin + " at http://www.cmsalliance.com/vote.aspx?userid=" + user_obj.id;

                            System.Net.Mail.SmtpClient smtp = new SmtpClient();
                            smtp.Send(msg);
                        }
                        catch (Exception) { }
                    }

                    #endregion

                    Response.Redirect("~/confirmed.aspx");
                }
                catch (Exception)
                {
                    msg_block.Visible = true;
                    msg_block.Attributes["class"] = "msg msg-error";
                    msg_block.InnerHtml = "<p>No Such Registered Code</p>";
                }
            }
        }
    }
}