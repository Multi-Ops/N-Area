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
    public partial class AddCategory : System.Web.UI.Page
    {
        protected ICategoryServices categoryServices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAirlineDetails();
            }
        }
        protected void GetAirlineDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["CatID"]);
            categoryServices = new CategoryServices(new TCContext());
            var getcitydetail = categoryServices.GetByID(id);
            if (getcitydetail != null)
            {
                txtCategory.Text = getcitydetail.CategoryName;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void InsertUpdate()
        {
            if (txtCategory.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                categoryServices = new CategoryServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["CatID"]);
                Int64 checkcityexist = categoryServices.CheckAlreadyExist(txtCategory.Text);
                if (id == 0 && checkcityexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Category Already Exist.";
                }
                else
                {

                    Category info = new Category();
                    info.CategoryName = txtCategory.Text;

                    if (id != 0)
                    {
                        info.ID = Convert.ToInt32(Request.QueryString["CatID"]);
                        info.UpdatedOn = DateTime.Now;
                        categoryServices.Update(info);
                        categoryServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'CategoryList';", true);
                    }
                    else
                    {
                        info.CreatedOn = DateTime.Now;
                        categoryServices.Insert(info);
                        categoryServices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'CategoryList';", true);
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