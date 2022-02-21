using DataAccessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.Reports
{
    public partial class RationReport : System.Web.UI.Page
    {
        ICityServices cityServices;
        IADServices aDServices;
        ICampServices campServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCities();
                BindData();
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

        protected void btnSearchAD_Click(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            #region Declare
            int bfOff = 0;
            int bfjco = 0;
            int bfor = 0;

            int bfoffNAFN = 0;
            int bfjcoNAFN = 0;
            int bforNAFN = 0;

            int bfoffNAAN = 0;
            int bfjcoNAAN = 0;
            int bforNAAN = 0;

            int bfoffNDFN = 0;
            int bfjcoNDFN = 0;
            int bforNDFN = 0;

            int bfoffNDAN = 0;
            int bfjcoNDAN = 0;
            int bforNDAN = 0;

            int airofc = 0;
            int busofc = 0;
            int trainofc = 0;
            int cancelofc = 0;
            int blcofc = 0;

            int airjco = 0;
            int busjco = 0;
            int trainjco = 0;
            int canceljco = 0;
            int blcjco = 0;

            int airor = 0;
            int busor = 0;
            int trainor = 0;
            int cancelor = 0;
            int blcor = 0;

            #endregion

            aDServices = new ADServices(new TCContext());
            campServices = new CampServices(new TCContext());
            var data = aDServices.GetAllADsForRation(Convert.ToInt32(ddlCities.SelectedValue));
            var todaysDep = aDServices.GetAllADsForRationBF(Convert.ToInt32(ddlCities.SelectedValue));
            if (data.Count > 0)
            {
                DateTime cdate = DateTime.Now;

                #region BF Category
                var offc = (from p in data
                            where p.CategoryName.ToLower().Contains("off") && (p.Date.Value.Date <= cdate.Date.AddDays(-1)) && p.IsManifest == false
                            select p).ToList();
                if (offc.Count > 0)
                {
                    if (todaysDep.Count > 0)
                    {
                        var offcT = (from p in todaysDep
                                     where p.CategoryName.ToLower().Contains("off")
                                     select p).ToList();
                        if (offcT.Count > 0)
                        {
                            bfOff = offc.Count + offcT.Count;
                        }
                    }
                    else
                    {
                        bfOff = offc.Count;
                    }
                }

                var jco = (from p in data
                           where p.CategoryName.ToLower().Contains("jc") && (p.Date.Value.Date <= cdate.Date.AddDays(-1)) && p.IsManifest == false
                           select p).ToList();
                if (jco.Count > 0)
                {
                    if (todaysDep.Count > 0)
                    {
                        var jcoT = (from p in todaysDep
                                    where p.CategoryName.ToLower().Contains("jc")
                                    select p).ToList();
                        if (jcoT.Count > 0)
                        {
                            bfjco = jco.Count + jcoT.Count;
                        }
                    }
                    else
                        bfjco = jco.Count;
                }

                var or = (from p in data
                          where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.IsManifest == false && (p.Date.Value.Date <= cdate.Date.AddDays(-1))
                          select p).ToList();
                if (or.Count > 0)
                {
                    if (todaysDep.Count > 0)
                    {
                        var bforT = (from p in todaysDep
                                     where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other")
                                     select p).ToList();
                        if (bforT.Count > 0)
                        {
                            bfor = or.Count + bforT.Count;
                        }
                    }
                    else
                        bfor = or.Count;
                }

                int totalBF = bfOff + bfor + bfjco;
                lblBFTotalOffc.Text = bfOff.ToString();
                lblBFTotalJCO.Text = bfjco.ToString();
                lblBFTotalOthers.Text = bfor.ToString();
                lblBFTotal.Text = totalBF.ToString();
                #endregion

                #region BF New Arrival FN
                var offcNAFN = (from p in data
                                where p.CategoryName.ToLower().Contains("of") && p.Session.ToLower() == "fn" && (p.Date.Value.Date == cdate.Date)
                                select p).ToList();
                if (offcNAFN.Count > 0)
                {
                    bfoffNAFN = offcNAFN.Count;
                }

                var jcoNAFN = (from p in data
                               where p.CategoryName.ToLower().Contains("jc") && p.Session.ToLower() == "fn" && (p.Date.Value.Date == cdate.Date)
                               select p).ToList();
                if (jcoNAFN.Count > 0)
                {
                    bfjcoNAFN = jcoNAFN.Count;
                }

                var orNAFN = (from p in data
                              where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.Session.ToLower() == "fn" && (p.Date.Value.Date == cdate.Date)
                              select p).ToList();
                if (orNAFN.Count > 0)
                {
                    bforNAFN = orNAFN.Count;
                }

                int totalBFAFN = bfoffNAFN + bforNAFN + bfjcoNAFN;
                newArrivalOFFCFN.Text = bfoffNAFN.ToString();
                newArrivalJCOFN.Text = bfjcoNAFN.ToString();
                newArrivalORFN.Text = bforNAFN.ToString();
                lblArrivalFNTotal.Text = totalBFAFN.ToString();
                #endregion

                #region BF New Arrival AN
                var offcNAAN = (from p in data
                                where p.CategoryName.ToLower().Contains("of") && p.Session.ToLower() == "an" && (p.Date.Value.Date == cdate.Date)
                                select p).ToList();
                if (offcNAAN.Count > 0)
                {
                    bfoffNAAN = offcNAAN.Count;
                }

                var jcoNAAN = (from p in data
                               where p.CategoryName.ToLower().Contains("jc") && p.Session.ToLower() == "an" && (p.Date.Value.Date == cdate.Date)
                               select p).ToList();
                if (jcoNAAN.Count > 0)
                {
                    bfjcoNAAN = jcoNAAN.Count;
                }

                var orNAAN = (from p in data
                              where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.Session.ToLower() == "an" && (p.Date.Value.Date == cdate.Date)
                              select p).ToList();
                if (orNAAN.Count > 0)
                {
                    bforNAAN = orNAAN.Count;
                }

                int totalBFAAN = bfoffNAAN + bforNAAN + bfjcoNAAN;
                newArrivalOFFCAN.Text = bfoffNAAN.ToString();
                newArrivalJCOAN.Text = bfjcoNAAN.ToString();
                newArrivalORCAN.Text = bforNAAN.ToString();
                lblArrivalANTotal.Text = totalBFAAN.ToString();
                #endregion

                var dataDep = aDServices.GetAllADsForRationDep(Convert.ToInt32(ddlCities.SelectedValue));

                if (dataDep.Count > 0)
                {
                    #region BF New Dep FN
                    var offcNDFN = (from p in dataDep
                                    where p.CategoryName.ToLower().Contains("of") && p.Session.ToLower() == "fn" && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                    select p).ToList();
                    if (offcNDFN.Count > 0)
                    {
                        bfoffNDFN = offcNDFN.Count;
                    }

                    var jcoNDFN = (from p in dataDep
                                   where p.CategoryName.ToLower().Contains("jc") && p.Session.ToLower() == "fn" && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                   select p).ToList();
                    if (jcoNDFN.Count > 0)
                    {
                        bfjcoNDFN = jcoNDFN.Count;
                    }

                    var orNDFN = (from p in dataDep
                                  where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.Session.ToLower() == "fn" && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                  select p).ToList();
                    if (orNDFN.Count > 0)
                    {
                        bforNDFN = orNDFN.Count;
                    }

                    int totalBFDFN = bfoffNDFN + bforNDFN + bfjcoNDFN;
                    departureOFFCFN.Text = bfoffNDFN.ToString();
                    departureJCOFN.Text = bfjcoNDFN.ToString();
                    departureORCFN.Text = bforNDFN.ToString();
                    lblDepFNTotal.Text = totalBFDFN.ToString();
                    #endregion

                    #region BF New Dep AN
                    var offcNDAN = (from p in dataDep
                                    where p.CategoryName.ToLower().Contains("of") && p.Session.ToLower() == "an" && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                    select p).ToList();
                    if (offcNDAN.Count > 0)
                    {
                        bfoffNDAN = offcNDAN.Count;
                    }

                    var jcoNDAN = (from p in dataDep
                                   where p.CategoryName.ToLower().Contains("jc") && p.Session.ToLower() == "an" && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                   select p).ToList();
                    if (jcoNDAN.Count > 0)
                    {
                        bfjcoNDAN = jcoNDAN.Count;
                    }

                    var orNDAN = (from p in dataDep
                                  where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.Session.ToLower() == "an" && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                  select p).ToList();
                    if (orNDAN.Count > 0)
                    {
                        bforNDAN = orNDAN.Count;
                    }

                    int totalBFDAN = bfoffNDAN + bforNDAN + bfjcoNDAN;
                    departureOFFCAN.Text = bfoffNDAN.ToString();
                    departureJCOAN.Text = bfjcoNDAN.ToString();
                    departureORCAN.Text = bforNDAN.ToString();
                    lblDepANTotal.Text = totalBFDAN.ToString();
                    #endregion

                    #region Balance CF
                    int CFTotalOfficer = (bfOff + bfoffNAFN + bfoffNAAN) - (bfoffNDFN + bfoffNDAN);
                    int CFTotalJCO = (bfjco + bfjcoNAFN + bfjcoNAAN) - (bfjcoNDFN + bfjcoNDAN);
                    int CFTotalOR = (bfor + bforNAFN + bforNAAN) - (bforNDFN + bforNDAN);

                    int CGTotal = CFTotalOfficer + CFTotalJCO + CFTotalOR;

                    lblofcCF.Text = CFTotalOfficer.ToString();
                    lbljcoCF.Text = CFTotalJCO.ToString();
                    lblorCF.Text = CFTotalOR.ToString();
                    lbltotalCF.Text = CGTotal.ToString();
                    #endregion

                    #region Dep
                    lblDepoffcBF.Text = bfOff.ToString();
                    lblBFJCO.Text = bfjco.ToString();
                    lblORBF.Text = bfor.ToString();
                    lblDepTotalBF.Text = totalBF.ToString();

                    int depofc = bfoffNAFN + bfoffNAAN;
                    lblDepoffcBFNewArrival.Text = depofc.ToString();

                    int depjco = bfjcoNAFN + bfjcoNAAN;
                    lblBFJCONewArrival.Text = depjco.ToString();

                    int depor = bforNAFN + bforNAAN;
                    lblORBFNewArrival.Text = depor.ToString();

                    int deptotal = depofc + depjco + depor;
                    lblDepTotalBFNewArrival.Text = depor.ToString();
                    #endregion

                    #region TransportWise
                    var ofcair = (from p in dataDep
                                  where p.CategoryName.ToLower().Contains("of") && p.TransportName.ToLower().Contains("air") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                  select p).ToList();
                    if (ofcair.Count > 0)
                    {
                        airofc = ofcair.Count;
                    }
                    var ofctrain = (from p in dataDep
                                    where p.CategoryName.ToLower().Contains("of") && p.TransportName.ToLower().Contains("train") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                    select p).ToList();
                    if (ofctrain.Count > 0)
                    {
                        trainofc = ofctrain.Count;
                    }
                    var ofcbus = (from p in dataDep
                                  where p.CategoryName.ToLower().Contains("of") && p.TransportName.ToLower().Contains("bus") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                  select p).ToList();
                    if (ofcbus.Count > 0)
                    {
                        busofc = ofcbus.Count;
                    }

                    //jco
                    var jcocair = (from p in dataDep
                                   where p.CategoryName.ToLower().Contains("jc") && p.TransportName.ToLower().Contains("air") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                   select p).ToList();
                    if (jcocair.Count > 0)
                    {
                        airjco = jcocair.Count;
                    }
                    var jcoctrain = (from p in dataDep
                                     where p.CategoryName.ToLower().Contains("jc") && p.TransportName.ToLower().Contains("train") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                     select p).ToList();
                    if (jcoctrain.Count > 0)
                    {
                        trainjco = jcoctrain.Count;
                    }
                    var jcocbus = (from p in dataDep
                                   where p.CategoryName.ToLower().Contains("jc") && p.TransportName.ToLower().Contains("bus") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                   select p).ToList();
                    if (jcocbus.Count > 0)
                    {
                        busjco = jcocbus.Count;
                    }

                    //or
                    var orcair = (from p in dataDep
                                  where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.TransportName.ToLower().Contains("air") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                  select p).ToList();
                    if (orcair.Count > 0)
                    {
                        airor = orcair.Count;
                    }
                    var orctrain = (from p in dataDep
                                    where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.TransportName.ToLower().Contains("train") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                    select p).ToList();
                    if (orctrain.Count > 0)
                    {
                        trainor = orctrain.Count;
                    }
                    var orcbus = (from p in dataDep
                                  where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && p.TransportName.ToLower().Contains("bus") && (p.MDate.Value.Date == cdate.Date) && p.IsManifest == true
                                  select p).ToList();
                    if (orcbus.Count > 0)
                    {
                        busor = orcbus.Count;
                    }

                    lblDepoffcBFAir.Text = airofc.ToString();
                    lblDepoffcBFBus.Text = busofc.ToString();
                    lblDepoffcBFTrain.Text = trainofc.ToString();

                    lblBFJCODepAir.Text = airjco.ToString();
                    lblBFJCODepTrain.Text = trainjco.ToString();
                    lblBFJCODepBus.Text = trainjco.ToString();

                    lblORBFAir.Text = airor.ToString();
                    lblORBFTrain.Text = trainor.ToString();
                    lblORBFBus.Text = trainor.ToString();

                    int airtotal = airofc + airjco + airor;
                    int bustotal = busofc + busjco + busor;
                    int traintotal = trainofc + trainjco + trainor;

                    lblDepTotalBFAir.Text = airtotal.ToString();
                    lblDepTotalBFBus.Text = bustotal.ToString();
                    lblDepTotalBFTrain.Text = traintotal.ToString();

                    #endregion

                    #region cancel
                    var offcancel = (from p in data
                                     where p.CategoryName.ToLower().Contains("off") && (p.Date.Value.Date <= cdate.Date) && p.IsManifest == false && p.IsOnHold == true
                                     select p).ToList();
                    if (offcancel.Count > 0)
                    {
                        cancelofc = offcancel.Count;
                    }

                    var jcocancel = (from p in data
                                     where p.CategoryName.ToLower().Contains("jc") && (p.Date.Value.Date <= cdate.Date) && p.IsManifest == false && p.IsOnHold == true
                                     select p).ToList();
                    if (jcocancel.Count > 0)
                    {
                        canceljco = jcocancel.Count;
                    }

                    var orcancel = (from p in data
                                    where p.CategoryName.ToLower().Contains("or") || p.CategoryName.ToLower().Contains("other") && (p.Date.Value.Date <= cdate.Date) && p.IsManifest == false && p.IsOnHold == true
                                    select p).ToList();
                    if (orcancel.Count > 0)
                    {
                        cancelor = orcancel.Count;
                    }

                    lblDepoffcBFLve.Text = cancelofc.ToString();
                    lblBFJCODepLVE.Text = canceljco.ToString();
                    lblORBFLVE.Text = cancelor.ToString();

                    int canceltotal = cancelofc + canceljco + cancelor;
                    lblDepTotalBFLVE.Text = canceltotal.ToString();
                    #endregion

                    #region balance
                    blcofc = (bfOff + depofc) - (airofc + busofc + trainofc + cancelofc);
                    blcjco = (bfjco + depjco) - (airjco + busjco + trainjco + canceljco);
                    blcor = (bfor + depor) - (airor + busor + trainor + cancelor);


                    lblBlnOffc.Text = blcofc.ToString();
                    lblBFJCODepBalance.Text = blcjco.ToString();
                    lblORBFBalance.Text = blcor.ToString();

                    int totalblc = blcjco + blcofc + blcor;
                    lblDepTotalBFBalance.Text = totalblc.ToString();
                    #endregion

                    #region str
                    blcBF.Text = totalBF.ToString();

                    lblstrArrFN.Text = totalBFAFN.ToString();
                    int perFNArr80 = totalBFAFN * 80 / 100;
                    Math.Round(Convert.ToDouble(perFNArr80));
                    lblstrArrFNFinal.Text = perFNArr80.ToString();

                    lblstrArrAN.Text = totalBFAAN.ToString();
                    int perFNArr40 = totalBFAAN * 40 / 100;
                    Math.Round(Convert.ToDouble(perFNArr40));
                    lblstrArrANFinal.Text = perFNArr40.ToString();

                    lblstrDepFN.Text = totalBFDFN.ToString();
                    int perFDep80 = totalBFDFN * 20 / 100;
                    Math.Round(Convert.ToDouble(perFDep80));
                    lblstrDepFNFinal.Text = perFDep80.ToString();

                    lblstrDepAN.Text = totalBFDAN.ToString();
                    int perFDep40 = totalBFDAN * 60 / 100;
                    Math.Round(Convert.ToDouble(perFDep40));
                    lblstrDepANFinal.Text = perFDep40.ToString();


                    int finaltoaldtring = ((perFNArr80 + perFNArr40) - (perFDep80 + perFDep40));
                    int fstr = finaltoaldtring + totalBF;
                    lblFstr.Text = fstr.ToString();

                    lblBalanceDep.Text = lblDepTotalBFBalance.Text;
                    #endregion

                }


            }
            lblDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            lblDepDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            lblDepFinalDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            lblSummaryDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");
            lblCity.Text = ddlCities.SelectedItem.Text;
            var camp = campServices.GetCampDetails();
            if (camp != null)
                lblUnit.Text = camp[0].CampName;
        }
    }
}