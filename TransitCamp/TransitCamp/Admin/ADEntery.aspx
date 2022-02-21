<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ADEntery.aspx.cs" Inherits="TransitCamp.Admin.ADEntery" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .chosen-container:focus-within {
            border-radius: 8px;
            box-shadow: 1px 3px 3px 3px !important;
        }

        .custom-ddl {
            border: none !important;
            width: 18%;
            text-align: left;
            margin: 0px 5px 0 0;
            float: left;
        }

        .custom-label {
            font-size: 16px;
            font-weight: bold;
            float: right;
            text-align: left;
            color: #198419;
        }

        .chosen-container-single .chosen-single span {
            margin-right: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <asp:ScriptManager ID="scriptmanager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="upAdentry">
                <ContentTemplate>
                    <div id="divRptrADNo" runat="server" class="mr-2 w-25 bg-white row float-left form-bg-image d-flex align-items-center mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-2 p-lg-2">
                        <div class="p-lg-4">
                            <h1 class="mb-0 h4 text-center text-md-center mt-1 p-3">Entry No</h1>
                            <a href="ADList">
                                <h5 class="mb-0 h6 text-center text-md-center mb-3" style="color: #4a5073;">List</h5>
                            </a>
                            <input name="SearchADEntry" type="text" id="myInput" onkeyup="myFunction()" class="form-control" placeholder="Search">
                            <div class="mt-2 mb-2"></div>
                            <div class="mb-3 background-search" style="height: 285px !important">
                                <ul id="myUL">
                                    <asp:Repeater runat="server" ID="rptADEntrySearch">
                                        <ItemTemplate>
                                            <li class="li-list"><a class="search-list" id='<%# Eval("ID") %>'>
                                                <div class="ml-3"><%# Eval("ADNO") %></div>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-center form-bg-image">
                        <div class="col-12 d-flex align-items-center justify-content-center">
                            <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-800" id="ADForm">
                                <div class="text-center text-md-center mb-4 mt-md-0">

                                    <div class="custom-headings">
                                        <div class="row">
                                            <div class="ddl-width">
                                                <%--<label for="email">City</label>--%>
                                                <span class="icon-arrange custom-ddl">
                                                    <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control border-none" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                                                </span>
                                                <asp:HiddenField runat="server" ID="hfID" />
                                                <%--<label for="email">Category</label>--%>
                                                <span class="icon-arrange custom-ddl">
                                                    <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" CssClass="form-control border-none">
                                                    </asp:DropDownList>
                                                </span>
                                                <div class="custom-label">
                                                    <asp:Label runat="server" ID="lblCurrentAD">
                                                    </asp:Label>
                                                    <br />
                                                    <asp:Label runat="server" ID="lblCurrentADKash">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                            <h1 class="mb-0 h3 custome-heading">Entry
                                            </h1>
                                        </div>
                                    </div>
                                </div>
                                <form action="#">
                                    <div class="row">
                                        <div class="col">
                                            <label for="email">I Card No</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtICard" CssClass="form-control border-none" placeholder="I card No" AutoPostBack="true" OnTextChanged="txtICard_TextChanged"></asp:TextBox>
                                            </span>
                                            <%--<asp:RequiredFieldValidator ControlToValidate="txtICard" runat="server" Display="Dynamic" ID="rfv1" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col">
                                            <label for="email">Army No</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtArmyNo" CssClass="form-control border-none" placeholder="Army No" OnTextChanged="txtArmyNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </span>
                                            <%--<asp:RequiredFieldValidator ControlToValidate="txtArmyNo" runat="server" Display="Dynamic" ID="RequiredFieldValidator1" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col">
                                            <label for="email">Rank</label>
                                            <span class="icon-arrange border-none">
                                                <asp:DropDownList runat="server" ID="ddlRank" CssClass="form-control border-none" AutoPostBack="true" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">Name</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control border-none" placeholder="Name"></asp:TextBox>
                                            </span>
                                            <%--<asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" Display="Dynamic" ID="RequiredFieldValidator2" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row mt-4 mb-3">
                                        <div class="col">
                                            <label for="email">Unit</label>
                                            <span class="icon-arrange border-none">
                                                <asp:DropDownList runat="server" ID="ddlUnit" CssClass="form-control border-none" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"></asp:DropDownList>
                                            </span>
                                        </div>

                                        <div class="col">
                                            <label for="email">Nature Of Move</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlMove" CssClass="form-control border-none"></asp:DropDownList>
                                            </span>
                                        </div>

                                        <div class="col">
                                            <label for="email">AD Type</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlADType" CssClass="form-control border-none"></asp:DropDownList>
                                            </span>
                                        </div>


                                    </div>
                                    <div class="row mt-4">

                                        <div class="col d-none">
                                            <label for="email">Lve No Of Days</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtLeaveNoOfDays" CssClass="form-control border-none" placeholder="No Of Days" Enabled="true" onchange="LeaveEmpty();"></asp:TextBox>
                                            </span>
                                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLeaveNoOfDays" ErrorMessage="Please Enter Only Numbers" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col d-none">
                                            <label for="email">From Date</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ReadOnly="false" TextMode="Date" ID="txtLeaveFromDate" CssClass="form-control border-none" data-date-format="dd-mm-yyyy" data-link-field="ContentPlaceHolder1_hfLeaveFromDate" onchange="Leavevalue();"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfLeaveFromDate" Value="" />
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">Medical Status</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlMedicalStatus" Enabled="true" CssClass="form-control border-none"></asp:DropDownList>
                                            </span>
                                        </div>

                                        <div class="col">
                                            <label for="email">FN/AN</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlSession" CssClass="form-control border-none">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="FN"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="AN"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>

                                        <div class="col">
                                            <label for="email">Date</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtDate" for="ContentPlaceHolder1_hfDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfDate"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfDate" Value="" />
                                            </span>
                                            <asp:RequiredFieldValidator ControlToValidate="txtDate" runat="server" Display="Dynamic" ID="RequiredFieldValidator5" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row mt-4 mb-3">
                                        <div class="col d-none">
                                            <label for="email">State</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtState" CssClass="form-control border-none" placeholder="State" Enabled="false"></asp:TextBox>
                                            </span>
                                            <%--<asp:RequiredFieldValidator ControlToValidate="txtAut" runat="server" Display="Dynamic" ID="RequiredFieldValidator3" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>--%>
                                        </div>


                                    </div>
                                    <div class="row mt-4 mb-3">



                                        <div class="col d-none">
                                            <label for="email">
                                                To Date
                                            </label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtLeaveToDate" ReadOnly="true" CssClass="form-control date to_datetime_Leave border-none" data-date-format="dd-mm-yyyy" data-link-field="ContentPlaceHolder1_hfLeaveToDate"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfLeaveToDate" Value="" />
                                            </span>
                                        </div>

                                        <div class="col">
                                            <label for="email">Brigade</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlBrigade" CssClass="form-control border-none" Enabled="false"></asp:DropDownList>
                                            </span>
                                            <div class="table" id="NameAuto1"></div>
                                        </div>

                                        <div class="col">
                                            <label for="email">DIV Name</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlDiv" CssClass="form-control border-none" Enabled="false"></asp:DropDownList>
                                            </span>
                                            <div class="table" id="NameAuto"></div>
                                        </div>

                                        <div class="col">
                                            <label for="email">FMN</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlHQ" CssClass="form-control border-none" Enabled="false"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="col d-none">
                                            <label for="email">FMN</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtFMN" CssClass="form-control border-none" placeholder="FMN" Visible="false"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="col d-none">
                                            <label for="email">Priority Name</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlPriority" CssClass="form-control border-none"></asp:DropDownList>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="row mt-4 mb-3">
                                        <div class="col d-none">
                                            <label for="email">Authority</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtAut" CssClass="form-control border-none" placeholder="Authority"></asp:TextBox>
                                            </span>
                                            <%--<asp:RequiredFieldValidator ControlToValidate="txtAut" runat="server" Display="Dynamic" ID="RequiredFieldValidator3" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col d-none">
                                            <label for="email">BP</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtBP" CssClass="form-control border-none" placeholder="BP"></asp:TextBox>
                                            </span>
                                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBP" ErrorMessage="Please Enter Decimal Type." ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                            <%--<asp:RequiredFieldValidator ControlToValidate="txtBP" runat="server" Display="Dynamic" ID="RequiredFieldValidator4" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col file-upload" id="fileupload">
                                            <label for="email">Refrence Doc</label>
                                            <span class="icon-arrange">
                                                <asp:FileUpload runat="server" ID="fuReference" CssClass="form-control border-none" />
                                            </span>
                                        </div>
                                    </div>
                                    <asp:Label runat="server" ID="lblExtraDays"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hfLblAbsentDays" />
                                    <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>

                                    <div class="d-flex justify-content-between align-items-top mb-3 pl-2 pr-2">
                                        <asp:CheckBox For="email" ID="chkTemporaryHold" runat="server" CssClass="CheckBox mt-4" Text="Is Cancel" OnClick="modelpopup();" />
                                        <asp:CheckBox For="email" ID="chkIsFly" runat="server" CssClass="CheckBox mt-4" Text="IsFly" Visible="false" />
                                        <asp:CheckBox For="email" ID="chkIsLoad" runat="server" CssClass="CheckBox mt-4" Text="IsLoad" Visible="false" />
                                        <asp:CheckBox For="email" ID="chkOnHoldStatus" runat="server" CssClass="CheckBox mt-4" Text="On Hold Status" OnClick="modelpopupOnHold();" />
                                        <span id="prio">
                                            <input type="checkbox" id="chkIsPriority" class="CheckBox mt-4" onclick="ShowHideDiv(this)" runat="server" />
                                            <p class="mt-4" style="font-weight: 600; margin-bottom: .5rem; float: left">
                                                Priority
                                            </p>
                                        </span>
                                        <label id="lblTemHold">On Temporary Hold.</label>
                                    </div>
                                    <div class="btnall">
                                        <asp:Button ID="btnSave" CssClass="btn btn-block btn-primary float-left btnmargin" Text="Save" runat="server" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnUpdate" CssClass="btn btn-block btn-primary float-left btnmargin" Text="Update" Visible="false" runat="server" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btnClear" CssClass="btn btn-block btn-primary float-left btnmargin" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnPrint" CssClass="btn btn-block btn-primary float-left btnmargin" Text="Print" runat="server" OnClick="btnPrint_Click" />
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title" id="myModalLabel">Ramark</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:TextBox runat="server" ID="txtOnTempHoldRemark" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Save changes</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="OnHold" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title" id="myModalLabel1">Ramark</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:TextBox runat="server" ID="txtOnHoldRemark" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Save changes</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">

    <script type="text/javascript">
        $('#<%=ddlCategory.ClientID%>').chosen();
        $('#<%=ddlRank.ClientID%>').chosen();
        $('#<%=ddlCity.ClientID%>').chosen();
        $('#<%=ddlUnit.ClientID%>').chosen();

        function enterEvent(e) {
            if (e.keyCode == 13) {
                $("input[id=ContentPlaceHolder1_btnSave]").click();
            }
        }

        function myFunction() {
            document.getElementById("myDropdown").classList.toggle("show");
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        $(function () {
            $(document).ready(function () {
                $('input:checkbox').click(function () {
                    $('input:checkbox').not(this).prop('checked', false);
                });
            });
        });
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('input:checkbox').click(function () {
                    $('input:checkbox').not(this).prop('checked', false);
                });
            });
        });

        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#<%=ddlCategory.ClientID%>').chosen();
                $('#<%=ddlRank.ClientID%>').chosen();
                $('#<%=ddlCity.ClientID%>').chosen();
                $('#<%=ddlUnit.ClientID%>').chosen();

            });
        });

        $(function () {
            $('.form_datetime').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                //startDate: new (Date),
            });
        });
        prm.add_endRequest(function () {
            $('.form_datetime').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                //startDate: new (Date),
            });
        });

        $(function () {
            $('.form_datetime_Leave').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0
            });
        });
        prm.add_endRequest(function () {
            $('.form_datetime_Leave').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0
            });
        });

        function LeaveEmpty() {
            var noofleaves = document.getElementById("ContentPlaceHolder1_txtLeaveNoOfDays");
            if (noofleaves.value != "") {


            }
            else {
                var HDateFrom = document.getElementById("ContentPlaceHolder1_hfLeaveFromDate");
                var HDateTo = document.getElementById("ContentPlaceHolder1_hfLeaveToDate");
                var txtDateTo = document.getElementById("ContentPlaceHolder1_txtLeaveToDate");
                var txtDatefrom = document.getElementById("ContentPlaceHolder1_txtLeaveFromDate");
                HDateFrom.value = "";
                HDateTo.value = "";
                txtDateTo.value = "";
                txtDatefrom.value = "";
            }
        }

        function Leavevalue() {
            var noofleaves = document.getElementById("ContentPlaceHolder1_txtLeaveNoOfDays");
            if (noofleaves.value != "") {
                var DateFrom = document.getElementById("ContentPlaceHolder1_txtLeaveFromDate");
                var HDateTo = document.getElementById("ContentPlaceHolder1_hfLeaveToDate");
                var txtDateTo = document.getElementById("ContentPlaceHolder1_txtLeaveToDate");
                Date.prototype.addDays = function (days) {
                    let date = new Date(this.valueOf());
                    date.setDate(date.getDate() + days);
                    return date;
                }
                let date = new Date(DateFrom.value);
                date.setDate(date.getDate() + parseInt(noofleaves.value));
                let datehidden = date;
                let dateto = date;

                var format = "dd MMMM yyyy"
                var formattodate = "dd-MM-yyyy"
                var utc = date.getUTCDate();
                date = formatDate(date, format, utc);
                dateto = formatDate(dateto, formattodate, utc);
                txtDateTo.value = dateto;

                var formathidden = "yyyy-MM-dd HH:mm:ss"
                var utchidden = datehidden.getUTCDate();
                datehidden = formatDate(datehidden, formathidden, utchidden);
                HDateTo.value = datehidden;

                var extradays = new Date(date)
                var datepresent = new Date();
                var time_difference = datepresent.getTime() - extradays.getTime();
                var lblCount = document.getElementById("ContentPlaceHolder1_lblExtraDays");
                var hfAbsentDays = document.getElementById("ContentPlaceHolder1_hfLblAbsentDays");
                var session = $("#ContentPlaceHolder1_ddlSession option:selected").text();

                //calculate days difference by dividing total milliseconds in a day
                var days_difference = time_difference / (1000 * 60 * 60 * 24);
                var daysextra = parseInt(days_difference);
                if (daysextra <= 0) {
                    lblCount.innerHTML = "Absent Days : " + daysextra + "";
                    lblCount.style.display = "none";
                    hfAbsentDays.value = daysextra;
                }
                else {
                    if (lblCount != "") {
                        if (session.toLowerCase() == "fn") {
                            var daydelete = daysextra - 1
                            lblCount.innerHTML = "Absent Days : " + daydelete + "";

                            if (daydelete <= 0) {
                                lblCount.style.display = "none";
                                hfAbsentDays.value = daysextra;
                            }
                            else {
                                lblCount.style.display = "block";
                                hfAbsentDays.value = daysextra;
                            }
                        }
                        else {
                            lblCount.innerHTML = "Absent Days : " + daysextra + "";
                            lblCount.style.display = "block";
                            hfAbsentDays.value = daysextra;
                        }
                    }
                }
            }
            else {
                alert("Add No Of Leaves!")
                ContentPlaceHolder1_txtLeaveFromDate.value = "";
                ContentPlaceHolder1_txtLeaveToDate.value = "";
            }
        }

        function myFunction() {
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            ul = document.getElementById("myUL");
            li = ul.getElementsByTagName("li");
            for (i = 0; i < li.length; i++) {
                a = li[i].getElementsByTagName("a")[0];
                txtValue = a.textContent || a.innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }
            }
        }
        prm.add_endRequest(function () {
            function myFunction() {
                var input, filter, ul, li, a, i, txtValue;
                input = document.getElementById("myInput");
                filter = input.value.toUpperCase();
                ul = document.getElementById("myUL");
                li = ul.getElementsByTagName("li");
                for (i = 0; i < li.length; i++) {
                    a = li[i].getElementsByTagName("a")[0];
                    txtValue = a.textContent || a.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        li[i].style.display = "";
                    } else {
                        li[i].style.display = "none";
                    }
                }
            }
        });

        function ShowHideDiv(chkPassport) {
            var dvPassport = document.getElementById("fileupload");
            dvPassport.style.display = chkPassport.checked ? "block" : "none";
        }
        prm.add_endRequest(function () {
            function ShowHideDiv(chkPassport) {
                var dvPassport = document.getElementById("fileupload");
                dvPassport.style.display = chkPassport.checked ? "block" : "none";
            }
        });

    </script>
    <script type="text/javascript">
        var id;
        $("#myUL").children('li').click(function () {
            id = $(this).children('a').attr('id');
            CallMethod();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("#myUL").children('li').click(function () {
                id = $(this).children('a').attr('id');
                CallMethod();
            });
        });

        function formatDate(date, format, utc) {
            var MMMM = ["\x00", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            var MMM = ["\x01", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var dddd = ["\x02", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            var ddd = ["\x03", "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
            function ii(i, len) { var s = i + ""; len = len || 2; while (s.length < len) s = "0" + s; return s; }

            var y = utc ? date.getUTCFullYear() : date.getFullYear();
            format = format.replace(/(^|[^\\])yyyy+/g, "$1" + y);
            format = format.replace(/(^|[^\\])yy/g, "$1" + y.toString().substr(2, 2));
            format = format.replace(/(^|[^\\])y/g, "$1" + y);

            var M = (utc ? date.getUTCMonth() : date.getMonth()) + 1;
            format = format.replace(/(^|[^\\])MMMM+/g, "$1" + MMMM[0]);
            format = format.replace(/(^|[^\\])MMM/g, "$1" + MMM[0]);
            format = format.replace(/(^|[^\\])MM/g, "$1" + ii(M));
            format = format.replace(/(^|[^\\])M/g, "$1" + M);

            var d = utc ? date.getUTCDate() : date.getDate();
            format = format.replace(/(^|[^\\])dddd+/g, "$1" + dddd[0]);
            format = format.replace(/(^|[^\\])ddd/g, "$1" + ddd[0]);
            format = format.replace(/(^|[^\\])dd/g, "$1" + ii(d));
            format = format.replace(/(^|[^\\])d/g, "$1" + d);

            var H = utc ? date.getUTCHours() : date.getHours();
            format = format.replace(/(^|[^\\])HH+/g, "$1" + ii(H));
            format = format.replace(/(^|[^\\])H/g, "$1" + H);

            var h = H > 12 ? H - 12 : H == 0 ? 12 : H;
            format = format.replace(/(^|[^\\])hh+/g, "$1" + ii(h));
            format = format.replace(/(^|[^\\])h/g, "$1" + h);

            var m = utc ? date.getUTCMinutes() : date.getMinutes();
            format = format.replace(/(^|[^\\])mm+/g, "$1" + ii(m));
            format = format.replace(/(^|[^\\])m/g, "$1" + m);

            var s = utc ? date.getUTCSeconds() : date.getSeconds();
            format = format.replace(/(^|[^\\])ss+/g, "$1" + ii(s));
            format = format.replace(/(^|[^\\])s/g, "$1" + s);

            var f = utc ? date.getUTCMilliseconds() : date.getMilliseconds();
            format = format.replace(/(^|[^\\])fff+/g, "$1" + ii(f, 3));
            f = Math.round(f / 10);
            format = format.replace(/(^|[^\\])ff/g, "$1" + ii(f));
            f = Math.round(f / 10);
            format = format.replace(/(^|[^\\])f/g, "$1" + f);

            var T = H < 12 ? "AM" : "PM";
            format = format.replace(/(^|[^\\])TT+/g, "$1" + T);
            format = format.replace(/(^|[^\\])T/g, "$1" + T.charAt(0));

            var t = T.toLowerCase();
            format = format.replace(/(^|[^\\])tt+/g, "$1" + t);
            format = format.replace(/(^|[^\\])t/g, "$1" + t.charAt(0));

            var tz = -date.getTimezoneOffset();
            var K = utc || !tz ? "Z" : tz > 0 ? "+" : "-";
            if (!utc) {
                tz = Math.abs(tz);
                var tzHrs = Math.floor(tz / 60);
                var tzMin = tz % 60;
                K += ii(tzHrs) + ":" + ii(tzMin);
            }
            format = format.replace(/(^|[^\\])K/g, "$1" + K);

            var day = (utc ? date.getUTCDay() : date.getDay()) + 1;
            format = format.replace(new RegExp(dddd[0], "g"), dddd[day]);
            format = format.replace(new RegExp(ddd[0], "g"), ddd[day]);

            format = format.replace(new RegExp(MMMM[0], "g"), MMMM[M]);
            format = format.replace(new RegExp(MMM[0], "g"), MMM[M]);

            format = format.replace(/\\(.)/g, "$1");

            return format;
        };

        function CallMethod() {
            $.ajax({
                type: "POST",
                url: "Admin/ADEntery.aspx/GetADNODetailsByID",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'ID':'" + id + "'}",
                success: function (data) {
                    var CategoryID = document.getElementById("ContentPlaceHolder1_ddlCategory");
                    var ICard = document.getElementById("ContentPlaceHolder1_txtICard");
                    var Name = document.getElementById("ContentPlaceHolder1_txtName");
                    var CityID = document.getElementById("ContentPlaceHolder1_ddlCity");
                    var Session = document.getElementById("ContentPlaceHolder1_ddlSession");
                    var ArmyNo = document.getElementById("ContentPlaceHolder1_txtArmyNo");
                    var UnitID = document.getElementById("ContentPlaceHolder1_ddlUnit");
                    var DivID = document.getElementById("ContentPlaceHolder1_ddlDiv");
                    var HQID = document.getElementById("ContentPlaceHolder1_ddlHQ");
                    var BrigadeID = document.getElementById("ContentPlaceHolder1_ddlBrigade");
                    var RankID = document.getElementById("ContentPlaceHolder1_ddlRank");
                    var Authority = document.getElementById("ContentPlaceHolder1_txtAut");
                    var MoveID = document.getElementById("ContentPlaceHolder1_ddlMove");
                    var PriorityID = document.getElementById("ContentPlaceHolder1_ddlPriority");
                    var ADType = document.getElementById("ContentPlaceHolder1_ddlADType");
                    var BP1 = document.getElementById("ContentPlaceHolder1_txtBP");
                    var hfID = document.getElementById("ContentPlaceHolder1_hfID");
                    var Date1 = document.getElementById("ContentPlaceHolder1_txtDate");
                    var HDate = document.getElementById("ContentPlaceHolder1_hfDate");
                    var HFromDate = document.getElementById("ContentPlaceHolder1_hfLeaveFromDate");
                    var FromDate = document.getElementById("ContentPlaceHolder1_txtLeaveFromDate");
                    var ToDate = document.getElementById("ContentPlaceHolder1_txtLeaveToDate");
                    var HToDate = document.getElementById("ContentPlaceHolder1_hfLeaveToDate");
                    var NoOfDaysLeave = document.getElementById("ContentPlaceHolder1_txtLeaveNoOfDays");
                    var FMN = document.getElementById("ContentPlaceHolder1_txtFMN");
                    var lblCount = document.getElementById("ContentPlaceHolder1_lblExtraDays");
                    var hfAbsentDays = document.getElementById("ContentPlaceHolder1_hfLblAbsentDays");
                    var catchoosen = document.getElementById("ContentPlaceHolder1_ddlCategory_chosen");
                    var medicalstatus = document.getElementById("ContentPlaceHolder1_ddlMedicalStatus");

                    var DateToFormat = new Date(data.Date);
                    var format = "yyyy-MM-dd HH:mm:ss"

                    if (DateToFormat != null) {

                        var utc = DateToFormat.getUTCDate();
                        var dateformat = formatDate(DateToFormat, format, utc);
                    }

                    var FromDateToFormat = new Date(data.LeaveFromDate);
                    let fromdateformat = FromDateToFormat;
                    fromdateformat = fromdateformat.setDate(fromdateformat.getDate() + 1);
                    if (FromDateToFormat != null) {
                        var utc = FromDateToFormat.getUTCDate();

                        var dateLeaveFrom = formatDate(FromDateToFormat, format, utc);
                        if (dateLeaveFrom == "1970-01-01 00:00:00") {
                            FromDate.value = "";
                            HFromDate.value = "";
                        }
                        else {
                            HFromDate.value = dateLeaveFrom;
                            var finalfromdate = new Date(fromdateformat);
                            var formateddate = finalfromdate.toISOString().substr(0, 10);
                            FromDate.value = formateddate;
                        }
                    }


                    var ToDateToFormat = new Date(data.LeaveToDate);
                    if (FromDateToFormat != null) {
                        var utc = ToDateToFormat.getUTCDate();

                        var dateLeaveTo = formatDate(ToDateToFormat, format, utc);
                        if (dateLeaveTo == "1970-01-01 00:00:00") {
                            ToDate.value = "";
                            HToDate.value = "";
                        }
                        else {
                            HToDate.value = dateLeaveTo;
                            ToDate.value = dateLeaveTo;
                        }
                    }
                    Date1.value = dateformat;
                    HDate.value = dateformat;
                    CategoryID.value = data.CategoryID;
                    $(CategoryID).val(CategoryID.value).trigger("chosen:updated");
                    ICard.value = data.ICard;
                    Name.value = data.Name;
                    CityID.value = data.CityID;
                    $(CityID).val(CityID.value).trigger("chosen:updated");
                    Session.value = data.SessionID;
                    ArmyNo.value = data.ArmyNo;
                    UnitID.value = data.UnitID;
                    $(UnitID).val(UnitID.value).trigger("chosen:updated");
                    DivID.value = data.DivID;
                    HQID.value = data.HQID;
                    BrigadeID.value = data.BrigadeID;
                    RankID.value = data.RankID;
                    $(RankID).val(RankID.value).trigger("chosen:updated");
                    Authority.value = data.Authority;
                    MoveID.value = data.MoveID;
                    PriorityID.value = data.PriorityID;
                    ADType.value = data.AdTypeID;
                    BP1.value = data.BP;
                    hfID.value = id;
                    //FMN.value = data.FMN;
                    NoOfDaysLeave.value = data.LeaveNoOfDays;
                    medicalstatus.value = data.MedicalStatusID;

                    if (data.NoOfAbsentDays != null) {
                        lblCount.innerHTML = "Absent Days : " + data.NoOfAbsentDays + "";
                        hfAbsentDays.value = data.NoOfAbsentDays;
                        lblCount.style.display = "block";
                    }
                    else {
                        lblCount.style.display = 'none';
                        hfAbsentDays.value = "";
                    }


                    var btnsave = document.getElementById('ContentPlaceHolder1_btnSave');
                    var btnupdate = document.getElementById('ContentPlaceHolder1_btnUpdate');
                    var elems = document.getElementsByClassName('CheckBox');
                    var lblerror = document.getElementById('ContentPlaceHolder1_lblError');
                    var chkPrio;
                    if (data.IsPriority == true) {
                        chkPrio = document.getElementById("ContentPlaceHolder1_chkIsPriority");
                        chkPrio.checked = true;
                    }
                    else {
                        chkPrio = document.getElementById("ContentPlaceHolder1_chkIsPriority");
                        chkPrio.checked = false;
                    }
                    if (data.IsLoad == true) {
                        chkPrio = document.getElementById("ContentPlaceHolder1_chkIsLoad");
                        chkPrio.checked = true;
                    }
                    else {
                        chkPrio = document.getElementById("ContentPlaceHolder1_chkIsLoad");
                        chkPrio.checked = false;
                    }
                    if (data.IsTempHold == true) {
                        for (var i = 0; i < elems.length; i += 1) {
                            elems[i].style.display = 'none';
                        }
                        document.getElementById('lblTemHold').style.display = 'block';
                        document.getElementById('prio').style.display = 'none';
                        if (lblerror != null) {
                            document.getElementById('ContentPlaceHolder1_lblError').style.display = 'none';
                        }
                        if (btnsave != null) {
                            document.getElementById('ContentPlaceHolder1_btnSave').style.display = 'none';
                        }
                        if (btnupdate != null) {
                            document.getElementById('ContentPlaceHolder1_btnUpdate').style.display = 'none';
                        }
                        document.getElementById('ContentPlaceHolder1_btnClear').style.display = 'none';
                        document.getElementById('ContentPlaceHolder1_btnPrint').style.display = 'none';

                        $("#ADForm").addClass("disabledbutton");
                    }
                    else {
                        for (var i = 0; i < elems.length; i += 1) {
                            elems[i].style.display = 'block';
                        }
                        document.getElementById('prio').style.display = 'block';
                        if (lblerror != null) {
                            document.getElementById('ContentPlaceHolder1_lblError').style.display = 'none';
                        }
                        if (btnsave != null) {
                            document.getElementById('ContentPlaceHolder1_btnSave').style.display = 'block';
                        }
                        if (btnupdate != null) {
                            document.getElementById('ContentPlaceHolder1_btnUpdate').style.display = 'block';
                        }
                        document.getElementById('ContentPlaceHolder1_btnClear').style.display = 'block';
                        document.getElementById('ContentPlaceHolder1_btnPrint').style.display = 'block';
                        document.getElementById('lblTemHold').style.display = 'none';
                        $("#ADForm").removeClass("disabledbutton");
                    }

                },
                failure: function (data) {
                    alert("fail");
                }
            });
        }

        $(document).ready(function () {
            var finalfromdate = new Date(Date.now());
            var formateddate = finalfromdate.toISOString().substr(0, 10);
            document.getElementById("ContentPlaceHolder1_txtLeaveFromDate").value = formateddate;
        });

    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        $(document).ready(function () {
            AutocompleteName();
            AutocompleteICard();
            AutocompleteArmyNO();
        });
        prm.add_endRequest(function () {
            $(document).ready(function () {
                AutocompleteName();
                AutocompleteICard();
                AutocompleteArmyNO();
            });
        });
        function AutocompleteName() {
            var list = new Array();
            $("#ContentPlaceHolder1_txtName").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Admin/ADEntery.aspx/GetNameAutocomplete",
                        data: "{'Info':'" + document.getElementById('ContentPlaceHolder1_txtName').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data != "") {
                                for (var i = 0; i < data.length; i++) {
                                    list[i] = " " + data[i].Name + "";
                                }
                                response(list);
                            }
                        },
                        error: function (result) {
                            //alert("No Match");
                        }
                    });
                }
            });
        }
        function AutocompleteICard() {
            var list = new Array();
            $("#ContentPlaceHolder1_txtICard").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Admin/ADEntery.aspx/GetIcardAutocomplete",
                        data: "{'Info':'" + document.getElementById('ContentPlaceHolder1_txtICard').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data != "") {
                                for (var i = 0; i < data.length; i++) {
                                    list[i] = " " + data[i].IDCardNo + "";
                                }
                                response(list);
                            }
                        },
                        error: function (result) {
                            //alert("No Match");
                        }
                    });
                }
            });
        }
        function AutocompleteArmyNO() {
            var list = new Array();
            $("#ContentPlaceHolder1_txtArmyNo").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Admin/ADEntery.aspx/GetArmyNoAutocomplete",
                        data: "{'Info':'" + document.getElementById('ContentPlaceHolder1_txtArmyNo').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data != "") {
                                for (var i = 0; i < data.length; i++) {
                                    list[i] = " " + data[i].ArmyNumber + "";
                                }
                                response(list);
                            }
                        },
                        error: function (result) {
                            //alert("No Match");
                        }
                    });
                }
            });
        }

        function modelpopup() {
            var checkontemhold = document.getElementById("ContentPlaceHolder1_chkTemporaryHold");
            if (checkontemhold.checked) {
                $('#myModal').modal();
            }
        };

        function modelpopupOnHold() {
            var checkontemhold = document.getElementById("ContentPlaceHolder1_chkOnHoldStatus");
            if (checkontemhold.checked) {
                $('#OnHold').modal();
            }
        };

        function Focus() {
            $('#ContentPlaceHolder1_ddlRank').trigger('chosen:activate');
            $('#ContentPlaceHolder1_ddlRank').trigger('chosen:update');
        }

    </script>
</asp:Content>
