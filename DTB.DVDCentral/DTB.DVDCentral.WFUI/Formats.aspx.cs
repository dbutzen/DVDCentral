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
    public partial class Formats : System.Web.UI.Page
    {
        List<Format> formats;
        Format format;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formats = new List<Format>();
                formats = FormatManager.Load();
                Rebind();
                Session["formats"] = formats;
            }
            else
            {
                formats = (List<Format>)Session["formats"];
            }
        }

        private void Rebind()
        {
            ddlFormats.DataSource = null;
            ddlFormats.DataSource = formats;
            ddlFormats.DataTextField = "Description";
            ddlFormats.DataValueField = "Id";
            ddlFormats.DataBind();
            txtDescription.Text = string.Empty;
        }

        protected void ddlFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            format = formats[ddlFormats.SelectedIndex];
            txtDescription.Text = format.Description;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                format = new Format();
                format.Description = txtDescription.Text;

                FormatManager.Insert(format);
                formats.Add(format);
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
                int index = ddlFormats.SelectedIndex;
                format = formats[ddlFormats.SelectedIndex];

                format.Description = txtDescription.Text;

                FormatManager.Update(format);
                formats[ddlFormats.SelectedIndex] = format;
                Rebind();

                //ddlFormats_SelectedIndexChanged(sender, e);
                ddlFormats.SelectedIndex = index;
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
                format = formats[ddlFormats.SelectedIndex];

                FormatManager.Delete(format.Id);

                formats.Remove(format);
                Rebind();


            }
            catch (Exception ex)
            {

                Response.Write("Error: " + ex.Message);
            }
        }
    }
}