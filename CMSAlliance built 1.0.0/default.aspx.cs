using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class _default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constants.user_session] != null) Server.Transfer("~/account/profile.aspx");
        else
        {

            if (!Page.IsPostBack)
            {
                dropdown_company_size.Items.Add(new ListItem("Me, Myself and I", "alone"));
                dropdown_company_size.Items.Add(new ListItem("Less than 5 of us", "small"));
                dropdown_company_size.Items.Add(new ListItem("Less than 10 of us", "medium"));
                dropdown_company_size.Items.Add(new ListItem("Lots of Employees", "large"));

                dropdown_employment.Items.Add(new ListItem("Freelancer", "freelancer"));
                dropdown_employment.Items.Add(new ListItem("Company Representative", "permanent"));

                fieldset_company_details.Visible = false;
            }
        }
    }

    public void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fieldset_company_details.Visible = (dropdown_employment.SelectedValue.ToLower() == "permanent");
    }

    protected void register_in_forum()
    {
    }

    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        Boolean validated = true;
        System.Drawing.Color error_color = System.Drawing.Color.Red, default_color = System.Drawing.Color.Black;

        #region Validate First and Last name

        lbl_error_user_firstname.ForeColor = (txt_user_firstname.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_user_firstname.ForeColor == default_color : false;

        lbl_error_user_lastname.ForeColor = (txt_user_lastname.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_user_lastname.ForeColor == default_color : false;

        #endregion

        #region E-Mail Handling

        lbl_error_user_email.ForeColor = (txt_user_email.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_user_email.ForeColor == default_color : false;

        if (lbl_error_user_email.ForeColor == default_color)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_user_email.Text, Constants.regex_valid_email))
            {
                lbl_error_user_email.ForeColor = default_color;
                lbl_error_user_email.Text = "Required";
            }
            else
            {
                lbl_error_user_email.ForeColor = error_color;
                lbl_error_user_email.Text = "Not Valid!";
                validated = false;
            }
        }

        if (lbl_error_user_email.ForeColor == default_color)
        {
            using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
            {
                try
                {
                    CmsAllianceModel.user user_obj = (from u in db.users where u.email.Trim().ToLower() == txt_user_email.Text.ToLower() select u).First<CmsAllianceModel.user>();
                    //Whoops already have that E-Mail in the database
                    lbl_error_user_email.ForeColor = error_color;
                    lbl_error_user_email.Text = "Not Available";
                    validated = false;
                }
                catch (Exception)
                {
                    //E-Mail is available

                    lbl_error_user_email.ForeColor = default_color;
                    lbl_error_user_email.Text = "Required";
                }
            }
        }

        #endregion

        #region Password Handling

        lbl_error_user_password.ForeColor = (txt_password.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_user_password.ForeColor == default_color : false;

        lbl_error_user_confirm_password.ForeColor = (txt_confirm_password.Text.Trim().Length == 0) ? error_color : default_color;
        validated = (validated) ? lbl_error_user_confirm_password.ForeColor == default_color : false;

        if (validated)
        {
            if (txt_password.Text != txt_confirm_password.Text)
            {
                lbl_error_user_confirm_password.ForeColor = error_color;
                lbl_error_user_confirm_password.Text = "Does not Match!";
                validated = false;
            }
            else
            {
                lbl_error_user_confirm_password.ForeColor = default_color;
                lbl_error_user_confirm_password.Text = "Required";
            }
        }

        #endregion

        #region Handle New Company Entries

        if (dropdown_employment.SelectedValue.ToLower() == "permanent")
        {
            //They want to add a Company
            lbl_error_company_name.ForeColor = (txt_new_company_name.Text.Trim().Length == 0) ? error_color : default_color;
            validated = (validated) ? lbl_error_company_name.ForeColor == default_color : false;

            #region Check to see if company name available

            if (lbl_error_company_name.ForeColor == default_color)
            {
                using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
                {
                    try
                    {
                        CmsAllianceModel.user company_obj = (from com in db.users where com.company_name.Trim().ToLower() == txt_new_company_name.Text.ToLower() select com).First<CmsAllianceModel.user>();
                        //Whoops already have that company!
                        lbl_error_company_name.ForeColor = error_color;
                        lbl_error_company_name.Text = "Not Available";
                        validated = false;
                    }
                    catch (Exception)
                    {
                        //Company Name is available

                        lbl_error_company_name.ForeColor = default_color;
                        lbl_error_company_name.Text = "Required";
                    }
                }
            }

            #endregion

            //They want to add a Company
            lbl_error_company_website.ForeColor = (txt_company_website.Text.Trim().Length == 0) ? error_color : default_color;
            validated = (validated) ? lbl_error_company_website.ForeColor == default_color : false;

            #region Validate URL

            if (lbl_error_company_website.ForeColor == default_color)
            {
                if (System.Uri.IsWellFormedUriString(txt_company_website.Text, UriKind.Absolute))
                {
                    lbl_error_company_website.ForeColor = default_color;
                    lbl_error_company_website.Text = "Required";
                }
                else
                {
                    lbl_error_company_website.ForeColor = error_color;
                    lbl_error_company_website.Text = "Not Valid!";
                    validated = false;
                }
            }

            #endregion

            #region Check to see if we can use that website

            if (lbl_error_company_website.ForeColor == default_color)
            {
                using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
                {
                    try
                    {
                        CmsAllianceModel.user company_obj = (from com in db.users where com.company_website.Trim().ToLower() == txt_company_website.Text.ToLower() select com).First<CmsAllianceModel.user>();
                        //Whoops already have that company!
                        lbl_error_company_website.ForeColor = error_color;
                        lbl_error_company_website.Text = "Not Available";
                        validated = false;
                    }
                    catch (Exception)
                    {
                        //Company Name is available

                        lbl_error_company_name.ForeColor = default_color;
                        lbl_error_company_name.Text = "Required";
                    }
                }
            }

            #endregion
        }

        #endregion

        #region Handle Valid Form Post

        if (validated)
        {
            try
            {
                using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
                {
                    //Create the user account
                    CmsAllianceModel.user new_user = new CmsAllianceModel.user();
                    new_user.email_confirmation_code = Constants.RandomString(10);

                    if (dropdown_employment.SelectedValue.ToLower() == "permanent")
                    {
                        new_user.company_name = txt_new_company_name.Text;
                        new_user.company_website = txt_company_website.Text;

                        int company_size = 1;
                        switch (dropdown_company_size.SelectedValue)
                        {
                            default: company_size = 1; break;
                            case "small": company_size = 2; break;
                            case "medium": company_size = 3; break;
                            case "large": company_size = 4; break;
                        }

                        new_user.company_size = company_size;
                    }
                    else
                    {
                        new_user.company_name = null;
                        new_user.company_website = null;
                        new_user.company_size = null;
                    }

                    new_user.email = txt_user_email.Text;
                    new_user.password = Constants.CalculateSHA1(txt_password.Text);
                    new_user.email_confirmed = null;
                    new_user.firstname = txt_user_firstname.Text;
                    new_user.lastname = txt_user_lastname.Text;
                    new_user.created = DateTime.Now;
                    new_user.approved = null;
                    new_user.lastlogin = null;
                    new_user.lastupdated = DateTime.Now;

                    db.AddTousers(new_user);

                    //Insert and get ID
                    db.SaveChanges();

                    //Send the User their Confirmation E-Mail
                    try
                    {
                        System.Net.Mail.MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["from.email"]);
                        msg.ReplyToList.Add(new MailAddress("admin@cmsalliance.com"));
                        msg.To.Add(new_user.email);
                        msg.Subject = "E-Mail Confirmation";
                        msg.Body = "Please Confirm your E-Mail by typing the Code: " + new_user.email_confirmation_code + " at http://www.cmsalliance.com/confirm.aspx";

                        System.Net.Mail.SmtpClient smtp = new SmtpClient();
                        smtp.Send(msg);
                    }
                    catch (Exception) { }

                    Response.Redirect("~/confirm.aspx");
                }

                //Register in the User Forum
                register_in_forum();
            }
            catch (Exception) { }
        }

        #endregion
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
    }
}