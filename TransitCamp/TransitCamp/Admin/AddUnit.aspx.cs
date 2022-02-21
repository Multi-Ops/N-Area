using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DataAccessLayer;
using DataLayer;
using BusinessLayer;
using System.Web.UI.WebControls;

namespace TransitCamp.Admin
{
    public partial class AddUnit : System.Web.UI.Page
    {
        protected IUnitServices unitServices;
        protected IHeadquarterServices headquarterServices;
        protected IDivisionServices divisionServices;
        protected ICityServices cityServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDiv();
                BindCity();
                BindHQ();
                GetDetails();
                BindBrigade();
            }
        }

        //bind headquarter
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

        //bind City
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

        //bind Brigade
        protected void BindBrigade()
        {
            divisionServices = new DivisionServices(new TCContext());
            var getlist = divisionServices.GetBrigDetails();
            ddlBrigade.DataSource = getlist;
            ddlBrigade.DataValueField = "Id";
            ddlBrigade.DataTextField = "Name";
            ddlBrigade.DataBind();
            ddlBrigade.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["UnitID"]);
            unitServices = new UnitServices(new TCContext());
            var getdetail = unitServices.GetID(id);
            if (getdetail != null)
            {
                txtUnit.Text = getdetail.UnitName;
                ddlHQ.SelectedValue = getdetail.HQID.ToString();
                ddlDiv.SelectedValue = getdetail.DivID.ToString();
                ddlCity.SelectedValue = getdetail.CityID.ToString();
                ddlBrigade.SelectedValue = getdetail.BrigadeID.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtUnit.Text == "" || ddlDiv.SelectedValue.ToString() == "" || ddlHQ.SelectedValue.ToString() == "" || ddlBrigade.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                unitServices = new UnitServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["UnitID"]);
                Int64 checkexist = unitServices.CheckAlreadyExist(txtUnit.Text);
                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Unit Already Exist.";
                }
                else
                {

                    BusinessLayer.Unit info = new BusinessLayer.Unit();
                    info.UnitName = txtUnit.Text;
                    info.HQID = Convert.ToInt64(ddlHQ.SelectedValue);
                    info.DivID = Convert.ToInt64(ddlDiv.SelectedValue);
                    info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                    info.BrigadeID = Convert.ToInt64(ddlBrigade.SelectedValue);
                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["UnitID"]);
                        info.UpdatedOn = DateTime.Now;
                        unitServices.Update(info);
                        unitServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'UnitList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        unitServices.Insert(info);
                        unitServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'UnitList';", true);
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