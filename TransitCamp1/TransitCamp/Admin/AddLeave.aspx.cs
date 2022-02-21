using DataAccessLayer;
using DataLayer;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Admin
{
    public partial class AddLeave : System.Web.UI.Page
    {
        protected ILeaveServices leaveServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDetails();
            }
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["LeaveID"]);
            leaveServices = new LeaveServices(new TCContext());
            var getdetail = leaveServices.GetID(id);
            if (getdetail != null)
            {
                txtleavetype.Text = getdetail.LeaveType;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtleavetype.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                leaveServices = new LeaveServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["LeaveID"]);
                Int64 checkexist = leaveServices.CheckAlreadyExist(txtleavetype.Text);
                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Leave Name Already Exist.";
                }
                else
                {

                    Leave info = new Leave();
                    info.LeaveType = txtleavetype.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["LeaveID"]);
                        info.UpdatedOn = DateTime.Now;
                        leaveServices.Update(info);
                        leaveServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'LeaveList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        leaveServices.Insert(info);
                        leaveServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'LeaveList';", true);
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