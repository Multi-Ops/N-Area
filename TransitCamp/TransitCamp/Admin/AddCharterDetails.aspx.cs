using BusinessLayer;
using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Admin
{
    public partial class AddCharterDetails : System.Web.UI.Page
    {
        protected IAirlineServices airlineServices;
        protected ICityServices cityServices;
        protected ICharterDetailsServices charterDetailsServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAirline();
                BindCities();
                GetDetails();
            }
        }

        //bind Airline
        protected void BindAirline()
        {
            airlineServices = new AirlineServices(new TCContext());
            var getlist = airlineServices.GetAirlineDetails();
            ddlAirline.DataSource = getlist;
            ddlAirline.DataValueField = "ID";
            ddlAirline.DataTextField = "AirlineName";
            ddlAirline.DataBind();
            ddlAirline.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind cities
        protected void BindCities()
        {
            cityServices = new CityServices(new TCContext());
            var getlist = cityServices.GetCityDetails();
            ddlToCity.DataSource = getlist;
            ddlToCity.DataValueField = "ID";
            ddlToCity.DataTextField = "CityName";
            ddlFromCity.DataSource = getlist;
            ddlFromCity.DataValueField = "ID";
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataBind();
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new ListItem("-- Select --", ""));
            ddlFromCity.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["CharterID"]);
            charterDetailsServices = new CharterDetailsServices(new TCContext());
            var getdetails = charterDetailsServices.GetByID(id);
            if (getdetails != null)
            {
                txtCharterDate.Text = getdetails.CharteredDate.ToString();
                txtCharterNo.Text = getdetails.CharterNo;
                txtFlightNo.Text = getdetails.FlightNo;
                ddlAirline.SelectedValue = getdetails.AirLineID.ToString();
                ddlFromCity.SelectedValue = getdetails.FromCityID.ToString();
                ddlToCity.SelectedValue = getdetails.ToCityID.ToString();
                hfCharterDate.Value = getdetails.CharteredDate.ToString();
                txtNoOfSeats.Text = getdetails.NumberOfSeats.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {

            if (txtFlightNo.Text == "" || txtCharterNo.Text == "" || hfCharterDate.Value == "" || txtNoOfSeats.Text == "" || ddlToCity.SelectedValue.ToString() == "" || ddlFromCity.SelectedValue.ToString() == "" || ddlAirline.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {

                charterDetailsServices = new CharterDetailsServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["CharterID"]);
                CharterDetails info = new CharterDetails();
                info.ToCityID = Convert.ToInt64(ddlToCity.SelectedValue);
                info.FromCityID = Convert.ToInt64(ddlFromCity.SelectedValue);
                info.AirLineID = Convert.ToInt64(ddlAirline.SelectedValue);
                info.CharteredDate = Convert.ToDateTime(hfCharterDate.Value);
                info.CharterNo = txtCharterNo.Text;
                info.FlightNo = txtFlightNo.Text;
                info.NumberOfSeats = Convert.ToInt32(txtNoOfSeats.Text);
                info.FromCity = ddlFromCity.SelectedItem.Text;
                info.ToCity = ddlToCity.SelectedItem.Text;

                var dateTime = Convert.ToDateTime(info.CharteredDate.Value);

                if (dateTime.ToString("tt", CultureInfo.InvariantCulture) == "AM")
                {
                    info.Session = "FN";
                }
                else
                {
                    info.Session = "AN";
                }

                if (id != 0)
                {
                    info.ID = Convert.ToInt32(Request.QueryString["CharterID"]);
                    info.UpdatedOn = DateTime.Now;
                    charterDetailsServices.Update(info);
                    charterDetailsServices.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'CharterDetailsList';", true);
                }
                else
                {
                    info.CreatedOn = DateTime.Now;
                    charterDetailsServices.Insert(info);
                    charterDetailsServices.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'CharterDetailsList';", true);
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