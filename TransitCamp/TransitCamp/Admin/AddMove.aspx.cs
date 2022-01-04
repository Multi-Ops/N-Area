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
    public partial class AddMove : System.Web.UI.Page
    {
        protected IMoveServices moveServices;
        protected ILevelServices levelServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLevel();
                GetAirlineDetails();
            }
        }

        //bind Level
        protected void BindLevel()
        {
            levelServices = new LevelServices(new TCContext());
            var getlist = levelServices.GetLevel();
            ddlLevel.DataSource = getlist;
            ddlLevel.DataValueField = "ID";
            ddlLevel.DataTextField = "LevelName";
            ddlLevel.DataBind();
            ddlLevel.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["MoveID"]);
            moveServices = new MoveServices(new TCContext());
            var getcitydetail = moveServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtMove.Text = getcitydetail.MoveName;
                ddlLevel.SelectedValue = getcitydetail.CategoryID.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtMove.Text == "" || ddlLevel.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                moveServices = new MoveServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["MoveID"]);
                Int64 checkcityexist = moveServices.CheckAlreadyExist(txtMove.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Move Name Already Exist.";
                }
                else
                {

                    Move move = new Move();
                    move.MoveName = txtMove.Text;
                    move.CategoryID = Convert.ToInt64(ddlLevel.SelectedValue);

                    if (id != 0)
                    {
                        move.ID = Convert.ToInt32(Request.QueryString["MoveID"]);
                        move.UpdatedOn = DateTime.Now;
                        moveServices.Update(move);
                        moveServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'MoveList';", true);
                    }
                    else
                    {
                        move.CreatedOn = DateTime.Now;
                        moveServices.Insert(move);
                        moveServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'MoveList';", true);
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