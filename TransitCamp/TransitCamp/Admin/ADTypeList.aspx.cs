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
    public partial class ADTypeList : System.Web.UI.Page
    {
        IADTypeServices adtypeservices;
        private Int32 PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            adtypeservices = new ADTypeServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            Paging.TotalItems = adtypeservices.TotalItems();
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

            var data = adtypeservices.Paging(PageSize, _skip);

            grdUser.DataSource = data;
            grdUser.DataBind();

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            adtypeservices = new ADTypeServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = adtypeservices.Paging(PageSize, _skip);
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
                    Response.Redirect("AddADType?ADTypeID=" + Convert.ToString(Id));
                }
                else if (e.CommandName == "Delete")
                {
                    adtypeservices = new ADTypeServices(new TCContext());
                    adtypeservices.Delete(Id);
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
            adtypeservices = new ADTypeServices(new TCContext());
            String SearchText = txtSearch.Text;
            var data = adtypeservices.GetSearchResult(SearchText);
            grdUser.DataSource = data;
            grdUser.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }
    }
}