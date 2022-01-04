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
    public partial class TransferCourierToCharterList : System.Web.UI.Page
    {
        ITransferCourierToCharterServices transferCourierToCharterServices;
        private Int32 PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            transferCourierToCharterServices = new TransferCourierToCharterServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            Paging.TotalItems = transferCourierToCharterServices.TotalItems();
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

            var data = transferCourierToCharterServices.Paging(PageSize, _skip);

            grdUser.DataSource = data;
            grdUser.DataBind();

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            transferCourierToCharterServices = new TransferCourierToCharterServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = transferCourierToCharterServices.Paging(PageSize, _skip);
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
                    Response.Redirect("TransferCourierToCharter?TCID=" + Convert.ToString(Id));
                }
                else if (e.CommandName == "Delete")
                {
                    transferCourierToCharterServices = new TransferCourierToCharterServices(new TCContext());
                    transferCourierToCharterServices.Delete(Id);
                    Bind();
                }
                else
                {

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
            transferCourierToCharterServices = new TransferCourierToCharterServices(new TCContext());
            String SearchText = txtSearch.Text;
            var data = transferCourierToCharterServices.GetSearchResult(SearchText);
            grdUser.DataSource = data;
            grdUser.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }
    }
}