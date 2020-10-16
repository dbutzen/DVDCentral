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
    public partial class Genres : System.Web.UI.Page
    {
        List<Genre> genres;
        Genre genre;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                genres = new List<Genre>();
                genres = GenreManager.Load();
                Rebind();
                Session["genres"] = genres;
            }
            else
            {
                genres = (List<Genre>)Session["genres"];
            }
        }

        private void Rebind()
        {
            ddlGenres.DataSource = null;
            ddlGenres.DataSource = genres;
            ddlGenres.DataTextField = "Description";
            ddlGenres.DataValueField = "Id";
            ddlGenres.DataBind();
            txtDescription.Text = string.Empty;
        }

        protected void ddlGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            genre = genres[ddlGenres.SelectedIndex];
            txtDescription.Text = genre.Description;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                genre = new Genre();
                genre.Description = txtDescription.Text;

                GenreManager.Insert(genre);
                genres.Add(genre);
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
                int index = ddlGenres.SelectedIndex;
                genre = genres[ddlGenres.SelectedIndex];

                genre.Description = txtDescription.Text;

                GenreManager.Update(genre);
                genres[ddlGenres.SelectedIndex] = genre;
                Rebind();

                //ddlGenres_SelectedIndexChanged(sender, e);
                ddlGenres.SelectedIndex = index;
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
                genre = genres[ddlGenres.SelectedIndex];

                GenreManager.Delete(genre.Id);

                genres.Remove(genre);
                Rebind();


            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }
    }
}