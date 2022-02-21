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
    public partial class AddHeadquarter : System.Web.UI.Page
    {
        protected IHeadquarterServices headquarterServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDetails();
            }
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["HQID"]);
            headquarterServices = new HeadquarterServices(new TCContext());
            var getdetail = headquarterServices.GetID(id);
            if (getdetail != null)
            {
                txtHName.Text = getdetail.HQName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtHName.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                headquarterServices = new HeadquarterServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["HQID"]);
                Int64 checkexist = headquarterServices.CheckAlreadyExist(txtHName.Text);
                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Headquarter Already Exist.";
                }
                else
                {

                    Headquarter hq = new Headquarter();
                    hq.HQName = txtHName.Text;

                    if (id != 0)
                    {
                        hq.ID = Convert.ToInt32(Request.QueryString["HQID"]);
                        hq.UpdatedOn = DateTime.Now;
                        headquarterServices.Update(hq);
                        headquarterServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'HeadquarterList';", true);
                    }
                    else
                    {
                        hq.CreatedOn = DateTime.Now;
                        headquarterServices.Insert(hq);
                        headquarterServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'HeadquarterList';", true);
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