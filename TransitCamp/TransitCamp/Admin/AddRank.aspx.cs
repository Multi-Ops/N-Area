using DataAccessLayer;
using DataLayer;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Admin
{
    public partial class AddRank : System.Web.UI.Page
    {
        protected IRankServices rankServices;
        protected ICategoryServices categoryServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLevel();
                GetDetails();
            }
        }

        //Bind Level Dropdown
        protected void BindLevel()
        {
            categoryServices = new CategoryServices(new TCContext());
            var getlist = categoryServices.details();
            ddlLevel.DataSource = getlist;
            ddlLevel.DataValueField = "ID";
            ddlLevel.DataTextField = "CategoryName";
            ddlLevel.DataBind();
            ddlLevel.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["RankID"]);
            rankServices = new RankServices(new TCContext());
            var getdetail = rankServices.GetID(id);
            if (getdetail != null)
            {
                txtRank.Text = getdetail.Rank;
                ddlLevel.SelectedValue = getdetail.LevelID.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtRank.Text == "" || ddlLevel.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                rankServices = new RankServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["RankID"]);
                Int64 checkexist = rankServices.CheckAlreadyExist(txtRank.Text);
                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Rank Name Already Exist.";
                }
                else
                {

                    Ranks info = new Ranks();
                    info.Rank = txtRank.Text;
                    info.LevelID = Convert.ToInt64(ddlLevel.SelectedValue);
                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["RankID"]);
                        info.UpdatedOn = DateTime.Now;
                        rankServices.Update(info);
                        rankServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'RankList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        rankServices.Insert(info);
                        rankServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'RankList';", true);
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