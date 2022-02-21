using BusinessLayer;
using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Bookings
{
    public partial class AddBooking : System.Web.UI.Page
    {
        protected IBookingServices bookingServices;
        protected IADServices aDServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Int32 id = Convert.ToInt32(Request.QueryString["ID"]);
                if (id == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No ID Found!');window.location = 'ADEntery';", true);
                }

                BindRooms();
            }
        }


        //bind cities
        protected void BindRooms()
        {
            bookingServices = new BookingServices(new TCContext());
            var getlist = bookingServices.GetDetailsRoom();
            chkSelectRooms.DataSource = getlist;
            chkSelectRooms.DataValueField = "ID";
            chkSelectRooms.DataTextField = "RoomName";
            chkSelectRooms.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Visible = true;

            int roomId = 0;

            Int32 AdId = Convert.ToInt32(Request.QueryString["ID"]);

            if (AdId != 0)
            {
                for (int i = 0; i < chkSelectRooms.Items.Count; i++)
                {
                    if (chkSelectRooms.Items[i].Selected == true)
                    {
                        roomId = Convert.ToInt32(chkSelectRooms.Items[i].Value);
                        if (roomId != 0)
                        {
                            bookingServices = new BookingServices(new TCContext());
                            var getroomdetails = bookingServices.GetIDRoom(roomId);
                            if (getroomdetails != null)
                            {
                                if (getroomdetails.IsShare == true)
                                {
                                    if (getroomdetails.MaxRoomCap == 0)
                                    {
                                        getroomdetails.IsFull = true;
                                        bookingServices.UpdateRoom(getroomdetails);
                                        bookingServices.Save();

                                        lblError.Visible = true;
                                        lblError.Text = "Room capacity is full.";
                                    }
                                    else
                                    {
                                        getroomdetails.MaxRoomCap = getroomdetails.MaxRoomCap - 1;
                                        bookingServices.UpdateRoom(getroomdetails);
                                        bookingServices.Save();

                                        Booking InfoBook = new Booking();
                                        InfoBook.ADId = AdId;
                                        InfoBook.BlockId = Convert.ToInt64(getroomdetails.BlockId);
                                        InfoBook.RoomId = Convert.ToInt32(getroomdetails.Id);

                                        bookingServices.InsertBooking(InfoBook);
                                        bookingServices.Save();
                                    }
                                }
                                else
                                {
                                    Booking InfoBook = new Booking();
                                    InfoBook.ADId = AdId;
                                    InfoBook.BlockId = Convert.ToInt64(getroomdetails.BlockId);
                                    InfoBook.RoomId = Convert.ToInt32(getroomdetails.Id);
                                    bookingServices.InsertBooking(InfoBook);
                                    bookingServices.Save();

                                    getroomdetails.IsFull = true;
                                    bookingServices.UpdateRoom(getroomdetails);
                                    bookingServices.Save();
                                }
                            }
                        }
                    }
                }

                aDServices = new ADServices(new TCContext());
                var getadbyid = aDServices.GetByID(AdId);

                if (chkISFly.Checked == true)
                    getadbyid.IsFly = true;
                if (chkIsLRC.Checked == true)
                    getadbyid.IsLRC = true;

                aDServices.Update(getadbyid);
                aDServices.Save();


                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'ADEntery';", true);

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
        }
    }
}