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
    public partial class AddADType : System.Web.UI.Page
    {
        protected IADTypeServices adtypeservices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }
        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["ADTypeID"]);
            adtypeservices = new ADTypeServices(new TCContext());
            var getcitydetail = adtypeservices.GetByID(id);
            if (getcitydetail != null)
            {
                txtADT.Text = getcitydetail.ADTypeName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtADT.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                adtypeservices = new ADTypeServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["ADTypeID"]);
                Int64 checkcityexist = adtypeservices.CheckAlreadyExist(txtADT.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "AD Type Already Exist.";
                }
                else
                {

                    ADType info = new ADType();
                    info.ADTypeName = txtADT.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["ADTypeID"]);
                        info.UpdatedOn = DateTime.Now;
                        adtypeservices.Update(info);
                        adtypeservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'ADTypeList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        adtypeservices.Insert(info);
                        adtypeservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'ADTypeList';", true);
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