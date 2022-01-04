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
    public partial class TransportDetails : System.Web.UI.Page
    {
        protected ITransportServices transportServices;
        protected ITransportDetailsServices transportDetailsServices;
        protected ICityServices cityServices;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTransport();
                GetDetails();
                BindCities();
            }
        }

        //bind Transport Type
        protected void BindTransport()
        {
            transportServices = new TransportServices(new TCContext());
            var getlist = transportServices.GetDetails();
            ddlTransportType.DataSource = getlist;
            ddlTransportType.DataValueField = "ID";
            ddlTransportType.DataTextField = "TransportName";
            ddlTransportType.DataBind();
            ddlTransportType.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind cities
        protected void BindCities()
        {
            cityServices = new CityServices(new TCContext());
            var getlist = cityServices.GetCityDetails();
            ddlCity.DataSource = getlist;
            ddlCity.DataValueField = "ID";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["TransDetailID"]);
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            var getdetails = transportDetailsServices.GetDetailsByID(id);
            if (getdetails != null)
            {
                ddlTransportType.SelectedValue = getdetails.TransportTypeID.ToString();
                ddlCity.SelectedValue = getdetails.CityID.ToString();
                txtDate.Text = getdetails.Date.ToString();
                hfDate.Value = getdetails.Date.ToString();
                txtTotalNoOfSeats.Text = getdetails.TotalNoOfSeats.ToString();
                txtNoOfSeats.Text = getdetails.NoOfSeats.ToString();
                txtTransportDetails.Text = getdetails.TransportDetail;
                txtPrioritySeats.Text = getdetails.PrioritySeats.ToString();
                txtLoad.Text = getdetails.Load.ToString();
                if (getdetails.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (getdetails.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            DateTime dateselection = Convert.ToDateTime(hfDate.Value);
            DateTime currentdate = DateTime.Now;
            Int32 id = Convert.ToInt32(Request.QueryString["TransDetailID"]);

            if (hfDate.Value == "" || ddlTransportType.SelectedValue.ToString() == "" || ddlCity.SelectedValue.ToString() == "" || ddlSession.SelectedValue == "0")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else if (dateselection.Date > currentdate.AddDays(2).Date && id == 0)
            {
                if (dateselection.Day == currentdate.Day)
                {
                    INU();
                }
                else if (dateselection.Day == currentdate.AddDays(1).Day)
                {
                    INU();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Only Select Valid Date.";
                }
            }
            else if (dateselection.Date < currentdate.AddDays(2).Date && id == 0)
            {
                INU();
            }
            else if (id != 0)
            {

                INU();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Only Select Valid Date.";
            }
        }

        protected void INU()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["TransDetailID"]);

            transportDetailsServices = new TransportDetailsServices(new TCContext());
            BusinessLayer.TransportDetails info = new BusinessLayer.TransportDetails();
            info.TransportDetail = txtTransportDetails.Text;
            info.TransportTypeID = Convert.ToInt64(ddlTransportType.SelectedValue);
            info.TotalNoOfSeats = Convert.ToInt64(txtTotalNoOfSeats.Text);
            info.NoOfSeats = Convert.ToInt64(txtNoOfSeats.Text);

            if (txtPrioritySeats.Text != "")
                info.PrioritySeats = Convert.ToInt64(txtPrioritySeats.Text);

            if (txtLoad.Text != "")
                info.Load = Convert.ToInt64(txtLoad.Text);

            info.Date = Convert.ToDateTime(hfDate.Value);
            info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
            info.Session = ddlSession.SelectedItem.Text;
            if (id == 0)
            {
                info.CreatedOn = DateTime.Now;
                Int64 ID = transportDetailsServices.InsertAndGetID(info);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'Manifest?TransportID=" + ID + "';", true);
            }
            else
            {
                info.ID = id;
                info.UpdatedOn = DateTime.Now;
                transportDetailsServices.Update(info);
                transportDetailsServices.Save();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'Manifest?TransportID=" + id + "';", true);
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

        protected void txtTotalNoOfSeats_TextChanged(object sender, EventArgs e)
        {
            int priorityseat = 0;
            int loadseat = 0;
            int totalnoofseat = 0;

            string priorityseats = txtPrioritySeats.Text;
            string loadseats = txtLoad.Text;
            string totalnoofseats = txtTotalNoOfSeats.Text;

            if (priorityseats == "")
                priorityseat = 0;
            else
                priorityseat = Convert.ToInt32(priorityseats);

            if (loadseats == "")
                loadseat = 0;
            else
                loadseat = Convert.ToInt32(loadseats);

            if (totalnoofseats == "")
                totalnoofseat = 0;
            else
                totalnoofseat = Convert.ToInt32(totalnoofseats);

            if (totalnoofseats != null)
            {
                lblError.Visible = false;
                if (txtPrioritySeats.Text != "")
                {
                    if (priorityseat > totalnoofseat)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Priority Seats Can Not Be Greater Than Or Equal To Total No Of Seats.";
                        txtPrioritySeats.Text = "";
                    }
                    else if (loadseat > totalnoofseat)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Load Can Not Be Greater Than Or Equal To Total No Of Seats.";
                        txtLoad.Text = "";
                    }
                    else
                    {
                        Int32 noofseats = totalnoofseat - priorityseat - loadseat;
                        txtNoOfSeats.Text = noofseats.ToString();
                    }
                }
                else
                {
                    txtNoOfSeats.Text = totalnoofseats;
                }
            }
            txtDate.Text = hfDate.Value;
        }

        protected void txtPrioritySeats_TextChanged(object sender, EventArgs e)
        {
            int priorityseat = 0;
            int loadseat = 0;
            int totalnoofseat = 0;
            Int32 noofseats = 0;
            string priorityseats = txtPrioritySeats.Text;
            string loadseats = txtLoad.Text;
            string totalnoofseats = txtTotalNoOfSeats.Text;

            if (priorityseats == "")
                priorityseat = 0;
            else
                priorityseat = Convert.ToInt32(priorityseats);

            if (loadseats == "")
                loadseat = 0;
            else
                loadseat = Convert.ToInt32(loadseats);

            if (totalnoofseats == "")
                totalnoofseat = 0;
            else
                totalnoofseat = Convert.ToInt32(totalnoofseats);

            int calloadandtotalseats = loadseat + priorityseat;


            if (txtTotalNoOfSeats.Text == "")
            {
                txtPrioritySeats.Text = "";
                lblError.Visible = true;
                lblError.Text = "Fill Total No Of Seats First.";
            }
            else
            {
                lblError.Visible = false;

                if (priorityseat > totalnoofseat)
                {
                    lblError.Visible = true;
                    lblError.Text = "Priority Seats Should Be Less Than or Equal to Total No Of Seats.";
                }
                else if (loadseat > totalnoofseat)
                {
                    lblError.Visible = true;
                    lblError.Text = "Load Should Be Less Than or Equal to Total No Of Seats.";
                }
                else if (calloadandtotalseats > priorityseat)
                {
                    txtLoad.Text = "";
                    lblError.Visible = true;
                    lblError.Text = "No Seats Left Please Increase Total No Of Seats.";
                    noofseats = totalnoofseat - priorityseat - loadseat;
                    txtNoOfSeats.Text = noofseats.ToString();
                }
                else
                {
                    noofseats = totalnoofseat - priorityseat - loadseat;
                    txtNoOfSeats.Text = noofseats.ToString();
                }
                txtDate.Text = hfDate.Value;
            }
        }

        protected void txtLoad_TextChanged(object sender, EventArgs e)
        {
            int priorityseat = 0;
            int loadseat = 0;
            int totalnoofseat = 0;
            Int32 noofseats = 0;
            string priorityseats = txtPrioritySeats.Text;
            string loadseats = txtLoad.Text;
            string totalnoofseats = txtTotalNoOfSeats.Text;

            if (priorityseats == "")
                priorityseat = 0;
            else
                priorityseat = Convert.ToInt32(priorityseats);

            if (loadseats == "")
                loadseat = 0;
            else
                loadseat = Convert.ToInt32(loadseats);

            if (totalnoofseats == "")
                totalnoofseat = 0;
            else
                totalnoofseat = Convert.ToInt32(totalnoofseats);

            int calprioandtotalseats = priorityseat + loadseat;

            if (txtTotalNoOfSeats.Text == "")
            {
                txtPrioritySeats.Text = "";
                lblError.Visible = true;
                lblError.Text = "Fill Total No Of Seats First.";
            }
            else
            {
                lblError.Visible = false;

                if (priorityseat > totalnoofseat)
                {
                    lblError.Visible = true;
                    lblError.Text = "Priority Seats Should Be Less Than or Equal to Total No Of Seats.";
                }
                else if (loadseat > totalnoofseat)
                {
                    lblError.Visible = true;
                    lblError.Text = "Load Should Be Less Than or Equal to Total No Of Seats.";
                }

                else if (calprioandtotalseats > totalnoofseat)
                {
                    txtLoad.Text = "";
                    lblError.Visible = true;
                    lblError.Text = "No Seats Left Please Increase Total No Of Seats.";
                    noofseats = totalnoofseat - priorityseat - loadseat;
                    txtNoOfSeats.Text = noofseats.ToString();
                }
                else
                {
                    noofseats = totalnoofseat - priorityseat - loadseat;
                    txtNoOfSeats.Text = noofseats.ToString();
                }
                txtDate.Text = hfDate.Value;
            }
        }

        protected void txtTransportDetails_TextChanged(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(Request.QueryString["TransDetailID"]);
            if (id == 0)
            {
                if (hfDate.Value == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Please Select Date First";
                }
            }
            txtDate.Text = hfDate.Value;
        }
    }
}