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
    public partial class CharterDetailsList : System.Web.UI.Page
    {
        ICharterDetailsServices charterDetailsServices;

        private Int32 PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            charterDetailsServices = new CharterDetailsServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            Paging.TotalItems = charterDetailsServices.TotalItems();

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

            var data = charterDetailsServices.Paging(PageSize, _skip);
            grdUser.DataSource = data;
            grdUser.DataBind();

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            charterDetailsServices = new CharterDetailsServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = charterDetailsServices.Paging(PageSize, _skip);
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
                    Response.Redirect("AddCharterDetails?CharterID=" + Convert.ToString(Id));
                }
                else if (e.CommandName == "Delete")
                {
                    charterDetailsServices = new CharterDetailsServices(new TCContext());
                    charterDetailsServices.Delete(Id);
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
            charterDetailsServices = new CharterDetailsServices(new TCContext());
            var searchdate = Convert.ToString(hfsearchdate.Value);

            if (searchdate == "")
            {
                Bind();
            }
            else
            {
                var data = charterDetailsServices.GetSearchResult(searchdate);
                grdUser.DataSource = data;
                grdUser.DataBind();
                hfsearchdate.Value = "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }
    }
}