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
    public partial class AddBillAttribute : System.Web.UI.Page
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
            Int32 id = Convert.ToInt32(Request.QueryString["BillAID"]);
            bookingServices = new BookingServices(new TCContext());
            var getdetail = bookingServices.GetIDBill(id);
            if (getdetail != null)
            {
                txtName.Text = getdetail.Name;
                txtLRCPrice.Text = getdetail.LRCPrice.ToString();
                txtNonLRCPrice.Text = getdetail.NonLRCPrice.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtNonLRCPrice.Text == string.Empty || txtName.Text == string.Empty || txtLRCPrice.Text == string.Empty)
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                bookingServices = new BookingServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["BillAID"]);
                Int64 checkexist = bookingServices.CheckAlreadyExistBill(txtName.Text);

                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Bill Attribute Already Exist.";
                }
                else
                {

                    BLLBillAttribute info = new BLLBillAttribute();
                    info.Name = txtName.Text;
                    info.LRCPrice = Convert.ToDouble(txtLRCPrice.Text);
                    info.NonLRCPrice = Convert.ToInt32(txtNonLRCPrice.Text);

                    if (id != 0)
                    {
                        info.Id = Convert.ToInt32(Request.QueryString["BillAID"]);
                        info.UpdatedOn = DateTime.Now;
                        bookingServices.UpdateBill(info);
                        bookingServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'BillAttributeList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        bookingServices.InsertBill(info);
                        bookingServices.Save();

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'BillAttributeList';", true);
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