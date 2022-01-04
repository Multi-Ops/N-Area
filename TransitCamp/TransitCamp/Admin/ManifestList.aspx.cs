using BusinessLayer;
using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helper;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;


namespace TransitCamp.Admin
{
    public partial class ManifestList : System.Web.UI.Page
    {
        IManifestServices manifestServices;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindManifest();
                Bind();
            }
        }

        protected void Bind()
        {
            manifestServices = new ManifestServices(new TCContext());

            var manifestno = Request.QueryString["ManifestNo"];
            if (manifestno != null)
            {
                var data = manifestServices.PagingManifest(manifestno.ToString());
                grdManifest.DataSource = data;
                grdManifest.DataBind();
            }
            else if (manifestno == null)
            {
                string item = rptManifest.Items[0].ToString();
                grdManifest.DataSource = "";
                grdManifest.DataBind();
            }
        }

        private void BindManifest()
        {
            manifestServices = new ManifestServices(new TCContext());
            var data = manifestServices.GetManifest();
            rptManifest.DataSource = data;
            rptManifest.DataBind();
        }

        public Object GetGridData(string ManifestNo)
        {
            manifestServices = new ManifestServices(new TCContext());
            var data = manifestServices.PagingManifest(ManifestNo.ToUpper().Trim());
            string jsonconvet = JsonConvert.SerializeObject(data);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", jsonconvet.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(jsonconvet);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return jsonconvet.ToArray();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static void aClick(string ManifestNo)
        {
            ManifestList manifest = new ManifestList();
            manifest.GetGridData(ManifestNo);
        }
    }
}