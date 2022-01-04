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
    public partial class AddAirline : System.Web.UI.Page
    {
        private IAirlineServices airlineservices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }

        protected void GetAirlineDetails()
        {
            Int32 airlineid = Convert.ToInt32(Request.QueryString["AirlineID"]);
            airlineservices = new AirlineServices(new TCContext());
            var getairlinedetail = airlineservices.GetAirlineByID(airlineid);
            if (getairlinedetail != null)
            {
                txtAName.Text = getairlinedetail.AirlineName;
                txtType.Text = getairlinedetail.Type;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtAName.Text == "" || txtType.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                airlineservices = new AirlineServices(new TCContext());
                Int32 airlineid = Convert.ToInt32(Request.QueryString["AirlineID"]);
                Int64 checkuserexist = airlineservices.CheckAlreadyExist(txtAName.Text);
                if (airlineid == 0 && checkuserexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "AirLine Already Exist.";
                }
                else
                {

                    Airline airline = new Airline();
                    airline.AirlineName = txtAName.Text;
                    airline.Type = txtType.Text;

                    if (airlineid != 0)
                    {
                        airline.ID = Convert.ToInt32(Request.QueryString["AirlineID"]);
                        airline.UpdatedOn = DateTime.Now;
                        airlineservices.Update(airline);
                        airlineservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'AirlineList';", true);
                    }
                    else
                    {
                        airline.CreatedOn = DateTime.Now;
                        airlineservices.Insert(airline);
                        airlineservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'AirlineList';", true);
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