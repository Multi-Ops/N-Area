using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Reports
{
    public partial class CancellationReportDateWise : System.Web.UI.Page
    {
        ITransportDetailsServices transportDetailServices;
        ICategoryServices categoryServices;
        ICampServices campServices;
        ICancelServices cancelServices;
        ICityServices cityServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCities();
            }
        }

        protected void BindCities()
        {
            cityServices = new CityServices(new TCContext());
            var data = cityServices.GetCityDetails();
            ddlCities.DataSource = data;
            ddlCities.DataValueField = "ID";
            ddlCities.DataTextField = "CityName";
            ddlCities.DataBind();
        }

        protected void BindSelected(DateTime fromDate, DateTime toDate)
        {
            transportDetailServices = new TransportDetailsServices(new TCContext());
            categoryServices = new CategoryServices(new TCContext());
            cancelServices = new CancelServices(new TCContext());

            int jcos = 0;
            int officers = 0;
            int load = 0;
            int other = 0;
            int total = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add("Manifest");
            dt.Columns.Add("TransportDetails");
            dt.Columns.Add("TransportType");
            dt.Columns.Add("Date");
            dt.Columns.Add("CancelDate");
            dt.Columns.Add("Jcos");
            dt.Columns.Add("Offices");
            dt.Columns.Add("Others");
            dt.Columns.Add("Load");
            dt.Columns.Add("Total");

            string fromdateday = fromDate.Day.ToString();
            string fromdatemonth = fromDate.Month.ToString();
            string fromdateyear = fromDate.Year.ToString();

            string todateday = toDate.Day.ToString();
            string todatemonth = toDate.Month.ToString();
            string todateyear = toDate.Year.ToString();

            string finalstringFrom = fromdateyear + "-" + fromdatemonth + "-" + fromdateday;
            string finalstringTo = todateyear + "-" + todatemonth + "-" + todateday;
            DateTime finaldateFrom = DateTime.Parse(finalstringFrom);
            DateTime finaldateTo = DateTime.Parse(finalstringTo);

            var data = cancelServices.GetManifestByDateRange(finaldateFrom, finaldateTo, Convert.ToInt32(ddlCities.SelectedValue));

            if (data != null)
            {
                foreach (var res in data)
                {
                    DataRow dr = dt.NewRow();

                    var getdatadetails = cancelServices.GetManifestForReportByDateRange(res.MenifestNo, finaldateFrom, finaldateTo, Convert.ToInt32(ddlCities.SelectedValue));

                    if (getdatadetails != null)
                    {
                        dr["Manifest"] = getdatadetails.MenifestNo;
                        dr["Date"] = getdatadetails.ManifestDate;
                        DateTime canceldate = Convert.ToDateTime(getdatadetails.CreatedOn);
                        dr["CancelDate"] = canceldate.ToString("dd-MM-yyyy");
                        var transportdata = transportDetailServices.GetDetailsByID(getdatadetails.TransportDetailID, Convert.ToInt32(ddlCities.SelectedValue));
                        if (transportdata != null)
                        {
                            dr["TransportDetails"] = transportdata.TransportDetail;
                            dr["TransportType"] = transportdata.TransportType;

                            var getcats = categoryServices.details();
                            if (getcats != null)
                            {
                                foreach (var cats in getcats)
                                {
                                    if (cats.CategoryName.Trim().ToLower() == "jcos" || cats.CategoryName.Trim().ToLower() == "jco")
                                    {
                                        var count = cancelServices.GetManifestADByCategoryID(cats.ID, getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
                                        if (Convert.ToInt32(count) != 0)
                                        {
                                            dr["Jcos"] = count;
                                            jcos = Convert.ToInt32(count);
                                        }
                                        else
                                        {
                                            dr["Jcos"] = 0;
                                            jcos = Convert.ToInt32(count);
                                        }
                                    }
                                    if (cats.CategoryName.Trim().ToLower() == "officers" || cats.CategoryName.Trim().ToLower() == "officer")
                                    {
                                        var count = cancelServices.GetManifestADByCategoryID(cats.ID, getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
                                        if (Convert.ToInt32(count) != 0)
                                        {
                                            dr["Offices"] = count;
                                            officers = Convert.ToInt32(count);
                                        }
                                        else
                                        {
                                            dr["Offices"] = 0;
                                            officers = Convert.ToInt32(count);
                                        }
                                    }
                                    if (cats.CategoryName.Trim().ToLower() == "others" || cats.CategoryName.Trim().ToLower() == "or" || cats.CategoryName.Trim().ToLower() == "other")
                                    {
                                        var count = cancelServices.GetManifestADByCategoryID(cats.ID, getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
                                        if (Convert.ToInt32(count) != 0)
                                        {
                                            dr["Others"] = count;
                                            other = Convert.ToInt32(count);
                                        }
                                        else
                                        {
                                            dr["Others"] = 0;
                                            other = Convert.ToInt32(count);
                                        }
                                    }
                                }
                                var loadcount = cancelServices.GetManifestADByLoad(getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
                                if (Convert.ToInt32(loadcount) != 0)
                                {
                                    dr["Load"] = loadcount;
                                    other = Convert.ToInt32(loadcount);
                                }
                                total = jcos + officers + other + load;
                                dr["Total"] = total;
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }

            campServices = new CampServices(new TCContext());
            var camp = campServices.GetCampDetails();
            if (camp != null)
                lblCamp.Text = camp[0].CampName;

            lblFromDate.Text = fromDate.ToString("dd-MM-yyyy");
            lblToDate.Text = toDate.ToString("dd-MM-yyyy");
            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            Response.Redirect("CancellationReport");
        }

        protected void btnSearchAD_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (hfFromDate.Value != "" && hfToDate.Value != "")
            {
                DateTime selecteddateFrom = Convert.ToDateTime(Helper.CommonFunctions.ConvertToCreatedOnDate(hfFromDate.Value));
                DateTime selecteddateTo = Convert.ToDateTime(Helper.CommonFunctions.ConvertToCreatedOnDate(hfToDate.Value));
                if (selecteddateFrom > selecteddateTo)
                {
                    lblError.Text = "Select correct Dates.";
                    lblError.Visible = true;
                }
                BindSelected(selecteddateFrom, selecteddateTo);
                lblCity.Text = ddlCities.SelectedItem.Text;
                hfFromDate.Value = "";
                hfToDate.Value = "";
            }
            else
            {
                lblError.Text = "Select correct Dates.";
                lblError.Visible = true;
            }
        }
    }
}