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
    public partial class ADReportToday : System.Web.UI.Page
    {
        IADServices aDServices;
        ICampServices campServices;
        ISignatureServices signatureServices;
        ICityServices cityServices;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bind();
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

        protected void Bind()
        {
            DateTime today = DateTime.Now;
            string todayday = today.Day.ToString();
            string todaymonth = today.Month.ToString();
            string todayear = today.Year.ToString();

            string finalstring = todayear + "-" + todaymonth + "-" + todayday;

            DataTable dt = new DataTable();

            aDServices = new ADServices(new TCContext());
            campServices = new CampServices(new TCContext());
            signatureServices = new SignatureServices(new TCContext());

            var campname = campServices.GetCampDetails();
            var sign = signatureServices.GetDetails();

            var data = aDServices.GetByDate(Convert.ToDateTime(finalstring));
            var finaldata = data.OrderBy(x => x.CategoryName).ToList();

            dt = ToDataTable(finaldata);

            grd.DataSource = dt;
            grd.DataBind();
            btnPrint.Visible = true;
            lblCamp.Text = campname[0].CampName;
            PrintDiv.Visible = true;
            lbltoday.Text = today.ToString("dd MMMM yyyy");
            txtFrom.Text = today.ToString("dd-MM-yyyy");
            hfFromDate.Value = today.ToString("dd-MM-yyy  hh:mm:ss");
        }

        protected void BindSearch(DateTime date, int cid)
        {
            DateTime selecteddate = date;
            string selectedday = selecteddate.Day.ToString();
            string selectedmonth = selecteddate.Month.ToString();
            string selectedyear = selecteddate.Year.ToString();

            string finalstring = selectedyear + "-" + selectedmonth + "-" + selectedday;

            DataTable dt = new DataTable();

            aDServices = new ADServices(new TCContext());
            campServices = new CampServices(new TCContext());
            signatureServices = new SignatureServices(new TCContext());

            var campname = campServices.GetCampDetails();
            var sign = signatureServices.GetDetails();

            var data = aDServices.GetByDate(Convert.ToDateTime(finalstring), cid);

            var finaldata = data.OrderBy(x => x.CategoryName).ToList();

            dt = ToDataTable(finaldata);

            grd.DataSource = dt;
            grd.DataBind();
            btnPrint.Visible = true;
            lblCamp.Text = campname[0].CampName;
            PrintDiv.Visible = true;
            txtFrom.Text = selecteddate.ToString("dd-MM-yyyy");
            lbltoday.Text = DateTime.Now.ToString("dd MMMM yyyy");

        }

        String currentId = null;
        int subTotalRowIndex = 0;
        int jcos = 0;
        int officer = 0;
        int other = 0;
        int total = 0;

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.DataItem as DataRowView != null)
                    {
                        DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                        String Id = Convert.ToString(dt.Rows[e.Row.RowIndex]["CategoryName"]);
                        if (Id.Trim().ToLower() == "JCOs".ToLower())
                            jcos++;
                        else if (Id.Trim().ToLower() == "Officer".ToLower() || Id.Trim().ToLower() == "Officers".ToLower())
                            officer++;
                        else if (Id.Trim().ToLower() == "Other".ToLower() || Id.Trim().ToLower() == "Or".ToLower() || Id.Trim().ToLower() == "Others".ToLower())
                            other++;

                        if (Id != currentId)
                        {
                            this.AddTotalRow(Id);

                            if (e.Row.RowIndex > 0)
                            {
                                for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                                {
                                }
                                subTotalRowIndex = e.Row.RowIndex;
                            }

                            currentId = Id;
                        }
                    }
                }
                total = jcos + officer + other;
                lblJCOs.Text = jcos.ToString();
                lblOfficer.Text = officer.ToString();
                lblOR.Text = other.ToString();
                lblNoOfTrans.Text = total.ToString();
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
            row.Cells.AddRange(new TableCell[7] { new TableCell { Text = cat, HorizontalAlign = HorizontalAlign.Left},
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell ()
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
            if (hfFromDate.Value != "")
            {
                BindSearch(Convert.ToDateTime(hfFromDate.Value), Convert.ToInt32(ddlCities.SelectedValue));
                lblCity.Text = ddlCities.SelectedItem.Text;
            }
        }
    }
}