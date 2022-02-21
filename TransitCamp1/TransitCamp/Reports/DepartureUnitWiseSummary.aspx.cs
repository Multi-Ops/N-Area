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
    public partial class DepartureUnitWiseSummary : System.Web.UI.Page
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
                Int64 id = Convert.ToInt64(ddlCity.SelectedValue);
                Bind(id);
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
        }

        protected void Bind(Int64 id)
        {
            string fmn = "";
            int officer = 0;
            int jcos = 0;
            int other = 0;
            int total = 0;
            int sn = 1;
            aDServices = new ADServices(new TCContext());
            campServices = new CampServices(new TCContext());
            unitServices = new UnitServices(new TCContext());
            DataTable dt = new DataTable();
            dt.Columns.Add("Unit");
            dt.Columns.Add("Officer");
            dt.Columns.Add("Jco");
            dt.Columns.Add("Other");
            dt.Columns.Add("Total");
            dt.Columns.Add("SNo");

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
                    var data = aDServices.CityWiseReportID(id, finaldate);
                    data = (from p in data
                            where p.DivName == f.UnitName
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
                        dr["Officer"] = officer;
                        dr["Jco"] = jcos;
                        dr["Other"] = other;
                        dr["Total"] = total;
                        dr["SNo"] = sn++;
                        dt.Rows.Add(dr);
                    }
                }
            }

            var camp = campServices.GetCampDetails();
            if (camp != null)
                lblCamp.Text = camp[0].CampName;
            else
                lblCamp.Text = "Transit Camp";

            btnPrint.Visible = true;
            PrintDiv.Visible = true;
            lblFrom.Text = finaldate.ToString("dd-MM-yyyy");
            txtFrom.Text = finaldate.ToString("dd-MM-yyyy");
            hfFromDate.Value = finaldate.ToString();
            lblCity.Text = ddlCity.SelectedItem.ToString();
            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void Bind(Int64 id, DateTime date)
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
            DateTime today = date;
            string todayday = today.Day.ToString();
            string todaymonth = today.Month.ToString();
            string todayear = today.Year.ToString();
            string finalstring = todayear + "-" + todaymonth + "-" + todayday;
            DateTime finaldate = DateTime.Parse(finalstring);

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
            txtFrom.Text = finaldate.ToString("dd-MM-yyyy");
            hfFromDate.Value = finaldate.ToString();
            lblCity.Text = ddlCity.SelectedItem.ToString();
            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //this.AddTotalRow("Total");
                }
            }
            catch (Exception ex)
            {

            }
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

        public DataTable ToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
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
                if (hfFromDate.Value == "" && ddlCity.SelectedValue != "")
                {
                    Int64 id = Convert.ToInt64(ddlCity.SelectedValue);
                    Bind(id);
                }
                else
                {
                    Int64 id = Convert.ToInt64(ddlCity.SelectedValue);
                    DateTime fromdate = Convert.ToDateTime(Helper.CommonFunctions.ConvertToCreatedOnDate(hfFromDate.Value));
                    Bind(id, fromdate);
                }
            }
        }
    }
}