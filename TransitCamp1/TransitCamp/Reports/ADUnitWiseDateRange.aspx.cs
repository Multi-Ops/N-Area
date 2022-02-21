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
    public partial class ADUnitWiseDateRange : System.Web.UI.Page
    {
        IADServices aDServices;
        ICampServices campServices;
        ICityServices cityServices;
        IUnitServices unitServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnPrint.Visible = false;
                PrintDiv.Visible = false;
                BindCity();
            }
        }

        protected void BindCity()
        {
            cityServices = new CityServices(new TCContext());
            var data = cityServices.GetCityDetails();
            ddlCity.DataSource = data;
            ddlCity.DataValueField = "ID";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void Bind(DateTime from, DateTime to, Int64 id)
        {
            int officer = 0;
            int officerTotal = 0;
            int jcos = 0;
            int jcosTotal = 0;
            int other = 0;
            int otherTotal = 0;
            int total = 0;
            int Grandtotal = 0;
            int sn = 1;
            aDServices = new ADServices(new TCContext());
            campServices = new CampServices(new TCContext());
            unitServices = new UnitServices(new TCContext());
            DataTable dt = new DataTable();
            dt.Columns.Add("SNo");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Officer");
            dt.Columns.Add("Jco");
            dt.Columns.Add("Other");
            dt.Columns.Add("Total");
            var formation = unitServices.GetUnitDetails();
            if (formation.Count > 0)
            {
                foreach (var f in formation)
                {
                    DataRow dr = dt.NewRow();
                    dr["Unit"] = f.UnitName;
                    var data = aDServices.CityWiseReport(from, to, id);

                    data = (from p in data
                            where p.UnitName == f.UnitName
                            select p).ToList();

                    officer = (from p in data
                               where p.CategoryName.ToLower().Contains("officer")
                               select p).Count();

                    jcos = (from p in data
                            where p.CategoryName.ToLower().Contains("jco")
                            select p).Count();

                    other = (from p in data
                             where p.CategoryName.ToLower().Contains("other") || p.CategoryName.ToLower().Contains("or")
                             select p).Count();

                    total = officer + jcos + other;

                    if (total != 0)
                    {
                        dr["SNo"] = sn++;
                        dr["Officer"] = officer;
                        dr["Jco"] = jcos;
                        dr["Other"] = other;
                        dr["Total"] = total;
                        dt.Rows.Add(dr);
                    }
                    officerTotal = officerTotal + officer;
                    jcosTotal = jcosTotal + jcos;
                    otherTotal = otherTotal + other;
                    Grandtotal = Grandtotal + total;
                }
            }

            DataRow drTotlal = dt.NewRow();

            drTotlal["SNo"] = "Total";
            drTotlal["Unit"] = "All";
            drTotlal["Officer"] = officerTotal;
            drTotlal["Jco"] = jcosTotal;
            drTotlal["Other"] = otherTotal;
            drTotlal["Total"] = Grandtotal;
            dt.Rows.Add(drTotlal);

            var camp = campServices.GetCampDetails();
            if (camp != null)
                lblCamp.Text = camp[0].CampName;
            else
                lblCamp.Text = "Transit Camp";


            btnPrint.Visible = true;
            PrintDiv.Visible = true;
            lblFrom.Text = from.ToString("dd-MM-yyyy");
            lblTO.Text = to.ToString("dd-MM-yyyy");
            txtFrom.Text = from.ToString("dd-MM-yyyy");
            hfFromDate.Value = from.ToString();
            hfToDate.Value = to.ToString();
            txtTo.Text = to.ToString("dd-MM-yyyy");
            lblCity.Text = ddlCity.SelectedItem.ToString();
            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void Bind(Int64 id)
        {
            int officer = 0;
            int officerTotal = 0;
            int jcos = 0;
            int jcosTotal = 0;
            int other = 0;
            int otherTotal = 0;
            int total = 0;
            int Grandtotal = 0;
            int sn = 1;

            aDServices = new ADServices(new TCContext());
            campServices = new CampServices(new TCContext());
            DataTable dt = new DataTable();
            DateTime today = DateTime.Now;
            string todayday = today.Day.ToString();
            string todaymonth = today.Month.ToString();
            string todayear = today.Year.ToString();
            string finalstring = todayear + "-" + todaymonth + "-" + todayday;
            DateTime finaldate = Convert.ToDateTime(finalstring);

            var formation = unitServices.GetUnitDetails();
            if (formation.Count > 0)
            {
                foreach (var f in formation)
                {
                    DataRow dr = dt.NewRow();
                    dr["Unit"] = f.UnitName;
                    var data = aDServices.CityWiseReportIDDeparture(id, finaldate);

                    data = (from p in data
                            where p.UnitName == f.UnitName
                            select p).ToList();

                    officer = (from p in data
                               where p.CategoryName.ToLower().Contains("officer")
                               select p).Count();

                    jcos = (from p in data
                            where p.CategoryName.ToLower().Contains("jco")
                            select p).Count();

                    other = (from p in data
                             where p.CategoryName.ToLower().Contains("other") || p.CategoryName.ToLower().Contains("or")
                             select p).Count();

                    total = officer + jcos + other;

                    if (total != 0)
                    {
                        dr["SNo"] = sn++;
                        dr["Officer"] = officer;
                        dr["Jco"] = jcos;
                        dr["Other"] = other;
                        dr["Total"] = total;
                        dt.Rows.Add(dr);
                    }
                    officerTotal = officerTotal + officer;
                    jcosTotal = jcosTotal + jcos;
                    otherTotal = otherTotal + other;
                    Grandtotal = Grandtotal + total;
                }
            }

            DataRow drTotlal = dt.NewRow();

            drTotlal["SNo"] = "Total";
            drTotlal["Unit"] = "All";
            drTotlal["Officer"] = officerTotal;
            drTotlal["Jco"] = jcosTotal;
            drTotlal["Other"] = otherTotal;
            drTotlal["Total"] = Grandtotal;
            dt.Rows.Add(drTotlal);

            var camp = campServices.GetCampDetails();
            if (camp != null)
                lblCamp.Text = camp[0].CampName;
            else
                lblCamp.Text = "Transit Camp";

            btnPrint.Visible = true;
            PrintDiv.Visible = true;
            lblFrom.Text = finaldate.ToString("dd-MM-yyyy");
            lblTO.Text = finaldate.ToString("dd-MM-yyyy");
            lblCity.Text = ddlCity.SelectedItem.ToString();
            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        private void AddTotalRow(string cat)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
            row.Font.Bold = true;
            row.CssClass = "cat-heading";
            row.Cells.AddRange(new TableCell[11] { new TableCell { Text = cat, HorizontalAlign = HorizontalAlign.Left},
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
            });

            grd.Controls[0].Controls.Add(row);
        }

        protected void btnSearchAD_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            if (ddlCity.SelectedValue == "")
            {
                lblError.Text = "Select From Dropdown.";
                lblError.Visible = true;
            }
            else
            {
                if (hfFromDate.Value != "" && hfToDate.Value == "")
                {
                    lblError.Text = "Select To Date";
                    lblError.Visible = true;
                    txtFrom.Text = hfFromDate.Value.ToString();
                }
                else if (hfFromDate.Value == "" && hfToDate.Value != "")
                {
                    lblError.Text = "Select From Date";
                    lblError.Visible = true;
                    txtFrom.Text = hfFromDate.Value.ToString();
                }
                else
                {
                    if (hfFromDate.Value == "" && hfToDate.Value == "" && ddlCity.SelectedValue != "")
                    {
                        Int64 id = Convert.ToInt64(ddlCity.SelectedValue);
                        Bind(id);
                    }
                    else
                    {
                        Int64 id = Convert.ToInt64(ddlCity.SelectedValue);
                        DateTime fromdate = Convert.ToDateTime(Helper.CommonFunctions.ConvertToCreatedOnDate(hfFromDate.Value));
                        DateTime todate = Convert.ToDateTime(Helper.CommonFunctions.ConvertToCreatedOnDate(hfToDate.Value));
                        if (todate < fromdate)
                        {
                            lblError.Text = "Select Valid From-To Date.";
                            lblError.Visible = true;
                        }
                        else
                        {
                            Bind(fromdate, todate, id);
                        }
                    }
                }
            }
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            Response.Redirect("ADDateRange");
        }
    }
}