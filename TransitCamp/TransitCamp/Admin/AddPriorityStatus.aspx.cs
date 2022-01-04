using BusinessLayer;
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
    public partial class AddPriorityStatus : System.Web.UI.Page
    {
        protected IPriorityStatusServices priorityStatusServices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }
        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["PriorityStatusID"]);
            priorityStatusServices = new PriorityStatusServices(new TCContext());
            var getcitydetail = priorityStatusServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtPriorityStatus.Text = getcitydetail.PStatusName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtPriorityStatus.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                priorityStatusServices = new PriorityStatusServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["PriorityStatusID"]);
                Int64 checkcityexist = priorityStatusServices.CheckAlreadyExist(txtPriorityStatus.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Priority Status Already Exist.";
                }
                else
                {

                    PriorityStatus info = new PriorityStatus();
                    info.PStatusName = txtPriorityStatus.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["PriorityStatusID"]);
                        info.UpdatedOn = DateTime.Now;
                        priorityStatusServices.Update(info);
                        priorityStatusServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'PriorityStatusList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        priorityStatusServices.Insert(info);
                        priorityStatusServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'PriorityStatusList';", true);
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