using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DataAccessLayer;
using TransitCamp.UserControl;
using BusinessLayer;


namespace TransitCamp.Admin
{
    public partial class ReserveManifestDetails : System.Web.UI.Page
    {
        IManifestServices manifestServices;
        IADServices aDServices;
        ITransportDetailsServices transportDetailsServices;
        ICancelServices cancelServices;

        private Int32 PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            var ManifestNo = Request.QueryString["ManifestNo"];
            DateTime date = Convert.ToDateTime(Request.QueryString["Date"]);
            int cityid = Convert.ToInt32(Request.QueryString["CityId"]);


            #region Paging
            manifestServices = new ManifestServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            if (ManifestNo != null)
                Paging.TotalItems = manifestServices.TotalItemsReserveManifestDetails(ManifestNo);
            else
                Paging.TotalItems = manifestServices.TotalItemsReserveManifestDetails();

            #endregion
            if (!IsPostBack)
            {
                if (ManifestNo != null)
                {
                    Bind(ManifestNo, date, cityid);
                    SetInputFocus();
                }
            }
        }


        public void SetInputFocus()
        {
            TextBox tbox = this.txtSearch.FindControl("txtSearch") as TextBox;
            if (tbox != null)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(tbox);
            }
        }


        private void Bind(string manifestNo, DateTime date, int cityid)
        {
            manifestServices = new ManifestServices(new TCContext());
            //Int32 _skip = PageSize * (Paging.CurrentPage - 1);
            Int32 _skip = 0;

            var data = manifestServices.PagingAll(PageSize, _skip, manifestNo, date, cityid);
            data = (from p in data
                    where p.IsReserve == true
                    select p).ToList();
            grdManifest.DataSource = data;
            grdManifest.DataBind();
            Session["manifestno"] = manifestNo;
            Session["date"] = date;
            Session["cityid"] = cityid;

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            manifestServices = new ManifestServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);
            if (Session["manifestno"] != null)
            {
                var data = manifestServices.PagingAll(PageSize, _skip, Session["manifestno"].ToString(), Convert.ToDateTime(Session["date"]), Convert.ToInt32(Session["cityid"]));
                data = (from p in data
                        where p.IsReserve == true
                        select p).ToList();
                grdManifest.DataSource = data;
                grdManifest.DataBind();
            }
        }

        protected void grdManifest_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdManifest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Int64 Id = Convert.ToInt64(grdManifest.DataKeys[row.RowIndex].Value);

                if (e.CommandName == "Reverse")
                {
                    manifestServices = new ManifestServices(new TCContext());
                    aDServices = new ADServices(new TCContext());
                    cancelServices = new CancelServices(new TCContext());
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
                        else
                        {
                            transportdetails.NoOfSeats = transportdetails.NoOfSeats + 1;
                            transportDetailsServices.Update(transportdetails);
                            transportDetailsServices.Save();
                        }
                        getdetails.ID = getdetailsmanifest.ADID;
                        getdetails.IsManifest = false;
                        //Insert Into Cancel
                        Cancel cancelinfo = new Cancel();
                        cancelinfo.ADID = getdetailsmanifest.ADID;
                        cancelinfo.ADNO = getdetailsmanifest.ADNO;
                        cancelinfo.ArmyNo = getdetailsmanifest.ArmyNo;
                        cancelinfo.CategoryID = getdetailsmanifest.CategoryID;
                        cancelinfo.CityID = getdetailsmanifest.CityID;
                        cancelinfo.CreatedOn = DateTime.Now;
                        cancelinfo.HQID = getdetailsmanifest.HQID;
                        cancelinfo.ICard = getdetailsmanifest.ICard;
                        cancelinfo.ManifestDate = getdetailsmanifest.ManifestDate;
                        cancelinfo.MenifestNo = getdetailsmanifest.MenifestNo;
                        cancelinfo.RankID = getdetailsmanifest.RankID;
                        cancelinfo.Name = getdetailsmanifest.Name;
                        cancelinfo.TransportDetailID = getdetailsmanifest.TransportDetailID;
                        cancelinfo.TransportDetails = getdetailsmanifest.TransportDetails;
                        cancelinfo.Session = getdetailsmanifest.Session;
                        cancelinfo.UnitID = getdetailsmanifest.UnitID;
                        cancelServices.IndivialInsert(cancelinfo);
                        cancelServices.IndividualSave();
                        //update AD Master
                        aDServices.Update(getdetails);
                        aDServices.Save();
                        manifestServices.Delete(Id);
                    }
                    Bind(Session["manifestno"].ToString(), Convert.ToDateTime(Session["date"]), Convert.ToInt32(Session["cityid"]));
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void search()
        {
            manifestServices = new ManifestServices(new TCContext());
            String SearchText = txtSearch.Text;
            var ManifestNo = Request.QueryString["ManifestNo"];

            var data = manifestServices.SearchResultsManifestDetails(SearchText, ManifestNo);
            data = (from p in data
                    where p.IsReserve == true
                    select p).ToList();
            grdManifest.DataSource = data;
            grdManifest.DataBind();
            SetInputFocus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                search();
            else
                Bind(Session["manifestno"].ToString(), Convert.ToDateTime(Session["date"]), Convert.ToInt32(Session["cityid"]));
        }

        protected void btnReverseManifest_Click(object sender, EventArgs e)
        {
            manifestServices = new ManifestServices(new TCContext());
            aDServices = new ADServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            cancelServices = new CancelServices(new TCContext());

            DateTime date = Convert.ToDateTime(Request.QueryString["Date"]);

            if (Session["manifestno"] != null)
            {
                var ADIDs = manifestServices.GetManifestWithManifestNo(Session["manifestno"].ToString().Trim(), date);
                ADIDs = (from p in ADIDs
                         where p.IsReserve == true
                         select p).ToList();
                if (ADIDs != null)
                {
                    foreach (var list in ADIDs)
                    {
                        Int64 ADID = list.ADID;
                        Int64 TransportID = list.TransportDetailID;
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
                            else
                            {
                                transportdetails.NoOfSeats = transportdetails.NoOfSeats + 1;
                                transportDetailsServices.Update(transportdetails);
                                transportDetailsServices.Save();
                            }
                            getdetails.ID = ADID;
                            getdetails.IsManifest = false;
                            //Insert Into Cancel
                            Cancel cancelinfo = new Cancel();
                            cancelinfo.ADID = list.ADID;
                            cancelinfo.ADNO = list.ADNO;
                            cancelinfo.ArmyNo = list.ArmyNo;
                            cancelinfo.CategoryID = list.CategoryID;
                            cancelinfo.CityID = list.CityID;
                            cancelinfo.CreatedOn = DateTime.Now;
                            cancelinfo.HQID = list.HQID;
                            cancelinfo.ICard = list.ICard;
                            cancelinfo.ManifestDate = list.ManifestDate;
                            cancelinfo.MenifestNo = list.MenifestNo;
                            cancelinfo.RankID = list.RankID;
                            cancelinfo.Name = list.Name;
                            cancelinfo.TransportDetailID = list.TransportDetailID;
                            cancelinfo.TransportDetails = list.TransportDetails;
                            cancelinfo.Session = list.Session;
                            cancelinfo.UnitID = list.UnitID;
                            cancelServices.Insert(cancelinfo);
                            cancelServices.Save();
                            //update AD Master
                            aDServices.Update(getdetails);
                            aDServices.Save();
                            manifestServices.Delete(ID);
                        }
                    }
                }
                Bind(Session["manifestno"].ToString(), Convert.ToDateTime(Session["date"]), Convert.ToInt32(Session["cityid"]));
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var ManifestNo = Request.QueryString["ManifestNo"];
            DateTime date = Convert.ToDateTime(Request.QueryString["Date"]);
            int cityid = Convert.ToInt32(Request.QueryString["CityId"]);

            Response.Redirect("ReserveManifestReport?ManifestNo=" + ManifestNo + "&" + "Date=" + Convert.ToDateTime(date) + "&" + "CityId=" + cityid + "");

        }
    }
}