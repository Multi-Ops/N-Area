<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="TransferCourierToCharter.aspx.cs" Inherits="TransitCamp.Admin.TransferCourierToCharter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <div class="mr-2 w-25 bg-white row float-left form-bg-image d-flex align-items-center mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-2 p-lg-2">
                <div class="p-lg-4">
                    <h1 class="mb-0 h4 text-center text-md-center mt-1 mb-3 p-3">AD Entry No</h1>
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
                    <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-800">
                        <div class="text-center text-md-center mb-4 mt-md-0">
                            <h1 class="mb-0 h3">Transfer Courier To Charter</h1>
                        </div>
                        <form action="#" method="post">
                            <div id="cont">
                                <div class="row">
                                    <div class="col">
                                        <label for="email">Category</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                    <div class="col">
                                        <label for="email">City</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                    <div class="col">
                                        <label for="email">Name</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control border-none" placeholder="Name"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtName" runat="server" Display="Dynamic" ID="RequiredFieldValidator2" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">I Card No</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtICard" CssClass="form-control border-none" placeholder="I card No"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtICard" runat="server" Display="Dynamic" ID="rfv1" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col">
                                        <label for="email">Date</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtDate" for="ContentPlaceHolder1_hfDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfDate"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfDate" Value="" />
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtDate" runat="server" Display="Dynamic" ID="RequiredFieldValidator5" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
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
                                        <label for="email">Army No</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtArmyNo" CssClass="form-control border-none" placeholder="Army No"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtArmyNo" runat="server" Display="Dynamic" ID="RequiredFieldValidator1" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col">
                                        <label for="email">Unit</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlUnit" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                    <div class="col">
                                        <label for="email">DIV Name</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlDiv" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                    <div class="col">
                                        <label for="email">Headquarter</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlHQ" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                    <div class="col">
                                        <label for="email">Rank</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlRank" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                </div>
                                <div class="row mt-4 mb-3">
                                    <div class="col">
                                        <label for="email">Seat No</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtSeatNo" CssClass="form-control border-none" placeholder="Seat NO"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtSeatNo" runat="server" Display="Dynamic" ID="RequiredFieldValidator4" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="txtSeatNo" runat="server"
                                            ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                            ValidationExpression="\d+">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">Authority</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtAut" CssClass="form-control border-none" placeholder="Authority"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtAut" runat="server" Display="Dynamic" ID="RequiredFieldValidator3" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">M,Move</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlMove" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                    <div class="col">
                                        <label for="email">Priority Name</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlPriority" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                </div>
                                <div class="row mt-4 mb-3">
                                    <div class="col">
                                        <label for="email">Priority Status</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlPriorityStatus" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                    <div class="col">
                                        <label for="email">Transfer Date</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtTransferDate" for="ContentPlaceHolder1_hfTransferDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime1 border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfTransferDate"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfTransferDate" Value="" />
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtTransferDate" runat="server" Display="Dynamic" ID="RequiredFieldValidator7" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">Flight Date</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtFightDate" for="ContentPlaceHolder1_hfFlightDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime2 border-none" data-date-format="dd-mm-yyyy" data-link-field="ContentPlaceHolder1_hfFlightDate"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfFlightDate" Value="" />
                                        </span>
                                        <asp:RequiredFieldValidator ControlToValidate="txtFightDate" runat="server" Display="Dynamic" ID="RequiredFieldValidator6" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-4 mb-3">
                                    <div class="col">
                                        <label for="email">Charter No</label>
                                        <span class="icon-arrange">
                                            <asp:DropDownList runat="server" ID="ddlCharterNo" CssClass="form-control border-none"></asp:DropDownList>
                                        </span>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-between align-items-top mb-3 pl-2 pr-2">
                                    <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                </div>
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-primary" Text="Save" runat="server" OnClick="btnSave_Click" />
                                <asp:Button ID="btnUpdate" CssClass="btn btn-block btn-primary" Text="Update" runat="server" Visible="false" OnClick="btnUpdate_Click" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
    <script type="text/javascript">
        $(function () {
            $('.form_datetime').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                startDate: new (Date),
            });
        });
        $(function () {
            $('.form_datetime2').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0,
                startDate: new (Date),
            });
        });
        $(function () {
            $('.form_datetime1').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                startDate: new (Date),
            });
        });
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
    </script>
    <script type="text/javascript">
        var id;
        $("#myUL").children('li').click(function () {
            id = $(this).children('a').attr('id');
            CallMethod();
        });

        function CallMethod() {
            $.ajax({
                type: "POST",
                url: "Admin/TransferCourierToCharter.aspx/GetADNODetailsByID",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'ID':'" + id + "'}",
                success: function (data) {
                    //alert("data is here Name: " + data.Name + ", Icard: " + data.ICard + "")
                    var CategoryID = document.getElementById("ContentPlaceHolder1_ddlCategory");
                    var ICard = document.getElementById("ContentPlaceHolder1_txtICard");
                    var Name = document.getElementById("ContentPlaceHolder1_txtName");
                    var CityID = document.getElementById("ContentPlaceHolder1_ddlCity");
                    //var Date = document.getElementById("ContentPlaceHolder1_txtDate");
                    //var hfDate = document.getElementById("ContentPlaceHolder1_hfDate");
                    var Session = document.getElementById("ContentPlaceHolder1_ddlSession");
                    var ArmyNo = document.getElementById("ContentPlaceHolder1_txtArmyNo");
                    var UnitID = document.getElementById("ContentPlaceHolder1_ddlUnit");
                    var DivID = document.getElementById("ContentPlaceHolder1_ddlDiv");
                    var HQID = document.getElementById("ContentPlaceHolder1_ddlHQ");
                    var RankID = document.getElementById("ContentPlaceHolder1_ddlRank");
                    var Authority = document.getElementById("ContentPlaceHolder1_txtAut");
                    var MoveID = document.getElementById("ContentPlaceHolder1_ddlMove");
                    var PriorityID = document.getElementById("ContentPlaceHolder1_ddlPriority");

                    CategoryID.value = data.CategoryID;
                    ICard.value = data.ICard;
                    Name.value = data.Name;
                    CityID.value = data.CityID;
                    //Date.value = date.Date;
                    //hfDate.value = date.Date;
                    Session.value = data.Session;
                    ArmyNo.value = data.ArmyNo;
                    UnitID.value = data.UnitID;
                    DivID.value = data.DivID;
                    HQID.value = data.HQID;
                    RankID.value = data.RankID;
                    Authority.value = data.Authority;
                    MoveID.value = data.MoveID;
                    PriorityID.value = data.PriorityID;
                },
                failure: function (data) {
                    alert("fail");
                }
            });
        }
    </script>
</asp:Content>
