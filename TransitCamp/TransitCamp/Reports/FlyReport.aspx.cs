using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Reports
{
    public partial class FlyReport : System.Web.UI.Page
    {
        IManifestServices manifestServices;
        ITransportDetailsServices transportDetailServices;
        ICategoryServices categoryServices;
        ICampServices campServices;
        ICityServices cityServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCities();
                BindSelected(DateTime.Now, Convert.ToInt32(ddlCities.SelectedValue));
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

        protected void BindSelected(DateTime date, int cid)
        {
            transportDetailServices = new TransportDetailsServices(new TCContext());
            categoryServices = new CategoryServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());

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
            dt.Columns.Add("ManifestDate");
            dt.Columns.Add("Jcos");
            dt.Columns.Add("Offices");
            dt.Columns.Add("Others");
            dt.Columns.Add("Load");
            dt.Columns.Add("Total");

            string dateday = date.Day.ToString();
            string datemonth = date.Month.ToString();
            string dateyear = date.Year.ToString();

            string finalstring = dateyear + "-" + datemonth + "-" + dateday;
            DateTime finaldate = DateTime.Parse(finalstring);

            var data = manifestServices.GetManifestDateWise(finaldate, Convert.ToInt32(ddlCities.SelectedValue));
            if (data != null)
            {
                foreach (var res in data)
                {
                    DataRow dr = dt.NewRow();

                    var getdatadetails = manifestServices.GetManifestForReportByDate(res.MenifestNo, finaldate, Convert.ToInt32(ddlCities.SelectedValue));
                    if (getdatadetails != null)
                    {
                        dr["Manifest"] = getdatadetails.MenifestNo;
                        dr["Date"] = getdatadetails.ManifestDate;
                        DateTime manifestDate = Convert.ToDateTime(getdatadetails.ManifestDate);
                        dr["ManifestDate"] = manifestDate.ToString("dd-MM-yyyy");
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
                                        var count = manifestServices.GetManifestADByCategoryID(cats.ID, getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
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
                                        var count = manifestServices.GetManifestADByCategoryID(cats.ID, getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
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
                                        var count = manifestServices.GetManifestADByCategoryID(cats.ID, getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
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
                                var loadcount = manifestServices.GetManifestADByLoad(getdatadetails.MenifestNo, Convert.ToInt32(ddlCities.SelectedValue));
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

            lblDate.Text = date.ToString("dd-MM-yyyy");
            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void btnSearchAD_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (hfFromDate.Value != "")
            {
                DateTime selecteddate = DateTime.Parse(hfFromDate.Value);
                BindSelected(selecteddate, Convert.ToInt32(ddlCities.SelectedValue));
                hfFromDate.Value = "";
                lblCity.Text = ddlCities.SelectedItem.Text;
            }
            else
            {
                lblError.Text = "Select Date";
                lblError.Visible = true;
            }
        }
    }
}