using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;
using DataAccessLayer;
using Org.BouncyCastle.Crypto.Tls;
using Helper;

namespace TransitCamp
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        private IUserServices userservices;
        private IHelper helper;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Int32 checkid = Convert.ToInt32(Request.QueryString["ID"]);
                if (checkid != 0)
                {
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    btnUpdate.Enabled = true;
                    GetUserDetails();
                }
            }

        }

        protected void GetUserDetails()
        {
            Int32 userid = Convert.ToInt32(Request.QueryString["ID"]);
            userservices = new UserServices(new TCContext());
            var getuserdetail = userservices.GetUserByID(userid);
            if (getuserdetail != null)
            {
                txtArmyNumber.Text = getuserdetail.ArmyNumber;
                txtCardNumber.Text = getuserdetail.IDCardNo;
                txtName.Text = getuserdetail.Name;
                txtRank.Text = getuserdetail.Rank;
                txtRegiment.Text = getuserdetail.Regiment;
                txtUserName.Text = getuserdetail.UserName;
                txtUserName.Enabled = false;
            }
        }

        protected void InsertUpdateuser()
        {
            if (txtArmyNumber.Text == "" || txtCardNumber.Text == "" || txtName.Text == "" || txtRank.Text == "" || txtRegiment.Text == "" || txtUserName.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {
                userservices = new UserServices(new TCContext());
                helper = new Helper.Helper();
                Int32 userid = Convert.ToInt32(Request.QueryString["ID"]);
                Int64 checkuserexist = userservices.UserExist(txtUserName.Text);
                if (userid == 0 && checkuserexist != 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "User Name Already Exist.";
                }
                else
                {

                    Users user = new Users();
                    user.Name = txtName.Text;
                    user.IDCardNo = txtCardNumber.Text;
                    user.ArmyNumber = txtArmyNumber.Text;
                    user.Rank = txtRank.Text;
                    user.Regiment = txtRegiment.Text;
                    user.UserName = txtUserName.Text;
                    user.UserRoleD = 2;
                    //encode password MD5
                    //var keyNew = helper.GeneratePassword(10);
                    //var password = helper.EncodePassword(txtPassword.Text, keyNew);
                    //user.Password = password;
                    //user.PasswordSalt = keyNew;

                    if (userid != 0)
                    {
                        user.ID = Convert.ToInt32(Request.QueryString["ID"]);
                        user.UpdatedOn = DateTime.Now;
                        userservices.Update(user);
                        userservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'UserList';", true);
                    }
                    else
                    {
                        user.CreatedOn = DateTime.Now;
                        userservices.Insert(user);
                        userservices.Save();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'UserList';", true);
                    }
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            InsertUpdateuser();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            InsertUpdateuser();
        }
    }
}