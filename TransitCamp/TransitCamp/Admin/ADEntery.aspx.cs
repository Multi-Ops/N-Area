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
using System.Threading;

namespace TransitCamp.Admin
{
    public partial class ADEntery : System.Web.UI.Page
    {
        protected ICityServices cityServices;
        protected ICharterDetailsServices charterDetailsServices;
        protected IRankServices rankServices;
        protected IUnitServices unitServices;
        protected IDivisionServices divisionServices;
        protected IHeadquarterServices headquarterServices;
        protected IMoveServices moveServices;
        protected IPriorityServices priorityServices;
        protected IADTypeServices aDTypeServices;
        protected IADServices aDServices;
        protected ICategoryServices categoryServices;
        protected IUserServices userServices;
        protected IMedicalStatusServices medicalStatusServices;
        protected IBookingServices bookingServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRank();
                BindDiv();
                BindBrig();
                BindUnit();
                BindCity();
                BindHQ();
                BindMove();
                BindPriority();
                BindADType();
                BindCategory();
                GetDetails();
                BindADEntry();
                BindMedicalStatus();
                HookOnFocus(this.Page as Control);
                SetInputFocus();
                checkADno();
                Page.ClientScript.RegisterStartupScript(typeof(ADEntery), "ScriptDoFocus", SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request["__LASTFOCUS"]), true);
            }
        }

        public void SetInputFocus()
        {
            TextBox tbox = this.txtArmyNo.FindControl("txtIcard") as TextBox;
            if (tbox != null)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(tbox);
            }
        }

        private void checkADno()
        {
            aDServices = new ADServices(new TCContext());
            var getupdatedetails = aDServices.GetADDetails();
            string lehJco = "";
            string KashJco = "";
            string lehOR = "";
            string kashOR = "";
            string lehOfc = "";
            string kashOfc = "";

            if (!string.IsNullOrEmpty(getupdatedetails.Count.ToString()))
            {
                var getLehAds = (from p in getupdatedetails
                                 orderby p.ID descending
                                 where p.CityName.ToLower().Contains("leh")
                                 select p).ToList();
                var getKasAds = (from p in getupdatedetails
                                 orderby p.ID descending
                                 where p.CityName.ToLower().Contains("thoise")
                                 select p).ToList();
                if (!string.IsNullOrEmpty(getLehAds.Count.ToString()))
                {
                    var lehOfficer = (from p in getLehAds
                                      orderby p.ID descending
                                      where p.CategoryName.ToLower().Contains("officer")
                                      select p).FirstOrDefault();
                    if (lehOfficer != null)
                    {
                        lehOfc = lehOfficer.ADNO;
                        lehOfc = splitstring(lehOfc);
                    }

                    var lehJCO = (from p in getLehAds
                                  orderby p.ID descending
                                  where p.CategoryName.ToLower().Contains("jc")
                                  select p).FirstOrDefault();
                    if (lehJCO != null)
                    {
                        lehJco = lehJCO.ADNO;
                        lehJco = splitstring(lehJco);
                    }

                    var lehOther = (from p in getLehAds
                                    orderby p.ID descending
                                    where p.CategoryName.ToLower().Contains("ot") || p.CategoryName.ToLower().Contains("or")
                                    select p).FirstOrDefault();
                    if (lehOther != null)
                    {
                        lehOR = lehOther.ADNO;
                        lehOR = splitstring(lehOR);
                    }
                }
                if (!string.IsNullOrEmpty(getKasAds.Count.ToString()))
                {
                    var kashOfficer = (from p in getKasAds
                                       orderby p.ID descending
                                       where p.CategoryName.ToLower().Contains("officer")
                                       select p).FirstOrDefault();
                    if (kashOfficer != null)
                    {
                        kashOfc = kashOfficer.ADNO;
                        kashOfc = splitstring(kashOfc);
                    }

                    var kashJCO = (from p in getKasAds
                                   orderby p.ID descending
                                   where p.CategoryName.ToLower().Contains("jc")
                                   select p).FirstOrDefault();
                    if (kashJCO != null)
                    {
                        KashJco = kashJCO.ADNO;
                        KashJco = splitstring(KashJco);
                    }

                    var kashOther = (from p in getKasAds
                                     orderby p.ID descending
                                     where p.CategoryName.ToLower().Contains("ot") || p.CategoryName.ToLower().Contains("or")
                                     select p).FirstOrDefault();
                    if (kashOther != null)
                    {
                        kashOR = kashOther.ADNO;
                        kashOR = splitstring(kashOR);
                    }
                }

                if (string.IsNullOrEmpty(lehOfc) && string.IsNullOrEmpty(lehOR) && string.IsNullOrEmpty(lehJco) && string.IsNullOrEmpty(kashOR) && string.IsNullOrEmpty(kashOfc) && string.IsNullOrEmpty(KashJco))
                    lblCurrentAD.Text = "";
                else
                {
                    if (string.IsNullOrEmpty(lehOfc) && string.IsNullOrEmpty(lehOR) && string.IsNullOrEmpty(lehJco))
                    {

                        lblCurrentAD.Text = "Thoise:" + kashOfc + " " + KashJco + " " + kashOR + "";
                    }
                    if (string.IsNullOrEmpty(kashOR) && string.IsNullOrEmpty(kashOfc) && string.IsNullOrEmpty(KashJco))
                    {
                        lblCurrentAD.Text = "LEH:" + lehOfc + " " + lehJco + " " + lehOR + " ";
                    }
                    if (!string.IsNullOrEmpty(lehOfc) || !string.IsNullOrEmpty(lehOR) || !string.IsNullOrEmpty(lehJco) || !string.IsNullOrEmpty(kashOR) || !string.IsNullOrEmpty(kashOfc) || !string.IsNullOrEmpty(KashJco))
                    {
                        string stringLeh = "LEH:" + lehOfc + " " + lehJco + " " + lehOR;
                        string stringKash = "Thoise:" + kashOfc + " " + KashJco + " " + kashOR;
                        if (string.IsNullOrEmpty(lehOfc) && string.IsNullOrEmpty(lehOR) && string.IsNullOrEmpty(lehJco))
                            lblCurrentAD.Text = "";
                        else
                            lblCurrentAD.Text = stringLeh;

                        lblCurrentADKash.Text = stringKash;
                    }
                }
            }
        }

        private string splitstring(string stringVal)
        {
            string finalstring = "";
            string[] splitAD = stringVal.Split(' ');
            string splitOne = splitAD[0].ToString();
            string splitTwo = splitAD[1].ToString();
            int splitTwoInt = Convert.ToInt32(splitTwo);
            int final = splitTwoInt++;
            if (final <= 9)
                finalstring = splitOne + " " + "0" + splitTwoInt.ToString();
            else
                finalstring = splitOne + " " + splitTwoInt.ToString();

            return finalstring;
        }

        private void HookOnFocus(Control CurrentControl)
        {
            //checks if control is one of TextBox, DropDownList, ListBox or Button
            if ((CurrentControl is TextBox) ||
                (CurrentControl is DropDownList) ||
                (CurrentControl is ListBox) ||
                (CurrentControl is Button))
                //adds a script which saves active control on receiving focus 
                //in the hidden field __LASTFOCUS.
                (CurrentControl as WebControl).Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id} catch (e) { }");
            //checks if the control has children
            if (CurrentControl.HasControls())
                //if yes do them all recursively
                foreach (Control CurrentChildControl in CurrentControl.Controls)
                    HookOnFocus(CurrentChildControl);
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// This script sets a focus to the control with a name to which
        /// REQUEST_LASTFOCUS was replaced. Setting focus heppens after the page
        /// (or update panel) was rendered. To delay setting focus the function
        /// window.setTimeout() will be used.
        /// <span class="code-SummaryComment"></summary></span>
        private const string SCRIPT_DOFOCUS =
            @"window.setTimeout('DoFocus()', 1);
    function DoFocus()
    {
        try {
            document.getElementById('REQUEST_LASTFOCUS').focus();
        } catch (ex) {}
    }";

        //bind Rank by category
        protected void BindRank()
        {
            ddlRank.DataSource = null;
            ddlRank.DataValueField = null;
            ddlRank.DataTextField = null;
            ddlRank.DataBind();


            rankServices = new RankServices(new TCContext());
            var getlist = rankServices.GetRankDetails();
            ddlRank.DataSource = getlist;
            ddlRank.DataValueField = "ID";
            ddlRank.DataTextField = "Rank";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Medical Status
        protected void BindMedicalStatus()
        {
            medicalStatusServices = new MedicalStatusServices(new TCContext());
            var getlist = medicalStatusServices.getmeddetails();
            ddlMedicalStatus.DataSource = getlist;
            ddlMedicalStatus.DataValueField = "ID";
            ddlMedicalStatus.DataTextField = "MedicalStatusName";
            ddlMedicalStatus.DataBind();
            ddlMedicalStatus.Items.Insert(0, new ListItem("-- Select --", ""));
        }

        //bind Rank by category
        protected void BindRankByCat()
        {
            string CatID = ddlCategory.SelectedValue.ToString();
            if (CatID != "")
            {
                BindRank();
                rankServices = new RankServices(new TCContext());
                var getlist = rankServices.GetRankDetailsByCatID(Convert.ToInt64(CatID));
                ddlRank.DataSource = getlist;
                ddlRank.DataValueField = "ID";
                ddlRank.DataTextField = "Rank";
                ddlRank.DataBind();
                ddlRank.Items.Insert(0, new ListItem("-- Select --", ""));
            }

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
            ddlCity.Items.Insert(0, new ListItem("City", ""));
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

        //bind Brigade
        protected void BindBrig()
        {
            divisionServices = new DivisionServices(new TCContext());
            var getlist = divisionServices.GetBrigDetails();
            ddlBrigade.DataSource = getlist;
            ddlBrigade.DataValueField = "Id";
            ddlBrigade.DataTextField = "Name";
            ddlBrigade.DataBind();
            ddlBrigade.Items.Insert(0, new ListItem("-- Select --", ""));
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

        //bind ADType 
        protected void BindADType()
        {
            aDTypeServices = new ADTypeServices(new TCContext());
            var getlist = aDTypeServices.GetADTypeDetails();
            ddlADType.DataSource = getlist;
            ddlADType.DataValueField = "ID";
            ddlADType.DataTextField = "ADTypeName";
            ddlADType.DataBind();
            ddlADType.Items.Insert(0, new ListItem("-- Select --", ""));
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
            ddlCategory.Items.Insert(0, new ListItem("Category", ""));
        }

        //bind AD Entry No
        private void BindADEntry()
        {
            aDServices = new ADServices(new TCContext());
            var data = aDServices.ADEntryNo();
            data = (from p in data
                    where p.IsTempHold == false && p.CheckOutDate.ToString() == string.Empty
                    orderby p.ID descending
                    select p).ToList();

            rptADEntrySearch.DataSource = data;
            rptADEntrySearch.DataBind();
        }

        protected void GetDetails()
        {
            Int32 id = Convert.ToInt32(Request.QueryString["ADID"]);
            aDServices = new ADServices(new TCContext());
            medicalStatusServices = new MedicalStatusServices(new TCContext());

            var getdetails = aDServices.GetByID(id);
            if (getdetails != null)
            {
                btnUpdate.Visible = true;
                btnSave.Visible = false;
                chkIsPriority.Checked = false;
                divRptrADNo.Visible = false;
                chkIsFly.Visible = true;

                if (getdetails.IsFly == true)
                    chkIsFly.Checked = true;
                else
                    chkIsFly.Checked = false;

                //temporary hold for 24 hours
                //if (getdetails.IsTempHold == true)
                //{
                //    chkIsPriority.Visible = false;
                //    if (getdetails.UpdatedOn == null)
                //    {
                //        DateTime dbDate = Convert.ToDateTime(getdetails.CreatedOn);
                //        if (DateTime.Now > dbDate.AddDays(1))
                //        {
                //            getdetails.ID = id;
                //            getdetails.IsTempHold = false;
                //            aDServices.Update(getdetails);
                //            aDServices.Save();
                //        }
                //    }
                //    else
                //    {
                //        DateTime dbDate = Convert.ToDateTime(getdetails.UpdatedOn);
                //        if (DateTime.Now > dbDate.AddDays(1))
                //        {
                //            getdetails.ID = id;
                //            getdetails.IsTempHold = false;
                //            aDServices.Update(getdetails);
                //            aDServices.Save();
                //        }
                //    }
                //}

                ddlCategory.SelectedValue = getdetails.CategoryID.ToString();
                ddlCity.SelectedValue = getdetails.CityID.ToString();
                ddlRank.SelectedValue = getdetails.RankID.ToString();
                ddlUnit.SelectedValue = getdetails.UnitID.ToString();
                ddlDiv.SelectedValue = getdetails.DivID.ToString();
                ddlHQ.SelectedValue = getdetails.HQID.ToString();
                ddlBrigade.SelectedValue = getdetails.BrigadeID.ToString();
                ddlMove.SelectedValue = getdetails.MoveID.ToString();
                ddlPriority.SelectedValue = getdetails.PriorityID.ToString();
                ddlADType.SelectedValue = getdetails.AdTypeID.ToString();
                txtOnHoldRemark.Text = getdetails.OnHoldRemark.ToString();
                txtOnTempHoldRemark.Text = getdetails.OnTemHoldRemark.ToString();

                if (getdetails.Session == "FN")
                {
                    ddlSession.SelectedValue = "1";
                }
                else
                {
                    ddlSession.SelectedValue = "2";
                }
                DateTime tDate = Convert.ToDateTime(getdetails.Date);
                DateTime fromLeaveDate = Convert.ToDateTime(getdetails.LeaveFromDate);
                DateTime tOLeaveDate = Convert.ToDateTime(getdetails.LeaveToDate);

                txtDate.Text = tDate.ToString("dd-MM-yyyy - hh:mm tt");
                hfDate.Value = tDate.ToString("yyyy-MM-dd HH:mm:ss");

                txtLeaveFromDate.Text = fromLeaveDate.ToString("dd MMMM yyyy - hh:mm tt");
                hfLeaveFromDate.Value = fromLeaveDate.ToString("yyyy-MM-dd HH:mm:ss");

                txtLeaveToDate.Text = tOLeaveDate.ToString("dd MMMM yyyy - hh:mm tt");
                hfLeaveToDate.Value = tOLeaveDate.ToString("yyyy-MM-dd HH:mm:ss");

                txtICard.Text = getdetails.ICard;
                txtArmyNo.Text = getdetails.ArmyNo;
                txtName.Text = getdetails.Name;
                txtAut.Text = getdetails.Authority;
                txtBP.Text = getdetails.BP;
                txtFMN.Text = getdetails.FMN;
                txtLeaveNoOfDays.Text = getdetails.LeaveNoOfDays.ToString();
                txtState.Text = getdetails.StateName;
                if (getdetails.MedicalStatusID != null)
                {
                    ddlMedicalStatus.SelectedValue = getdetails.MedicalStatusID.ToString();
                }
                if (getdetails.IsOnHold == true)
                {
                    chkOnHoldStatus.Checked = true;
                }
                else
                {
                    chkOnHoldStatus.Checked = false;
                }
                if (getdetails.IsLoad == false)
                    chkIsLoad.Checked = false;
                else
                    chkIsLoad.Checked = true;

                if (getdetails.IsTempHold == true)
                {
                    chkTemporaryHold.Attributes.Add("style", "display:none");
                    chkOnHoldStatus.Attributes.Add("style", "display:none");
                    lblError.Visible = true;
                    lblError.Attributes.Add("style", "display:block");
                    lblError.Text = "On Temporary Hold.";
                    btnUpdate.Attributes.Add("style", "display:none");
                    txtArmyNo.Enabled = false;
                    txtAut.Enabled = false;
                    txtBP.Enabled = false;
                    txtDate.Enabled = false;
                    txtICard.Enabled = false;
                    txtName.Enabled = false;
                    ddlADType.Enabled = false;
                    ddlCategory.Enabled = false;
                    ddlCity.Enabled = false;
                    ddlDiv.Enabled = false;
                    ddlHQ.Enabled = false;
                    ddlMove.Enabled = false;
                    ddlPriority.Enabled = false;
                    ddlRank.Enabled = false;
                    ddlSession.Enabled = false;
                    ddlUnit.Enabled = false;
                    ddlPriority.Visible = false;
                }
                else
                {
                    chkTemporaryHold.Checked = false;
                }

                if (getdetails.IsPriority == true)
                {
                    chkIsPriority.Checked = true;
                }
                else
                {
                    chkIsPriority.Checked = false;
                }
            }
            else
            {
                var getlist = medicalStatusServices.getmeddetails();
                string shape = "shape-1";
                if (getlist != null)
                    foreach (var res in getlist)
                    {
                        if (res.MedicalStatusName.ToLower().Trim() == shape.ToLower().Trim())
                        {
                            ddlMedicalStatus.SelectedValue = res.ID.ToString();
                            break;
                        }
                    }
                DateTime nowDate = DateTime.Now;
                txtDate.Text = nowDate.ToString("dd-MM-yyyy - hh:mm tt");
                hfDate.Value = nowDate.ToString("yyyy-MM-dd HH:mm:ss");
                if (nowDate.Hour >= 12)
                {
                    ddlSession.SelectedValue = "2";
                }
                else
                {
                    ddlSession.SelectedValue = "1";
                }
            }
        }

        public string getADNODetails(Int64 ID)
        {
            aDServices = new ADServices(new TCContext());
            var getdetails = aDServices.GetByID(ID);
            BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();

            //temporary hold for 24 hours
            if (getdetails.IsTempHold == true)
            {
                if (getdetails.UpdatedOn == null)
                {
                    DateTime dbDate = Convert.ToDateTime(getdetails.CreatedOn);
                    if (DateTime.Now > dbDate.AddDays(1))
                    {
                        getdetails.ID = ID;
                        getdetails.IsTempHold = false;
                        aDServices.Update(getdetails);
                        aDServices.Save();
                    }
                }
                else
                {
                    DateTime dbDate = Convert.ToDateTime(getdetails.UpdatedOn);
                    if (DateTime.Now > dbDate.AddDays(1))
                    {
                        getdetails.ID = ID;
                        getdetails.IsTempHold = false;
                        aDServices.Update(getdetails);
                    }
                }
            }
            info.CategoryID = getdetails.CategoryID;
            info.ICard = getdetails.ICard;
            info.Name = getdetails.Name;
            info.CityID = getdetails.CityID;
            info.Date = getdetails.Date;
            if (getdetails.Session == "FN")
            {
                info.SessionID = 1;
            }
            else
            {
                info.SessionID = 2;
            }

            info.ArmyNo = getdetails.ArmyNo;
            info.UnitID = getdetails.UnitID;
            info.DivID = getdetails.DivID;
            info.BrigadeID = getdetails.BrigadeID;
            info.HQID = getdetails.HQID;
            info.RankID = getdetails.RankID;
            info.Authority = getdetails.Authority;
            info.MoveID = getdetails.MoveID;
            info.PriorityID = getdetails.PriorityID;
            info.AdTypeID = getdetails.AdTypeID;
            info.BP = getdetails.BP;
            info.IsTempHold = getdetails.IsTempHold;
            info.IsPriority = getdetails.IsPriority;
            info.LeaveFromDate = getdetails.LeaveFromDate;
            info.LeaveToDate = getdetails.LeaveToDate;
            info.LeaveNoOfDays = getdetails.LeaveNoOfDays;
            info.FMN = getdetails.FMN;
            info.NoOfAbsentDays = getdetails.NoOfAbsentDays;
            info.IsLoad = getdetails.IsLoad;
            info.MedicalStatusID = getdetails.MedicalStatusID;

            string jsonconvet = JsonConvert.SerializeObject(info);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", jsonconvet.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(jsonconvet);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return jsonconvet;
        }

        public object GetName(string Name)
        {
            userServices = new UserServices(new TCContext());
            List<Users> user = new List<Users>();
            user = userServices.AutoCompleteName(Name);
            string jsonconvet = JsonConvert.SerializeObject(user);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", jsonconvet.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(jsonconvet);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return jsonconvet;
        }

        public object GetICard(string ICard)
        {
            userServices = new UserServices(new TCContext());
            List<Users> user = new List<Users>();
            user = userServices.AutoCompleteIcard(ICard);
            string jsonconvet = JsonConvert.SerializeObject(user);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", jsonconvet.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(jsonconvet);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return jsonconvet;
        }

        public object GetArmyNo(string ArmyNo)
        {
            userServices = new UserServices(new TCContext());
            List<Users> user = new List<Users>();
            user = userServices.AutoCompleteArmyNo(ArmyNo);
            string jsonconvet = JsonConvert.SerializeObject(user);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", jsonconvet.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(jsonconvet);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return jsonconvet;
        }

        protected void clear()
        {
            ddlCategory.SelectedValue = "";
            ddlCity.SelectedValue = "";
            txtICard.Text = "";
            txtArmyNo.Text = "";
            txtName.Text = "";
            ddlRank.SelectedValue = "";
            ddlUnit.SelectedValue = "";
            ddlDiv.SelectedValue = "";
            ddlHQ.SelectedValue = "";
            txtAut.Text = "";
            ddlMove.SelectedValue = "";
            ddlPriority.SelectedValue = "";
            ddlADType.SelectedValue = "";
            txtFMN.Text = "";
            txtBP.Text = "";
            txtLeaveNoOfDays.Text = "";
            hfLeaveFromDate.Value = "";
            txtLeaveToDate.Text = "";
            hfLeaveToDate.Value = "";
            chkIsPriority.Checked = false;
            chkOnHoldStatus.Checked = false;
            chkTemporaryHold.Checked = false;
            Session["HFID"] = null;
            Session.Clear();
            Session.Abandon();
        }

        protected void InsertUpdate()
        {
            try
            {
                if (ddlCategory.SelectedValue.ToString() == "" || ddlCity.SelectedValue.ToString() == "" || ddlRank.SelectedValue.ToString() == ""
              || ddlUnit.SelectedValue.ToString() == "" || ddlDiv.SelectedValue.ToString() == "" || ddlHQ.SelectedValue.ToString() == "" || ddlSession.SelectedValue.ToString() == "0")
                {
                    lblError.Visible = true;
                    lblError.Text = "Fill Mandatory Dropdowns.";
                }
                else
                {
                    Int64 id;
                    aDServices = new ADServices(new TCContext());
                    userServices = new UserServices(new TCContext());
                    userServices = new UserServices(new TCContext());
                    cityServices = new CityServices(new TCContext());
                    categoryServices = new CategoryServices(new TCContext());
                    bookingServices = new BookingServices(new TCContext());

                    if (Convert.ToString(Session["HFID"]) == string.Empty)
                        id = Convert.ToInt32(Request.QueryString["ADID"]);
                    else
                        id = Convert.ToInt64(Session["HFID"]);

                    BusinessLayer.ADEntery info = new BusinessLayer.ADEntery();
                    if (fuReference.HasFile)
                    {
                        string str = fuReference.FileName;
                        fuReference.PostedFile.SaveAs(Server.MapPath("~/Content/Docs/" + str));
                        string Image = "~/Content/Docs/" + str.ToString();
                        info.DocumentUrl = Image;
                    }

                    if (chkIsPriority.Checked == true)
                        info.IsPriority = true;
                    else
                        info.IsPriority = false;

                    info.CategoryID = Convert.ToInt64(ddlCategory.SelectedValue);
                    info.CityID = Convert.ToInt64(ddlCity.SelectedValue);
                    info.RankID = Convert.ToInt64(ddlRank.SelectedValue);
                    info.Date = Convert.ToDateTime(hfDate.Value);
                    info.ICard = txtICard.Text;
                    info.ArmyNo = txtArmyNo.Text;
                    info.Name = txtName.Text;
                    info.UnitID = Convert.ToInt64(ddlUnit.SelectedValue);
                    info.DivID = Convert.ToInt64(ddlDiv.SelectedValue);
                    info.HQID = Convert.ToInt64(ddlHQ.SelectedValue);
                    info.BrigadeID = Convert.ToInt64(ddlBrigade.SelectedValue);
                    info.Authority = txtAut.Text;
                    info.OnTemHoldRemark = txtOnTempHoldRemark.Text;
                    info.OnHoldRemark = txtOnHoldRemark.Text;

                    if (ddlMove.SelectedValue.ToString() != "")
                        info.MoveID = Convert.ToInt64(ddlMove.SelectedValue);
                    else
                    {
                        moveServices = new MoveServices(new TCContext());
                        var getmovetype = moveServices.GetMoveDetails();
                        if (getmovetype != null)
                        {
                            foreach (var res in getmovetype)
                            {
                                if (res.MoveName.ToLower() == "other" || res.MoveName.ToLower() == "or" || res.MoveName.ToLower() == "others")
                                {
                                    Int64 ID = res.ID;
                                    info.MoveID = ID;
                                    break;
                                }
                            }
                        }
                    }

                    if (ddlPriority.SelectedValue.ToString() != "")
                        info.PriorityID = Convert.ToInt64(ddlPriority.SelectedValue);

                    if (ddlADType.SelectedValue.ToString() != "")
                        info.AdTypeID = Convert.ToInt64(ddlADType.SelectedValue);
                    else
                    {
                        aDTypeServices = new ADTypeServices(new TCContext());
                        var getadtype = aDTypeServices.GetADTypeDetails();
                        if (getadtype != null)
                        {
                            foreach (var res in getadtype)
                            {
                                if (res.ADTypeName.ToLower() == "green")
                                {
                                    Int64 ID = res.ID;
                                    info.AdTypeID = ID;
                                    break;
                                }
                            }
                        }
                    }

                    info.BP = txtBP.Text;
                    info.FMN = txtFMN.Text;
                    if (txtLeaveFromDate.Text != "")
                        info.LeaveFromDate = Convert.ToDateTime(txtLeaveFromDate.Text);

                    if (hfLeaveToDate.Value != "")
                        info.LeaveToDate = Convert.ToDateTime(hfLeaveToDate.Value);

                    if (txtLeaveNoOfDays.Text != "")
                        info.LeaveNoOfDays = Convert.ToInt32(txtLeaveNoOfDays.Text);


                    if (hfLblAbsentDays.Value != "")
                    {
                        if (Convert.ToInt32(hfLblAbsentDays.Value) > 0)
                            info.NoOfAbsentDays = Convert.ToInt32(hfLblAbsentDays.Value);
                    }

                    info.Session = ddlSession.SelectedItem.Text;
                    info.IsManifest = false;
                    //Insert Into Users
                    Users users = new Users();
                    users.ArmyNumber = info.ArmyNo;
                    users.IDCardNo = info.ICard;
                    users.RankID = info.RankID;
                    users.Name = info.Name;
                    users.UnitID = info.UnitID;
                    users.DivID = info.DivID;
                    users.HQID = info.HQID;
                    users.CategoryID = info.CategoryID;

                    if (chkOnHoldStatus.Checked == true)
                        info.IsOnHold = true;
                    else
                        info.IsOnHold = false;
                    if (chkTemporaryHold.Checked == true)
                        info.IsTempHold = true;
                    else
                        info.IsTempHold = false;

                    if (chkOnHoldStatus.Checked == true)
                        info.IsOnHold = true;
                    else
                        info.IsOnHold = false;

                    if (chkIsLoad.Checked == false)
                        info.IsLoad = false;
                    else
                        info.IsLoad = true;

                    if (id != 0)
                    {
                        var getbyid = aDServices.GetByID(id);
                        info = getbyid;
                        info.ADNO = getbyid.ADNO;
                        info.ID = id;
                        info.IsReserve = getbyid.IsReserve;


                        if (info.UpdatedOn != null)
                        {
                            info.UpdatedOn = getbyid.UpdatedOn;
                        }
                        else
                        {
                            if (chkOnHoldStatus.Checked == true)
                                info.UpdatedOn = DateTime.Now;
                            else if (chkTemporaryHold.Checked == true)
                                info.UpdatedOn = DateTime.Now;
                        }

                        if (chkOnHoldStatus.Checked == false && chkTemporaryHold.Checked == false)
                        {
                            info.UpdatedOn = null;
                        }

                        if (chkIsFly.Checked == true)
                            info.IsFly = true;
                        else
                            info.IsFly = false;


                        users.ADID = id;
                        users.UpdatedOn = DateTime.Now;
                        userServices.Update(users);
                        userServices.Save();
                        aDServices.Update(info);
                        aDServices.Save();
                        clear();

                        //check booking
                        var checkbooking = bookingServices.GetBookingByADID(id);
                        if (checkbooking == null)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'AddBooking?ID=" + id + "';", true);
                        }
                        else
                        {
                            Response.Redirect("ADEntery");
                        }
                        //Response.Redirect("ADEntery");
                    }
                    else
                    {
                        var catVal = ddlCategory.SelectedValue;
                        var cityVal = ddlCity.SelectedValue;
                        string cityString = new string(ddlCity.SelectedItem.Text.Take(1).ToArray());
                        var getupdatedetails = aDServices.GetADNO(Convert.ToInt64(catVal), Convert.ToInt64(cityVal), cityString.Trim());

                        //get category and city 
                        var city = cityServices.GetCityIDNoJoin(Convert.ToInt64(cityVal));
                        var cat = categoryServices.GetByID(Convert.ToInt64(catVal));
                        if (getupdatedetails != null)
                            if (getupdatedetails.CategoryID == Convert.ToInt32(catVal) && getupdatedetails.CityID == Convert.ToInt32(cityVal))
                            {
                                if (city != null && cat != null && getupdatedetails == null)
                                {
                                    //string catName = new string(cat.CategoryName.Take(2).ToArray());
                                    //string cityName = new string(city.CityName.Take(1).ToArray());
                                    string catName = new string(ddlCategory.SelectedItem.Text.Take(2).ToArray());
                                    string cityName = new string(ddlCity.SelectedItem.Text.Take(1).ToArray());
                                    if (catName.ToLower().Trim().Contains("ot") || catName.ToLower().Trim().Contains("or") || catName.ToLower().Trim().Contains("other"))
                                        info.ADNO = "OR/" + cityName.ToUpper() + " 01";
                                    else if (catName.ToLower().Trim().Contains("of") || catName.ToLower().Trim().Contains("officer"))
                                        info.ADNO = "OFFR/" + cityName.ToUpper() + " 01";
                                    else if (catName.ToLower().Trim().Contains("jc") || catName.ToLower().Trim().Contains("jco"))
                                        info.ADNO = "JC/" + cityName.ToUpper() + " 01";
                                    else
                                        info.ADNO = "" + catName.ToUpper() + "/" + cityName.ToUpper() + " 01";
                                }
                                if (city != null && cat != null && getupdatedetails != null)
                                {
                                    int cityID = Convert.ToInt32(ddlCity.SelectedValue);
                                    string cityStr = new string(ddlCity.SelectedItem.Text.Take(1).ToArray());

                                    //var updateADNO = aDServices.GetADNO(Convert.ToInt64(ddlCategory.SelectedValue), cityID, cityStr.Trim());
                                    var updateADNO = aDServices.GetADNO(cityStr.Trim(), Convert.ToInt32(catVal));
                                    if (updateADNO != null)
                                    {
                                        string rowADNO = updateADNO.ADNO;
                                        string[] subs = rowADNO.Split(' ');
                                        string sub1 = subs[0].ToString();
                                        int sub2 = Convert.ToInt32(subs[1]);
                                        int add = sub2 + 1;
                                        //string addstrings;

                                        if (add <= 9)
                                        {
                                            string catName = new string(ddlCategory.SelectedItem.Text.Take(2).ToArray());
                                            string cityName = new string(ddlCity.SelectedItem.Text.Take(1).ToArray());
                                            if (catName.ToLower().Trim().Contains("ot") || catName.ToLower().Trim().Contains("or") || catName.ToLower().Trim().Contains("other"))
                                                info.ADNO = "OR/" + cityName.ToUpper() + " 0" + "" + add + "";
                                            else if (catName.ToLower().Trim().Contains("of") || catName.ToLower().Trim().Contains("officer"))
                                                info.ADNO = "OFFR/" + cityName.ToUpper() + " 0" + "" + add + "";
                                            else if (catName.ToLower().Trim().Contains("jc") || catName.ToLower().Trim().Contains("jco"))
                                                info.ADNO = "JC/" + cityName.ToUpper() + " 0" + "" + add + "";
                                            else
                                                info.ADNO = "" + catName.ToUpper() + "/" + cityName.ToUpper() + " 0" + "" + add + "";

                                            //addstrings = sub1 + " " + "0" + add + "";
                                            //info.ADNO = addstrings;
                                        }
                                        else
                                        {
                                            string catName = new string(ddlCategory.SelectedItem.Text.Take(2).ToArray());
                                            string cityName = new string(ddlCity.SelectedItem.Text.Take(1).ToArray());
                                            if (catName.ToLower().Trim().Contains("ot") || catName.ToLower().Trim().Contains("or") || catName.ToLower().Trim().Contains("other"))
                                                info.ADNO = "OR/" + cityName.ToUpper() + " " + "" + add + "";
                                            else if (catName.ToLower().Trim().Contains("of") || catName.ToLower().Trim().Contains("officer"))
                                                info.ADNO = "OFFR/" + cityName.ToUpper() + " " + "" + add + "";
                                            else if (catName.ToLower().Trim().Contains("jc") || catName.ToLower().Trim().Contains("jco"))
                                                info.ADNO = "JC/" + cityName.ToUpper() + " " + "" + add + "";
                                            else
                                                info.ADNO = "" + catName.ToUpper() + "/" + cityName.ToUpper() + " " + "" + add + "";

                                            //addstrings = sub1 + " " + "" + add + "";
                                            //info.ADNO = addstrings;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.Text = "AD No Issue!";
                                return;
                            }
                        else
                        {
                            if (city != null && cat != null && getupdatedetails == null)
                            {
                                //string catName = new string(cat.CategoryName.Take(2).ToArray());
                                //string cityName = new string(city.CityName.Take(1).ToArray());
                                string catName = new string(ddlCategory.SelectedItem.Text.Take(2).ToArray());
                                string cityName = new string(ddlCity.SelectedItem.Text.Take(1).ToArray());
                                if (catName.ToLower().Trim().Contains("ot") || catName.ToLower().Trim().Contains("or") || catName.ToLower().Trim().Contains("other"))
                                    info.ADNO = "OR/" + cityName.ToUpper() + " 01";
                                else if (catName.ToLower().Trim().Contains("of") || catName.ToLower().Trim().Contains("officer"))
                                    info.ADNO = "OFFR/" + cityName.ToUpper() + " 01";
                                else if (catName.ToLower().Trim().Contains("jc") || catName.ToLower().Trim().Contains("jco"))
                                    info.ADNO = "JC/" + cityName.ToUpper() + " 01";
                                else
                                    info.ADNO = "" + catName.ToUpper() + "/" + cityName.ToUpper() + " 01";
                            }

                        }

                        if (chkOnHoldStatus.Checked == true)
                            info.UpdatedOn = DateTime.Now;
                        else if (chkTemporaryHold.Checked == true)
                            info.UpdatedOn = DateTime.Now;

                        if (chkOnHoldStatus.Checked == false && chkTemporaryHold.Checked == false)
                        {
                            info.UpdatedOn = null;
                        }

                        info.IsReserve = false;
                        info.CreatedOn = DateTime.Now;
                        info.IsFly = false;
                        info.IsLRC = false;

                        Int64 ADID = aDServices.Insert(info);
                        users.CreatedOn = DateTime.Now;
                        users.ADID = ADID;
                        userServices.Insert(users);
                        userServices.Save();
                        clear();

                        var checkbooking = bookingServices.GetBookingByADID(id);
                        if (checkbooking == null)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Saved!');window.location = 'AddBooking?ID=" + ADID + "';", true);
                        }
                        else
                        {
                            Response.Redirect("ADEntery");
                        }
                        //Response.Redirect("ADEntery");
                    }
                }
                Session["HFID"] = string.Empty;
                Session.Clear();
                Session.Abandon();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfID.Value != "")
            {
                Session["HFID"] = hfID.Value;
                InsertUpdate();
            }
            else
            {
                InsertUpdate();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            InsertUpdate();
        }

        protected void txtICard_TextChanged(object sender, EventArgs e)
        {
            userServices = new UserServices(new TCContext());
            var getbyICard = userServices.GetUserByICard(txtICard.Text.Trim());
            if (getbyICard != null)
            {
                txtArmyNo.Text = getbyICard.ArmyNumber;
                txtName.Text = getbyICard.Name;
                ddlCategory.SelectedValue = getbyICard.CategoryID.ToString();
                ddlCity.SelectedValue = getbyICard.CityID.ToString();
                BindRankByCat();
                ddlRank.SelectedValue = getbyICard.RankID.ToString();
                ddlDiv.SelectedValue = getbyICard.DivID.ToString();
                ddlHQ.SelectedValue = getbyICard.HQID.ToString();
                ddlUnit.SelectedValue = getbyICard.UnitID.ToString();
                ddlBrigade.SelectedValue = getbyICard.BrigadeID.ToString();
            }
            else
            {
                txtName.Text = "";
                ddlRank.SelectedValue = "";
                ddlDiv.SelectedValue = "";
                ddlHQ.SelectedValue = "";
                ddlUnit.SelectedValue = "";
                ddlBrigade.SelectedValue = "";
            }
            TextBox tbox = this.txtName.FindControl("txtArmyNo") as TextBox;
            if (tbox != null)
                ScriptManager.GetCurrent(this.Page).SetFocus(tbox);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLeaveNoOfDays.Text = "";
            string catID = ddlCategory.SelectedValue.ToString();
            if (catID != "")
            {
                BindRankByCat();
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CityID = ddlCity.SelectedValue.ToString();
            if (CityID == "")
            {
                txtState.Text = "";
                ddlMedicalStatus.SelectedValue = "";
            }
            else
            {
                cityServices = new CityServices(new TCContext());
                var data = cityServices.GetCityID(Convert.ToInt64(CityID));
                txtState.Text = data.StateName;
                ddlMedicalStatus.SelectedValue = data.MedicalStatusID.ToString();
            }
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLeaveNoOfDays.Text = "";
            unitServices = new UnitServices(new TCContext());
            string unitID = ddlUnit.SelectedValue.ToString();
            if (unitID != "")
            {
                var data = unitServices.GetDIVHQByUnitID(Convert.ToInt64(unitID));
                if (data != null)
                {
                    ddlDiv.SelectedValue = data.DivID.ToString();
                    ddlHQ.SelectedValue = data.HQID.ToString();
                    ddlCity.SelectedValue = data.CityID.ToString();
                    ddlBrigade.SelectedValue = data.BrigadeID.ToString();
                }
            }
            else
            {
                ddlDiv.SelectedValue = "";
                ddlHQ.SelectedValue = "";
                ddlCity.SelectedValue = "";
            }

            DropDownList ddl = this.ddlMove.FindControl("ddlMove") as DropDownList;
            if (ddl != null)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(ddl);
            }
        }

        protected void txtArmyNo_TextChanged(object sender, EventArgs e)
        {
            userServices = new UserServices(new TCContext());
            categoryServices = new CategoryServices(new TCContext());
            var getbyICard = userServices.GetUserByArmyNo(txtArmyNo.Text.Trim());
            if (getbyICard != null)
            {
                BindRank();
                txtICard.Text = getbyICard.IDCardNo;
                txtName.Text = getbyICard.Name;
                ddlRank.SelectedValue = getbyICard.RankID.ToString();
                ddlDiv.SelectedValue = getbyICard.DivID.ToString();
                ddlHQ.SelectedValue = getbyICard.HQID.ToString();
                ddlUnit.SelectedValue = getbyICard.UnitID.ToString();
                ddlCategory.SelectedValue = getbyICard.CategoryID.ToString();
                ddlCity.SelectedValue = getbyICard.CityID.ToString();
            }
            else
            {
                string JC = txtArmyNo.Text.Trim().ToLower();
                var catids = categoryServices.details();

                if (JC.Contains("jc") == true)
                {
                    if (catids != null)
                    {
                        foreach (var catres in catids)
                        {
                            if (catres.CategoryName.ToLower().Contains("jco"))
                            {
                                ddlCategory.SelectedValue = catres.ID.ToString();
                                BindRankByCat();
                                break;
                            }
                        }
                    }
                }

                else if (JC.ToLower().Contains("off") == true)
                {
                    if (catids != null)
                    {
                        foreach (var catres in catids)
                        {
                            if (catres.CategoryName.ToLower().Contains("off"))
                            {
                                ddlCategory.SelectedValue = catres.ID.ToString();
                                BindRankByCat();
                                break;
                            }
                        }
                    }
                }

                else
                {
                    if (catids != null)
                    {
                        foreach (var catres in catids)
                        {
                            if (catres.CategoryName.ToLower().Contains("off") || catres.CategoryName.ToLower().Contains("offr") || catres.CategoryName.ToLower().Contains("officer"))
                            {
                                ddlCategory.SelectedValue = catres.ID.ToString();
                                BindRankByCat();
                                break;
                            }
                        }
                    }
                }

                txtName.Text = "";
                ddlRank.SelectedValue = "";
                ddlDiv.SelectedValue = "";
                ddlHQ.SelectedValue = "";
                ddlUnit.SelectedValue = "";
                ddlCity.SelectedValue = "";

                DropDownList dl = this.ddlRank.FindControl("ddlRank") as DropDownList;
                dl.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "Focus();", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("ADEntery");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static void GetADNODetailsByID(Int64 ID)
        {
            ADEntery aDEntery = new ADEntery();
            aDEntery.getADNODetails(ID);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static void GetNameAutocomplete(string Info)
        {
            ADEntery aDEntery = new ADEntery();
            aDEntery.GetName(Info);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static void GetIcardAutocomplete(string Info)
        {
            ADEntery aDEntery = new ADEntery();
            aDEntery.GetICard(Info);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static void GetArmyNoAutocomplete(string Info)
        {
            ADEntery aDEntery = new ADEntery();
            aDEntery.GetArmyNo(Info);
        }

        protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox txtName = this.txtName.FindControl("txtName") as TextBox;
            if (txtName != null)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(txtName);
            }
        }
    }
}