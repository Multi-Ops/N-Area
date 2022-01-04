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
    public partial class AddOutLogic : System.Web.UI.Page
    {
        protected IOutLogicServices outLogicServices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }

        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["OLID"]);
            outLogicServices = new OutLogicServices(new TCContext());
            var getcitydetail = outLogicServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtOLN.Text = getcitydetail.OutLogicName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtOLN.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                outLogicServices = new OutLogicServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["OLID"]);
                Int64 checkcityexist = outLogicServices.CheckAlreadyExist(txtOLN.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "OutLogic Already Exist.";
                }
                else
                {

                    OutLogic info = new OutLogic();
                    info.OutLogicName = txtOLN.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["OLID"]);
                        info.UpdatedOn = DateTime.Now;
                        outLogicServices.Update(info);
                        outLogicServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'OutLogicList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        outLogicServices.Insert(info);
                        outLogicServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'OutLogicList';", true);
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