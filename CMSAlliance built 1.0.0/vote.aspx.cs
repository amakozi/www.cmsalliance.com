using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class vote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["userid"] != null)
        {
            using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
            {
                int userid = 0;
                if (Int32.TryParse(Request["userid"].ToString(), out userid))
                {
                    var users = (from u in db.users where u.id == userid select u);
                    if (users.Count() > 0)
                    {
                        CmsAllianceModel.user user_obj = users.First<CmsAllianceModel.user>();
                        if (user_obj.approved.HasValue) Response.Redirect("~/"); //This user is already approved, no need for voting
                        else
                        {
                            //Not Approved, now we check if the current user has voted yet.
                            if (Session[Constants.user_session] != null)
                            {
                                CmsAllianceModel.user current_user_obj = (CmsAllianceModel.user)Session[Constants.user_session];

                                var logged_users = (from v in db.votes where v.voter == current_user_obj.id && v.votee == user_obj.id select v);
                                if (logged_users.Count() > 0)
                                {
                                    Response.Redirect("~/");
                                }
                                else
                                {
                                    init_page(user_obj, current_user_obj);
                                }
                            }
                            else Response.Redirect("~/account/login.aspx?return=" + Server.UrlEncode(Request.Url.ToString()));
                        }
                    }
                    else Response.Redirect("~/");
                }
                else Response.Redirect("~/");
            }
        }
        else Response.Redirect("~/");
    }

    public void redirct_to_home()
    {
        Response.Redirect("~/");
    }

    protected void init_page(CmsAllianceModel.user user_obj, CmsAllianceModel.user current_user_obj)
    {
        new_member_logo.Src = "~/" + ( (user_obj.image != null) ? "resources/uploads/" + user_obj.image : "resources/img/logo.png" );

        lbl_header_name.Text = user_obj.firstname + " " + user_obj.lastname;
        lbl_name.Text = user_obj.firstname + " " + user_obj.lastname;

        if (user_obj.company_name != null)
        {
            company_block.Visible = true;

            link_company_name.NavigateUrl = user_obj.company_website;
            link_company_name.Text = user_obj.company_name;

            link_website.NavigateUrl = user_obj.company_website;
            link_website.Text = user_obj.company_website;
        }
        else company_block.Visible = false;

        link_email.NavigateUrl = "mailto:" + user_obj.email;
        link_email.Text = user_obj.email;

        lbl_created.Text = user_obj.created.ToString();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //NO
        create_vote(false);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //YES
        create_vote(true);
    }

    public void create_vote(Boolean answer)
    {
        int userid = Convert.ToInt32(Request["userid"]);
        using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
        {
            CmsAllianceModel.user user_obj = (from u in db.users where u.id == userid select u).First<CmsAllianceModel.user>();
            CmsAllianceModel.user current_user_obj = (CmsAllianceModel.user)Session[Constants.user_session];

            #region Create Voting Record
            try
            {
                CmsAllianceModel.vote vote_obj = new CmsAllianceModel.vote();
                vote_obj.votee = user_obj.id;
                vote_obj.voter = current_user_obj.id;
                vote_obj.answer = answer;
                vote_obj.created = DateTime.Now;
                vote_obj.lastupdated = DateTime.Now;
                db.AddTovotes(vote_obj);
                db.SaveChanges();

                Session[Constants.voted_session] = vote_obj;
            }
            catch (Exception) { }
            #endregion

            #region Check if User is now approved

            //First we Count the total approved users in the Alliance
            int total_approved_members = (from u in db.users where u.approved != null && u.email_confirmed != null select u).Count();

            //Now we Get the list of all the votes that the user (THat's wating for approval has)
            List<CmsAllianceModel.vote> votes = (from v in db.votes where v.votee == user_obj.id select v).ToList<CmsAllianceModel.vote>();
            int answer_yes = 0, answer_no = 0;

            foreach (CmsAllianceModel.vote vote_obj in votes)
            {
                if (vote_obj.answer) answer_yes++;
                else answer_no++;
            }

            int current_voting_percentage = (answer_yes / total_approved_members) * 100;

            if (current_voting_percentage > Constants.approval_percentage)
            {
                //Their Approved!!!

                user_obj.approved = DateTime.Now;
                db.SaveChanges();

                //Send out the email
            }

            #endregion
        }

        
        Response.Redirect("~/voted.aspx");
    }
}