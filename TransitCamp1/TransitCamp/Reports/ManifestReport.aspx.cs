using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DataAccessLayer;

namespace TransitCamp.Reports
{
    public partial class ManifestReport : System.Web.UI.Page
    {
        IManifestServices manifestServices;
        ICategoryServices categoryServices;
        ICampServices campServices;
        ICityServices cityServices;
        ITransportDetailsServices transportDetailsServices;
        ISignatureServices signatureServices;


        protected void Page_Load(object sender, EventArgs e)
        {
            var ManifestNo = Request.QueryString["ManifestNo"];
            if (ManifestNo != null)
            {
                BindGridCat(ManifestNo);
            }
        }

        protected void BindGridCat(string ManifestNo)
        {
            //get category IDs
            categoryServices = new CategoryServices(new TCContext());
            manifestServices = new ManifestServices(new TCContext());
            campServices = new CampServices(new TCContext());
            cityServices = new CityServices(new TCContext());
            transportDetailsServices = new TransportDetailsServices(new TCContext());
            signatureServices = new SignatureServices(new TCContext());

            var sign = signatureServices.GetDetails();
            var CategoryIDs = categoryServices.details();
            if (CategoryIDs != null)
            {
                string CatID = Convert.ToString(CategoryIDs[0].ID);
                if (CatID != "")
                {
                    var data = manifestServices.PagingManifest(ManifestNo, Convert.ToInt64(CatID));
                    var campname = campServices.GetCampDetails();

                    if (data.Count != 0)
                    {
                        var city = cityServices.GetCityID(Convert.ToInt64(data[0].CityID));
                        var transportdetails = transportDetailsServices.GetDetailsByID(Convert.ToInt64(data[0].TransportDetailID));

                        if (campname.Count != 0)
                            lblCamp.Text = campname[0].CampName.ToUpper();

                        if (city != null)
                            lblCity.Text = city.CityName.ToUpper();

                        if (transportdetails != null)
                            lblDate.Text = transportdetails.Date.ToString();

                        lblManifestNo.Text = data[0].MenifestNo;
                        lblTransportDetails.Text = data[0].TransportDetails;
                        lblcat.Text = data[0].CategoryName;
                        lblRptRank.Text = sign.RankName;
                        lblRptName.Text = sign.SignatureName;
                        lblRptDate.Text = DateTime.Now.ToString("dd MMMM yyyy");

                        grdManifestReport.DataSource = data;
                        grdManifestReport.DataBind();
                    }
                    else
                    {
                        PrintDiv.Visible = false;
                        btnPrint.Visible = false;
                    }
                }

                string CatID1 = Convert.ToString(CategoryIDs[1].ID);
                if (CatID1 != "")
                {
                    var data = manifestServices.PagingManifest(ManifestNo, Convert.ToInt64(CatID1));
                    var campname = campServices.GetCampDetails();
                    if (data.Count != 0)
                    {
                        var city = cityServices.GetCityID(Convert.ToInt64(data[0].CityID));
                        var transportdetails = transportDetailsServices.GetDetailsByID(Convert.ToInt64(data[0].TransportDetailID));

                        if (campname.Count != 0)
                            lblCamp1.Text = campname[0].CampName.ToUpper();

                        if (city != null)
                            lblCity1.Text = city.CityName.ToUpper();

                        if (transportdetails != null)
                            lblDate1.Text = transportdetails.Date.ToString();

                        lblManifestNo1.Text = data[0].MenifestNo;
                        lblTransportDetails1.Text = data[0].TransportDetails;
                        lblCat1.Text = data[0].CategoryName;

                        lblRptRank1.Text = sign.RankName;
                        lblRptName1.Text = sign.SignatureName;
                        lblRptDate1.Text = DateTime.Now.ToString("dd MMMM yyyy");

                        grdCat1.DataSource = data;
                        grdCat1.DataBind();
                    }
                    else
                    {
                        Print2.Visible = false;
                        Button1.Visible = false;
                    }
                }

                string CatID2 = Convert.ToString(CategoryIDs[2].ID);
                if (CatID2 != "")
                {
                    var data = manifestServices.PagingManifest(ManifestNo, Convert.ToInt64(CatID2));
                    var campname = campServices.GetCampDetails();
                    if (data.Count != 0)
                    {
                        var city = cityServices.GetCityID(Convert.ToInt64(data[0].CityID));
                        var transportdetails = transportDetailsServices.GetDetailsByID(Convert.ToInt64(data[0].TransportDetailID));

                        if (campname.Count != 0)
                            lblCamp2.Text = campname[0].CampName.ToUpper();

                        if (city != null)
                            lblCity2.Text = city.CityName.ToUpper();

                        if (transportdetails != null)
                            lblDate2.Text = transportdetails.Date.ToString();

                        lblManifestNo2.Text = data[0].MenifestNo;
                        lblTransportDetails2.Text = data[0].TransportDetails;
                        lblCat3.Text = data[0].CategoryName;

                        lblRptRank2.Text = sign.RankName;
                        lblRptName2.Text = sign.SignatureName;
                        lblRptDate2.Text = DateTime.Now.ToString("dd MMMM yyyy");


                        grdCat2.DataSource = data;
                        grdCat2.DataBind();
                    }
                    else
                    {
                        Print3.Visible = false;
                        Button2.Visible = false;
                    }
                }
            }

        }

        protected void btnPrintCombine_Click(object sender, EventArgs e)
        {
            var ManifestNo = Request.QueryString["ManifestNo"];
            if (ManifestNo.ToString() != "")
                Response.Redirect("ManifestCombineReport?ManifestNo=" + ManifestNo + "");
        }
    }
}