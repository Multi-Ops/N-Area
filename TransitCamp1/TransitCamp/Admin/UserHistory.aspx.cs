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

namespace TransitCamp.Admin
{
    public partial class UserHistory : System.Web.UI.Page
    {
        IUserServices userservices;
        private Int32 PageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Paging
            userservices = new UserServices(new TCContext());
            Paging.Paging_Click += Paging_Paging_Click;
            Paging.PageSize = PageSize;
            Paging.TotalItems = userservices.TotalItemsUserHistory();

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

            DataTable dt = new DataTable();
            dt.Columns.Add("ArmyNo");
            dt.Columns.Add("ADNo");
            dt.Columns.Add("ICardNo");

            var data = userservices.PaggingByIDCardGroupByICard(PageSize, _skip);
            if (data.Count.ToString() != string.Empty)
            {
                foreach (var item in data)
                {
                    DataRow dr = dt.NewRow();

                    var res = userservices.GetUserByArmyNo(item.ArmyNumber);
                    if (res != null)
                    {
                        dr["ArmyNo"] = res.ArmyNumber;
                        dr["ADNo"] = res.ADNO;
                        dr["ICardNo"] = res.IDCardNo;
                        dt.Rows.Add(dr);
                    }
                }
            }
            grdUser.DataSource = dt;
            grdUser.DataBind();

        }

        private void Paging_Paging_Click(object sender, EventArgs e)
        {
            userservices = new UserServices(new TCContext());
            Int32 _skip = PageSize * (Paging.CurrentPage - 1);

            var data = userservices.PaggingByIDCardGroupByICard(PageSize, PageSize);
            grdUser.DataSource = data;
            grdUser.DataBind();
        }

        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string armyno = Convert.ToString(grdUser.DataKeys[row.RowIndex].Value);

                if (e.CommandName == "ShowDetails")
                    Response.Redirect("UserHistoryDetails?ArmyNumber=" + Convert.ToString(armyno));

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

        protected void search(string searchtext)
        {
            userservices = new UserServices(new TCContext());
            if (searchtext == "")
            {
                Bind();
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ArmyNo");
                dt.Columns.Add("ADNo");
                dt.Columns.Add("ICardNo");

                var data = userservices.GetSearchResultBySearchText(searchtext);

                if (data.Count.ToString() != string.Empty)
                {
                    foreach (var item in data)
                    {
                        DataRow dr = dt.NewRow();

                        var res = userservices.GetUserByArmyNo(item.ArmyNumber);
                        if (res != null)
                        {
                            dr["ArmyNo"] = res.ArmyNumber;
                            dr["ADNo"] = res.ADNO;
                            dr["ICardNo"] = res.IDCardNo;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                grdUser.DataSource = dt;
                grdUser.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            search(txtSearch.Text.Trim());
        }
    }
}