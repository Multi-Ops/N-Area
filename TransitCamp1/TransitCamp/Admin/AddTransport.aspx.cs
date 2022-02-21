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
    public partial class AddTransport : System.Web.UI.Page
    {
        protected ITransportServices transportServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GetDetails();
            }
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["TransportID"]);
            transportServices = new TransportServices(new TCContext());
            var getdetail = transportServices.GetID(id);
            if (getdetail != null)
            {
                txtTransport.Text = getdetail.TransportName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtTransport.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                transportServices = new TransportServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["TransportID"]);
                Int64 checkexist = transportServices.CheckAlreadyExist(txtTransport.Text);
                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Transport Already Exist.";
                }
                else
                {

                    Transport info = new Transport();
                    info.TransportName = txtTransport.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["TransportID"]);
                        info.UpdatedOn = DateTime.Now;
                        transportServices.Update(info);
                        transportServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'TransportList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        transportServices.Insert(info);
                        transportServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'TransportList';", true);
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