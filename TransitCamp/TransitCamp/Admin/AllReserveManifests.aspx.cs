using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DataAccessLayer;
using TransitCamp.UserControl;
using System.Data;
using BusinessLayer;

namespace TransitCamp.Admin
{
    public partial class AllReserveManifests : System.Web.UI.Page
    {
        IManifestServices manifestServices;
        private Int32 PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            manifestServices = new ManifestServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            if (hfFromDate.Value != "")
                Paging.TotalItems = manifestServices.TotalItemsReserve(Convert.ToDateTime(hfFromDate.Value));
            else
                Paging.TotalItems = manifestServices.TotalItemsReserve(DateTime.Now);

            #endregion
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            string transportNo = "";
            string manifestDate = "";
            string session = "";
            string manifestno = "";
            string cityName = "";
            int cityid = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add("TransportNo");
            dt.Columns.Add("ManifestDate");
            dt.Columns.Add("Session");
            dt.Columns.Add("Manifestno");
            dt.Columns.Add("City");
            dt.Columns.Add("CityId");

            manifestServices = new ManifestServices(new TCContext());
            //Int32 _skip = PageSize * (Paging.CurrentPage - 1);
            Int32 _skip = 0;
            var data = manifestServices.GetManifestReserve(PageSize, _skip, DateTime.Now);
            foreach (var res in data)
            {
                DataRow dr = dt.NewRow();

                var datadetails = manifestServices.GetManifestWithManifestNo(res.MenifestNo, DateTime.Now);
                datadetails = (from p in datadetails
                               where p.IsReserve == true
                               select p).ToList();
                if (datadetails.Count > 0)
                {
                    transportNo = datadetails[0].TransportDetails;
                    manifestDate = datadetails[0].ManifestDate.ToString();
                    session = datadetails[0].Session;
                    manifestno = datadetails[0].MenifestNo;
                    cityName = datadetails[0].CityName;
                    cityid = Convert.ToInt32(datadetails[0].CityID);
                }
                dr["ManifestDate"] = manifestDate;
                dr["Manifestno"] = manifestno;
                dr["Session"] = session;
                dr["TransportNo"] = transportNo;
                dr["City"] = cityName;
                dr["CityId"] = cityid;

                dt.Rows.Add(dr);
            }

            grdManifest.DataSource = dt;
            grdManifest.DataBind();
            txtFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");
            hfFromDate.Value = DateTime.Now.ToString();
        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            manifestServices = new ManifestServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = manifestServices.PagingAll(PageSize, _skip);
            grdManifest.DataSource = data;
            grdManifest.DataBind();

        }

        protected void grdManifest_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdManifest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string manifestno = Convert.ToString(grdManifest.DataKeys[row.RowIndex].Value);
                string cityId = ((HiddenField)grdManifest.Rows[row.RowIndex].Cells[3].FindControl("hfcid")).Value;

                if (e.CommandName == "Details")
                {
                    Response.Redirect("ReserveManifestDetails?ManifestNo=" + manifestno + "&" + "Date=" + Convert.ToDateTime(hfFromDate.Value) + "&" + "CityId=" + cityId + "");
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void search()
        {
            string transportNo = "";
            string manifestDate = "";
            string session = "";
            string manifestno = "";
            string city = "";
            int cityid = 0;


            DataTable dt = new DataTable();
            dt.Columns.Add("TransportNo");
            dt.Columns.Add("ManifestDate");
            dt.Columns.Add("Session");
            dt.Columns.Add("Manifestno");
            dt.Columns.Add("City");
            dt.Columns.Add("CityId");


            manifestServices = new ManifestServices(new TCContext());
            String SearchText = hfFromDate.Value;
            DateTime date = Convert.ToDateTime(SearchText);
            var data = manifestServices.GetManifestSearch(date);

            foreach (var res in data)
            {
                DataRow dr = dt.NewRow();

                var datadetails = manifestServices.GetManifestWithManifestNo(res.MenifestNo, date);
                datadetails = (from p in datadetails
                               where p.IsReserve == true
                               select p).Distinct().ToList();
                if (datadetails.Count > 0)
                {
                    transportNo = datadetails[0].TransportDetails;
                    manifestDate = datadetails[0].ManifestDate.ToString();
                    session = datadetails[0].Session;
                    manifestno = datadetails[0].MenifestNo;
                    city = datadetails[0].CityName;
                }
                dr["ManifestDate"] = manifestDate;
                dr["Manifestno"] = manifestno;
                dr["Session"] = session;
                dr["TransportNo"] = transportNo;
                dr["City"] = city;
                dr["CityId"] = cityid;
                dt.Rows.Add(dr);
            }
            grdManifest.DataSource = dt;
            grdManifest.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (hfFromDate.Value != "")
                search();
            else
                Bind();

            txtFrom.Text = Convert.ToDateTime(hfFromDate.Value).ToString("dd-MM-yyyy");
        }
    }
}