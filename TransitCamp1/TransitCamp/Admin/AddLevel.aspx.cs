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
    public partial class AddLevel : System.Web.UI.Page
    {
        protected ILevelServices levelServices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }
        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["LevelID"]);
            levelServices = new LevelServices(new TCContext());
            var getcitydetail = levelServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtLevel.Text = getcitydetail.LevelName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtLevel.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                levelServices = new LevelServices(new TCContext());

                Int32 id = Convert.ToInt32(Request.QueryString["LevelID"]);
                Int64 checkcityexist = levelServices.CheckAlreadyExist(txtLevel.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Level Already Exist.";
                }
                else
                {

                    Level info = new Level();
                    info.LevelName = txtLevel.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["LevelID"]);
                        info.UpdatedOn = DateTime.Now;
                        levelServices.Update(info);
                        levelServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'LevelList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        levelServices.Insert(info);
                        levelServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'LevelList';", true);
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