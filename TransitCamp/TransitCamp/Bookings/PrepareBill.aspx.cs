using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TransitCamp.Bookings
{
    public partial class PrepareBill : System.Web.UI.Page
    {
        IADServices aDServices;
        IBookingServices bookingServices;

        private Int32 PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var adid = Convert.ToInt32(Request.QueryString["Id"]);
                if (adid != 0)
                {
                    ElectricityBillCalculate();
                }
            }
        }

        private void ElectricityBillCalculate()
        {
            Electricity.Visible = true;
            PrintDiv.Visible = false;
            btnPrint.Visible = false;
        }

        private void Bind(double ePrice, double noOfdays)
        {
            bookingServices = new BookingServices(new TCContext());
            aDServices = new ADServices(new TCContext());
            DataTable dt = new DataTable();

            var adid = Convert.ToInt32(Request.QueryString["Id"]);

            if (adid.ToString() != null)
            {
                var getbooking = bookingServices.GetBookingByADID(adid);
                if (getbooking != null)
                {
                    var data = bookingServices.GetDetailsBill();
                    if (data != null)
                    {
                        var addet = aDServices.GetByID(adid);
                        if (addet != null)
                        {
                            //room details
                            var roomdet = bookingServices.GetIDRoom((int)(getbooking.RoomId));
                            if (roomdet != null)
                            {
                                if (roomdet.IsShare)
                                {
                                    if (addet.IsLRC == true)
                                    {
                                        var roomattributes = bookingServices.GetDetailsBill();
                                        if (roomattributes != null)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dt.Columns.Add("Attributes");
                                            dt.Columns.Add("Price");

                                            foreach (var result in roomattributes)
                                            {
                                                DataRow dr1 = dt.NewRow();

                                                result.LRCPrice = (result.LRCPrice * noOfdays) / 2;
                                                dr1["Attributes"] = result.Name;
                                                dr1["Price"] = result.LRCPrice.ToString("f2");
                                                dt.Rows.Add(dr1);
                                            }

                                            roomdet.RoomPrice = (roomdet.RoomPrice * noOfdays) / 2;
                                            dr["Attributes"] = "Room Rent";
                                            dr["Price"] = roomdet.RoomPrice.ToString("f2"); ;
                                            dt.Rows.Add(dr);

                                            ePrice = ePrice / 2;
                                            dr["Attributes"] = "Electricity";
                                            dr["Price"] = ePrice.ToString("f2");
                                            dt.Rows.Add(dr);

                                            Decimal TotalPrice = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price"]));

                                            DataRow drTotal = dt.NewRow();
                                            drTotal["Attributes"] = "Total";
                                            drTotal["Price"] = TotalPrice.ToString("f2"); ;
                                            dt.Rows.Add(drTotal);

                                            grdUser.DataSource = dt;
                                            grdUser.DataBind();

                                            Electricity.Visible = false;
                                            PrintDiv.Visible = true;
                                            btnPrint.Visible = true;

                                            lblBillNo.InnerText = roomdet.Id.ToString();
                                            lblName.InnerText = addet.Name;
                                            lblDate.InnerText = DateTime.Now.ToString("dd-mm-yyyy");
                                            lblRank.InnerText = addet.RankName;
                                            lblUnit.InnerText = addet.UnitName;
                                            lblRoom.InnerText = roomdet.RoomName;

                                        }
                                    }
                                    else
                                    {
                                        var roomattributes = bookingServices.GetDetailsBill();
                                        if (roomattributes != null)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dt.Columns.Add("Attributes");
                                            dt.Columns.Add("Price");

                                            foreach (var result in roomattributes)
                                            {
                                                DataRow dr1 = dt.NewRow();

                                                result.NonLRCPrice = (result.NonLRCPrice * noOfdays) / 2;
                                                dr1["Attributes"] = result.Name;
                                                dr1["Price"] = result.NonLRCPrice.ToString("f2"); ;
                                                dt.Rows.Add(dr1);
                                            }

                                            roomdet.RoomPrice = (roomdet.RoomPrice * noOfdays) / 2;
                                            dr["Attributes"] = "Room Rent";
                                            dr["Price"] = roomdet.RoomPrice.ToString("f2"); ;

                                            ePrice = ePrice / 2;
                                            dr["Attributes"] = "Electricity";
                                            dr["Price"] = ePrice.ToString("f2"); ;
                                            dt.Rows.Add(dr);


                                            Decimal TotalPrice = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price"]));

                                            DataRow drTotal = dt.NewRow();
                                            drTotal["Attributes"] = "Total";
                                            drTotal["Price"] = TotalPrice.ToString("f2"); ;
                                            dt.Rows.Add(drTotal);

                                            grdUser.DataSource = dt;
                                            grdUser.DataBind();

                                            Electricity.Visible = false;
                                            PrintDiv.Visible = true;
                                            btnPrint.Visible = true;

                                            lblBillNo.InnerText = roomdet.Id.ToString();
                                            lblName.InnerText = addet.Name;
                                            lblDate.InnerText = DateTime.Now.ToString("dd-mm-yyyy");
                                            lblRank.InnerText = addet.RankName;
                                            lblUnit.InnerText = addet.UnitName;
                                            lblRoom.InnerText = roomdet.RoomName;

                                        }
                                    }
                                }
                                else
                                {
                                    if (addet.IsLRC == true)
                                    {
                                        var roomattributes = bookingServices.GetDetailsBill();
                                        if (roomattributes != null)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dt.Columns.Add("Attributes");
                                            dt.Columns.Add("Price");

                                            foreach (var result in roomattributes)
                                            {
                                                DataRow dr1 = dt.NewRow();

                                                result.LRCPrice = result.LRCPrice * noOfdays;
                                                dr1["Attributes"] = result.Name;
                                                dr1["Price"] = result.LRCPrice.ToString("f2");
                                                dt.Rows.Add(dr1);
                                            }

                                            roomdet.RoomPrice = roomdet.RoomPrice * noOfdays;
                                            dr["Attributes"] = "Room Rent";
                                            dr["Price"] = roomdet.RoomPrice.ToString("f2");


                                            dr["Attributes"] = "Electricity";
                                            dr["Price"] = ePrice.ToString("f2");

                                            dt.Rows.Add(dr);

                                            Decimal TotalPrice = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price"]));

                                            DataRow drTotal = dt.NewRow();
                                            drTotal["Attributes"] = "Total";
                                            drTotal["Price"] = TotalPrice.ToString("f2");
                                            dt.Rows.Add(drTotal);

                                            grdUser.DataSource = dt;
                                            grdUser.DataBind();

                                            Electricity.Visible = false;
                                            PrintDiv.Visible = true;
                                            btnPrint.Visible = true;

                                            lblBillNo.InnerText = roomdet.Id.ToString();
                                            lblName.InnerText = addet.Name;
                                            lblDate.InnerText = DateTime.Now.ToString("dd-mm-yyyy");
                                            lblRank.InnerText = addet.RankName;
                                            lblUnit.InnerText = addet.UnitName;
                                            lblRoom.InnerText = roomdet.RoomName;
                                        }
                                    }
                                    else
                                    {
                                        var roomattributes = bookingServices.GetDetailsBill();
                                        if (roomattributes != null)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dt.Columns.Add("Attributes");
                                            dt.Columns.Add("Price");

                                            foreach (var result in roomattributes)
                                            {
                                                DataRow dr1 = dt.NewRow();

                                                result.NonLRCPrice = result.NonLRCPrice * noOfdays;
                                                dr1["Attributes"] = result.Name;
                                                dr1["Price"] = result.NonLRCPrice.ToString("f2");
                                                dt.Rows.Add(dr1);
                                            }

                                            roomdet.RoomPrice = roomdet.RoomPrice * noOfdays;
                                            dr["Attributes"] = "Room Rent";
                                            dr["Price"] = roomdet.RoomPrice.ToString("f2");

                                            dr["Attributes"] = "Electricity";
                                            dr["Price"] = ePrice.ToString("f2");

                                            dt.Rows.Add(dr);

                                            Decimal TotalPrice = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price"]));

                                            DataRow drTotal = dt.NewRow();
                                            drTotal["Attributes"] = "Total";
                                            drTotal["Price"] = TotalPrice.ToString("f2");
                                            dt.Rows.Add(drTotal);

                                            grdUser.DataSource = dt;
                                            grdUser.DataBind();

                                            Electricity.Visible = false;
                                            PrintDiv.Visible = true;
                                            btnPrint.Visible = true;

                                            lblBillNo.InnerText = roomdet.Id.ToString();
                                            lblName.InnerText = addet.Name;
                                            lblDate.InnerText = DateTime.Now.ToString("dd-mm-yyyy");
                                            lblRank.InnerText = addet.RankName;
                                            lblUnit.InnerText = addet.UnitName;
                                            lblRoom.InnerText = roomdet.RoomName;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Error!');window.location = ADList", true);
            }
        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnFinalBill_Click(object sender, EventArgs e)
        {
            var adid = Convert.ToInt32(Request.QueryString["Id"]);
            if (adid != 0)
            {
                bookingServices = new BookingServices(new TCContext());
                aDServices = new ADServices(new TCContext());

                //get difference of dates
                var getaddetails = aDServices.GetByID(adid);
                if (getaddetails != null)
                {
                    if (getaddetails.CheckOutDate != null)
                    {
                        dynamic diff;
                        diff = Math.Round((getaddetails.CheckOutDate - getaddetails.Date).Value.TotalDays);
                        if (diff > 0)
                        {
                            double pricePerUnit = Convert.ToDouble(txtUnitPrice.Text);
                            double DateDiff = diff;
                            double finalPrice = pricePerUnit * Convert.ToDouble(txtUnitConsumed.Text);
                            Bind(finalPrice, (int)DateDiff);
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Same Day Checkout No Bill Found!');window.location = ADList", true);

                    }
                }
            }
        }
    }
}