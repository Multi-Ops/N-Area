using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Reports
{
    public partial class DepartureTransportWise : System.Web.UI.Page
    {
        IADServices aDServices;
        ICampServices campServices;
        ITransportServices transportServices;
        IManifestServices manifestServices;
        ICityServices cityServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnPrint.Visible = false;
                PrintDiv.Visible = false;
                BindDropDown();
                BindCities();

            }
        }

        protected void BindCities()
        {
            cityServices = new CityServices(new TCContext());
            var data = cityServices.GetCityDetails();
            ddlCities.DataSource = data;
            ddlCities.DataValueField = "ID";
            ddlCities.DataTextField = "CityName";
            ddlCities.DataBind();
        }

        protected void BindDropDown()
        {
            transportServices = new TransportServices(new TCContext());
            var data = transportServices.GetDetails();
            ddlTransport.DataSource = data;
            ddlTransport.DataValueField = "ID";
            ddlTransport.DataTextField = "TransportName";
            ddlTransport.DataBind();
            ddlTransport.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void Bind(DateTime from, DateTime to, Int64 id, int cID, string type)
        {
            #region declare
            int lve = 0;
            int td = 0;
            int posting = 0;
            int other = 0;
            int advparty = 0;
            int total = 0;
            int sno = 1;
            int grantTotalJco = 0;
            int grantTotalofficer = 0;
            int grantTotalOther = 0;
            int grantTotal = 0;
            int lveJcoInt = 0;
            int lveOfficerInt = 0;
            int lveOtherInt = 0;
            int lveTotalInt = 0;
            int tdJcoInt = 0;
            int tdOfficerInt = 0;
            int tdOtherInt = 0;
            int tdTotalInt = 0;
            int postingJcoInt = 0;
            int postingOfficerInt = 0;
            int postingOtherInt = 0;
            int postingTotalInt = 0;
            int advJcoInt = 0;
            int advOfficerInt = 0;
            int advOtherInt = 0;
            int advTotalInt = 0;
            int otherJcoInt = 0;
            int otherOfficerInt = 0;
            int otherOtherInt = 0;
            int otherTotalInt = 0;
            #endregion
            DataTable dt = new DataTable();
            #region dt column declare
            dt.Columns.Add("SNO");
            dt.Columns.Add("ADNO");
            dt.Columns.Add("ArmyNo");
            dt.Columns.Add("RankName");
            dt.Columns.Add("Name");
            dt.Columns.Add("UnitName");
            dt.Columns.Add("MoveName");
            dt.Columns.Add("ICard");
            dt.Columns.Add("Date");
            dt.Columns.Add("DepDate");
            dt.Columns.Add("Session");
            dt.Columns.Add("FMN");
            dt.Columns.Add("CityName");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("TransportName");
            #endregion

            aDServices = new ADServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            campServices = new CampServices(new TCContext());
            transportServices = new TransportServices(new TCContext());
            var tDetails = transportServices.GetDetails();

            if (type.ToLower().Trim() == "all")
            {
                if (tDetails != null)
                {
                    var data = manifestServices.DepartureTransportTypeReport(from, to, id, cID);

                    foreach (var res in tDetails)
                    {
                        if (data != null)
                        {


                            foreach (var r in data)
                            {
                                DataRow dr = dt.NewRow();

                                if (r.MoveID != null)
                                {
                                    if (r.MoveName.ToLower() == "lve")
                                        lve++;
                                    else if (r.MoveName.ToLower() == "td/rtu")
                                        td++;
                                    else if (r.MoveName.ToLower() == "posting")
                                        posting++;
                                    else if (r.MoveName.ToLower() == "adv party")
                                        advparty++;
                                    else
                                        other++;
                                }
                                dr["SNO"] = sno++;
                                dr["ADNO"] = r.ADNO;
                                dr["ArmyNo"] = r.ArmyNo;
                                dr["RankName"] = r.RankName;
                                dr["Name"] = r.Name;
                                dr["UnitName"] = r.UnitName;
                                dr["MoveName"] = r.MoveName;
                                dr["ICard"] = r.ICard;
                                dr["Date"] = r.ArrDate;
                                dr["DepDate"] = r.DepDate;
                                dr["Session"] = r.Session;
                                dr["FMN"] = r.DivName;
                                dr["CityName"] = r.CityName;
                                dr["CategoryName"] = r.CategoryName;
                                dr["TransportName"] = res.TransportName;
                                dt.Rows.Add(dr);
                            }
                        }

                        var finaldata = data.OrderBy(x => x.TransportName).ToList();

                        dt = ToDataTable(finaldata);
                    }

                    #region lve
                    var lveOfficer = (from p in data
                                      where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "lve"
                                      select p).ToList();
                    if (lveOfficer.Count > 0)
                    {
                        foreach (var c in lveOfficer)
                        {
                            lveOfficerInt++;
                        }
                    }

                    var lveJco = (from p in data
                                  where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "lve"
                                  select p).ToList();
                    if (lveJco.Count > 0)
                    {
                        foreach (var c in lveJco)
                        {
                            lveJcoInt++;
                        }
                    }

                    var lveOther = (from p in data
                                    where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "lve"
                                    select p).ToList();
                    if (lveOther.Count > 0)
                    {
                        foreach (var c in lveOther)
                        {
                            lveOtherInt++;
                        }
                    }


                    lveTotalInt = lveOtherInt + lveOfficerInt + lveJcoInt;
                    #endregion

                    #region td/rtu
                    var tdOfficer = (from p in data
                                     where p.CategoryName.ToLower().Contains("officer") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                                     select p).ToList();
                    if (tdOfficer.Count > 0)
                    {
                        foreach (var c in tdOfficer)
                        {
                            tdOfficerInt++;
                        }
                    }

                    var tdJco = (from p in data
                                 where p.CategoryName.ToLower().Contains("jco") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                                 select p).ToList();
                    if (tdJco.Count > 0)
                    {
                        foreach (var c in tdJco)
                        {
                            tdJcoInt++;
                        }
                    }

                    var tdOther = (from p in data
                                   where p.CategoryName.ToLower().Contains("other") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                                   select p).ToList();
                    if (tdOther.Count > 0)
                    {
                        foreach (var c in tdOther)
                        {
                            tdOtherInt++;
                        }
                    }

                    tdTotalInt = tdOtherInt + tdOfficerInt + tdJcoInt;
                    #endregion

                    #region posting
                    var postingOfficer = (from p in data
                                          where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "posting"
                                          select p).ToList();
                    if (postingOfficer.Count > 0)
                    {
                        foreach (var c in postingOfficer)
                        {
                            postingOfficerInt++;
                        }
                    }

                    var postingJco = (from p in data
                                      where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "posting"
                                      select p).ToList();
                    if (postingJco.Count > 0)
                    {
                        foreach (var c in postingJco)
                        {
                            postingJcoInt++;
                        }
                    }

                    var postingOther = (from p in data
                                        where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "posting"
                                        select p).ToList();
                    if (postingOther.Count > 0)
                    {
                        foreach (var c in postingOther)
                        {
                            postingOtherInt++;
                        }
                    }

                    postingTotalInt = postingOtherInt + postingOfficerInt + postingJcoInt;
                    #endregion

                    #region adv party
                    var advOfficer = (from p in data
                                      where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "adv party"
                                      select p).ToList();
                    if (advOfficer.Count > 0)
                    {
                        foreach (var c in advOfficer)
                        {
                            advOfficerInt++;
                        }
                    }

                    var advJco = (from p in data
                                  where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "adv party"
                                  select p).ToList();
                    if (advJco.Count > 0)
                    {
                        foreach (var c in advJco)
                        {
                            advJcoInt++;
                        }
                    }

                    var advOther = (from p in data
                                    where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "adv party"
                                    select p).ToList();
                    if (advOther.Count > 0)
                    {
                        foreach (var c in advOther)
                        {
                            advOtherInt++;
                        }
                    }

                    advTotalInt = advOtherInt + advOfficerInt + advJcoInt;
                    #endregion

                    #region other
                    var otherOfficer = (from p in data
                                        where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "other"
                                        select p).ToList();
                    if (otherOfficer.Count > 0)
                    {
                        foreach (var c in otherOfficer)
                        {
                            otherOfficerInt++;
                        }
                    }

                    var otherJco = (from p in data
                                    where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "other"
                                    select p).ToList();
                    if (otherJco.Count > 0)
                    {
                        foreach (var c in otherJco)
                        {
                            otherJcoInt++;
                        }
                    }

                    var OtherOther = (from p in data
                                      where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "other"
                                      select p).ToList();
                    if (OtherOther.Count > 0)
                    {
                        foreach (var c in OtherOther)
                        {
                            otherOtherInt++;
                        }
                    }

                    otherTotalInt = otherOtherInt + otherOfficerInt + otherJcoInt;
                    #endregion

                    #region total for all
                    grantTotalJco = lveJcoInt + tdJcoInt + postingJcoInt + advJcoInt + otherJcoInt;
                    grantTotalofficer = lveOfficerInt + tdOfficerInt + advOfficerInt + postingOfficerInt + otherOfficerInt;
                    grantTotalOther = lveOtherInt + tdOtherInt + advOtherInt + postingOtherInt + otherOtherInt;
                    grantTotal = otherTotalInt + tdTotalInt + advTotalInt + postingTotalInt + lveTotalInt;
                    #endregion
                }
            }

            else
            {
                var data = manifestServices.DepartureTransportTypeReport(from, to, id, cID);
                if (data != null)
                {
                    #region lve
                    var lveOfficer = (from p in data
                                      where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "lve"
                                      select p).ToList();
                    if (lveOfficer.Count > 0)
                    {
                        foreach (var c in lveOfficer)
                        {
                            lveOfficerInt++;
                        }
                    }

                    var lveJco = (from p in data
                                  where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "lve"
                                  select p).ToList();
                    if (lveJco.Count > 0)
                    {
                        foreach (var c in lveJco)
                        {
                            lveJcoInt++;
                        }
                    }

                    var lveOther = (from p in data
                                    where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "lve"
                                    select p).ToList();
                    if (lveOther.Count > 0)
                    {
                        foreach (var c in lveOther)
                        {
                            lveOtherInt++;
                        }
                    }


                    lveTotalInt = lveOtherInt + lveOfficerInt + lveJcoInt;
                    #endregion

                    #region td/rtu
                    var tdOfficer = (from p in data
                                     where p.CategoryName.ToLower().Contains("officer") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                                     select p).ToList();
                    if (tdOfficer.Count > 0)
                    {
                        foreach (var c in tdOfficer)
                        {
                            tdOfficerInt++;
                        }
                    }

                    var tdJco = (from p in data
                                 where p.CategoryName.ToLower().Contains("jco") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                                 select p).ToList();
                    if (tdJco.Count > 0)
                    {
                        foreach (var c in tdJco)
                        {
                            tdJcoInt++;
                        }
                    }

                    var tdOther = (from p in data
                                   where p.CategoryName.ToLower().Contains("other") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                                   select p).ToList();
                    if (tdOther.Count > 0)
                    {
                        foreach (var c in tdOther)
                        {
                            tdOtherInt++;
                        }
                    }

                    tdTotalInt = tdOtherInt + tdOfficerInt + tdJcoInt;
                    #endregion

                    #region posting
                    var postingOfficer = (from p in data
                                          where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "posting"
                                          select p).ToList();
                    if (postingOfficer.Count > 0)
                    {
                        foreach (var c in postingOfficer)
                        {
                            postingOfficerInt++;
                        }
                    }

                    var postingJco = (from p in data
                                      where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "posting"
                                      select p).ToList();
                    if (postingJco.Count > 0)
                    {
                        foreach (var c in postingJco)
                        {
                            postingJcoInt++;
                        }
                    }

                    var postingOther = (from p in data
                                        where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "posting"
                                        select p).ToList();
                    if (postingOther.Count > 0)
                    {
                        foreach (var c in postingOther)
                        {
                            postingOtherInt++;
                        }
                    }

                    postingTotalInt = postingOtherInt + postingOfficerInt + postingJcoInt;
                    #endregion

                    #region adv party
                    var advOfficer = (from p in data
                                      where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "adv party"
                                      select p).ToList();
                    if (advOfficer.Count > 0)
                    {
                        foreach (var c in advOfficer)
                        {
                            advOfficerInt++;
                        }
                    }

                    var advJco = (from p in data
                                  where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "adv party"
                                  select p).ToList();
                    if (advJco.Count > 0)
                    {
                        foreach (var c in advJco)
                        {
                            advJcoInt++;
                        }
                    }

                    var advOther = (from p in data
                                    where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "adv party"
                                    select p).ToList();
                    if (advOther.Count > 0)
                    {
                        foreach (var c in advOther)
                        {
                            advOtherInt++;
                        }
                    }

                    advTotalInt = advOtherInt + advOfficerInt + advJcoInt;
                    #endregion

                    #region other
                    var otherOfficer = (from p in data
                                        where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "other"
                                        select p).ToList();
                    if (otherOfficer.Count > 0)
                    {
                        foreach (var c in otherOfficer)
                        {
                            otherOfficerInt++;
                        }
                    }

                    var otherJco = (from p in data
                                    where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "other"
                                    select p).ToList();
                    if (otherJco.Count > 0)
                    {
                        foreach (var c in otherJco)
                        {
                            otherJcoInt++;
                        }
                    }

                    var OtherOther = (from p in data
                                      where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "other"
                                      select p).ToList();
                    if (OtherOther.Count > 0)
                    {
                        foreach (var c in OtherOther)
                        {
                            otherOtherInt++;
                        }
                    }

                    otherTotalInt = otherOtherInt + otherOfficerInt + otherJcoInt;
                    #endregion

                    #region total for all
                    grantTotalJco = lveJcoInt + tdJcoInt + postingJcoInt + advJcoInt + otherJcoInt;
                    grantTotalofficer = lveOfficerInt + tdOfficerInt + advOfficerInt + postingOfficerInt + otherOfficerInt;
                    grantTotalOther = lveOtherInt + tdOtherInt + advOtherInt + postingOtherInt + otherOtherInt;
                    grantTotal = otherTotalInt + tdTotalInt + advTotalInt + postingTotalInt + lveTotalInt;
                    #endregion

                    foreach (var r in data)
                    {
                        DataRow dr = dt.NewRow();

                        if (r.MoveID != null)
                        {
                            if (r.MoveName.ToLower() == "lve")
                                lve++;
                            else if (r.MoveName.ToLower() == "td/rtu")
                                td++;
                            else if (r.MoveName.ToLower() == "posting")
                                posting++;
                            else if (r.MoveName.ToLower() == "adv party")
                                advparty++;
                            else
                                other++;
                        }
                        dr["SNO"] = sno++;
                        dr["ADNO"] = r.ADNO;
                        dr["ArmyNo"] = r.ArmyNo;
                        dr["RankName"] = r.RankName;
                        dr["Name"] = r.Name;
                        dr["UnitName"] = r.UnitName;
                        dr["MoveName"] = r.MoveName;
                        dr["ICard"] = r.ICard;
                        dr["Date"] = r.ArrDate;
                        dr["DepDate"] = r.DepDate;
                        dr["Session"] = r.Session;
                        dr["FMN"] = r.DivName;
                        dr["CityName"] = r.CityName;
                        dr["CategoryName"] = r.CategoryName;
                        dt.Rows.Add(dr);
                    }
                }

                var finaldata = data.OrderBy(x => x.CategoryName).ToList();

                dt = ToDataTable(finaldata);

            }

            #region calculation
            lblLveJcos.Text = lveJcoInt.ToString();
            lblTDJco.Text = tdJcoInt.ToString();
            lblPJco.Text = postingJcoInt.ToString();
            lblADVJco.Text = advJcoInt.ToString();
            lblOtherJco.Text = otherJcoInt.ToString();
            lblGTJCO.Text = grantTotalJco.ToString();

            lblLveOfficer.Text = lveOfficerInt.ToString();
            lblTDOfficer.Text = tdOfficerInt.ToString();
            lblPOfficer.Text = postingOfficerInt.ToString();
            lblADVOfficer.Text = advOfficerInt.ToString();
            lblOtherOfficer.Text = otherOfficerInt.ToString();
            lblGTOfficer.Text = grantTotalofficer.ToString();

            lblLveOther.Text = lveOtherInt.ToString();
            lblTDOther.Text = tdOtherInt.ToString();
            lblPother.Text = postingOtherInt.ToString();
            lblADVOther.Text = advOtherInt.ToString();
            lblOtherOther.Text = otherOtherInt.ToString();
            lblGTOther.Text = grantTotalOther.ToString();

            lblLveTotal.Text = lveTotalInt.ToString();
            lblTDTotal.Text = tdTotalInt.ToString();
            lblPTotal.Text = postingTotalInt.ToString();
            lblADVTotal.Text = advTotalInt.ToString();
            lblOtherTotal.Text = otherTotalInt.ToString();
            lblGTotal.Text = grantTotal.ToString();
            #endregion


            var camp = campServices.GetCampDetails();
            if (camp != null)
                lblCamp.Text = camp[0].CampName;
            else
                lblCamp.Text = "Transit Camp";

            btnPrint.Visible = true;
            PrintDiv.Visible = true;
            lblFrom.Text = from.ToString("dd-MM-yyyy");
            lblTO.Text = to.ToString("dd-MM-yyyy");
            txtFrom.Text = from.ToString("dd-MM-yyyy");
            hfFromDate.Value = from.ToString();
            hfToDate.Value = to.ToString();
            txtTo.Text = to.ToString("dd-MM-yyyy");
            lblTransport.Text = ddlTransport.SelectedItem.ToString();
            grd.DataSource = dt;
            grd.DataBind();
        }

        protected void Bind(Int64 id)
        {
            #region declare
            int lve = 0;
            int td = 0;
            int posting = 0;
            int other = 0;
            int advparty = 0;
            int total = 0;
            int sno = 1;
            int grantTotalJco = 0;
            int grantTotalofficer = 0;
            int grantTotalOther = 0;
            int grantTotal = 0;
            int lveJcoInt = 0;
            int lveOfficerInt = 0;
            int lveOtherInt = 0;
            int lveTotalInt = 0;
            int tdJcoInt = 0;
            int tdOfficerInt = 0;
            int tdOtherInt = 0;
            int tdTotalInt = 0;
            int postingJcoInt = 0;
            int postingOfficerInt = 0;
            int postingOtherInt = 0;
            int postingTotalInt = 0;
            int advJcoInt = 0;
            int advOfficerInt = 0;
            int advOtherInt = 0;
            int advTotalInt = 0;
            int otherJcoInt = 0;
            int otherOfficerInt = 0;
            int otherOtherInt = 0;
            int otherTotalInt = 0;
            #endregion
            aDServices = new ADServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            campServices = new CampServices(new TCContext());
            DataTable dt = new DataTable();

            #region dt column declare
            dt.Columns.Add("SNO");
            dt.Columns.Add("ADNO");
            dt.Columns.Add("ArmyNo");
            dt.Columns.Add("RankName");
            dt.Columns.Add("Name");
            dt.Columns.Add("UnitName");
            dt.Columns.Add("MoveName");
            dt.Columns.Add("ICard");
            dt.Columns.Add("Date");
            dt.Columns.Add("Session");
            dt.Columns.Add("FMN");
            dt.Columns.Add("CityName");
            dt.Columns.Add("CategoryName");
            #endregion

            DateTime today = DateTime.Now;
            string todayday = today.Day.ToString();
            string todaymonth = today.Month.ToString();
            string todayear = today.Year.ToString();

            string finalstring = todayear + "-" + todaymonth + "-" + todayday;
            DateTime finaldate = Convert.ToDateTime(finalstring);

            var data = manifestServices.DepartureTransportTypeReportID(id, finaldate);
            if (data != null)
            {
                #region lve
                var lveOfficer = (from p in data
                                  where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "lve"
                                  select p).ToList();
                if (lveOfficer.Count > 0)
                {
                    foreach (var c in lveOfficer)
                    {
                        lveOfficerInt++;
                    }
                }

                var lveJco = (from p in data
                              where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "lve"
                              select p).ToList();
                if (lveJco.Count > 0)
                {
                    foreach (var c in lveJco)
                    {
                        lveJcoInt++;
                    }
                }

                var lveOther = (from p in data
                                where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "lve"
                                select p).ToList();
                if (lveOther.Count > 0)
                {
                    foreach (var c in lveOther)
                    {
                        lveOtherInt++;
                    }
                }


                lveTotalInt = lveOtherInt + lveOfficerInt + lveJcoInt;
                #endregion

                #region td/rtu
                var tdOfficer = (from p in data
                                 where p.CategoryName.ToLower().Contains("officer") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                                 select p).ToList();
                if (tdOfficer.Count > 0)
                {
                    foreach (var c in tdOfficer)
                    {
                        tdOfficerInt++;
                    }
                }

                var tdJco = (from p in data
                             where p.CategoryName.ToLower().Contains("jco") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                             select p).ToList();
                if (tdJco.Count > 0)
                {
                    foreach (var c in tdJco)
                    {
                        tdJcoInt++;
                    }
                }

                var tdOther = (from p in data
                               where p.CategoryName.ToLower().Contains("other") && (p.MoveName.ToLower() == "td" || p.MoveName.ToLower() == "rtu")
                               select p).ToList();
                if (tdOther.Count > 0)
                {
                    foreach (var c in tdOther)
                    {
                        tdOtherInt++;
                    }
                }

                tdTotalInt = tdOtherInt + tdOfficerInt + tdJcoInt;
                #endregion

                #region posting
                var postingOfficer = (from p in data
                                      where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "posting"
                                      select p).ToList();
                if (postingOfficer.Count > 0)
                {
                    foreach (var c in postingOfficer)
                    {
                        postingOfficerInt++;
                    }
                }

                var postingJco = (from p in data
                                  where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "posting"
                                  select p).ToList();
                if (postingJco.Count > 0)
                {
                    foreach (var c in postingJco)
                    {
                        postingJcoInt++;
                    }
                }

                var postingOther = (from p in data
                                    where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "posting"
                                    select p).ToList();
                if (postingOther.Count > 0)
                {
                    foreach (var c in postingOther)
                    {
                        postingOtherInt++;
                    }
                }

                postingTotalInt = postingOtherInt + postingOfficerInt + postingJcoInt;
                #endregion

                #region adv party
                var advOfficer = (from p in data
                                  where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "adv party"
                                  select p).ToList();
                if (advOfficer.Count > 0)
                {
                    foreach (var c in advOfficer)
                    {
                        advOfficerInt++;
                    }
                }

                var advJco = (from p in data
                              where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "adv party"
                              select p).ToList();
                if (advJco.Count > 0)
                {
                    foreach (var c in advJco)
                    {
                        advJcoInt++;
                    }
                }

                var advOther = (from p in data
                                where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "adv party"
                                select p).ToList();
                if (advOther.Count > 0)
                {
                    foreach (var c in advOther)
                    {
                        advOtherInt++;
                    }
                }

                advTotalInt = advOtherInt + advOfficerInt + advJcoInt;
                #endregion

                #region other
                var otherOfficer = (from p in data
                                    where p.CategoryName.ToLower().Contains("officer") && p.MoveName.ToLower() == "other"
                                    select p).ToList();
                if (otherOfficer.Count > 0)
                {
                    foreach (var c in otherOfficer)
                    {
                        otherOfficerInt++;
                    }
                }

                var otherJco = (from p in data
                                where p.CategoryName.ToLower().Contains("jco") && p.MoveName.ToLower() == "other"
                                select p).ToList();
                if (otherJco.Count > 0)
                {
                    foreach (var c in otherJco)
                    {
                        otherJcoInt++;
                    }
                }

                var OtherOther = (from p in data
                                  where p.CategoryName.ToLower().Contains("other") && p.MoveName.ToLower() == "other"
                                  select p).ToList();
                if (OtherOther.Count > 0)
                {
                    foreach (var c in OtherOther)
                    {
                        otherOtherInt++;
                    }
                }

                otherTotalInt = otherOtherInt + otherOfficerInt + otherJcoInt;
                #endregion

                #region total for all
                grantTotalJco = lveJcoInt + tdJcoInt + postingJcoInt + advJcoInt + otherJcoInt;
                grantTotalofficer = lveOfficerInt + tdOfficerInt + advOfficerInt + postingOfficerInt + otherOfficerInt;
                grantTotalOther = lveOtherInt + tdOtherInt + advOtherInt + postingOtherInt + otherOtherInt;
                grantTotal = otherTotalInt + tdTotalInt + advTotalInt + postingTotalInt + lveTotalInt;
                #endregion

                foreach (var r in data)
                {
                    DataRow dr = dt.NewRow();

                    if (r.MoveID != null)
                    {
                        if (r.MoveName.ToLower() == "lve")
                            lve++;
                        else if (r.MoveName.ToLower() == "td/rtu")
                            td++;
                        else if (r.MoveName.ToLower() == "posting")
                            posting++;
                        else if (r.MoveName.ToLower() == "adv party")
                            advparty++;
                        else
                            other++;
                    }
                    dr["SNO"] = sno++;
                    dr["ADNO"] = r.ADNO;
                    dr["ArmyNo"] = r.ArmyNo;
                    dr["RankName"] = r.RankName;
                    dr["Name"] = r.Name;
                    dr["UnitName"] = r.UnitName;
                    dr["MoveName"] = r.MoveName;
                    dr["ICard"] = r.ICard;
                    dr["Date"] = r.Date;
                    dr["Session"] = r.Session;
                    dr["FMN"] = r.DivName;
                    dr["CityName"] = r.CityName;
                    dr["CategoryName"] = r.CategoryName;
                    dt.Rows.Add(dr);
                }
            }

            var finaldata = data.OrderBy(x => x.CategoryName).ToList();

            dt = ToDataTable(finaldata);

            var camp = campServices.GetCampDetails();
            if (camp != null)
                lblCamp.Text = camp[0].CampName;
            else
                lblCamp.Text = "Transit Camp";


            #region calculation
            lblLveJcos.Text = lveJcoInt.ToString();
            lblTDJco.Text = tdJcoInt.ToString();
            lblPJco.Text = postingJcoInt.ToString();
            lblADVJco.Text = advJcoInt.ToString();
            lblOtherJco.Text = otherJcoInt.ToString();
            lblGTJCO.Text = grantTotalJco.ToString();

            lblLveOfficer.Text = lveOfficerInt.ToString();
            lblTDOfficer.Text = tdOfficerInt.ToString();
            lblPOfficer.Text = postingOfficerInt.ToString();
            lblADVOfficer.Text = advOfficerInt.ToString();
            lblOtherOfficer.Text = otherOfficerInt.ToString();
            lblGTOfficer.Text = grantTotalofficer.ToString();

            lblLveOther.Text = lveOtherInt.ToString();
            lblTDOther.Text = tdOtherInt.ToString();
            lblPother.Text = postingOtherInt.ToString();
            lblADVOther.Text = advOtherInt.ToString();
            lblOtherOther.Text = otherOtherInt.ToString();
            lblGTOther.Text = grantTotalOther.ToString();

            lblLveTotal.Text = lveTotalInt.ToString();
            lblTDTotal.Text = tdTotalInt.ToString();
            lblPTotal.Text = postingTotalInt.ToString();
            lblADVTotal.Text = advTotalInt.ToString();
            lblOtherTotal.Text = otherTotalInt.ToString();
            lblGTotal.Text = grantTotal.ToString();
            #endregion

            btnPrint.Visible = true;
            PrintDiv.Visible = true;
            lblFrom.Text = finaldate.ToString("dd-MM-yyyy");
            lblTO.Text = finaldate.ToString("dd-MM-yyyy");
            lblTransport.Text = ddlTransport.SelectedItem.ToString();
            grd.DataSource = dt;
            grd.DataBind();
        }

        String currentId = null;
        int subTotalRowIndex = 0;
        int jcos = 0;
        int officer = 0;
        int other = 0;
        int total = 0;

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToString(ddlTransport.SelectedItem.Text.Trim().ToLower()) == "all")
                    {
                        if (e.Row.DataItem as DataRowView != null)
                        {
                            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                            String Id = Convert.ToString(dt.Rows[e.Row.RowIndex]["TransportName"]);
                            String CatName = Convert.ToString(dt.Rows[e.Row.RowIndex]["CategoryName"]);
                            if (CatName.Trim().ToLower() == "JCOs".ToLower())
                                jcos++;
                            else if (CatName.Trim().ToLower() == "Officer".ToLower() || Id.Trim().ToLower() == "Officers".ToLower())
                                officer++;
                            else if (CatName.Trim().ToLower() == "Other".ToLower() || Id.Trim().ToLower() == "Or".ToLower() || Id.Trim().ToLower() == "Others".ToLower())
                                other++;

                            if (Id != currentId)
                            {
                                this.AddTotalRow(Id);

                                if (e.Row.RowIndex > 0)
                                {
                                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                                    {
                                    }
                                    subTotalRowIndex = e.Row.RowIndex;
                                }

                                currentId = Id;
                            }
                        }
                    }
                    else
                    {
                        if (e.Row.DataItem as DataRowView != null)
                        {
                            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                            String Id = Convert.ToString(dt.Rows[e.Row.RowIndex]["CategoryName"]);
                            if (Id.Trim().ToLower() == "JCOs".ToLower())
                                jcos++;
                            else if (Id.Trim().ToLower() == "Officer".ToLower() || Id.Trim().ToLower() == "Officers".ToLower())
                                officer++;
                            else if (Id.Trim().ToLower() == "Other".ToLower() || Id.Trim().ToLower() == "Or".ToLower() || Id.Trim().ToLower() == "Others".ToLower())
                                other++;

                            if (Id != currentId)
                            {
                                this.AddTotalRow(Id);

                                if (e.Row.RowIndex > 0)
                                {
                                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                                    {
                                    }
                                    subTotalRowIndex = e.Row.RowIndex;
                                }

                                currentId = Id;
                            }
                        }
                    }

                    total = jcos + officer + other;
                    lblJCOs.Text = jcos.ToString();
                    lblOfficer.Text = officer.ToString();
                    lblOR.Text = other.ToString();
                    lblNoOfTrans.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AddTotalRow(string cat)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
            row.Font.Bold = true;
            row.CssClass = "cat-heading";
            row.Cells.AddRange(new TableCell[12] { new TableCell { Text = cat, HorizontalAlign = HorizontalAlign.Left},
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
                                        new TableCell (),
            });

            grd.Controls[0].Controls.Add(row);
        }

        public DataTable ToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        protected void btnSearchAD_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            if (ddlTransport.SelectedValue == "")
            {
                lblError.Text = "Select From Dropdown.";
                lblError.Visible = true;
            }
            else
            {
                if (hfFromDate.Value != "" && hfToDate.Value == "")
                {
                    lblError.Text = "Select To Date";
                    lblError.Visible = true;
                    txtFrom.Text = hfFromDate.Value.ToString();
                }
                else if (hfFromDate.Value == "" && hfToDate.Value != "")
                {
                    lblError.Text = "Select From Date";
                    lblError.Visible = true;
                    txtFrom.Text = hfFromDate.Value.ToString();
                }
                else
                {
                    if (hfFromDate.Value == "" && hfToDate.Value == "" && ddlTransport.SelectedValue != "")
                    {
                        Int64 id = Convert.ToInt64(ddlTransport.SelectedValue);
                        Bind(id);
                    }
                    else
                    {
                        Int64 id = Convert.ToInt64(ddlTransport.SelectedValue);
                        int cid = Convert.ToInt32(ddlCities.SelectedValue);
                        DateTime fromdate = Convert.ToDateTime(Helper.CommonFunctions.ConvertToCreatedOnDate(hfFromDate.Value));
                        DateTime todate = Convert.ToDateTime(Helper.CommonFunctions.ConvertToCreatedOnDate(hfToDate.Value));
                        if (todate < fromdate)
                        {
                            lblError.Text = "Select Valid From-To Date.";
                            lblError.Visible = true;
                        }
                        else
                        {
                            string type = ddlTransport.SelectedItem.Text;
                            Bind(fromdate, todate, id, cid, type);
                            lblCity.Text = ddlCities.SelectedItem.Text;
                        }
                    }
                }
            }
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartureTransportWiseDateWise");
        }
    }
}