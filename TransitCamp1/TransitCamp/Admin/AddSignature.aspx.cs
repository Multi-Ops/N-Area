using DataAccessLayer;
using DataLayer;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TransitCamp.Admin
{
    public partial class AddSignature : System.Web.UI.Page
    {
        protected ISignatureServices signatureServices;
        protected IHeadquarterServices headquarterServices;
        protected IUnitServices unitServices;
        protected IRankServices rankServices;
        protected IDivisionServices divisionServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDetails();
                BindHQ();
                BindUnit();
                BindRank();
                BindDiv();
            }
        }

        //bind headquarters
        protected void BindHQ()
        {
            headquarterServices = new HeadquarterServices(new TCContext());
            var getlist = headquarterServices.GetHQDetails();
            dllHq.DataSource = getlist;
            dllHq.DataValueField = "ID";
            dllHq.DataTextField = "HQName";
            dllHq.DataBind();
            dllHq.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Units
        protected void BindUnit()
        {
            unitServices = new UnitServices(new TCContext());
            var getlist = unitServices.GetUnitDetails();
            ddlUnit.DataSource = getlist;
            ddlUnit.DataValueField = "ID";
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Rank
        protected void BindRank()
        {
            rankServices = new RankServices(new TCContext());
            var getlist = rankServices.GetRankDetails();
            ddlRank.DataSource = getlist;
            ddlRank.DataValueField = "ID";
            ddlRank.DataTextField = "Rank";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Division
        protected void BindDiv()
        {
            divisionServices = new DivisionServices(new TCContext());
            var getlist = divisionServices.GetDivtDetails();
            ddlDiv.DataSource = getlist;
            ddlDiv.DataValueField = "ID";
            ddlDiv.DataTextField = "DivName";
            ddlDiv.DataBind();
            ddlDiv.Items.Insert(0, new ListItem("-- Select --", ""));
        }


        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["SignatureID"]);
            signatureServices = new SignatureServices(new TCContext());
            var getdetail = signatureServices.GetByID(id);
            if (getdetail != null)
            {
                txtSign.Text = getdetail.SignatureName;
                ddlDiv.SelectedValue = getdetail.DivID.ToString();
                dllHq.SelectedValue = getdetail.HQID.ToString();
                ddlUnit.SelectedValue = getdetail.UnitID.ToString();
                ddlRank.SelectedValue = getdetail.RankID.ToString();

                //ddlRank.SelectedItem.Text = getdetail.RankName;
                //ddlUnit.SelectedItem.Text = getdetail.UnitName;
                //dllHq.SelectedItem.Text = getdetail.HeadQuarterName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtSign.Text == "" || dllHq.SelectedValue.ToString() == "" || ddlUnit.SelectedValue.ToString() == "" || ddlRank.SelectedValue.ToString() == "" || ddlDiv.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                signatureServices = new SignatureServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["SignatureID"]);
                Int64 checkexist = signatureServices.CheckAlreadyExist(txtSign.Text);
                if (id == 0 && checkexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Signature Already Exist.";
                }
                else
                {

                    Signature info = new Signature();
                    info.SignatureName = txtSign.Text;
                    info.HQID = Convert.ToInt64(dllHq.SelectedValue);
                    info.RankID = Convert.ToInt64(ddlRank.SelectedValue);
                    info.DivID = Convert.ToInt64(ddlDiv.SelectedValue);
                    info.UnitID = Convert.ToInt64(ddlUnit.SelectedValue);

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["SignatureID"]);
                        info.UpdatedOn = DateTime.Now;
                        signatureServices.Update(info);
                        signatureServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'SignatureList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        signatureServices.Insert(info);
                        signatureServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'SignatureList';", true);
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