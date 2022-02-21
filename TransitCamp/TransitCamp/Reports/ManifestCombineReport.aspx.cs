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
    public partial class ManifestCombineReport : System.Web.UI.Page
    {
        IManifestServices manifestServices;
        ICategoryServices categoryServices;
        ICampServices campServices;
        ICityServices cityServices;
        ITransportDetailsServices transportDetailsServices;
        ISignatureServices signatureServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            var ManifestNo = Request.QueryString["ManifestNo"];
            DateTime date = Convert.ToDateTime(Request.QueryString["Date"]);
            int cityId = Convert.ToInt32(Request.QueryString["CityId"]);

            if (ManifestNo != null)
            {
                BindGridCat(ManifestNo, date, cityId);
            }
        }
        protected void BindGridCat(string ManifestNo, DateTime date, int cityId)
        {
            //get category IDs
            categoryServices = new CategoryServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            campServices = new CampServices(new TCContext());
            cityServices = new CityServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            signatureServices = new SignatureServices(new TCContext());

            var CategoryIDs = categoryServices.details();
            if (CategoryIDs != null)
            {
                string CatID = Convert.ToString(CategoryIDs[0].ID);
                if (CatID != "")
                {
                    DataTable dt = new DataTable();

                    var data = manifestServices.PagingManifest(ManifestNo, date, cityId);
                    data = (from p in data
                            where p.IsReserve == false
                            select p).ToList();
                    var finaldata = data.OrderBy(x => x.CategoryName).ToList();
                    var sign = signatureServices.GetDetails();

                    dt = ToDataTable(finaldata);

                    var campname = campServices.GetCampDetails();
                    if (data.Count != 0)
                    {
                        var city = cityServices.GetCityID(Convert.ToInt64(data[0].CityID));
                        var transportdetails = transportDetailsServices.GetDetailsByID(Convert.ToInt64(data[0].TransportDetailID));

                        if (campname.Count != 0)
                            lblCamp.Text = campname[0].CampName.ToUpper();

                        if (city != null)
                            lblCity.Text = city.CityName.ToUpper();

                        if (transportdetails != null)
                        {
                            DateTime tDate = Convert.ToDateTime(transportdetails.Date);
                            lblDate.Text = tDate.ToString("dd/MM/yyyy hh:mm tt");
                            lblTransportTypeName.Text = transportdetails.TransportType;
                        }

                        lblManifestNo.Text = data[0].MenifestNo;
                        lblTransportDetails.Text = transportdetails.TransportDetail;

                        //lblRptRank.Text = sign.RankName;
                        //lblRptName.Text = sign.SignatureName;
                        lblRptDate.Text = DateTime.Now.ToString("MMMM yyyy");

                        grdManifestReport.DataSource = dt;
                        grdManifestReport.DataBind();
                    }
                    else
                    {
                        PrintDiv.Visible = false;
                        btnPrint.Visible = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertBox", "alert('No Data Available!');", true);
                    }
                }
            }
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
                //lblJCOs.Text = jcos.ToString();
                //lblOfficer.Text = officer.ToString();
                //lblOR.Text = other.ToString();
                //lblNoOfTrans.Text = total.ToString();
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
            row.Cells.AddRange(new TableCell[9] { new TableCell { Text = cat, HorizontalAlign = HorizontalAlign.Left},
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell ()
            });

            grdManifestReport.Controls[0].Controls.Add(row);
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

    }
}