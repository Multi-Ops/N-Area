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
    public partial class ADList : System.Web.UI.Page
    {
        IADServices aDServices;
        IManifestServices manifestServices;
        IBookingServices bookingServices;
        private Int32 PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            aDServices = new ADServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            Paging.TotalItems = aDServices.TotalItems();
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

            var data = aDServices.Paging(PageSize, _skip);

            grdUser.DataSource = data;
            grdUser.DataBind();

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            aDServices = new ADServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = aDServices.Paging(PageSize, _skip);
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
                else if (e.CommandName == "Reserve")
                {
                    aDServices = new ADServices(new TCContext());
                    aDServices.Reserve(Id);
                    aDServices.Save();
                    Bind();
                }
                else if (e.CommandName == "Load")
                {
                    aDServices = new ADServices(new TCContext());
                    aDServices.Load(Id);
                    aDServices.Save();
                    Bind();
                }
                else if (e.CommandName == "Checkout")
                {
                    bookingServices = new BookingServices(new TCContext());
                    aDServices = new ADServices(new TCContext());
                    var getad = aDServices.GetByID(Id);
                    if (getad != null)
                    {
                        getad.CheckOutDate = DateTime.Now;
                        aDServices.Update(getad);
                        aDServices.Save();
                    }

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
            aDServices = new ADServices(new TCContext());
            String SearchText = txtSearch.Text;
            var data = aDServices.GetSearchResult(SearchText);
            grdUser.DataSource = data;
            grdUser.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }
    }
}