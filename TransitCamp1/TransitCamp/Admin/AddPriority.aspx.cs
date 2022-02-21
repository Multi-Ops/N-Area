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
    public partial class AddPriority : System.Web.UI.Page
    {
        protected IPriorityServices priorityServices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }
        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["PriorityID"]);
            priorityServices = new PriorityServices(new TCContext());
            var getcitydetail = priorityServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtPriority.Text = getcitydetail.PriorityName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtPriority.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                priorityServices = new PriorityServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["PriorityID"]);
                Int64 checkcityexist = priorityServices.CheckAlreadyExist(txtPriority.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Priority Name Already Exist.";
                }
                else
                {

                    Priority priority = new Priority();
                    priority.PriorityName = txtPriority.Text;

                    if (id != 0)
                    {
                        priority.ID = Convert.ToInt32(Request.QueryString["PriorityID"]);
                        priority.UpdatedOn = DateTime.Now;
                        priorityServices.Update(priority);
                        priorityServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'PriorityList';", true);
                    }
                    else
                    {
                        priority.CreatedOn = DateTime.Now;
                        priorityServices.Insert(priority);
                        priorityServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'PriorityList';", true);
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