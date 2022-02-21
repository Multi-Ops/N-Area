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
    public partial class ADTDEntry : System.Web.UI.Page
    {
        protected ICityServices cityServices;
        protected IADTDServices adTDServices;
        protected IRankServices rankServices;
        protected IUnitServices unitServices;
        protected IHeadquarterServices headquarterServices;
        protected IMoveServices moveServices;
        protected IPriorityServices priorityServices;
        protected ILevelServices levelServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRank();
                BindUnit();
                BindCity();
                BindHQ();
                BindMove();
                BindPriority();
                BindLevel();
                GetDetails();
            }
        }

        //bind Rank
        protected void BindRank()
        {
            rankServices = new RankServices(new TCContext());
            var getlist = rankServices.GetRankDetails();
            ddlRank.DataSource = getlist;
            ddlRank.DataValueField = "ID";
            ddlRank.DataTextField = "Rank";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind city
        protected void BindCity()
        {
            cityServices = new CityServices(new TCContext());
            var getlist = cityServices.GetCityDetails();
            ddlCity.DataSource = getlist;
            ddlCity.DataValueField = "ID";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Unit
        protected void BindUnit()
        {
            unitServices = new UnitServices(new TCContext());
            var getlist = unitServices.GetUnitDetails();
            ddlUnit.DataSource = getlist;
            ddlUnit.DataValueField = "ID";
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind HQ
        protected void BindHQ()
        {
            headquarterServices = new HeadquarterServices(new TCContext());
            var getlist = headquarterServices.GetHQDetails();
            ddlHQ.DataSource = getlist;
            ddlHQ.DataValueField = "ID";
            ddlHQ.DataTextField = "HQName";
            ddlHQ.DataBind();
            ddlHQ.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind M,Move
        protected void BindMove()
        {
            moveServices = new MoveServices(new TCContext());
            var getlist = moveServices.GetMoveDetails();
            ddlMove.DataSource = getlist;
            ddlMove.DataValueField = "ID";
            ddlMove.DataTextField = "MoveName";
            ddlMove.DataBind();
            ddlMove.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Priority 
        protected void BindPriority()
        {
            priorityServices = new PriorityServices(new TCContext());
            var getlist = priorityServices.GetPriorityDetails();
            ddlPriority.DataSource = getlist;
            ddlPriority.DataValueField = "ID";
            ddlPriority.DataTextField = "PriorityName";
            ddlPriority.DataBind();
            ddlPriority.Items.Insert(0, new ListItem("-- Select --", ""));
        }


        //bind Category
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


        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["ADTDID"]);
            adTDServices = new ADTDServices(new TCContext());
            var getdetails = adTDServices.GetByID(id);
            if (getdetails != null)
            {
                ddlLevel.SelectedValue = getdetails.LavelID.ToString();
                ddlCity.SelectedValue = getdetails.CityID.ToString();
                ddlRank.SelectedValue = getdetails.RankID.ToString();
                ddlUnit.SelectedValue = getdetails.UnitID.ToString();
                ddlHQ.SelectedValue = getdetails.HQID.ToString();
                ddlMove.SelectedValue = getdetails.MoveID.ToString();
                ddlPriority.SelectedValue = getdetails.PriorityID.ToString();
                txtDate.Text = getdetails.OutDate.ToString();
                hfDate.Value = getdetails.OutDate.ToString();
                txtICard.Text = getdetails.ICard;
                txtArmyNo.Text = getdetails.ArmyNo;
                txtName.Text = getdetails.Name;
                if (getdetails.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else
                {
                    ddlSession.SelectedValue = "2";
                }
                txtRemark.Text = getdetails.Remark;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {

            if (ddlLevel.SelectedValue.ToString() == "" || ddlCity.SelectedValue.ToString() == "" || ddlRank.SelectedValue.ToString() == ""
                || ddlUnit.SelectedValue.ToString() == "" || ddlHQ.SelectedValue.ToString() == "" || ddlSession.SelectedValue.ToString() == "0"
                || ddlMove.SelectedValue.ToString() == "" || ddlPriority.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                adTDServices = new ADTDServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["ADTDID"]);
                BusinessLayer.ADTDEntry info = new BusinessLayer.ADTDEntry();
                info.LavelID = Convert.ToInt64(ddlLevel.SelectedValue);
                info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                info.RankID = Convert.ToInt64(ddlRank.SelectedValue);
                info.OutDate = Convert.ToDateTime(hfDate.Value);
                info.ICard = txtICard.Text;
                info.ArmyNo = txtArmyNo.Text;
                info.Name = txtName.Text;
                info.UnitID = Convert.ToInt64(ddlUnit.SelectedValue);
                info.HQID = Convert.ToInt64(ddlHQ.SelectedValue);
                info.MoveID = Convert.ToInt64(ddlMove.SelectedValue);
                info.PriorityID = Convert.ToInt64(ddlPriority.SelectedValue);
                info.Session = ddlSession.SelectedItem.Text;
                info.Remark = txtRemark.Text;

                if (id != 0)
                {
                    info.ID = Convert.ToInt32(Request.QueryString["ADTDID"]);
                    info.UpdatedOn = DateTime.Now;
                    adTDServices.Update(info);
                    adTDServices.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'ADTDList';", true);
                }
                else
                {
                    info.CreatedOn = DateTime.Now;
                    adTDServices.Insert(info);
                    adTDServices.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'ADTDList';", true);
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