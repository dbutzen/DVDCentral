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
    public partial class Directors : System.Web.UI.Page
    {
        List<Director> directors;
        Director director;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                directors = new List<Director>();
                directors = DirectorManager.Load();
                Rebind();
                Session["directors"] = directors;
            }
            else
            {
                directors = (List<Director>)Session["directors"];
            }
        }

        private void Rebind()
        {
            ddlDirectors.DataSource = null;
            ddlDirectors.DataSource = directors;            
            ddlDirectors.DataTextField = "FullName";
            ddlDirectors.DataValueField = "Id";
            ddlDirectors.DataBind();
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
        }

        protected void ddlDirectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            director = directors[ddlDirectors.SelectedIndex];
            txtFirstName.Text = director.FirstName;
            txtLastName.Text = director.LastName;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                director = new Director();
                director.FirstName = txtFirstName.Text;
                director.LastName = txtLastName.Text;

                DirectorManager.Insert(director);
                directors.Add(director);
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
                int index = ddlDirectors.SelectedIndex;
                director = directors[ddlDirectors.SelectedIndex];

                director.FirstName = txtFirstName.Text;
                director.LastName = txtLastName.Text;

                DirectorManager.Update(director);
                directors[ddlDirectors.SelectedIndex] = director;
                Rebind();

                ddlDirectors_SelectedIndexChanged(sender, e);
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
                director = directors[ddlDirectors.SelectedIndex];

                DirectorManager.Delete(director.Id);

                directors.Remove(director);
                Rebind();


            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }
    }
}