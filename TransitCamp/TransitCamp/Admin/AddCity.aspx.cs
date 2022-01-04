using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;
using DataAccessLayer;

namespace TransitCamp.Admin
{
    public partial class AddCity : System.Web.UI.Page
    {
        protected ICityServices cityservices;
        protected IMedicalStatusServices medicalStatusServices;
        protected IOutLogicServices outLogicServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMedicalStatus();
                GetAirlineDetails();
                BindOutLogic();
            }
        }

        //bind MedicalStatus
        protected void BindMedicalStatus()
        {
            medicalStatusServices = new MedicalStatusServices(new TCContext());
            var getlist = medicalStatusServices.getmeddetails();
            ddlMedstat.DataSource = getlist;
            ddlMedstat.DataValueField = "ID";
            ddlMedstat.DataTextField = "MedicalStatusName";
            ddlMedstat.DataBind();
            ddlMedstat.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Outlogic
        protected void BindOutLogic()
        {
            outLogicServices = new OutLogicServices(new TCContext());
            var getlist = outLogicServices.getOLDetails();
            ddlOL.DataSource = getlist;
            ddlOL.DataValueField = "ID";
            ddlOL.DataTextField = "OutLogicName";
            ddlOL.DataBind();
            ddlOL.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetAirlineDetails()
        {
            Int32 cityid = Convert.ToInt32(Request.QueryString["CityID"]);
            cityservices = new CityServices(new TCContext());
            var getcitydetail = cityservices.GetCityID(cityid);
            if (getcitydetail != null)
            {
                txtCName.Text = getcitydetail.CityName;
                txtState.Text = getcitydetail.StateName;
                ddlOL.SelectedValue = getcitydetail.OutLogicID.ToString();
                ddlMedstat.SelectedValue = getcitydetail.MedicalStatusID.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtCName.Text == "" || txtState.Text == "" || ddlMedstat.SelectedValue.ToString() == "" || ddlOL.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                cityservices = new CityServices(new TCContext());
                Int32 cityid = Convert.ToInt32(Request.QueryString["CityID"]);
                Int64 checkcityexist = cityservices.CheckAlreadyExist(txtCName.Text);
                if (cityid == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "City Already Exist.";
                }
                else
                {

                    City city = new City();
                    city.CityName = txtCName.Text;
                    city.OutLogicID = Convert.ToInt64(ddlOL.SelectedValue);
                    city.MedicalStatusID = Convert.ToInt64(ddlMedstat.SelectedValue);
                    city.StateName = txtState.Text;
                    if (cityid != 0)
                    {
                        city.ID = Convert.ToInt32(Request.QueryString["CityID"]);
                        city.UpdatedOn = DateTime.Now;
                        cityservices.Update(city);
                        cityservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'CitiesList';", true);
                    }
                    else
                    {
                        city.CreatedOn = DateTime.Now;
                        cityservices.Insert(city);
                        cityservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'CitiesList';", true);
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