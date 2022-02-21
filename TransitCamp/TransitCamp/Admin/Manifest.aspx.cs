using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DataAccessLayer;
using System.Data;
using System.Reflection;
using BusinessLayer;

namespace TransitCamp.Admin
{
    public partial class Manifest : System.Web.UI.Page
    {
        ITransportDetailsServices transportDetailsServices;
        IADServices aDServices;
        ITransportServices transportServices;
        ICityServices cityServices;
        IManifestServices manifestServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindADEntryLoad();
                BindCities();
                GetTransportDetails();
                BindTransportType();
                //BindCat();
                BindCityPriority();
                BindCityReserve();
                BindCityGeneral();
                BindCityLoad();
                clear();
                SetInputFocus();
                //BindCatReserve();
                //BindCatGeneral();

                var transportid = Request.QueryString["TransportID"];
                if (transportid != null)
                {
                    Int64 ID = Convert.ToInt64(transportid);

                    if (ID != 0)
                    {
                        manifestServices = new ManifestServices(new TCContext());
                        var getdetails = transportDetailsServices.GetDetailsByID(ID);
                        var getmanifestdetails = manifestServices.GetByTransportID(ID);
                        var getmanifestno = manifestServices.GetManifestNumberByDes(Convert.ToDateTime(getdetails.Date));

                        DateTime date = Convert.ToDateTime(getdetails.Date);
                        txtDate.Text = date.ToString();
                        hfDate.Value = date.ToString();
                        txtNoOfSeats.Text = getdetails.NoOfSeats.ToString();
                        txtPrioritySeats.Text = getdetails.PrioritySeats.ToString();
                        txtLoad.Text = getdetails.Load.ToString();
                        ddlTransportType.SelectedValue = getdetails.TransportTypeID.ToString();
                        ddlTransport.SelectedValue = getdetails.ID.ToString();
                        hfddlTransportDetails.Value = getdetails.ID.ToString();
                        ddlCity.SelectedValue = getdetails.CityID.ToString();
                        Session["ddlCity"] = ddlCity.SelectedValue;
                        lblTotalSeats.Text = getdetails.TotalNoOfSeats.ToString();
                        if (getdetails.Session == "FN")
                        {
                            ddlSession.SelectedValue = "1";
                        }
                        if (getdetails.Session == "AN")
                        {
                            ddlSession.SelectedValue = "2";
                        }
                        Int32 seatsleft = Convert.ToInt32(getdetails.NoOfSeats) + Convert.ToInt32(getdetails.PrioritySeats) + Convert.ToInt32(getdetails.Load);
                        lblSeatsinfo.Text = seatsleft.ToString();

                        if (getmanifestdetails != null)
                        {
                            txtManifestNo.Text = getmanifestdetails.MenifestNo;
                            txtManifestNo.Enabled = false;
                            btnDeleteManifest.Visible = true;
                            btnPrint.Visible = true;
                        }
                        else
                        {
                            if (getmanifestno != null)
                            {
                                var getmanifestnocity = manifestServices.GetManifestNumberByDesCityWise(Convert.ToDateTime(getdetails.Date), Convert.ToInt32(ddlCity.SelectedValue));

                                if (getmanifestnocity != null)
                                {
                                    int manifestNoInt = Convert.ToInt32(getmanifestnocity.MenifestNo);
                                    manifestNoInt = manifestNoInt + 1;
                                    txtManifestNo.Text = manifestNoInt.ToString();
                                }
                                else
                                    txtManifestNo.Text = "1";

                            }
                            else
                                txtManifestNo.Text = "1";
                            txtManifestNo.Enabled = false;
                            btnDeleteManifest.Visible = false;
                            btnPrint.Visible = false;
                        }

                        Session["TransportID"] = ID;
                        Bind(ID);
                        BindADEntryPriority();
                        BindADEntryGeneral();
                        BindADEntryReserve();
                        BindADEntryLoad();


                    }
                }
            }
        }

        public void SetInputFocus()
        {
            TextBox tbox = this.txtManifestNo.FindControl("txtManifestNo") as TextBox;
            if (tbox != null)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(tbox);
            }
        }

        //Bind Grid From Manifest
        private void Bind(Int64 TransportID)
        {
            manifestServices = new ManifestServices(new TCContext());
            if (TransportID != 0)
            {
                var isReserveFilterTrue = new List<BusinessLayer.Manifest>();
                var isReserveFilterFalse = new List<BusinessLayer.Manifest>();
                var data = manifestServices.ManifestListing(TransportID);
                if (data.Count > 0)
                {
                    foreach (var res in data)
                    {
                        if (res.IsReserve == true)
                        {
                            isReserveFilterTrue = (from p in data
                                                   where p.IsReserve == true
                                                   select p).ToList();
                        }

                        if (res.IsReserve == false)
                        {
                            isReserveFilterFalse = (from p in data
                                                    where p.IsReserve == false
                                                    select p).ToList();
                        }
                        ListtoDataTable lsDTReserveTrue = new ListtoDataTable();
                        DataTable dtTrue = lsDTReserveTrue.ToDataTable(isReserveFilterTrue);
                        if (dtTrue != null)
                        {
                            grdReserve.DataSource = dtTrue;
                            grdReserve.DataBind();
                        }
                        else
                        {
                            grdReserve.DataSource = null;
                            grdReserve.DataBind();
                        }

                        ListtoDataTable lsDTReserveFalse = new ListtoDataTable();
                        DataTable dtFalse = lsDTReserveFalse.ToDataTable(isReserveFilterFalse);
                        if (dtFalse != null)
                        {
                            grdUser.DataSource = dtFalse;
                            grdUser.DataBind();
                        }
                        else
                        {
                            grdUser.DataSource = null;
                            grdUser.DataBind();
                        }
                    }
                }
                else
                {
                    grdReserve.DataSource = null;
                    grdReserve.DataBind();
                    grdUser.DataSource = null;
                    grdUser.DataBind();
                }
            }
        }

        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Int32 Id = Convert.ToInt32(grdUser.DataKeys[row.RowIndex].Value);

                if (e.CommandName == "Edit")
                {
                    Response.Redirect("ADEntery?ADID=" + Convert.ToString(Id));
                }
                else if (e.CommandName == "Delete")
                {
                    manifestServices = new ManifestServices(new TCContext());
                    aDServices = new ADServices(new TCContext());
                    transportDetailsServices = new TransportDetailsServices(new TCContext());
                    var getdetailsmanifest = manifestServices.GetByID(Id);
                    var getdetails = aDServices.GetByID(getdetailsmanifest.ADID);
                    var transportdetails = transportDetailsServices.GetDetailsByID(getdetailsmanifest.TransportDetailID);
                    if (getdetails != null)
                    {
                        transportdetails.ID = getdetailsmanifest.TransportDetailID;
                        if (getdetails.IsPriority == true)
                        {
                            transportdetails.PrioritySeats = transportdetails.PrioritySeats + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        else if (getdetails.IsLoad == true)
                        {
                            transportdetails.Load = transportdetails.Load + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        else
                        {
                            transportdetails.NoOfSeats = transportdetails.NoOfSeats + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        getdetails.ID = getdetailsmanifest.ADID;
                        getdetails.IsManifest = false;
                        aDServices.Update(getdetails);
                        aDServices.Save();
                    }

                    manifestServices.Delete(Id);
                    BindADEntryPriority();
                    BindADEntryGeneral();
                    BindADEntryReserve();
                    BindADEntryLoad();
                    BindCities();
                    GetTransportDetails();
                    //BindCat();
                    BindCityReserve();
                    BindCityGeneral();
                    BindCityPriority();
                    Bind(transportdetails.ID);
                    ddlTransport.SelectedValue = transportdetails.ID.ToString();
                    ddlCity.SelectedValue = transportdetails.CityID.ToString();
                    txtNoOfSeats.Text = transportdetails.NoOfSeats.ToString();
                    txtPrioritySeats.Text = transportdetails.PrioritySeats.ToString();
                    txtLoad.Text = transportdetails.Load.ToString();
                    Int64 seatsavailable = Convert.ToInt64(transportdetails.NoOfSeats) + Convert.ToInt64(transportdetails.PrioritySeats) + Convert.ToInt64(transportdetails.Load);
                    lblSeatsinfo.Text = seatsavailable.ToString();
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }

        }

        protected void grdUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }

        /// <summary>
        /// Bind City By Property Priority
        /// </summary>
        private void BindCityPriority()
        {
            cityServices = new CityServices(new TCContext());
            var result = cityServices.GetCityDetails();
            ddlCityPriority.DataSource = result;
            ddlCityPriority.DataValueField = "ID";
            ddlCityPriority.DataTextField = "CityName";
            ddlCityPriority.DataBind();
            ddlCityPriority.Items.Insert(0, new ListItem("--City--", ""));
        }

        /// <summary>
        /// Bind City By Property General
        /// </summary>
        private void BindCityGeneral()
        {
            cityServices = new CityServices(new TCContext());
            var result = cityServices.GetCityDetails();
            ddlCityGeneral.DataSource = result;
            ddlCityGeneral.DataValueField = "ID";
            ddlCityGeneral.DataTextField = "CityName";
            ddlCityGeneral.DataBind();
            ddlCityGeneral.Items.Insert(0, new ListItem("--City--", ""));
        }

        /// <summary>
        /// Bind City By Property Reserve
        /// </summary>
        private void BindCityReserve()
        {
            cityServices = new CityServices(new TCContext());
            var result = cityServices.GetCityDetails();
            ddlCityReserve.DataSource = result;
            ddlCityReserve.DataValueField = "ID";
            ddlCityReserve.DataTextField = "CityName";
            ddlCityReserve.DataBind();
            ddlCityReserve.Items.Insert(0, new ListItem("--City--", ""));
        }

        private void BindCityLoad()
        {
            cityServices = new CityServices(new TCContext());
            var result = cityServices.GetCityDetails();
            ddlCityLoad.DataSource = result;
            ddlCityLoad.DataValueField = "ID";
            ddlCityLoad.DataTextField = "CityName";
            ddlCityLoad.DataBind();
            ddlCityLoad.Items.Insert(0, new ListItem("--City--", ""));
        }

        //Bind TransportDetails Dropdown
        protected void GetTransportDetails()
        {
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            var result = transportDetailsServices.GetDetails();
            ddlTransport.DataSource = result;
            ddlTransport.DataValueField = "ID";
            ddlTransport.DataTextField = "TransportDetail";
            ddlTransport.DataBind();
            ddlTransport.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //Bind City Dropdown
        protected void BindCities()
        {
            cityServices = new CityServices(new TCContext());
            var result = cityServices.GetCityDetails();
            ddlCity.DataSource = result;
            ddlCity.DataValueField = "ID";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //Bind category Dropdown
        //protected void BindCat()
        //{
        //    categoryServices = new CategoryServices(new TCContext());
        //    var result = categoryServices.details();
        //    ddlCatPriority.DataSource = result;
        //    ddlCatPriority.DataValueField = "ID";
        //    ddlCatPriority.DataTextField = "CategoryName";
        //    ddlCatPriority.DataBind();
        //    ddlCatPriority.Items.Insert(0, new ListItem("-- Select --", ""));
        //}

        //Bind category Dropdown reserve
        //protected void BindCatReserve()
        //{
        //    categoryServices = new CategoryServices(new TCContext());
        //    var result = categoryServices.details();
        //    ddlCatReserve.DataSource = result;
        //    ddlCatReserve.DataValueField = "ID";
        //    ddlCatReserve.DataTextField = "CategoryName";
        //    ddlCatReserve.DataBind();
        //    ddlCatReserve.Items.Insert(0, new ListItem("-- Select --", ""));
        //}

        //Bind category Dropdown General
        //protected void BindCatGeneral()
        //{
        //    categoryServices = new CategoryServices(new TCContext());
        //    var result = categoryServices.details();
        //    ddlCatGeneral.DataSource = result;
        //    ddlCatGeneral.DataValueField = "ID";
        //    ddlCatGeneral.DataTextField = "CategoryName";
        //    ddlCatGeneral.DataBind();
        //    ddlCatGeneral.Items.Insert(0, new ListItem("-- Select --", ""));
        //}

        //Bind Transport Dropdown
        protected void BindTransportType()
        {
            transportServices = new TransportServices(new TCContext());
            var result = transportServices.GetDetails();
            ddlTransportType.DataSource = result;
            ddlTransportType.DataValueField = "ID";
            ddlTransportType.DataTextField = "TransportName";
            ddlTransportType.DataBind();
            ddlTransportType.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind AD Entry No PriorityWise
        private void BindADEntryPriority()
        {
            aDServices = new ADServices(new TCContext());

            if (!string.IsNullOrEmpty(ddlCity.SelectedValue.ToString()))
            {
                Int64 cityID = Convert.ToInt64(ddlCity.SelectedValue);
                var data = aDServices.ADEntryNoPriority(cityID);
                data = (from p in data
                        where p.IsFly == true
                        select p).ToList();
                rptPriority.DataSource = data;
                rptPriority.DataBind();
            }
            else
            {
                var data = aDServices.ADEntryNoPriority();
                data = (from p in data
                        where p.IsFly == true
                        select p).ToList();
                rptPriority.DataSource = data;
                rptPriority.DataBind();
            }
        }

        //bind AD Entry No Priority category Wise
        private void BindADEntryPriorityCat(Int64 CatID)
        {
            aDServices = new ADServices(new TCContext());
            var data = aDServices.ADEntryNoPriorityCat(CatID);
            data = (from p in data
                    where p.IsFly == true
                    select p).ToList();
            rptPriority.DataSource = data;
            rptPriority.DataBind();
        }

        //bind AD Entry No Priority City Wise
        private void BindADEntryPriorityCity(Int64 CityID)
        {
            aDServices = new ADServices(new TCContext());
            var data = aDServices.ADEntryNoPriorityCity(CityID);
            data = (from p in data
                    where p.IsFly == true
                    select p).ToList();
            rptPriority.DataSource = data;
            rptPriority.DataBind();
        }

        //bind AD Entry No General City Wise
        private void BindADEntryGeneralCity(Int64 CityID)
        {
            aDServices = new ADServices(new TCContext());
            var data = aDServices.ADEntryNoGeneralCity(CityID);
            // get Ads eligible to fly 
            data = (from p in data
                    where p.IsFly == true
                    select p).ToList();

            rptGeneral.DataSource = data;
            rptGeneral.DataBind();
        }

        //bind AD Entry No Reserve City Wise
        private void BindADEntryReserveCity(Int64 CityID)
        {
            aDServices = new ADServices(new TCContext());
            var data = aDServices.ADEntryNoReserveCity(CityID);
            rptReserve.DataSource = data;
            rptReserve.DataBind();
        }

        //bind AD Entry No Load City Wise
        private void BindADEntryLoadCity(Int64 CityID)
        {
            aDServices = new ADServices(new TCContext());
            var data = aDServices.ADEntryNoLoadCity(CityID);
            rptLoad.DataSource = data;
            rptLoad.DataBind();
        }

        //bind AD Entry No General
        private void BindADEntryGeneral()
        {
            aDServices = new ADServices(new TCContext());

            if (!string.IsNullOrEmpty(ddlCity.SelectedValue.ToString()))
            {
                Int64 cityID = Convert.ToInt64(ddlCity.SelectedValue);
                var data = aDServices.ADEntryNoGeneralCityWise(cityID);
                data = (from p in data
                        where p.IsFly == true
                        select p).ToList();
                rptGeneral.DataSource = data;
                rptGeneral.DataBind();
            }
            else
            {
                var data = aDServices.ADEntryNoGeneral();
                data = (from p in data
                        where p.IsFly == true
                        select p).ToList();
                rptGeneral.DataSource = data;
                rptGeneral.DataBind();
            }
        }

        //bind AD Entry No Resreve
        private void BindADEntryReserve()
        {
            aDServices = new ADServices(new TCContext());

            if (!string.IsNullOrEmpty(ddlCity.SelectedValue.ToString()))
            {
                Int64 cityID = Convert.ToInt64(ddlCity.SelectedValue);
                var data = aDServices.ADEntryNoReserveCityWise(cityID);
                rptReserve.DataSource = data;
                rptReserve.DataBind();
            }
            else
            {
                var data = aDServices.ADEntryNoReserve();
                rptReserve.DataSource = data;
                rptReserve.DataBind();
            }
        }

        //bind AD Entry No Load
        private void BindADEntryLoad()
        {
            aDServices = new ADServices(new TCContext());

            if (!string.IsNullOrEmpty(ddlCity.SelectedValue.ToString()))
            {
                Int64 cityID = Convert.ToInt64(ddlCity.SelectedValue);
                var data = aDServices.ADEntryNoLoad(cityID);
                rptLoad.DataSource = data;
                rptLoad.DataBind();
            }
            else
            {
                if (Session["ddlCity"] != null)
                {
                    var data = aDServices.ADEntryNoLoad(Convert.ToInt64(Session["ddlCity"]));
                    rptLoad.DataSource = data;
                    rptLoad.DataBind();
                }
                else
                {
                    var data = aDServices.ADEntryNoLoad();
                    rptLoad.DataSource = data;
                    rptLoad.DataBind();
                }
            }
        }

        protected void clear()
        {
            txtDate.Text = "";
            txtNoOfSeats.Text = "";
            txtLoad.Text = "";
            txtPrioritySeats.Text = "";
            ddlTransportType.SelectedValue = "";
            ddlTransport.SelectedValue = "";
            ddlSession.SelectedValue = "0";
            ddlCity.SelectedValue = "";
            lblTotalSeats.Text = "";
            lblSeatsinfo.Text = "";
            txtManifestNo.Text = "";
            btnDeleteManifest.Visible = false;
            btnPrint.Visible = false;
            grdUser.DataSource = null;
            grdUser.DataBind();
            grdReserve.DataSource = null;
            grdReserve.DataBind();
        }

        protected void ddlTransport_SelectedIndexChanged(object sender, EventArgs e)
        {
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            string sID = Convert.ToString(hfddlTransportDetails.Value);
            if (sID == "")
            {
                clear();
            }
            else
            {
                Int64 ID = Convert.ToInt64(sID);

                if (ID != 0)
                {
                    Session["MTID"] = ID;
                    lblError.Visible = false;
                    var getdetails = transportDetailsServices.GetDetailsByID(ID);
                    var getmanifestdetails = manifestServices.GetByTransportID(ID);
                    var getmanifestno = manifestServices.GetManifestNumberByDes(Convert.ToDateTime(getdetails.Date));
                    //if (getmanifestdetails != null)
                    //{
                    //    txtManifestNo.Text = getmanifestdetails.MenifestNo;
                    //    txtManifestNo.Enabled = false;
                    //    btnDeleteManifest.Visible = true;
                    //    btnPrint.Visible = true;
                    //}
                    //else
                    //{
                    //    if (getmanifestno != null)
                    //    {
                    //        int manifestNoInt = Convert.ToInt32(getmanifestno.MenifestNo);
                    //        manifestNoInt = manifestNoInt + 1;
                    //        txtManifestNo.Text = manifestNoInt.ToString();
                    //    }
                    //    else
                    //        txtManifestNo.Text = "1";
                    //    txtManifestNo.Enabled = false;
                    //    //txtManifestNo.Text = "";
                    //    btnDeleteManifest.Visible = false;
                    //    btnPrint.Visible = false;
                    //}
                    DateTime date = Convert.ToDateTime(getdetails.Date);
                    txtDate.Text = date.ToString();
                    hfDate.Value = date.ToString();
                    txtNoOfSeats.Text = getdetails.NoOfSeats.ToString();
                    txtPrioritySeats.Text = getdetails.PrioritySeats.ToString();
                    txtLoad.Text = getdetails.Load.ToString();
                    ddlTransportType.SelectedValue = getdetails.TransportTypeID.ToString();
                    ddlTransport.SelectedValue = getdetails.ID.ToString();
                    ddlCity.SelectedValue = getdetails.CityID.ToString();
                    Session["ddlCity"] = ddlCity.SelectedValue;
                    lblTotalSeats.Text = getdetails.TotalNoOfSeats.ToString();
                    if (getdetails.Session == "FN")
                    {
                        ddlSession.SelectedValue = "1";
                    }
                    if (getdetails.Session == "AN")
                    {
                        ddlSession.SelectedValue = "2";
                    }
                    Int32 seatsleft = Convert.ToInt32(getdetails.NoOfSeats) + Convert.ToInt32(getdetails.PrioritySeats) + Convert.ToInt32(getdetails.Load);
                    lblSeatsinfo.Text = seatsleft.ToString();

                    if (getmanifestdetails != null)
                    {
                        txtManifestNo.Text = getmanifestdetails.MenifestNo;
                        txtManifestNo.Enabled = false;
                        btnDeleteManifest.Visible = true;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        if (getmanifestno != null)
                        {
                            var getmanifestnocity = manifestServices.GetManifestNumberByDesCityWise(Convert.ToDateTime(getdetails.Date), Convert.ToInt32(ddlCity.SelectedValue));

                            if (getmanifestnocity != null)
                            {
                                int manifestNoInt = Convert.ToInt32(getmanifestnocity.MenifestNo);
                                manifestNoInt = manifestNoInt + 1;
                                txtManifestNo.Text = manifestNoInt.ToString();
                            }
                        }
                        else
                            txtManifestNo.Text = "1";
                        txtManifestNo.Enabled = false;
                        btnDeleteManifest.Visible = false;
                        btnPrint.Visible = false;
                    }

                    Session["TransportID"] = ID;
                }
            }
            Bind(Convert.ToInt32(Session["MTID"]));
            BindADEntryPriority();
            BindADEntryGeneral();
            BindADEntryReserve();
            BindADEntryLoad();
        }

        protected void btnfinalize_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            BusinessLayer.Manifest info = new BusinessLayer.Manifest();

            Int64 finalseatsavailable;

            if (ddlTransport.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Please Select Transport First.";
            }
            else if (ddlCity.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select City.";
            }
            else
            {

                var checkseats = transportDetailsServices.GetDetailsByID(Convert.ToInt64(ddlTransport.SelectedValue));
                if (checkseats != null)
                {
                    Int64 totalseatsavailable = Convert.ToInt64(checkseats.PrioritySeats) + Convert.ToInt64(checkseats.NoOfSeats) + Convert.ToInt64(checkseats.Load);
                    if (totalseatsavailable == 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "No More Seats Availble";
                    }
                    else
                    {
                        HiddenField hfpriorityID = (HiddenField)rptPriority.Items[0].FindControl("hfPriorityID");

                        var getprioritystatus = aDServices.GetByID(Convert.ToInt64(hfpriorityID.Value));
                        if (getprioritystatus != null)
                        {
                            if (checkseats.PrioritySeats == 0 && getprioritystatus.IsPriority == true)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Priority Seats Are Full.";
                            }
                            else if (checkseats.NoOfSeats == 0 && getprioritystatus.IsPriority == false)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Genaral Seats Are Full.";
                            }
                            else
                            {
                                lblError.Visible = false;
                                lblError.Text = "";
                                foreach (RepeaterItem rptr in rptPriority.Items)
                                {
                                    CheckBox chklist = (CheckBox)rptr.FindControl("chkPriority");
                                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                                    Int64 ID = Convert.ToInt64(hfID.Value);
                                    Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                    if (chklist.Checked == true)
                                    {
                                        var getdetails = aDServices.GetByID(ID);
                                        var gettransportdetails = transportDetailsServices.GetDetailsByID(TransportDetailID);
                                        if (gettransportdetails.PrioritySeats == 0 && getdetails.IsPriority == true)
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = "Priority Seats Are Full.";
                                        }
                                        else if (gettransportdetails.NoOfSeats == 0 && getdetails.IsPriority == false)
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = "Genaral Seats Are Full.";
                                        }
                                        else
                                        {

                                            getdetails.ID = ID;
                                            getdetails.IsManifest = true;
                                            info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                            info.Session = ddlSession.SelectedItem.Text;
                                            info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                            info.ADNO = getdetails.ADNO;
                                            info.ADID = getdetails.ID;
                                            info.ArmyNo = getdetails.ArmyNo;
                                            info.RankID = getdetails.RankID;
                                            info.Name = getdetails.Name;
                                            info.UnitID = getdetails.UnitID;
                                            info.ICard = getdetails.ICard;
                                            info.HQID = getdetails.HQID;
                                            info.CategoryID = getdetails.CategoryID;
                                            info.CreatedOn = DateTime.Now;
                                            info.TransportDetails = ddlTransport.SelectedItem.Text;
                                            info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                            info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                                            //update AD 

                                            aDServices.Update(getdetails);
                                            aDServices.Save();

                                            //Insert Manifest
                                            manifestServices.Insert(info);
                                            manifestServices.Save();

                                            if (getdetails.IsPriority == true)
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.PrioritySeats);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.PrioritySeats = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtPrioritySeats.Text = gettransportdetails.PrioritySeats.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                            else
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.NoOfSeats);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.NoOfSeats = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtPrioritySeats.Text = gettransportdetails.PrioritySeats.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                        }
                                    }
                                }
                                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!')", true);

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindADEntryLoad();
                                BindCities();
                                GetTransportDetails();
                                Bind(checkseats.ID);
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                if (checkseats.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (checkseats.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = checkseats.ID.ToString();
                                ddlCity.SelectedValue = checkseats.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void btnFinalizeGeneral_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            BusinessLayer.Manifest info = new BusinessLayer.Manifest();
            Int64 finalseatsavailable;

            if (ddlTransport.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Please Select Transport First.";
            }
            else if (ddlCity.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select City.";
            }
            else
            {

                var checkseats = transportDetailsServices.GetDetailsByID(Convert.ToInt64(ddlTransport.SelectedValue));
                if (checkseats != null)
                {
                    Int64 totalseatsavailable = Convert.ToInt64(checkseats.PrioritySeats) + Convert.ToInt64(checkseats.NoOfSeats) + Convert.ToInt64(checkseats.Load);
                    if (totalseatsavailable == 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "No More Seats Availble";
                    }
                    else
                    {
                        HiddenField hfpriorityID = (HiddenField)rptGeneral.Items[0].FindControl("hfPriorityID");

                        var getprioritystatus = aDServices.GetByID(Convert.ToInt64(hfpriorityID.Value));
                        if (getprioritystatus != null)
                        {
                            if (checkseats.PrioritySeats == 0 && getprioritystatus.IsPriority == true)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Priority Seats Are Full.";
                            }
                            else if (checkseats.NoOfSeats == 0 && getprioritystatus.IsPriority == false)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Genaral Seats Are Full.";
                            }
                            else
                            {
                                lblError.Visible = false;
                                lblError.Text = "";
                                foreach (RepeaterItem rptr in rptGeneral.Items)
                                {
                                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDGeneral");
                                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                                    Int64 ID = Convert.ToInt64(hfID.Value);
                                    Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                    if (chklist.Checked == true)
                                    {
                                        var getdetails = aDServices.GetByID(ID);
                                        var gettransportdetails = transportDetailsServices.GetDetailsByID(TransportDetailID);
                                        if (gettransportdetails.PrioritySeats == 0 && getdetails.IsPriority == true)
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = "Priority Seats Are Full.";
                                        }
                                        else if (gettransportdetails.NoOfSeats == 0 && getdetails.IsPriority == false)
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = "Genaral Seats Are Full.";
                                        }
                                        else
                                        {
                                            getdetails.ID = ID;
                                            getdetails.IsManifest = true;
                                            info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                            info.Session = ddlSession.SelectedItem.Text;
                                            info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                            info.ADNO = getdetails.ADNO;
                                            info.ADID = getdetails.ID;
                                            info.ArmyNo = getdetails.ArmyNo;
                                            info.RankID = getdetails.RankID;
                                            info.Name = getdetails.Name;
                                            info.UnitID = getdetails.UnitID;
                                            info.ICard = getdetails.ICard;
                                            info.TransportDetails = ddlTransport.SelectedItem.Text;
                                            info.HQID = getdetails.HQID;
                                            info.CreatedOn = DateTime.Now;
                                            info.CategoryID = getdetails.CategoryID;
                                            info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                            info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                                            //update AD 

                                            aDServices.Update(getdetails);
                                            aDServices.Save();

                                            //Insert Manifest
                                            manifestServices.Insert(info);
                                            manifestServices.Save();

                                            if (getdetails.IsPriority == true)
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.PrioritySeats);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.PrioritySeats = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtPrioritySeats.Text = gettransportdetails.PrioritySeats.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                            else
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.NoOfSeats);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.NoOfSeats = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtPrioritySeats.Text = gettransportdetails.PrioritySeats.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                        }
                                    }
                                }
                                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!')", true);

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindCities();
                                GetTransportDetails();
                                BindADEntryLoad();
                                Bind(checkseats.ID);
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                if (checkseats.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (checkseats.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = checkseats.ID.ToString();
                                ddlCity.SelectedValue = checkseats.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void btnFinalizeReserve_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            BusinessLayer.Manifest info = new BusinessLayer.Manifest();
            Int64 finalseatsavailable;

            if (ddlTransport.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Please Select Transport First.";
            }
            else if (ddlCity.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select City.";
            }
            else
            {

                var checkseats = transportDetailsServices.GetDetailsByID(Convert.ToInt64(ddlTransport.SelectedValue));
                if (checkseats != null)
                {
                    Int64 totalseatsavailable = Convert.ToInt64(checkseats.PrioritySeats) + Convert.ToInt64(checkseats.Load) + Convert.ToInt64(checkseats.NoOfSeats);
                    if (totalseatsavailable == 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "No More Seats Availble";
                    }
                    else
                    {
                        HiddenField hfpriorityID = (HiddenField)rptReserve.Items[0].FindControl("hfPriorityID");

                        var getprioritystatus = aDServices.GetByID(Convert.ToInt64(hfpriorityID.Value));
                        if (getprioritystatus != null)
                        {
                            if (checkseats.PrioritySeats == 0 && getprioritystatus.IsPriority == true)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Priority Seats Are Full.";
                            }
                            else if (checkseats.NoOfSeats == 0 && getprioritystatus.IsPriority == false)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Genaral Seats Are Full.";
                            }
                            else
                            {
                                lblError.Visible = false;
                                lblError.Text = "";
                                foreach (RepeaterItem rptr in rptReserve.Items)
                                {
                                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDReserve");
                                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                                    Int64 ID = Convert.ToInt64(hfID.Value);
                                    Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                    if (chklist.Checked == true)
                                    {
                                        var getdetails = aDServices.GetByID(ID);
                                        var gettransportdetails = transportDetailsServices.GetDetailsByID(TransportDetailID);
                                        if (gettransportdetails.PrioritySeats == 0 && getdetails.IsPriority == true)
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = "Priority Seats Are Full.";
                                        }
                                        else if (gettransportdetails.NoOfSeats == 0 && getdetails.IsPriority == false)
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = "Genaral Seats Are Full.";
                                        }
                                        else
                                        {
                                            getdetails.ID = ID;
                                            getdetails.IsManifest = true;
                                            info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                            info.Session = ddlSession.SelectedItem.Text;
                                            info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                            info.ADNO = getdetails.ADNO;
                                            info.ADID = getdetails.ID;
                                            info.ArmyNo = getdetails.ArmyNo;
                                            info.RankID = getdetails.RankID;
                                            info.Name = getdetails.Name;
                                            info.UnitID = getdetails.UnitID;
                                            info.ICard = getdetails.ICard;
                                            info.HQID = getdetails.HQID;
                                            info.CategoryID = getdetails.CategoryID;
                                            info.TransportDetails = ddlTransport.SelectedItem.Text;
                                            info.CreatedOn = DateTime.Now;
                                            info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                            info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                                            //update AD 

                                            aDServices.Update(getdetails);
                                            aDServices.Save();

                                            //Insert Manifest
                                            manifestServices.Insert(info);
                                            manifestServices.Save();

                                            if (getdetails.IsPriority == true)
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.PrioritySeats);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.PrioritySeats = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtPrioritySeats.Text = gettransportdetails.PrioritySeats.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                            else
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.NoOfSeats);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.NoOfSeats = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtPrioritySeats.Text = gettransportdetails.PrioritySeats.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                        }
                                    }
                                }
                                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!')", true);

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindCities();
                                GetTransportDetails();
                                Bind(checkseats.ID);
                                BindADEntryLoad();
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                if (checkseats.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (checkseats.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = checkseats.ID.ToString();
                                ddlCity.SelectedValue = checkseats.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void btnFinalizeLoad_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            BusinessLayer.Manifest info = new BusinessLayer.Manifest();
            Int64 finalseatsavailable;

            if (ddlTransport.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Please Select Transport First.";
            }
            else if (ddlCity.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select City.";
            }
            else
            {

                var checkseats = transportDetailsServices.GetDetailsByID(Convert.ToInt64(ddlTransport.SelectedValue));
                if (checkseats != null)
                {
                    Int64 totalseatsavailable = Convert.ToInt64(checkseats.PrioritySeats) + Convert.ToInt64(checkseats.NoOfSeats) + Convert.ToInt64(checkseats.Load);
                    if (totalseatsavailable == 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "No More Seats Availble";
                    }
                    else
                    {
                        HiddenField hfpriorityID = (HiddenField)rptLoad.Items[0].FindControl("hfpriorityID");

                        var getprioritystatus = aDServices.GetByID(Convert.ToInt64(hfpriorityID.Value));
                        if (getprioritystatus != null)
                        {
                            if (checkseats.Load == 0 && getprioritystatus.IsPriority == false)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Load Is Full.";
                            }
                            else
                            {
                                lblError.Visible = false;
                                lblError.Text = "";
                                foreach (RepeaterItem rptr in rptLoad.Items)
                                {
                                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDLoad");
                                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                                    Int64 ID = Convert.ToInt64(hfID.Value);
                                    Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                    if (chklist.Checked == true)
                                    {
                                        var getdetails = aDServices.GetByID(ID);
                                        var gettransportdetails = transportDetailsServices.GetDetailsByID(TransportDetailID);
                                        if (gettransportdetails.Load == 0 && getprioritystatus.IsPriority == false)
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = "Load Is Full.";
                                        }
                                        else
                                        {
                                            getdetails.ID = ID;
                                            getdetails.IsManifest = true;
                                            info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                            info.Session = ddlSession.SelectedItem.Text;
                                            info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                            info.ADNO = getdetails.ADNO;
                                            info.ADID = getdetails.ID;
                                            info.ArmyNo = getdetails.ArmyNo;
                                            info.RankID = getdetails.RankID;
                                            info.Name = getdetails.Name;
                                            info.UnitID = getdetails.UnitID;
                                            info.ICard = getdetails.ICard;
                                            info.TransportDetails = ddlTransport.SelectedItem.Text;
                                            info.HQID = getdetails.HQID;
                                            info.CreatedOn = DateTime.Now;
                                            info.CategoryID = getdetails.CategoryID;
                                            info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                            info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                                            //update AD 

                                            aDServices.Update(getdetails);
                                            aDServices.Save();

                                            //Insert Manifest
                                            manifestServices.Insert(info);
                                            manifestServices.Save();

                                            if (getdetails.IsLoad == true)
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.Load);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.Load = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtLoad.Text = gettransportdetails.Load.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                            else
                                            {
                                                Int64 getPrioritySeatsFromDb = Convert.ToInt64(gettransportdetails.NoOfSeats);
                                                finalseatsavailable = getPrioritySeatsFromDb - 1;
                                                gettransportdetails.ID = TransportDetailID;
                                                gettransportdetails.NoOfSeats = finalseatsavailable;
                                                transportDetailsServices.Update(gettransportdetails);
                                                transportDetailsServices.Save();
                                                txtPrioritySeats.Text = gettransportdetails.PrioritySeats.ToString();
                                                Int64 seatinfo = Convert.ToInt64(gettransportdetails.PrioritySeats) + Convert.ToInt64(gettransportdetails.NoOfSeats) + Convert.ToInt64(gettransportdetails.Load);
                                                txtNoOfSeats.Text = gettransportdetails.NoOfSeats.ToString();
                                                lblSeatsinfo.Text = seatinfo.ToString();
                                            }
                                        }
                                    }
                                }
                                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!')", true);

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindCities();
                                GetTransportDetails();
                                Bind(checkseats.ID);
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                BindADEntryLoad();
                                if (checkseats.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (checkseats.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = checkseats.ID.ToString();
                                ddlCity.SelectedValue = checkseats.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void btnDeleteManifest_Click(object sender, EventArgs e)
        {
            manifestServices = new ManifestServices(new TCContext());
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());

            var ADIDs = manifestServices.GetManifestWithManifestNo(txtManifestNo.Text.Trim(), Convert.ToInt32(ddlTransport.SelectedValue));
            if (ADIDs != null)
            {
                foreach (var list in ADIDs)
                {
                    Int64 ADID = list.ADID;
                    Int64 TransportID = list.TransportDetailID;
                    Session["TID"] = TransportID;
                    Int64 ID = list.ID;
                    var getdetails = aDServices.GetByID(ADID);
                    var transportdetails = transportDetailsServices.GetDetailsByID(TransportID);
                    if (getdetails != null)
                    {
                        transportdetails.ID = TransportID;
                        if (getdetails.IsPriority == true)
                        {
                            transportdetails.PrioritySeats = transportdetails.PrioritySeats + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        else if (getdetails.IsLoad == true)
                        {
                            transportdetails.Load = transportdetails.Load + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        else
                        {
                            transportdetails.NoOfSeats = transportdetails.NoOfSeats + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        getdetails.ID = ADID;
                        getdetails.IsManifest = false;
                        aDServices.Update(getdetails);
                        aDServices.Save();
                        manifestServices.Delete(ID);
                    }
                }
                transportDetailsServices.Delete(ADIDs[0].TransportDetailID);

                //clear();
                //BindADEntryPriority();
                //BindADEntryGeneral();
                //BindADEntryReserve();
                //BindADEntryLoad();
                //GetTransportDetails();
                //BindTransportType();
                //BindCityPriority();
                //BindCityReserve();
                //BindCityGeneral();
                //BindCityLoad();
                Response.Redirect("Manifest");
            }
        }

        protected void txtManifestNo_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            manifestServices = new ManifestServices(new TCContext());
            string sID = Convert.ToString(hfddlTransportDetails.Value);
            if (sID != "")
            {
                Int64 ID = Convert.ToInt64(sID);
                var CheckManifestExist = manifestServices.CheckManifestExist(txtManifestNo.Text.Trim());
                if (CheckManifestExist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Madifest No Exist";
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Please Select Transport Details.";
            }
            //DropDownList tbox = this.ddlTransport.FindControl("ddlTransport") as DropDownList;
            //if (tbox != null)
            //{
            //    ScriptManager.GetCurrent(this.Page).SetFocus(tbox);
            //}
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string manifestno = txtManifestNo.Text.Trim();
            DateTime date = Convert.ToDateTime(hfDate.Value);
            int cityid = Convert.ToInt32(ddlCity.SelectedValue);
            if (manifestno != null)
            {
                Response.Redirect("ManifestCombineReport?ManifestNo=" + manifestno + "&" + "Date=" + Convert.ToDateTime(date) + "&" + "CityId=" + cityid + "");
            }
        }

        protected void ddlCityGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            string sID = Convert.ToString(hfCityGeneral.Value);

            if (sID == "")
            {
                BindADEntryGeneral();
                ddlCityGeneral.SelectedValue = "";
            }
            else
            {
                Int64 ID = Convert.ToInt64(sID);
                BindADEntryGeneralCity(ID);
                ddlCityGeneral.SelectedValue = ID.ToString();
            }
        }

        protected void ddlCityPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            string sID = Convert.ToString(hfCityPriority.Value);

            if (sID == "")
            {
                BindADEntryPriority();
                ddlCityPriority.SelectedValue = "";
            }
            else
            {
                Int64 ID = Convert.ToInt64(sID);
                BindADEntryPriorityCity(ID);
                ddlCityPriority.SelectedValue = ID.ToString();
            }
        }

        protected void ddlCityReserve_SelectedIndexChanged(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            string sID = Convert.ToString(hfCityReserve.Value);

            if (sID == "")
            {
                BindADEntryReserve();
                ddlCityReserve.SelectedValue = "";
            }
            else
            {
                Int64 ID = Convert.ToInt64(sID);
                BindADEntryReserveCity(ID);
                ddlCityReserve.SelectedValue = ID.ToString();
            }
        }

        protected void ddlLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            string sID = Convert.ToString(hfCityLoad.Value);

            if (sID == "")
            {
                BindADEntryLoad();
                ddlCityLoad.SelectedValue = "";
            }
            else
            {
                Int64 ID = Convert.ToInt64(sID);
                BindADEntryLoadCity(ID);
                ddlCityLoad.SelectedValue = ID.ToString();
            }
        }

        protected void btnAddBulk_Click(object sender, EventArgs e)
        {
            if (txtAddNo.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Enter Number To Finalize.";
            }
            else if (Convert.ToInt32(txtAddNo.Text) < 0)
            {
                lblError.Visible = true;
                lblError.Text = "Number Can Not Be Less Than 0.";
            }
            else if (ddlTransport.SelectedValue == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select Transport.";
            }
            else
            {
                lblError.Visible = false;
                lblError.Text = "";
                aDServices = new ADServices(new TCContext());
                transportDetailsServices = new TransportDetailsServices(new TCContext());
                manifestServices = new ManifestServices(new TCContext());
                //var allads = aDServices.GetAllADLeft();
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var gettransportdetails = transportDetailsServices.GetDetailsByID(TransportDetailID);
                var allads = aDServices.GetAllADLeft(Convert.ToInt64(gettransportdetails.CityID));
                allads = allads.Where(x => x.IsFly == true).ToList();
                BusinessLayer.ADEntery infoAD = new BusinessLayer.ADEntery();
                BusinessLayer.Manifest info = new BusinessLayer.Manifest();

                int totalseats = Convert.ToInt32(gettransportdetails.PrioritySeats) + Convert.ToInt32(gettransportdetails.NoOfSeats) + Convert.ToInt32(gettransportdetails.Load);
                if (totalseats == 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "All Seats Are Full.";
                }
                else
                {
                    if (ddlBulkAdd.SelectedItem.Text.ToLower() == "priority")
                    {
                        var priorityAds = from p in allads
                                          where p.IsPriority == true && p.IsTempHold == false && (p.CategoryName.ToLower().Contains("off") || p.CategoryName.ToLower().Contains("officer"))
                                          select p;
                        foreach (var res in priorityAds.Take(Convert.ToInt32(txtAddNo.Text)))
                        {
                            var getprioseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                            if (getprioseats.PrioritySeats == 0)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Priority Seats Are Full.";
                            }
                            else
                            {
                                var getadbyid = aDServices.GetByID(res.ID);

                                info.ADID = getadbyid.ID;
                                info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                info.Session = ddlSession.SelectedItem.Text;
                                info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                info.ADNO = getadbyid.ADNO;
                                info.ADID = getadbyid.ID;
                                info.ArmyNo = getadbyid.ArmyNo;
                                info.RankID = getadbyid.RankID;
                                info.Name = getadbyid.Name;
                                info.UnitID = getadbyid.UnitID;
                                info.ICard = getadbyid.ICard;
                                info.TransportDetails = ddlTransport.SelectedItem.Text;
                                info.HQID = getadbyid.HQID;
                                info.CreatedOn = DateTime.Now;
                                info.CategoryID = getadbyid.CategoryID;
                                info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                info.CityID = Convert.ToInt64(ddlCity.SelectedValue);

                                //insert in manifest
                                manifestServices.Insert(info);
                                manifestServices.Save();

                                //update AD
                                infoAD = getadbyid;
                                infoAD.IsManifest = true;
                                infoAD.ID = res.ID;
                                aDServices.Update(infoAD);
                                aDServices.Save();

                                //update transport seats
                                getprioseats.PrioritySeats = getprioseats.PrioritySeats - 1;
                                transportDetailsServices.Update(getprioseats);
                                transportDetailsServices.Save();
                                txtNoOfSeats.Text = getprioseats.NoOfSeats.ToString();
                                txtPrioritySeats.Text = getprioseats.PrioritySeats.ToString();
                                txtLoad.Text = getprioseats.Load.ToString();
                                lblSeatsinfo.Text = (getprioseats.NoOfSeats + getprioseats.PrioritySeats + getprioseats.Load).ToString();
                                lblTotalSeats.Text = getprioseats.TotalNoOfSeats.ToString();

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindCities();
                                GetTransportDetails();
                                Bind(TransportDetailID);
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                BindADEntryLoad();
                                if (gettransportdetails.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (gettransportdetails.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = gettransportdetails.ID.ToString();
                                ddlCity.SelectedValue = gettransportdetails.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                    else if (ddlBulkAdd.SelectedItem.Text.ToLower() == "load")
                    {
                        var loadAds = from p in allads
                                      where p.IsPriority == false && p.IsTempHold == false && p.IsLoad == true && (p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other"))
                                      select p;
                        foreach (var res in loadAds.Take(Convert.ToInt32(txtAddNo.Text)))
                        {
                            var getLoadSeats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                            if (getLoadSeats.Load == 0)
                            {
                                lblError.Visible = true;
                                lblError.Text = "Load Is Full.";
                            }
                            else
                            {
                                var getadbyid = aDServices.GetByID(res.ID);

                                info.ADID = getadbyid.ID;
                                info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                info.Session = ddlSession.SelectedItem.Text;
                                info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                info.ADNO = getadbyid.ADNO;
                                info.ADID = getadbyid.ID;
                                info.ArmyNo = getadbyid.ArmyNo;
                                info.RankID = getadbyid.RankID;
                                info.Name = getadbyid.Name;
                                info.UnitID = getadbyid.UnitID;
                                info.ICard = getadbyid.ICard;
                                info.TransportDetails = ddlTransport.SelectedItem.Text;
                                info.HQID = getadbyid.HQID;
                                info.CreatedOn = DateTime.Now;
                                info.CategoryID = getadbyid.CategoryID;
                                info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                info.CityID = Convert.ToInt64(ddlCity.SelectedValue);

                                //insert in manifest
                                manifestServices.Insert(info);
                                manifestServices.Save();

                                //update AD
                                infoAD = getadbyid;
                                infoAD.IsManifest = true;
                                infoAD.ID = res.ID;
                                aDServices.Update(infoAD);
                                aDServices.Save();

                                //update transport seats
                                getLoadSeats.Load = getLoadSeats.Load - 1;
                                transportDetailsServices.Update(getLoadSeats);
                                transportDetailsServices.Save();
                                txtNoOfSeats.Text = getLoadSeats.NoOfSeats.ToString();
                                txtPrioritySeats.Text = getLoadSeats.PrioritySeats.ToString();
                                txtLoad.Text = getLoadSeats.Load.ToString();
                                lblSeatsinfo.Text = (getLoadSeats.NoOfSeats + getLoadSeats.PrioritySeats + getLoadSeats.Load).ToString();
                                lblTotalSeats.Text = getLoadSeats.TotalNoOfSeats.ToString();

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindCities();
                                GetTransportDetails();
                                Bind(TransportDetailID);
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                BindADEntryLoad();
                                if (gettransportdetails.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (gettransportdetails.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = gettransportdetails.ID.ToString();
                                ddlCity.SelectedValue = gettransportdetails.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                    else if (ddlBulkAdd.SelectedItem.Text.ToLower() == "normal")
                    {
                        var generalAds = from p in allads
                                         where p.IsPriority == false && p.IsTempHold == false && p.IsLoad == false && p.IsReserve == false && (p.CategoryName.ToLower().Contains("off") || p.CategoryName.ToLower().Contains("officer"))
                                         select p;
                        foreach (var res in generalAds.Take(Convert.ToInt32(txtAddNo.Text)))
                        {
                            var getgeneralSeats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                            if (getgeneralSeats.NoOfSeats == 0)
                            {
                                lblError.Visible = true;
                                lblError.Text = "General Seats Are Full.";
                            }
                            else
                            {
                                var getadbyid = aDServices.GetByID(res.ID);

                                info.ADID = getadbyid.ID;
                                info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                info.Session = ddlSession.SelectedItem.Text;
                                info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                info.ADNO = getadbyid.ADNO;
                                info.ADID = getadbyid.ID;
                                info.ArmyNo = getadbyid.ArmyNo;
                                info.RankID = getadbyid.RankID;
                                info.Name = getadbyid.Name;
                                info.UnitID = getadbyid.UnitID;
                                info.ICard = getadbyid.ICard;
                                info.TransportDetails = ddlTransport.SelectedItem.Text;
                                info.HQID = getadbyid.HQID;
                                info.CreatedOn = DateTime.Now;
                                info.CategoryID = getadbyid.CategoryID;
                                info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                info.CityID = Convert.ToInt64(ddlCity.SelectedValue);

                                //insert in manifest
                                manifestServices.Insert(info);
                                manifestServices.Save();

                                //update AD
                                infoAD = getadbyid;
                                infoAD.IsManifest = true;
                                infoAD.ID = res.ID;
                                aDServices.Update(infoAD);
                                aDServices.Save();

                                //update transport seats
                                getgeneralSeats.NoOfSeats = getgeneralSeats.NoOfSeats - 1;
                                transportDetailsServices.Update(getgeneralSeats);
                                transportDetailsServices.Save();
                                txtNoOfSeats.Text = getgeneralSeats.NoOfSeats.ToString();
                                txtPrioritySeats.Text = getgeneralSeats.PrioritySeats.ToString();
                                txtLoad.Text = getgeneralSeats.Load.ToString();
                                lblSeatsinfo.Text = (getgeneralSeats.NoOfSeats + getgeneralSeats.PrioritySeats + getgeneralSeats.Load).ToString();
                                lblTotalSeats.Text = getgeneralSeats.TotalNoOfSeats.ToString();

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindCities();
                                GetTransportDetails();
                                Bind(TransportDetailID);
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                BindADEntryLoad();
                                if (gettransportdetails.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (gettransportdetails.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = gettransportdetails.ID.ToString();
                                ddlCity.SelectedValue = gettransportdetails.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                    else if (ddlBulkAdd.SelectedItem.Text.ToLower() == "reserve")
                    {
                        var generalAds = from p in allads
                                         where p.IsPriority == false && p.IsTempHold == false && p.IsLoad == false && p.IsReserve == true && (p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other"))
                                         select p;
                        foreach (var res in generalAds.Take(Convert.ToInt32(txtAddNo.Text)))
                        {
                            var getgeneralSeats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                            if (getgeneralSeats.NoOfSeats == 0)
                            {
                                lblError.Visible = true;
                                lblError.Text = "General Seats Are Full.";
                            }
                            else
                            {
                                var getadbyid = aDServices.GetByID(res.ID);

                                info.ADID = getadbyid.ID;
                                info.ManifestDate = Convert.ToDateTime(hfDate.Value);
                                info.Session = ddlSession.SelectedItem.Text;
                                info.MenifestNo = txtManifestNo.Text.Trim().ToUpper();
                                info.ADNO = getadbyid.ADNO;
                                info.ADID = getadbyid.ID;
                                info.ArmyNo = getadbyid.ArmyNo;
                                info.RankID = getadbyid.RankID;
                                info.Name = getadbyid.Name;
                                info.UnitID = getadbyid.UnitID;
                                info.ICard = getadbyid.ICard;
                                info.TransportDetails = ddlTransport.SelectedItem.Text;
                                info.HQID = getadbyid.HQID;
                                info.CreatedOn = DateTime.Now;
                                info.CategoryID = getadbyid.CategoryID;
                                info.TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                                info.CityID = Convert.ToInt64(ddlCity.SelectedValue);

                                //insert in manifest
                                manifestServices.Insert(info);
                                manifestServices.Save();

                                //update AD
                                infoAD = getadbyid;
                                infoAD.IsManifest = true;
                                infoAD.ID = res.ID;
                                aDServices.Update(infoAD);
                                aDServices.Save();

                                //update transport seats
                                getgeneralSeats.NoOfSeats = getgeneralSeats.NoOfSeats - 1;
                                transportDetailsServices.Update(getgeneralSeats);
                                transportDetailsServices.Save();
                                txtNoOfSeats.Text = getgeneralSeats.NoOfSeats.ToString();
                                txtPrioritySeats.Text = getgeneralSeats.PrioritySeats.ToString();
                                txtLoad.Text = getgeneralSeats.Load.ToString();
                                lblSeatsinfo.Text = (getgeneralSeats.NoOfSeats + getgeneralSeats.PrioritySeats + getgeneralSeats.Load).ToString();
                                lblTotalSeats.Text = getgeneralSeats.TotalNoOfSeats.ToString();

                                BindADEntryPriority();
                                BindADEntryGeneral();
                                BindADEntryReserve();
                                BindCities();
                                GetTransportDetails();
                                Bind(TransportDetailID);
                                //BindCat();
                                BindCityReserve();
                                BindCityGeneral();
                                BindCityPriority();
                                BindADEntryLoad();
                                if (gettransportdetails.Session == "FN")
                                {
                                    ddlSession.SelectedValue = "1";
                                }
                                else if (gettransportdetails.Session == "AN")
                                {
                                    ddlSession.SelectedValue = "2";
                                }
                                ddlTransport.SelectedValue = gettransportdetails.ID.ToString();
                                ddlCity.SelectedValue = gettransportdetails.CityID.ToString();
                                btnDeleteManifest.Visible = true;
                                btnPrint.Visible = true;
                                txtManifestNo.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void grdReserve_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdReserve_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Int32 Id = Convert.ToInt32(grdReserve.DataKeys[row.RowIndex].Value);

                if (e.CommandName == "Edit")
                {
                    Response.Redirect("ADEntery?ADID=" + Convert.ToString(Id));
                }
                else if (e.CommandName == "Delete")
                {
                    manifestServices = new ManifestServices(new TCContext());
                    aDServices = new ADServices(new TCContext());
                    transportDetailsServices = new TransportDetailsServices(new TCContext());
                    var getdetailsmanifest = manifestServices.GetByID(Id);
                    var getdetails = aDServices.GetByID(getdetailsmanifest.ADID);
                    var transportdetails = transportDetailsServices.GetDetailsByID(getdetailsmanifest.TransportDetailID);
                    if (getdetails != null)
                    {
                        transportdetails.ID = getdetailsmanifest.TransportDetailID;
                        if (getdetails.IsPriority == true)
                        {
                            transportdetails.PrioritySeats = transportdetails.PrioritySeats + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        else if (getdetails.IsLoad == true)
                        {
                            transportdetails.Load = transportdetails.Load + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        else
                        {
                            transportdetails.NoOfSeats = transportdetails.NoOfSeats + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        getdetails.ID = getdetailsmanifest.ADID;
                        getdetails.IsManifest = false;
                        aDServices.Update(getdetails);
                        aDServices.Save();
                    }

                    manifestServices.Delete(Id);
                    BindADEntryPriority();
                    BindADEntryGeneral();
                    BindADEntryReserve();
                    BindADEntryLoad();
                    BindCities();
                    GetTransportDetails();
                    //BindCat();
                    BindCityReserve();
                    BindCityGeneral();
                    BindCityPriority();
                    Bind(transportdetails.ID);
                    ddlTransport.SelectedValue = transportdetails.ID.ToString();
                    ddlCity.SelectedValue = transportdetails.CityID.ToString();
                    txtNoOfSeats.Text = transportdetails.NoOfSeats.ToString();
                    txtPrioritySeats.Text = transportdetails.PrioritySeats.ToString();
                    txtLoad.Text = transportdetails.Load.ToString();
                    Int64 seatsavailable = Convert.ToInt64(transportdetails.NoOfSeats) + Convert.ToInt64(transportdetails.PrioritySeats) + Convert.ToInt64(transportdetails.Load);
                    lblSeatsinfo.Text = seatsavailable.ToString();
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }

        }

        protected void btnPrintReserve_Click(object sender, EventArgs e)
        {
            string manifestno = txtManifestNo.Text.Trim();
            DateTime date = Convert.ToDateTime(hfDate.Value);
            int cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (manifestno != null)
            {
                Response.Redirect("ReserveManifestReport?ManifestNo=" + manifestno + "&" + "Date=" + Convert.ToDateTime(date) + "&" + "CityId=" + cityid + "");
            }
        }

        protected void btnMovePriorityGen_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptGeneral.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDGeneral");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = true;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnMoveReserveGen_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptGeneral.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDGeneral");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsReserve = true;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnMoveLoadGen_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptGeneral.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDGeneral");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsLoad = true;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void txtBUlkMoveFromGen_Click(object sender, EventArgs e)
        {
            if (ddlBulkMoveFromGeneral.SelectedValue == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Select Type From DropDown.";
            }

            if (txtBulkMove.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Please Fill Value In Text Box";
            }
            if (txtBulkMove.Text != string.Empty)
            {
                if (Convert.ToInt32(txtBulkMove.Text) < 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Value Can Not be Less Than Zero.";
                }
                else
                {
                    lblError.Visible = false;

                    if (ddlTransport.SelectedValue.ToString() != string.Empty)
                    {
                        Int64 TId = Convert.ToInt64(ddlTransport.SelectedValue);
                        transportDetailsServices = new TransportDetailsServices(new TCContext());
                        var checkseats = transportDetailsServices.GetDetailsByID(TId);

                        if (TId.ToString() != string.Empty)
                        {
                            aDServices = new ADServices(new TCContext());
                            BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();

                            var data = aDServices.ADEntryNoGeneralCity(Convert.ToInt64(ddlCity.SelectedValue)).Take(Convert.ToInt32(txtBulkMove.Text));

                            if (data != null)
                            {
                                if (ddlBulkMoveFromGeneral.SelectedValue == "1")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsPriority = true;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlBulkMoveFromGeneral.SelectedValue == "2")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsLoad = true;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlBulkMoveFromGeneral.SelectedValue == "3")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsReserve = true;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }

                            }
                            BindADEntryPriority();
                            BindADEntryGeneral();
                            BindADEntryReserve();
                            BindCities();
                            GetTransportDetails();
                            BindADEntryLoad();
                            Bind(checkseats.ID);
                            //BindCat();
                            BindCityReserve();
                            BindCityGeneral();
                            BindCityPriority();
                            if (checkseats.Session == "FN")
                            {
                                ddlSession.SelectedValue = "1";
                            }
                            else if (checkseats.Session == "AN")
                            {
                                ddlSession.SelectedValue = "2";
                            }
                            ddlTransport.SelectedValue = checkseats.ID.ToString();
                            ddlCity.SelectedValue = checkseats.CityID.ToString();
                            btnDeleteManifest.Visible = true;
                            btnPrint.Visible = true;
                            txtManifestNo.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnBulkMoveReserve_Click(object sender, EventArgs e)
        {
            if (ddlBulkMoveFromGeneral.SelectedValue == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Select Type From DropDown.";
            }

            if (txtBulkFromReserve.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Please Fill Value In Text Box";
            }
            if (txtBulkFromReserve.Text != string.Empty)
            {
                if (Convert.ToInt32(txtBulkFromReserve.Text) < 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Value Can Not be Less Than Zero.";
                }
                else
                {
                    lblError.Visible = false;

                    if (ddlTransport.SelectedValue.ToString() != string.Empty)
                    {
                        Int64 TId = Convert.ToInt64(ddlTransport.SelectedValue);
                        transportDetailsServices = new TransportDetailsServices(new TCContext());
                        var checkseats = transportDetailsServices.GetDetailsByID(TId);

                        if (TId.ToString() != string.Empty)
                        {
                            aDServices = new ADServices(new TCContext());
                            BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();

                            var data = aDServices.ADEntryNoReserveCity(Convert.ToInt64(ddlCity.SelectedValue)).Take(Convert.ToInt32(txtBulkFromReserve.Text));

                            if (data != null)
                            {
                                if (ddlBulkFromReserve.SelectedValue == "1")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsPriority = true;
                                        info.IsReserve = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlBulkFromReserve.SelectedValue == "2")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsLoad = true;
                                        info.IsReserve = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlBulkFromReserve.SelectedValue == "3")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsReserve = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }

                            }
                            BindADEntryPriority();
                            BindADEntryGeneral();
                            BindADEntryReserve();
                            BindCities();
                            GetTransportDetails();
                            BindADEntryLoad();
                            Bind(checkseats.ID);
                            //BindCat();
                            BindCityReserve();
                            BindCityGeneral();
                            BindCityPriority();
                            if (checkseats.Session == "FN")
                            {
                                ddlSession.SelectedValue = "1";
                            }
                            else if (checkseats.Session == "AN")
                            {
                                ddlSession.SelectedValue = "2";
                            }
                            ddlTransport.SelectedValue = checkseats.ID.ToString();
                            ddlCity.SelectedValue = checkseats.CityID.ToString();
                            btnDeleteManifest.Visible = true;
                            btnPrint.Visible = true;
                            txtManifestNo.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnNormalMove_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptReserve.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDReserve");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = false;
                            Info.IsReserve = false;
                            Info.IsLoad = false;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnLoadMove_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptReserve.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDReserve");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = false;
                            Info.IsReserve = false;
                            Info.IsLoad = true;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnPriorityMove_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptReserve.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDReserve");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = true;
                            Info.IsReserve = false;
                            Info.IsLoad = false;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnLoadBulkMove_Click(object sender, EventArgs e)
        {
            if (ddlLoadBulk.SelectedValue == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Select Type From DropDown.";
            }

            if (txtLoadBulk.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Please Fill Value In Text Box";
            }
            if (txtLoadBulk.Text != string.Empty)
            {
                if (Convert.ToInt32(txtLoadBulk.Text) < 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Value Can Not be Less Than Zero.";
                }
                else
                {
                    lblError.Visible = false;

                    if (ddlTransport.SelectedValue.ToString() != string.Empty)
                    {
                        Int64 TId = Convert.ToInt64(ddlTransport.SelectedValue);
                        transportDetailsServices = new TransportDetailsServices(new TCContext());
                        var checkseats = transportDetailsServices.GetDetailsByID(TId);

                        if (TId.ToString() != string.Empty)
                        {
                            aDServices = new ADServices(new TCContext());
                            BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();

                            var data = aDServices.ADEntryNoLoadCity(Convert.ToInt64(ddlCity.SelectedValue)).Take(Convert.ToInt32(txtLoadBulk.Text));

                            if (data != null)
                            {
                                if (ddlLoadBulk.SelectedValue == "1")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsPriority = true;
                                        info.IsLoad = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlLoadBulk.SelectedValue == "2")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsReserve = false;
                                        info.IsLoad = false;
                                        info.IsPriority = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlLoadBulk.SelectedValue == "3")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsReserve = true;
                                        info.IsLoad = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }

                            }
                            BindADEntryPriority();
                            BindADEntryGeneral();
                            BindADEntryReserve();
                            BindCities();
                            GetTransportDetails();
                            BindADEntryLoad();
                            Bind(checkseats.ID);
                            //BindCat();
                            BindCityReserve();
                            BindCityGeneral();
                            BindCityPriority();
                            if (checkseats.Session == "FN")
                            {
                                ddlSession.SelectedValue = "1";
                            }
                            else if (checkseats.Session == "AN")
                            {
                                ddlSession.SelectedValue = "2";
                            }
                            ddlTransport.SelectedValue = checkseats.ID.ToString();
                            ddlCity.SelectedValue = checkseats.CityID.ToString();
                            btnDeleteManifest.Visible = true;
                            btnPrint.Visible = true;
                            txtManifestNo.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnPriorityLoad_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptLoad.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDLoad");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = true;
                            Info.IsReserve = false;
                            Info.IsLoad = false;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnNormalLoad_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptLoad.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDLoad");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = false;
                            Info.IsReserve = false;
                            Info.IsLoad = false;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnReserveLoad_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptLoad.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkIDLoad");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = false;
                            Info.IsReserve = true;
                            Info.IsLoad = false;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnBulkMovePriority_Click(object sender, EventArgs e)
        {
            if (ddlPriorityBulk.SelectedValue == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Select Type From DropDown.";
            }

            if (txtPriorityBulk.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Please Fill Value In Text Box";
            }
            if (txtPriorityBulk.Text != string.Empty)
            {
                if (Convert.ToInt32(txtPriorityBulk.Text) < 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Value Can Not be Less Than Zero.";
                }
                else
                {
                    lblError.Visible = false;

                    if (ddlTransport.SelectedValue.ToString() != string.Empty)
                    {
                        Int64 TId = Convert.ToInt64(ddlTransport.SelectedValue);
                        transportDetailsServices = new TransportDetailsServices(new TCContext());
                        var checkseats = transportDetailsServices.GetDetailsByID(TId);

                        if (TId.ToString() != string.Empty)
                        {
                            aDServices = new ADServices(new TCContext());
                            BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();

                            var data = aDServices.ADEntryNoPriorityCity(Convert.ToInt64(ddlCity.SelectedValue)).Take(Convert.ToInt32(txtPriorityBulk.Text));

                            if (data != null)
                            {
                                if (ddlPriorityBulk.SelectedValue == "1")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsPriority = false;
                                        info.IsLoad = true;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlPriorityBulk.SelectedValue == "2")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsReserve = false;
                                        info.IsLoad = false;
                                        info.IsPriority = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }
                                else if (ddlPriorityBulk.SelectedValue == "3")
                                {
                                    foreach (var res in data)
                                    {
                                        var aDDetails = aDServices.GetByID(res.ID);
                                        info = aDDetails;
                                        info.IsReserve = true;
                                        info.IsPriority = false;
                                        aDServices.Update(info);
                                        aDServices.Save();
                                    }
                                }

                            }
                            BindADEntryPriority();
                            BindADEntryGeneral();
                            BindADEntryReserve();
                            BindCities();
                            GetTransportDetails();
                            BindADEntryLoad();
                            Bind(checkseats.ID);
                            //BindCat();
                            BindCityReserve();
                            BindCityGeneral();
                            BindCityPriority();
                            if (checkseats.Session == "FN")
                            {
                                ddlSession.SelectedValue = "1";
                            }
                            else if (checkseats.Session == "AN")
                            {
                                ddlSession.SelectedValue = "2";
                            }
                            ddlTransport.SelectedValue = checkseats.ID.ToString();
                            ddlCity.SelectedValue = checkseats.CityID.ToString();
                            btnDeleteManifest.Visible = true;
                            btnPrint.Visible = true;
                            txtManifestNo.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnPrioLoad_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptPriority.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkPriority");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = false;
                            Info.IsReserve = false;
                            Info.IsLoad = true;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnPrioNormal_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptPriority.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkPriority");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = false;
                            Info.IsReserve = false;
                            Info.IsLoad = false;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = false;
                txtManifestNo.Enabled = false;
            }
        }

        protected void btnPrioReserve_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            if (ddlTransport.SelectedValue.ToString() != string.Empty)
            {
                Int64 TransportDetailID = Convert.ToInt64(ddlTransport.SelectedValue);
                var checkseats = transportDetailsServices.GetDetailsByID(TransportDetailID);
                foreach (RepeaterItem rptr in rptPriority.Items)
                {
                    CheckBox chklist = (CheckBox)rptr.FindControl("chkPriority");
                    HiddenField hfID = (HiddenField)rptr.FindControl("hfPriorityID");
                    Int64 ID = Convert.ToInt64(hfID.Value);
                    if (chklist.Checked == true)
                    {
                        var getAdD = aDServices.GetByID(ID);
                        if (getAdD != null)
                        {
                            BusinessLayer.ADEntery Info = new BusinessLayer.ADEntery();
                            Info = getAdD;
                            Info.IsPriority = false;
                            Info.IsReserve = true;
                            Info.IsLoad = false;
                            aDServices.Update(Info);
                            aDServices.Save();
                        }
                    }
                }
                BindADEntryPriority();
                BindADEntryGeneral();
                BindADEntryReserve();
                BindCities();
                GetTransportDetails();
                BindADEntryLoad();
                Bind(TransportDetailID);
                //BindCat();
                BindCityReserve();
                BindCityGeneral();
                BindCityPriority();
                if (checkseats.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else if (checkseats.Session == "AN")
                {
                    ddlSession.SelectedValue = "2";
                }
                ddlTransport.SelectedValue = checkseats.ID.ToString();
                ddlCity.SelectedValue = checkseats.CityID.ToString();
                btnDeleteManifest.Visible = true;
                btnPrint.Visible = true;
                txtManifestNo.Enabled = false;
            }
        }
    }
}