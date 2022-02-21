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
    public partial class Brigade : System.Web.UI.Page
    {
        protected IDivisionServices divisionservices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDivDetails();
            }
        }


        protected void GetDivDetails()
        {
            Int32 divid = Convert.ToInt32(Request.QueryString["DivID"]);
            divisionservices = new DivisionServices(new TCContext());
            var getdivdetail = divisionservices.GetBrigID(divid);
            if (getdivdetail != null)
            {
                txtDName.Text = getdivdetail.Name;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtDName.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                divisionservices = new DivisionServices(new TCContext());
                Int32 divid = Convert.ToInt32(Request.QueryString["DivID"]);
                Int64 checkexist = divisionservices.CheckAlreadyExistBrig(txtDName.Text);
                if (divid == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Division Already Exist.";
                }
                else
                {

                    BusinessLayer.Brigade div = new BusinessLayer.Brigade();
                    div.Name = txtDName.Text;

                    if (divid != 0)
                    {
                        div.Id = Convert.ToInt32(Request.QueryString["DivID"]);
                        div.UpdatedOn = DateTime.Now;
                        divisionservices.UpdateBrig(div);
                        divisionservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'BrigadeList';", true);
                    }
                    else
                    {
                        div.CreatedOn = DateTime.Now;
                        divisionservices.InsertBrig(div);
                        divisionservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'BrigadeList';", true);
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