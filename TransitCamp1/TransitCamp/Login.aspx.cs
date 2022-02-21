using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessLayer;
using DataLayer;
using Helper;
using System.Web.Security;

namespace TransitCamp
{
    public partial class Login : System.Web.UI.Page
    {
        private IUserServices userServices;
        private IHelper helper;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserID"]) != null)
            {
                Response.Redirect("Dashboard");
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            userServices = new UserServices(new TCContext());
            helper = new Helper.Helper();
            Int64 checkuserexist = userServices.UserExist(txtUserName.Text);
            if (checkuserexist != 0)
            {
                var getuserdetail = userServices.GetUserByID(checkuserexist);
                var passwordsalt = getuserdetail.PasswordSalt;
                var encodingPassword = helper.EncodePassword(txtPassword.Text, passwordsalt);
                int checkpassword = userServices.CheckPassword(encodingPassword, checkuserexist);
                if (checkpassword != 0)
                {
                    Session["UserID"] = checkuserexist;
                    Session["UserRoleID"] = getuserdetail.UserRoleD;
                    Response.Redirect("Dashboard?id=" + checkuserexist + "");
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Password Not Matched!";
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Incorrect User Name!";
            }

        }
    }
}