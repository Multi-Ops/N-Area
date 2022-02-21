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
    public partial class AddBlock : System.Web.UI.Page
    {
        protected IBookingServices bookingServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDetails();
            }
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["BlockID"]);
            bookingServices = new BookingServices(new TCContext());
            var getdetail = bookingServices.GetID(id);
            if (getdetail != null)
            {
                txtBlockName.Text = getdetail.BlockName;
                txtDefaultPrice.Text = getdetail.DefaultPrice.ToString();
                txtMaxRoomAvailable.Text = getdetail.MaxRoomAvailable.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtBlockName.Text == string.Empty || txtDefaultPrice.Text == string.Empty || txtMaxRoomAvailable.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                bookingServices = new BookingServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["BlockID"]);
                Int64 checkexist = bookingServices.CheckAlreadyExist(txtBlockName.Text);
                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Block Already Exist.";
                }
                else
                {

                    BllBlock info = new BllBlock();
                    info.BlockName = txtBlockName.Text;
                    info.DefaultPrice = Convert.ToDouble(txtDefaultPrice.Text);
                    info.MaxRoomAvailable = Convert.ToInt32(txtMaxRoomAvailable.Text);

                    if (id != 0)
                    {
                        info.Id = Convert.ToInt32(Request.QueryString["BlockID"]);
                        info.UpdatedOn = DateTime.Now;
                        bookingServices.Update(info);
                        bookingServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'BlockList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        bookingServices.Insert(info);
                        bookingServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'BlockList';", true);
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