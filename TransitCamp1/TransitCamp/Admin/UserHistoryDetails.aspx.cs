using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DataAccessLayer;
using TransitCamp.UserControl;

namespace TransitCamp.Admin
{
    public partial class UserHistoryDetails : System.Web.UI.Page
    {
        IUserServices userservices;
        IADServices aDServices;
        ITransportDetailsServices transportDetailsServices;
        IManifestServices manifestServices;
        private Int32 PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            userservices = new UserServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;

            #endregion
            if (!IsPostBack)
            {
                string ICard = Request.QueryString["ArmyNumber"];
                if (ICard.Trim() != "")
                {
                    Paging.TotalItems = userservices.TotalItemsUserHistory(ICard);
                    Bind();
                }
            }
        }

        private void Bind()
        {
            string ICard = Request.QueryString["ArmyNumber"];
            if (ICard.Trim() != "")
            {
                userservices = new UserServices(new TCContext());
                manifestServices = new ManifestServices(new TCContext());
                aDServices = new ADServices(new TCContext());
                transportDetailsServices = new TransportDetailsServices(new TCContext());

                Paging.CurrentPage = 1;

                Int32 _skip = PageSize * (Paging.CurrentPage - 1);

                var infoList = new List<BusinessLayer.Users>();

                var data = userservices.PaggingByIDCard(PageSize, _skip, ICard);

                var ManifestData = (from p in data
                                    where p.IsManifest == true
                                    select p).ToList();
                var FData = (from p in data
                             where p.IsManifest == false
                             select p).ToList();
                if (ManifestData.Count.ToString() != string.Empty)
                {
                    foreach (var item in ManifestData)
                    {
                        Int64 id = Convert.ToInt64(item.ID);
                        var manifestdetails = manifestServices.GetByADID(id);
                        if (manifestdetails.ToString() != null)
                        {
                            var manDet = manifestServices.GetByID(manifestdetails);
                            var info = new BusinessLayer.Users();
                            info.ID = Convert.ToInt64(item.ADID);
                            info.ADNO = Convert.ToString(item.ADNO);
                            info.Rank = Convert.ToString(item.Rank);
                            info.Name = Convert.ToString(item.Name);
                            info.IDCardNo = Convert.ToString(item.IDCardNo);
                            info.Session = Convert.ToString(item.Session);
                            info.DDate = Convert.ToString(manDet.ManifestDate);
                            info.CreatedOn = Convert.ToDateTime(item.CreatedOn);
                            info.DSession = Convert.ToString(manDet.Session);
                            info.FlightNo = Convert.ToString(manDet.TransportDetails);
                            info.ADID = manDet.ADID;
                            info.UnitName = manDet.UnitName;
                            info.HQName = manDet.HQName;
                            info.ArmyNumber = manDet.ArmyNo;
                            infoList.Add(info);
                        }
                    }
                }
                if (FData.Count.ToString() != string.Empty)
                {
                    foreach (var item in FData)
                    {
                        Int64 id = Convert.ToInt64(item.ID);
                        var manifestdetails = manifestServices.GetByADID(id);
                        if (manifestdetails.ToString() != null)
                        {
                            var info = new BusinessLayer.Users();
                            info.ID = Convert.ToInt64(item.ADID);
                            info.ADNO = Convert.ToString(item.ADNO);
                            info.Rank = Convert.ToString(item.Rank);
                            info.Name = Convert.ToString(item.Name);
                            info.IDCardNo = Convert.ToString(item.IDCardNo);
                            info.Session = Convert.ToString(item.Session);
                            info.CreatedOn = Convert.ToDateTime(item.CreatedOn);
                            info.ADID = item.ADID;
                            info.UnitName = item.UnitName;
                            info.HQName = item.HQName;
                            info.ArmyNumber = item.ArmyNumber;
                            infoList.Add(info);
                        }
                    }
                }
                grdUser.DataSource = infoList;
                grdUser.DataBind();
                if (data.Count != 0)
                    lblName.Text = data[0].Name;
            }
        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            string ICard = Request.QueryString["ArmyNumber"];
            if (ICard.Trim() != "")
            {
                userservices = new UserServices(new TCContext());
                manifestServices = new ManifestServices(new TCContext());
                aDServices = new ADServices(new TCContext());
                transportDetailsServices = new TransportDetailsServices(new TCContext());

                Int32 _skip = PageSize * (Paging.CurrentPage - 1);

                var data = userservices.PaggingByIDCard(PageSize, _skip, ICard);
                data = (from p in data
                        where p.IsManifest == true
                        select p).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        Int64 id = Convert.ToInt64(item.ID);
                        var manifestdetails = manifestServices.GetByADID(id);
                        if (manifestdetails.ToString() != null)
                        {
                            var manDet = manifestServices.GetByID(manifestdetails);
                            data.Add(new BusinessLayer.Users
                            {
                                FlightNo = manDet.TransportDetails,
                                DDate = manDet.ManifestDate.ToString(),
                                DSession = manDet.Session,
                            });
                        }
                    }
                }
                grdUser.DataSource = data;
                grdUser.DataBind();
                lblName.Text = data[0].Name;
            }
        }

        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


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
    }
}