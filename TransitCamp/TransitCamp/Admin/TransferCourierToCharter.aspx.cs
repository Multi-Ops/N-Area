using BusinessLayer;
using DataAccessLayer;
using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace TransitCamp.Admin
{
    public partial class TransferCourierToCharter : System.Web.UI.Page
    {
        protected ICityServices cityServices;
        protected ICharterDetailsServices charterDetailsServices;
        protected IRankServices rankServices;
        protected IUnitServices unitServices;
        protected IDivisionServices divisionServices;
        protected IHeadquarterServices headquarterServices;
        protected IMoveServices moveServices;
        protected IPriorityServices priorityServices;
        protected IPriorityStatusServices priorityStatusServices;
        protected ITransferCourierToCharterServices transferCourierToCharter;
        protected IADServices aDServices;
        protected ICategoryServices categoryServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindADEntry();
                BindRank();
                BindDiv();
                BindUnit();
                BindCity();
                BindHQ();
                BindMove();
                BindPriority();
                BindCharterNo();
                BindPriorityStatus();
                BindCategory();
                GetDetails();
            }
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

        //bind city
        protected void BindCity()
        {
            cityServices = new CityServices(new TCContext());
            var getlist = cityServices.GetCityDetails();
            ddlCity.DataSource = getlist;
            ddlCity.DataValueField = "ID";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Unit
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

        //bind HQ
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

        //bind M,Move
        protected void BindMove()
        {
            moveServices = new MoveServices(new TCContext());
            var getlist = moveServices.GetMoveDetails();
            ddlMove.DataSource = getlist;
            ddlMove.DataValueField = "ID";
            ddlMove.DataTextField = "MoveName";
            ddlMove.DataBind();
            ddlMove.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Priority 
        protected void BindPriority()
        {
            priorityServices = new PriorityServices(new TCContext());
            var getlist = priorityServices.GetPriorityDetails();
            ddlPriority.DataSource = getlist;
            ddlPriority.DataValueField = "ID";
            ddlPriority.DataTextField = "PriorityName";
            ddlPriority.DataBind();
            ddlPriority.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Priority 
        protected void BindPriorityStatus()
        {
            priorityStatusServices = new PriorityStatusServices(new TCContext());
            var getlist = priorityStatusServices.GetPriorityStatusDetails();
            ddlPriorityStatus.DataSource = getlist;
            ddlPriorityStatus.DataValueField = "ID";
            ddlPriorityStatus.DataTextField = "PStatusName";
            ddlPriorityStatus.DataBind();
            ddlPriorityStatus.Items.Insert(0, new ListItem("-- Select --", ""));
        }


        //bind Charter No
        protected void BindCharterNo()
        {
            charterDetailsServices = new CharterDetailsServices(new TCContext());
            var getlist = charterDetailsServices.GetAllCharterNo();
            ddlCharterNo.DataSource = getlist;
            ddlCharterNo.DataValueField = "ID";
            ddlCharterNo.DataTextField = "CharterNo";
            ddlCharterNo.DataBind();
            ddlCharterNo.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Category
        protected void BindCategory()
        {
            categoryServices = new CategoryServices(new TCContext());
            var getlist = categoryServices.details();
            ddlCategory.DataSource = getlist;
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind AD Entry No
        private void BindADEntry()
        {
            aDServices = new ADServices(new TCContext());
            var data = aDServices.ADEntryNo();
            rptADEntrySearch.DataSource = data;
            rptADEntrySearch.DataBind();
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["TCID"]);
            transferCourierToCharter = new TransferCourierToCharterServices(new TCContext());
            var getdetails = transferCourierToCharter.GetByID(id);
            if (getdetails != null)
            {
                ddlCategory.SelectedValue = getdetails.CategoryID.ToString();
                ddlCity.SelectedValue = getdetails.CityID.ToString();
                ddlRank.SelectedValue = getdetails.RankID.ToString();
                ddlUnit.SelectedValue = getdetails.UnitID.ToString();
                ddlDiv.SelectedValue = getdetails.DivID.ToString();
                ddlHQ.SelectedValue = getdetails.HQID.ToString();
                ddlMove.SelectedValue = getdetails.MoveID.ToString();
                ddlPriority.SelectedValue = getdetails.PriorityID.ToString();
                ddlPriorityStatus.SelectedValue = getdetails.PriorityStatusID.ToString();
                ddlCharterNo.SelectedItem.Text = getdetails.CharterNo.ToString();
                if (getdetails.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else
                {
                    ddlSession.SelectedValue = "2";
                }
                txtDate.Text = getdetails.Date.ToString();
                hfDate.Value = getdetails.Date.ToString();
                txtTransferDate.Text = getdetails.TransferDate.ToString();
                hfTransferDate.Value = getdetails.TransferDate.ToString();
                txtFightDate.Text = getdetails.FlightDate.ToString();
                hfFlightDate.Value = getdetails.FlightDate.ToString();
                txtICard.Text = getdetails.ICardNo;
                txtArmyNo.Text = getdetails.ArmyNo;
                txtName.Text = getdetails.Name;
                txtAut.Text = getdetails.Authority;
                txtSeatNo.Text = getdetails.SeatNo.ToString();
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        protected void getADNODetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["TCADID"]);
            aDServices = new ADServices(new TCContext());
            var getdetails = aDServices.GetByID(id);
            if (getdetails != null)
            {
                ddlCategory.SelectedValue = getdetails.CategoryID.ToString();
                ddlCity.SelectedValue = getdetails.CityID.ToString();
                ddlRank.SelectedValue = getdetails.RankID.ToString();
                ddlUnit.SelectedValue = getdetails.UnitID.ToString();
                ddlDiv.SelectedValue = getdetails.DivID.ToString();
                ddlHQ.SelectedValue = getdetails.HQID.ToString();
                ddlMove.SelectedValue = getdetails.MoveID.ToString();
                ddlPriority.SelectedValue = getdetails.PriorityID.ToString();
                if (getdetails.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else
                {
                    ddlSession.SelectedValue = "2";
                }
                txtDate.Text = getdetails.Date.ToString();
                hfDate.Value = getdetails.Date.ToString();
                txtICard.Text = getdetails.ICard;
                txtArmyNo.Text = getdetails.ArmyNo;
                txtName.Text = getdetails.Name;
                txtAut.Text = getdetails.Authority;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
        }

        public string getADNODetails(Int64 ID)
        {
            aDServices = new ADServices(new TCContext());
            var getdetails = aDServices.GetByID(ID);
            BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();
            info.CategoryID = getdetails.CategoryID;
            info.ICard = getdetails.ICard;
            info.Name = getdetails.Name;
            info.CityID = getdetails.CityID;
            info.Date = getdetails.Date;
            info.Session = getdetails.Session;
            info.ArmyNo = getdetails.ArmyNo;
            info.UnitID = getdetails.UnitID;
            info.DivID = getdetails.DivID;
            info.HQID = getdetails.HQID;
            info.RankID = getdetails.RankID;
            info.Authority = getdetails.Authority;
            info.MoveID = getdetails.MoveID;
            info.PriorityID = getdetails.PriorityID;

            string jsonconvet = JsonConvert.SerializeObject(info);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", jsonconvet.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(jsonconvet);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return jsonconvet;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static void GetADNODetailsByID(Int64 ID)
        {
            TransferCourierToCharter transfer = new TransferCourierToCharter();
            transfer.getADNODetails(ID);
        }


        protected void InsertUpdate()
        {

            if (ddlCategory.SelectedValue.ToString() == "" || ddlCity.SelectedValue.ToString() == "" || ddlRank.SelectedValue.ToString() == ""
                || ddlUnit.SelectedValue.ToString() == "" || ddlDiv.SelectedValue.ToString() == "" || ddlHQ.SelectedValue.ToString() == "" || ddlSession.SelectedValue.ToString() == "0"
                || ddlMove.SelectedValue.ToString() == "" || ddlPriority.SelectedValue.ToString() == "" || ddlCharterNo.SelectedItem.Text == "" || ddlPriorityStatus.SelectedValue.ToString() == "")
            {
                lblError.Visible = true;
                lblError.Text = "Fill All Fields.";
            }
            else
            {

                transferCourierToCharter = new TransferCourierToCharterServices(new TCContext());
                Int32 id = Convert.ToInt32(Request.QueryString["TCID"]);
                BusinessLayer.TransferCourierToCharter info = new BusinessLayer.TransferCourierToCharter();
                info.CategoryID = Convert.ToInt64(ddlCategory.SelectedValue);
                info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                info.RankID = Convert.ToInt64(ddlRank.SelectedValue);
                info.Date = Convert.ToDateTime(hfDate.Value);
                info.ICardNo = txtICard.Text;
                info.ArmyNo = txtArmyNo.Text;
                info.Name = txtName.Text;
                info.UnitID = Convert.ToInt64(ddlUnit.SelectedValue);
                info.DivID = Convert.ToInt64(ddlDiv.SelectedValue);
                info.HQID = Convert.ToInt64(ddlHQ.SelectedValue);
                info.Authority = txtAut.Text;
                info.MoveID = Convert.ToInt64(ddlMove.SelectedValue);
                info.PriorityID = Convert.ToInt64(ddlPriority.SelectedValue);
                info.CharterNo = ddlCharterNo.SelectedItem.Text;
                info.SeatNo = Convert.ToInt64(txtSeatNo.Text);
                info.PriorityStatusID = Convert.ToInt64(ddlPriorityStatus.SelectedValue);
                info.Session = ddlSession.SelectedItem.Text;
                info.TransferDate = Convert.ToDateTime(hfTransferDate.Value);
                info.FlightDate = Convert.ToDateTime(hfFlightDate.Value);

                if (id != 0)
                {
                    info.ID = Convert.ToInt32(Request.QueryString["TCID"]);
                    info.UpdatedOn = DateTime.Now;
                    transferCourierToCharter.Update(info);
                    transferCourierToCharter.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Updated!');window.location = 'TransferCourierToCharterList';", true);
                }
                else
                {
                    info.CreatedOn = DateTime.Now;
                    transferCourierToCharter.Insert(info);
                    transferCourierToCharter.Save();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'TransferCourierToCharterList';", true);
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