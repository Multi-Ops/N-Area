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
    public partial class AddDivision : System.Web.UI.Page
    {
        protected IHeadquarterServices headquarterServices;
        protected IDivisionServices divisionservices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindHQ();
                GetDivDetails();
            }
        }

        //bind headquarters
        protected void BindHQ()
        {
            headquarterServices = new HeadquarterServices(new TCContext());
            var getlist = headquarterServices.GetHQDetails();
            ddlHQ.DataSource = getlist;
            ddlHQ.DataValueField = "ID";
            ddlHQ.DataTextField = "HQName";
            ddlHQ.DataBind();
            ddlHQ.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        protected void GetDivDetails()
        {
            Int32 divid = Convert.ToInt32(Request.QueryString["DivID"]);
            divisionservices = new DivisionServices(new TCContext());
            var getdivdetail = divisionservices.GetDivID(divid);
            if (getdivdetail != null)
            {
                txtDName.Text = getdivdetail.DivName;
                ddlHQ.SelectedValue = getdivdetail.HQID.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtDName.Text == "" || ddlHQ.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                divisionservices = new DivisionServices(new TCContext());
                Int32 divid = Convert.ToInt32(Request.QueryString["DivID"]);
                Int64 checkexist = divisionservices.CheckAlreadyExist(txtDName.Text);
                if (divid == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Division Already Exist.";
                }
                else
                {

                    Division div = new Division();
                    div.DivName = txtDName.Text;
                    div.HQID = Convert.ToInt64(ddlHQ.SelectedValue);


                    if (divid != 0)
                    {
                        div.ID = Convert.ToInt32(Request.QueryString["DivID"]);
                        div.UpdatedOn = DateTime.Now;
                        divisionservices.Update(div);
                        divisionservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'DivisionList';", true);
                    }
                    else
                    {
                        div.CreatedOn = DateTime.Now;
                        divisionservices.Insert(div);
                        divisionservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'DivisionList';", true);
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