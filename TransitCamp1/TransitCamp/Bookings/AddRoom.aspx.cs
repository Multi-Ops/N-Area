using DataAccessLayer;
using DataLayer;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Bookings
{
    public partial class AddRoom : System.Web.UI.Page
    {
        protected IBookingServices bookingServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBlocks();
                GetDetails();
            }
        }

        protected void BindBlocks()
        {
            bookingServices = new BookingServices(new TCContext());
            var getlist = bookingServices.GetDetails();
            ddlblock.DataSource = getlist;
            ddlblock.DataValueField = "ID";
            ddlblock.DataTextField = "BlockName";
            ddlblock.DataBind();
            ddlblock.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["RoomID"]);
            bookingServices = new BookingServices(new TCContext());
            var getdetail = bookingServices.GetIDRoom(id);
            if (getdetail != null)
            {
                txtRoomName.Text = getdetail.RoomName;
                ddlblock.SelectedValue = getdetail.BlockId.ToString();
                txtMaxCount.Text = getdetail.MaxRoomCap.ToString();
                txtRoomPrice.Text = getdetail.RoomPrice.ToString();
                if (getdetail.IsBillShare == true)
                    chkIsBillShare.Checked = true;
                if (getdetail.IsShare == true)
                    chkShareable.Checked = true;

                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtRoomName.Text == string.Empty || txtMaxCount.Text == string.Empty || txtRoomPrice.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                bookingServices = new BookingServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["RoomID"]);
                Int64 checkexist = bookingServices.CheckAlreadyExistRoom(txtRoomName.Text);

                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Room Already Exist.";
                }
                else
                {

                    BllRoom infoRoom = new BllRoom();
                    infoRoom.RoomPrice = Convert.ToDouble(txtRoomPrice.Text);
                    infoRoom.RoomName = txtRoomName.Text;
                    infoRoom.MaxRoomCap = Convert.ToInt32(txtMaxCount.Text);
                    infoRoom.BlockId = Convert.ToInt64(ddlblock.SelectedValue);

                    if (chkShareable.Checked)
                        infoRoom.IsShare = true;
                    else
                        infoRoom.IsShare = false;

                    if (id != 0)
                    {
                        infoRoom.Id = Convert.ToInt32(Request.QueryString["RoomID"]);
                        infoRoom.UpdatedOn = DateTime.Now;
                        if (chkShareable.Checked == true)
                            infoRoom.IsBillShare = true;

                        bookingServices.UpdateRoom(infoRoom);
                        bookingServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'RoomList';", true);
                    }
                    else
                    {
                        infoRoom.IsFull = false;
                        infoRoom.IsBillShare = false;
                        infoRoom.CreatedOn = DateTime.Now;
                        bookingServices.InsertRoom(infoRoom);
                        bookingServices.Save();

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'RoomList';", true);
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertUpdate();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            InsertUpdate();
        }
    }
}