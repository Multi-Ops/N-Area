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
    public partial class UserList : System.Web.UI.Page
    {
        IUserServices userservices;
        private Int32 PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            userservices = new UserServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            Paging.TotalItems = userservices.TotalItems();

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

            var data = userservices.Paging(PageSize, _skip);

            grdUser.DataSource = data;
            grdUser.DataBind();

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            userservices = new UserServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = userservices.Paging(PageSize, _skip);
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
                    Response.Redirect("CreateAccount?ID=" + Convert.ToString(Id));
                }
                else if (e.CommandName == "Delete")
                {
                    userservices = new UserServices(new TCContext());
                    userservices.Delete(Id);
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

        protected void search(int searchtype)
        {
            userservices = new UserServices(new TCContext());
            String SearchText = txtSearch.Text;
            var data = userservices.GetSearchResult(SearchText, searchtype);
            grdUser.DataSource = data;
            grdUser.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                search(0);
                ddlsearch.SelectedValue = "0";
            }
            else
            {
                int searchtype = Convert.ToInt32(ddlsearch.SelectedValue);
                if (searchtype != 0)
                {
                    search(searchtype);
                }
            }
        }
    }
}