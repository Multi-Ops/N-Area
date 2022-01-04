using BusinessLayer;
using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Admin
{
    public partial class AddCampList : System.Web.UI.Page
    {
        protected ICampServices campServices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }
        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["CampID"]);
            campServices = new CampServices(new TCContext());
            var getcitydetail = campServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtCamp.Text = getcitydetail.CampName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtCamp.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                campServices = new CampServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["CampID"]);
                Int64 checkcityexist = campServices.CheckAlreadyExist(txtCamp.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Camp Already Exist.";
                }
                else
                {

                    Camp info = new Camp();
                    info.CampName = txtCamp.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["CampID"]);
                        info.UpdatedOn = DateTime.Now;
                        campServices.Update(info);
                        campServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'CampList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        campServices.Insert(info);
                        campServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'CampList';", true);
                    }
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertUpdate();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            InsertUpdate();
        }
    }
}