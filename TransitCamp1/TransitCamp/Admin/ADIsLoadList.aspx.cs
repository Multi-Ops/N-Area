using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Admin
{
    public partial class ADIsLoadList : System.Web.UI.Page
    {
        IADServices aDServices;
        IManifestServices manifestServices;
        private Int32 PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            aDServices = new ADServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            Paging.TotalItems = aDServices.TotalItemsLoad();
            #endregion
            if (!IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {

            Paging.CurrentPage = 1;

            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = aDServices.PagingLoad(PageSize, _skip);

            grdUser.DataSource = data;
            grdUser.DataBind();

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = aDServices.PagingLoad(PageSize, _skip);
            grdUser.DataSource = data;
            grdUser.DataBind();

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
                    aDServices = new ADServices(new TCContext());
                    aDServices.Delete(Id);

                    manifestServices = new ManifestServices(new TCContext());
                    Int64 getdetails = manifestServices.GetByADID(Id);
                    if (getdetails != 0)
                    {
                        manifestServices.Delete(getdetails);
                    }
                    Bind();
                }
                else if (e.CommandName == "Cancel")
                {
                    aDServices = new ADServices(new TCContext());
                    BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();

                    var getdata = aDServices.GetByID(Id);
                    if (getdata != null)
                    {
                        info = getdata;
                        info.ID = Id;
                        info.IsLoad = false;
                        if (getdata.LeaveFromDate == null)
                        {
                            info.LeaveFromDate = null;
                            info.LeaveToDate = null;
                        }
                        aDServices.Update(info);
                        aDServices.Save();
                        Response.Redirect("ADIsLoadList");
                    }
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

        protected void search()
        {
            aDServices = new ADServices(new TCContext());
            String SearchText = txtSearch.Text;
            var data = aDServices.GetSearchResultLoad(SearchText);
            grdUser.DataSource = data;
            grdUser.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }
    }
}