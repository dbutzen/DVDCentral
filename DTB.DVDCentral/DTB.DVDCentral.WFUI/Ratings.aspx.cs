using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;

namespace DTB.DVDCentral.WFUI
{
    public partial class Ratings : System.Web.UI.Page
    {
        List<Rating> ratings;
        Rating rating;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ratings = new List<Rating>();
                ratings = RatingManager.Load();
                Rebind();
                Session["ratings"] = ratings;
            }
            else
            {
                ratings = (List<Rating>)Session["ratings"];
            }
        }

        private void Rebind()
        {
            ddlRatings.DataSource = null;
            ddlRatings.DataSource = ratings;
            ddlRatings.DataTextField = "Description";
            ddlRatings.DataValueField = "Id";
            ddlRatings.DataBind();
            txtDescription.Text = string.Empty;
        }

        protected void ddlRatings_SelectedIndexChanged(object sender, EventArgs e)
        {
            rating = ratings[ddlRatings.SelectedIndex];
            txtDescription.Text = rating.Description;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                rating = new Rating();
                rating.Description = txtDescription.Text;

                RatingManager.Insert(rating);
                ratings.Add(rating);
                Rebind();
            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int index = ddlRatings.SelectedIndex;
                rating = ratings[ddlRatings.SelectedIndex];

                rating.Description = txtDescription.Text;

                RatingManager.Update(rating);
                ratings[ddlRatings.SelectedIndex] = rating;
                Rebind();

                //ddlRatings_SelectedIndexChanged(sender, e);
                ddlRatings.SelectedIndex = index;
            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                rating = ratings[ddlRatings.SelectedIndex];

                RatingManager.Delete(rating.Id);

                ratings.Remove(rating);
                Rebind();


            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }
    }
}