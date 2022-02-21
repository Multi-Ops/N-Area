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
    public partial class AddMedicalStatus : System.Web.UI.Page
    {
        protected IMedicalStatusServices medicalStatusServices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }
        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["MedID"]);
            medicalStatusServices = new MedicalStatusServices(new TCContext());
            var getcitydetail = medicalStatusServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtMedicalStatus.Text = getcitydetail.MedicalStatusName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtMedicalStatus.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                medicalStatusServices = new MedicalStatusServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["MedID"]);
                Int64 checkcityexist = medicalStatusServices.CheckAlreadyExist(txtMedicalStatus.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Medical Status Already Exist.";
                }
                else
                {

                    MedicalStatus info = new MedicalStatus();
                    info.MedicalStatusName = txtMedicalStatus.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["MedID"]);
                        info.UpdatedOn = DateTime.Now;
                        medicalStatusServices.Update(info);
                        medicalStatusServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'MedicalStatusList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        medicalStatusServices.Insert(info);
                        medicalStatusServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'MedicalStatusList';", true);
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