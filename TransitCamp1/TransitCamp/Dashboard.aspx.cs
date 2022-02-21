using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using System.Net;
using System.ComponentModel;

namespace TransitCamp
{
    public partial class Dashboard : System.Web.UI.Page
    {
        ITransportDetailsServices transportDetailsServices;
        IManifestServices manifestServices;
        ICityServices cityServices;
        ICategoryServices categoryServices;
        IADServices aDServices;
        IMoveServices moveServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater();
                BindADLeft();
            }
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            Int64 userid = Convert.ToInt64(Session["UserID"]);
            if (userid != 0)
            {
                Response.Redirect("CreateAccount?ID=" + userid + "");
            }
        }

        protected void BindADLeft()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Jco", typeof(string));
            dt.Columns.Add("Officer", typeof(string));
            dt.Columns.Add("Other", typeof(int));
            dt.Columns.Add("OnTempHold", typeof(int));
            dt.Columns.Add("OnHoldStatus", typeof(int));
            dt.Columns.Add("Priority", typeof(int));
            dt.Columns.Add("Reserve", typeof(int));
            dt.Columns.Add("Load", typeof(int));
            dt.Columns.Add("Rest", typeof(int));
            dt.Columns.Add("Lve", typeof(int));
            dt.Columns.Add("td", typeof(int));
            dt.Columns.Add("Posting", typeof(int));
            dt.Columns.Add("ADVParty", typeof(int));
            dt.Columns.Add("MoveOther", typeof(int));
            dt.Columns.Add("ADsLeftAfterExcluding", typeof(int));
            dt.Columns.Add("Total", typeof(int));

            int Jcos = 0;
            int other = 0;
            int officer = 0;
            int ontemphold = 0;
            int reserve = 0;
            int load = 0;
            int priority = 0;
            int total = 0;
            int rest = 0;
            int lve = 0;
            int td = 0;
            int rtu = 0;
            int posting = 0;
            int advparty = 0;
            int moveother = 0;
            int tdrtu = 0;
            int onHoldStatus = 0;
            int aDsLeftAfterExc = 0;


            aDServices = new ADServices(new TCContext());

            var ads = aDServices.GetAllADLeftModify();
            if (ads != null)
            {
                Jcos = (from p in ads
                        where p.CategoryName.ToLower().Contains("jco")
                        select p).Count();
                officer = (from p in ads
                           where p.CategoryName.ToLower().Contains("officer")
                           select p).Count();
                other = (from p in ads
                         where p.CategoryName.ToLower().Contains("other") || p.CategoryName.Contains("or")
                         select p).Count();
                ontemphold = (from p in ads
                              where p.IsOnHold == true
                              select p).Count();
                onHoldStatus = (from p in ads
                                where p.IsTempHold == true
                                select p).Count();
                reserve = (from p in ads
                           where p.IsReserve == true
                           select p).Count();
                load = (from p in ads
                        where p.IsLoad == true
                        select p).Count();
                priority = (from p in ads
                            where p.IsPriority == true
                            select p).Count();
                rest = (from p in ads
                        where p.IsOnHold == false && p.IsTempHold == false && p.IsReserve == false && p.IsLoad == false && p.IsPriority == false
                        select p).Count();

                lve = (from p in ads
                       where p.MoveName.ToLower().Contains("lve")
                       select p).Count();

                td = (from p in ads
                      where p.MoveName.ToLower() == "td"
                      select p).Count();

                rtu = (from p in ads
                       where p.MoveName.ToLower() == "rtu"
                       select p).Count();

                posting = (from p in ads
                           where p.MoveName.ToLower().Contains("posting")
                           select p).Count();

                advparty = (from p in ads
                            where p.MoveName.ToLower().Contains("adv party")
                            select p).Count();

                moveother = (from p in ads
                             where p.MoveName.ToLower() != "adv party" && p.MoveName.ToLower() != "posting" && p.MoveName.ToLower() != "td" && p.MoveName.ToLower() != "rtu" && p.MoveName.ToLower() != "lve"
                             select p).Count();

                tdrtu = td + rtu;
                total = ads.Count();

                //ads left after excluding onhold and cancel ads 
                int excludeRes = onHoldStatus + ontemphold;
                aDsLeftAfterExc = total - excludeRes;

                DataRow dr = dt.NewRow();
                dr["Jco"] = Jcos;
                dr["Officer"] = officer;
                dr["Other"] = other;
                dr["OnTempHold"] = ontemphold;
                dr["Priority"] = priority;
                dr["Reserve"] = reserve;
                dr["Load"] = load;
                dr["Rest"] = rest;
                dr["Lve"] = lve;
                dr["td"] = tdrtu;
                dr["Posting"] = posting;
                dr["ADVParty"] = advparty;
                dr["MoveOther"] = moveother;
                dr["OnHoldStatus"] = onHoldStatus;
                dr["ADsLeftAfterExcluding"] = aDsLeftAfterExc;
                dr["Total"] = total;

                dt.Rows.Add(dr);
            }
            rptAdsLeft.DataSource = dt;
            rptAdsLeft.DataBind();
        }

        protected void BindRepeater()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TransportDetail", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("NoOfOfficers", typeof(int));
            dt.Columns.Add("NoOfJCOs", typeof(int));
            dt.Columns.Add("NoOfOthers", typeof(int));
            dt.Columns.Add("Total", typeof(int));

            transportDetailsServices = new TransportDetailsServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            cityServices = new CityServices(new TCContext());
            categoryServices = new CategoryServices(new TCContext());

            var data = transportDetailsServices.GetDetails();
            int jcos = 0;
            int Officer = 0;
            int Other = 0;
            string cityname = "";

            if (data != null)
                foreach (var dataloop in data)
                {
                    if (data.Count >= 1)
                    {
                        object manifestdata = manifestServices.GetByTransportIDDateWise(dataloop.ID);
                        if (manifestdata != null)
                        {
                            var manifestdatalist = manifestServices.GetManifestWithManifestNo(manifestdata.ToString());
                            var transportdata = transportDetailsServices.GetDetailsByID(manifestdatalist[0].TransportDetailID);
                            if (transportdata != null)
                            {
                                string transportdetails = transportdata.TransportDetail;
                                var citydata = cityServices.GetCityID(Convert.ToInt64(transportdata.CityID));
                                if (citydata != null)
                                    cityname = citydata.CityName;

                                if (manifestdatalist.Count >= 1)
                                {
                                    var getallcats = categoryServices.details();
                                    if (getallcats != null)
                                    {
                                        foreach (var cat in getallcats)
                                        {
                                            var catwise = from p in manifestdatalist
                                                          where p.CategoryID == cat.ID
                                                          select p;
                                            if (cat.CategoryName.ToString().Trim().ToLower() == "jcos")
                                                jcos = catwise.Count();
                                            else if (cat.CategoryName.ToString().Trim().ToLower() == "officer")
                                                Officer = catwise.Count();
                                            else if (cat.CategoryName.ToString().Trim().ToLower() == "other")
                                                Other = catwise.Count();
                                        }
                                    }
                                    int total = manifestdatalist.Count;
                                    DataRow dr = dt.NewRow();
                                    dr["Total"] = total;
                                    dr["TransportDetail"] = transportdetails;
                                    dr["City"] = cityname;
                                    dr["NoOfOfficers"] = Officer;
                                    dr["NoOfJCOs"] = jcos;
                                    dr["NoOfOthers"] = Other;

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                }

            rptSummary.DataSource = dt;
            rptSummary.DataBind();
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            btnBackup.Enabled = false;

            string constr = "server=localhost;user=root;pwd=123456;database=transitcampinit;";
            string file = "";
            // Important Additional Connection Options
            constr += "charset=utf8;convertzerodatetime=true;";
            MySqlCommand cmd = new MySqlCommand();
            var conn = new MySqlConnection(constr);
            string dir = @"C:\TransitCampDataBackup\";
            string filename = "TransitBackup(" + DateTime.Now.ToString("dd-MMMM-yyyy") + ").sql";

            //Create directory if not exists
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (Directory.Exists(dir))
                file = dir + filename;

            MySqlBackup mb = new MySqlBackup(cmd);
            cmd.Connection = conn;
            conn.Open();
            mb.ExportToFile(file);
            conn.Clone();

            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.Exists)
            {
                Response.Clear();
                Response.AppendHeader("Content-Disposition", ("attachment; filename=" + filename));
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(fileInfo.FullName);
                Response.End();
            }
            btnBackup.Enabled = true;
        }
    }
}