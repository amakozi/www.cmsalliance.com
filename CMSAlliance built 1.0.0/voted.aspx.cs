using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class voted : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constants.voted_session] == null) Response.Redirect("~/");

        CmsAllianceModel.vote vote_obj = (CmsAllianceModel.vote)Session[Constants.voted_session];

        //Clear the Session Variable
        Session[Constants.voted_session] = null;

        using (CmsAllianceModel.CAEntities db = new CmsAllianceModel.CAEntities())
        {
            CmsAllianceModel.user user_obj = (from u in db.users where u.id == vote_obj.votee select u).First<CmsAllianceModel.user>();
            CmsAllianceModel.user current_user_obj = (CmsAllianceModel.user)Session[Constants.user_session];

            //First we Count the total approved users in the Alliance
            int total_approved_members = (from u in db.users where u.approved != null && u.email_confirmed != null select u).Count();

            //Now we Get the list of all the votes that the user (THat's wating for approval has)
            List<CmsAllianceModel.vote> votes = (from v in db.votes where v.votee == user_obj.id select v).ToList<CmsAllianceModel.vote>();
            int answer_yes = 0, answer_no = 0;

            foreach (CmsAllianceModel.vote loop_vote_obj in votes)
            {
                if (loop_vote_obj.answer) answer_yes++;
                else answer_no++;
            }

            int current_voting_percentage = (answer_yes / total_approved_members) * 100;

            System.Text.StringBuilder bldr = new System.Text.StringBuilder();

            bldr.Append("<h2>Thank your for Voting on " + user_obj.firstname + " " + user_obj.lastname + "</h2>");

            bldr.Append("<p>Our of a total of " + votes.Count + ", " + answer_yes + " were yes and " + answer_no + " were no</p>");
            bldr.Append("" + user_obj.firstname + " " + user_obj.lastname + " currently has " + current_voting_percentage + "% of the votes required to become a member. He needs " + Constants.approval_percentage + "% to become a member");
            
            lbl_page_content.Text = bldr.ToString();
        }
    }
}