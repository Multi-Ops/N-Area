﻿using DataAccessLayer;
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
    public partial class CharterADEntery : System.Web.UI.Page
    {
        protected ICityServices cityServices;
        protected ICharterDetailsServices charterDetailsServices;
        protected IRankServices rankServices;
        protected IUnitServices unitServices;
        protected IDivisionServices divisionServices;
        protected IHeadquarterServices headquarterServices;
        protected IMoveServices moveServices;
        protected IPriorityServices priorityServices;
        protected IADTypeServices aDTypeServices;
        protected ICategoryServices categoryServices;
        protected ICampServices campServices;
        protected IPriorityStatusServices priorityStatusServices;
        protected ICharterADServices charterADServices;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRank();
                BindDiv();
                BindUnit();
                BindCity();
                BindHQ();
                BindMove();
                BindPriority();
                BindCategory();
                BindCamp();
                PriorityStatus();
                CharterNo();
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

        //bind Division
        protected void BindDiv()
        {
            divisionServices = new DivisionServices(new TCContext());
            var getlist = divisionServices.GetDivtDetails();
            ddlDiv.DataSource = getlist;
            ddlDiv.DataValueField = "ID";
            ddlDiv.DataTextField = "DivName";
            ddlDiv.DataBind();
            ddlDiv.Items.Insert(0, new ListItem("-- Select --", ""));
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
        protected void BindCategory()
        {
            categoryServices = new CategoryServices(new TCContext());
            var getlist = categoryServices.details();
            ddlCategory.DataSource = getlist;
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Camp
        protected void BindCamp()
        {
            campServices = new CampServices(new TCContext());
            var getlist = campServices.GetCampDetails();
            ddlCamp.DataSource = getlist;
            ddlCamp.DataValueField = "ID";
            ddlCamp.DataTextField = "CampName";
            ddlCamp.DataBind();
            ddlCamp.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Priority Status
        protected void PriorityStatus()
        {
            priorityStatusServices = new PriorityStatusServices(new TCContext());
            var getlist = priorityStatusServices.GetPriorityStatusDetails();
            ddlPStatus.DataSource = getlist;
            ddlPStatus.DataValueField = "ID";
            ddlPStatus.DataTextField = "PStatusName";
            ddlPStatus.DataBind();
            ddlPStatus.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind CharterNo
        protected void CharterNo()
        {
            charterDetailsServices = new CharterDetailsServices(new TCContext());
            var getlist = charterDetailsServices.GetAllCharterNo();
            ddlCharterNo.DataSource = getlist;
            ddlCharterNo.DataValueField = "ID";
            ddlCharterNo.DataTextField = "CharterNo";
            ddlCharterNo.DataBind();
            ddlCharterNo.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["CADID"]);
            charterADServices = new CharterADServices(new TCContext());
            var getdetails = charterADServices.GetByID(id);
            if (getdetails != null)
            {
                ddlCategory.SelectedValue = getdetails.CategoryID.ToString();
                ddlCity.SelectedValue = getdetails.CityID.ToString();
                ddlRank.SelectedValue = getdetails.RankID.ToString();
                ddlUnit.SelectedValue = getdetails.UnitID.ToString();
                ddlDiv.SelectedValue = getdetails.DivID.ToString();
                ddlHQ.SelectedValue = getdetails.HQID.ToString();
                ddlMove.SelectedValue = getdetails.MoveID.ToString();
                ddlPriority.SelectedValue = getdetails.PriorityID.ToString();
                ddlPStatus.SelectedValue = getdetails.PriorityStatusID.ToString();
                ddlCamp.SelectedValue = getdetails.CampID.ToString();
                ddlCharterNo.SelectedItem.Text = getdetails.CharterNO;
                txtSeatNo.Text = getdetails.SeatNo.ToString();
                txtDate.Text = getdetails.Date.ToString();
                hfDate.Value = getdetails.Date.ToString();
                txtICard.Text = getdetails.ICard;
                txtArmyNo.Text = getdetails.ArmyNo;
                txtName.Text = getdetails.Name;
                DateTime fdate = Convert.ToDateTime(getdetails.FlightDate);
                var finalfdate = fdate.ToString("dd-mm-yyyy");
                txtFightDate.Text = finalfdate;
                hfFlightDate.Value = getdetails.FlightDate.ToString();
                if (getdetails.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else
                {
                    ddlSession.SelectedValue = "2";
                }
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {

            if (ddlCategory.SelectedValue.ToString() == "" || ddlCity.SelectedValue.ToString() == "" || ddlRank.SelectedValue.ToString() == ""
                || ddlUnit.SelectedValue.ToString() == "" || ddlDiv.SelectedValue.ToString() == "" || ddlHQ.SelectedValue.ToString() == "" || ddlSession.SelectedValue.ToString() == "0"
                || ddlMove.SelectedValue.ToString() == "" || ddlPriority.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                charterADServices = new CharterADServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["CADID"]);
                BusinessLayer.CharterADEntery info = new BusinessLayer.CharterADEntery();
                info.CategoryID = Convert.ToInt64(ddlCategory.SelectedValue);
                info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                info.RankID = Convert.ToInt64(ddlRank.SelectedValue);
                info.Date = Convert.ToDateTime(hfDate.Value);
                info.ICard = txtICard.Text;
                info.ArmyNo = txtArmyNo.Text;
                info.Name = txtName.Text;
                info.UnitID = Convert.ToInt64(ddlUnit.SelectedValue);
                info.DivID = Convert.ToInt64(ddlDiv.SelectedValue);
                info.HQID = Convert.ToInt64(ddlHQ.SelectedValue);
                info.MoveID = Convert.ToInt64(ddlMove.SelectedValue);
                info.PriorityID = Convert.ToInt64(ddlPriority.SelectedValue);
                info.Session = ddlSession.SelectedItem.Text;
                info.CharterNO = ddlCharterNo.SelectedItem.Text;
                info.FlightDate = Convert.ToDateTime(hfFlightDate.Value);
                info.PriorityStatusID = Convert.ToInt64(ddlPStatus.SelectedValue);
                info.CampID = Convert.ToInt64(ddlCamp.SelectedValue);
                info.SeatNo = Convert.ToInt64(txtSeatNo.Text);

                if (id != 0)
                {
                    info.ID = Convert.ToInt32(Request.QueryString["CADID"]);
                    info.UpdatedOn = DateTime.Now;
                    charterADServices.Update(info);
                    charterADServices.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'CharterADList';", true);
                }
                else
                {
                    info.CreatedOn = DateTime.Now;
                    charterADServices.Insert(info);
                    charterADServices.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'CharterADList';", true);
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