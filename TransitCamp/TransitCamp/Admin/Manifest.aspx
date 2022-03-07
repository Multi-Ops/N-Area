<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Manifest.aspx.cs" Inherits="TransitCamp.Admin.Manifest" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .required {
            color: #ff0000;
        }

        .custom-ad {
            width: 30%;
        }

        .custom-right-nav {
            width: 100% !important;
            margin: 15px 0 0 0 !important;
        }

        .leftborder {
            border-right: 1px solid #d1d7e0;
        }

        .bulkadd {
            float: left;
            margin: 0px 0px 0px -4px;
            padding: 0px 0px 5px 0px;
        }

        .table-responsive {
            height: 285px;
        }

        .btn {
            background-color: #2196F3;
            color: white;
            padding: 16px;
            font-size: 16px;
            border: none;
            outline: none;
        }

        .dropdown {
            position: absolute;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f1f1f1;
            z-index: 1;
        }

            .dropdown-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

                .dropdown-content a:hover {
                    background-color: #ddd
                }

        .dropdown:hover .dropdown-content {
            display: block;
            bottom: 0px;
        }

        .btn:hover, .dropdown:hover .btn {
            background-color: #262B40;
        }

        .custom-select {
            margin: 2px 0px 0 0px !important;
            padding: 0px 10px 0 10px !important;
            height: 25px !important;
            font-size: 12px !important;
            float: right !important;
        }

        .custom-display {
            display: inline-block !important;
            width: 60px !important;
            font-size: 12px !important;
        }

        .custom-bulk {
            float: left;
            width: 85px !important;
            font-size: 12px !important;
            padding: 0px 10px 0 10px !important;
            margin: 2px 0px 2px 0px !important;
        }

        input#ContentPlaceHolder1_txtBUlkMoveFromGen {
            font-size: 12px;
        }

        hr {
            width: 100%;
            margin: 0px 0px 5px 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <asp:ScriptManager ID="scriptmanager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="upSide" runat="server">
                <ContentTemplate>
                    <div class="bulkadd float-left w-100">
                        <div class="col">
                            <asp:TextBox runat="server" ID="txtAddNo" Placeholder="Digits" TextMode="Number" CssClass="border-none" Width="70px" Height="25px"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="ddlBulkAdd" CssClass="border-none" Width="120px" Height="25px">
                                <asp:ListItem Value="1" Text="Normal"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Priority"></asp:ListItem>
                                <%--         <asp:ListItem Value="3" Text="Load"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Reserve"></asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:Button runat="server" ID="btnAddBulk" Text="Finalize" CssClass="btn-primary border-none" Height="25px" Width="120px" OnClick="btnAddBulk_Click" />
                        </div>
                    </div>
                    <div id="divRptrADNo" runat="server" class="mr-2 w-25 custom-ad bg-white row float-left form-bg-image d-flex align-items-center mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light">
                        <div class="custom-right-nav leftborder d-none">
                            <h1 class="mb-0 h5 text-center text-md-center mt-1 mb-1">Priority</h1>
                            <asp:DropDownList runat="server" ID="ddlCityPriority" CssClass="form-control mb-1" AutoPostBack="true" OnSelectedIndexChanged="ddlCityPriority_SelectedIndexChanged" Visible="false">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hfCityPriority" runat="server" />
                            <input name="SearchADEntryPriority" type="text" id="myInputPriority" onkeyup="myFunctionPriority()" class="form-control" placeholder="Search">
                            <div class="mt-1 mb-1"></div>
                            <div class="overflow-auto mb-1 background-search" style="height: 180px  !important">
                                <ul id="myULPriority">
                                    <asp:Repeater runat="server" ID="rptPriority">
                                        <ItemTemplate>
                                            <label>
                                                <li class="li-list float-left"><a class="search-list float-left" id='<%# Eval("ID") %>'>
                                                    <asp:HiddenField runat="server" ID="hfPriorityID" Value='<%# Eval("ID") %>' />
                                                    <asp:CheckBox runat="server" ID='chkPriority' CssClass="form-control border-none float-left chkAD" />
                                                    <div class="ml-3 pl-2 float-left"><%# Eval("ADNO") %></div>
                                                </a></li>
                                            </label>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <asp:Button runat="server" ID="btnfinalizePriority" CssClass="btn btn-block btn-primary custom-display" Text="Finalize" OnClick="btnfinalize_Click" />
                            <div class="dropdown">
                                <div class="dropdown-content">
                                    <div class="bulkMove">
                                        <asp:TextBox runat="server" ID="txtPriorityBulk" Placeholder="Digits" TextMode="Number" CssClass="border-none custom-bulk" Width="70px" Height="25px"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="ddlPriorityBulk" CssClass="border-none custom-bulk" Width="120px" Height="25px">
                                            <%--<asp:ListItem Value="1" Text="Load"></asp:ListItem>--%>
                                            <asp:ListItem Value="2" Text="Normal"></asp:ListItem>
                                            <%--<asp:ListItem Value="3" Text="Reserve"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:Button runat="server" ID="btnBulkMovePriority" Text="Bulk Move" CssClass="btn btn-block btn-primary border-none custom-bulk" Height="25px" Width="120px" OnClick="btnBulkMovePriority_Click" />
                                    </div>
                                    <hr />

                                    <%--<asp:Button runat="server" Text="Load" CssClass="btn btn-block btn-primary custom-select" ID="btnPrioLoad" OnClick="btnPrioLoad_Click" />--%>
                                    <asp:Button runat="server" Text="Normal" CssClass="btn btn-block btn-primary custom-select" ID="btnPrioNormal" OnClick="btnPrioNormal_Click" />
                                    <%--<asp:Button runat="server" Text="Reserve" CssClass="btn btn-block btn-primary custom-select" ID="btnPrioReserve" OnClick="btnPrioReserve_Click" />--%>
                                </div>
                                <button type="button" class="btn btn-block btn-primary custom-display">Move</button>
                            </div>
                        </div>
                        <div class="custom-right-nav">
                            <h1 class="mb-0 h5 text-center text-md-center mt-1 mb-1">Normal</h1>
                            <asp:DropDownList runat="server" Visible="false" ID="ddlCityGeneral" CssClass="form-control mb-1" AutoPostBack="true" OnSelectedIndexChanged="ddlCityGeneral_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hfCityGeneral" runat="server" />
                            <input name="SearchADEntryGeneral" type="text" id="myInputGerenral" onkeyup="myFunctionGerenral()" class="form-control" placeholder="Search">
                            <div class="mt-1 mb-1"></div>
                            <div class="overflow-auto mb-1 background-search" style="height: 180px !important">
                                <ul id="myULGeneral">
                                    <asp:Repeater runat="server" ID="rptGeneral">
                                        <ItemTemplate>
                                            <label>
                                                <li class="li-list float-left"><a class="search-list float-left" id='<%# Eval("ID") %>'>
                                                    <asp:HiddenField runat="server" ID="hfPriorityID" Value='<%# Eval("ID") %>' />
                                                    <asp:CheckBox runat="server" ID='chkIDGeneral' CssClass="form-control border-none float-left chkAD" />
                                                    <div class="ml-3 pl-2 float-left"><%# Eval("Name") %></div>
                                                </a></li>
                                            </label>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <asp:Button runat="server" ID="btnFinalizeGeneral" CssClass="btn btn-block btn-primary custom-display" Text="Finalize" OnClick="btnFinalizeGeneral_Click" />
                            <div class="dropdown">
                                <div class="dropdown-content">
                                    <div class="bulkMove">
                                        <asp:TextBox runat="server" ID="txtBulkMove" Placeholder="Digits" TextMode="Number" CssClass="border-none custom-bulk" Width="70px" Height="25px"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="ddlBulkMoveFromGeneral" CssClass="border-none custom-bulk" Width="120px" Height="25px">
                                            <asp:ListItem Value="1" Text="Priority"></asp:ListItem>
                                            <%--<asp:ListItem Value="2" Text="Load"></asp:ListItem>--%>
                                            <%--<asp:ListItem Value="3" Text="Reserve"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:Button runat="server" ID="txtBUlkMoveFromGen" Text="Bulk Move" CssClass="btn btn-block btn-primary border-none custom-bulk" Height="25px" Width="120px" OnClick="txtBUlkMoveFromGen_Click" />
                                    </div>
                                    <hr />

                                    <asp:Button runat="server" Text="Priority" CssClass="btn btn-block btn-primary custom-select" ID="btnMovePriorityGen" OnClick="btnMovePriorityGen_Click" />
                                    <%--                                    <asp:Button runat="server" Text="Load" CssClass="btn btn-block btn-primary custom-select" ID="btnMoveLoadGen" OnClick="btnMoveLoadGen_Click" />
                                    <asp:Button runat="server" Text="Reserve" CssClass="btn btn-block btn-primary custom-select" ID="btnMoveReserveGen" OnClick="btnMoveReserveGen_Click" />--%>
                                </div>
                                <button type="button" class="btn btn-block btn-primary custom-display">Move</button>
                            </div>
                        </div>
                        <div class="pb-2 custom-right-nav leftborder d-none">
                            <h1 class="mb-0 h5 text-center text-md-center mt-1 mb-1">Reserve</h1>
                            <asp:DropDownList runat="server" Visible="false" ID="ddlCityReserve" CssClass="form-control mb-1" AutoPostBack="true" OnSelectedIndexChanged="ddlCityReserve_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hfCityReserve" runat="server" />
                            <input name="SearchADEntryReserve" type="text" id="myInputReserve" onkeyup="myFunctionReserve()" class="form-control" placeholder="Search">
                            <div class="mt-1 mb-1"></div>
                            <div class="overflow-auto mb-1 background-search" style="height: 180px !important">
                                <ul id="myULReserve">
                                    <asp:Repeater runat="server" ID="rptReserve">
                                        <ItemTemplate>
                                            <label>
                                                <li class="li-list float-left"><a class="search-list float-left" id='<%# Eval("ID") %>'>
                                                    <asp:HiddenField runat="server" ID="hfPriorityID" Value='<%# Eval("ID") %>' />
                                                    <asp:CheckBox runat="server" ID='chkIDReserve' CssClass="form-control border-none float-left chkAD" />
                                                    <div class="ml-3 pl-2 float-left"><%# Eval("ADNO") %></div>
                                                </a></li>
                                            </label>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <asp:Button runat="server" ID="btnFinalizeReserve" CssClass="btn btn-block btn-primary custom-display" Text="Finalize" OnClick="btnFinalizeReserve_Click" />
                            <div class="dropdown">
                                <div class="dropdown-content">
                                    <div class="bulkMove">
                                        <asp:TextBox runat="server" ID="txtBulkFromReserve" Placeholder="Digits" TextMode="Number" CssClass="border-none custom-bulk" Width="70px" Height="25px"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="ddlBulkFromReserve" CssClass="border-none custom-bulk" Width="120px" Height="25px">
                                            <asp:ListItem Value="1" Text="Priority"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Load"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button runat="server" ID="btnBulkMoveReserve" Text="Bulk Move" CssClass="btn btn-block btn-primary border-none custom-bulk" Height="25px" Width="120px" OnClick="btnBulkMoveReserve_Click" />
                                    </div>
                                    <hr />

                                    <asp:Button runat="server" Text="Normal" CssClass="btn btn-block btn-primary custom-select" ID="btnNormalMove" OnClick="btnNormalMove_Click" />
                                    <asp:Button runat="server" Text="Load" CssClass="btn btn-block btn-primary custom-select" ID="btnLoadMove" OnClick="btnLoadMove_Click" />
                                    <asp:Button runat="server" Text="Priority" CssClass="btn btn-block btn-primary custom-select" ID="btnPriorityMove" OnClick="btnPriorityMove_Click" />
                                </div>
                                <button type="button" class="btn btn-block btn-primary custom-display">Move</button>
                            </div>
                        </div>
                        <div class="pb-2 custom-right-nav d-none">
                            <h1 class="mb-0 h5 text-center text-md-center mt-1 mb-1">Load</h1>
                            <asp:HiddenField ID="hfCatReserve" runat="server" />
                            <asp:DropDownList runat="server" Visible="false" ID="ddlCityLoad" CssClass="form-control mb-1" AutoPostBack="true" OnSelectedIndexChanged="ddlLoad_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hfCityLoad" runat="server" />
                            <input name="SearchADEntryLoad" type="text" id="myInputLoad" onkeyup="myFunctionLoad()" class="form-control" placeholder="Search">
                            <div class="mt-1 mb-1"></div>
                            <div class="overflow-auto mb-1 background-search" style="height: 180px !important">
                                <ul id="myULLoad">
                                    <asp:Repeater runat="server" ID="rptLoad">
                                        <ItemTemplate>
                                            <label>
                                                <li class="li-list float-left"><a class="search-list float-left" id='<%# Eval("ID") %>'>
                                                    <asp:HiddenField runat="server" ID="hfPriorityID" Value='<%# Eval("ID") %>' />
                                                    <asp:CheckBox runat="server" ID='chkIDLoad' CssClass="form-control border-none float-left chkAD" />
                                                    <div class="ml-3 pl-2 float-left"><%# Eval("ADNO") %></div>
                                                </a></li>
                                            </label>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <asp:Button runat="server" ID="btnFinalizeLoad" CssClass="btn btn-block btn-primary custom-display" Text="Finalize" OnClick="btnFinalizeLoad_Click" />
                            <div class="dropdown">
                                <div class="dropdown-content">
                                    <div class="bulkMove">
                                        <asp:TextBox runat="server" ID="txtLoadBulk" Placeholder="Digits" TextMode="Number" CssClass="border-none custom-bulk" Width="70px" Height="25px"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="ddlLoadBulk" CssClass="border-none custom-bulk" Width="120px" Height="25px">
                                            <asp:ListItem Value="1" Text="Priority"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Normal"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Reserve"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button runat="server" ID="btnLoadBulkMove" Text="Bulk Move" CssClass="btn btn-block btn-primary border-none custom-bulk" Height="25px" Width="120px" OnClick="btnLoadBulkMove_Click" />
                                    </div>
                                    <hr />

                                    <asp:Button runat="server" Text="Priority" CssClass="btn btn-block btn-primary custom-select" ID="btnPriorityLoad" OnClick="btnPriorityLoad_Click" />
                                    <asp:Button runat="server" Text="Normal" CssClass="btn btn-block btn-primary custom-select" ID="btnNormalLoad" OnClick="btnNormalLoad_Click" />
                                    <asp:Button runat="server" Text="Reserve" CssClass="btn btn-block btn-primary custom-select" ID="btnReserveLoad" OnClick="btnReserveLoad_Click" />
                                </div>
                                <button type="button" class="btn btn-block btn-primary custom-display">Move</button>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-center form-bg-image">
                        <div class="col-12 d-flex align-items-center justify-content-center">
                            <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-900">
                                <div class="text-center text-md-center mb-4 mt-md-0">
                                    <h1 class="mb-0 h3">Manifest</h1>
                                </div>
                                <form action="#">
                                    <div class="row">
                                        <div class="col">
                                            <label for="email">Manifest No <span class="required">*</span></label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtManifestNo" CssClass="form-control border-none" placeholder="Manifest No" AutoPostBack="true" OnTextChanged="txtManifestNo_TextChanged"></asp:TextBox>
                                            </span>
                                            <asp:RequiredFieldValidator ID="rf1" runat="server" ForeColor="Red" ErrorMessage="Required!" ControlToValidate="txtManifestNo"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col">
                                            <label for="email">Transport</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlTransport" CssClass="form-control border-none" OnSelectedIndexChanged="ddlTransport_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:HiddenField ID="hfddlTransportDetails" runat="server" />
                                            </span>
                                            <a style="font-size: 12px; color: rgb(46 54 80 / 90%); margin-left: 5px" href="TransportDetails">Add Transport Details</a>
                                        </div>
                                        <div class="col d-none">
                                            <label for="email">Transport Type</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlTransportType" CssClass="form-control border-none" Enabled="false"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">City</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control border-none" Enabled="false"></asp:DropDownList>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="row mt-4">

                                        <div class="col">
                                            <label for="email">Date</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtDate" for="ContentPlaceHolder1_hfDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfDate"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfDate" Value="" />
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">FN/AN</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlSession" CssClass="form-control border-none" Enabled="false">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="FN"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="AN"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">No Of Seats</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtNoOfSeats" CssClass="form-control border-none" placeholder="0"></asp:TextBox>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="row mt-4 mb-3">


                                        <div class="col d-none">
                                            <label for="email">Prior Seats</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtPrioritySeats" CssClass="form-control border-none" placeholder="0"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="col d-none">
                                            <label for="email">Load Seats</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtLoad" CssClass="form-control border-none" placeholder="0"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">Total Seats</label>
                                            <span class="icon-arrange">
                                                <asp:Label runat="server" CssClass="form-control border-none" ID="lblTotalSeats"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">Seats Left</label>
                                            <span class="icon-arrange">
                                                <asp:Label runat="server" CssClass="form-control border-none" ID="lblSeatsinfo"></asp:Label>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="w-100 float-left">
                                        <asp:Button ID="btnPrint" Text="Print Manifest" CssClass="float-right btn w-25 btn-block btn-primary ml-1" runat="server" OnClick="btnPrint_Click" />
                                        <asp:Button ID="btnDeleteManifest" Text="Reverse Manifest" CssClass="float-right w-25 btn btn btn-primary" OnClick="btnDeleteManifest_Click" runat="server" />
                                    </div>
                                    <div class="d-flex justify-content-between align-items-top mb-3 pl-2 pr-2">
                                        <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </div>

                                </form>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="container">
                            <div id="PrintDiv" runat="server">
                                <div class="grdHeading  mt-2">
                                    <h4>Normal</h4>
                                </div>
                                <div class="table-container">
                                    <div class="table-wrp">
                                        <div class="table-responsive table-grid float-left w-100 shadow-soft border rounded border-light bg-white">
                                            <asp:GridView runat="server" CssClass="table table-striped border-none table-grid-one" ID="grdUser" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="grdUser_RowDeleting" OnRowCommand="grdUser_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%#Eval("ADNO") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Army No">
                                                        <ItemTemplate>
                                                            <%#Eval("ArmyNo") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rank">
                                                        <ItemTemplate>
                                                            <%#Eval("Rank") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%#Eval("Name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Name">
                                                        <ItemTemplate>
                                                            <%#Eval("UnitName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Headquarter">
                                                        <ItemTemplate>
                                                            <%#Eval("HQName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ICard">
                                                        <ItemTemplate>
                                                            <%#Eval("ICard") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <span>
                                                                <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this entry?');" Text="Reverse" />
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No data to display
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row d-none" style="width: 72%; float: right;">
                        <div class="container">
                            <div id="printDivReserve" runat="server">
                                <div class="grdHeading mt-2">
                                    <h4>Reserve</h4>
                                </div>
                                <div class="table-container ">
                                    <div class="table-wrp">
                                        <div class="table-responsive table-grid float-left w-100 shadow-soft border rounded border-light bg-white">
                                            <asp:GridView runat="server" CssClass="table table-striped border-none table-grid-one" ID="grdReserve" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="grdReserve_RowDeleting" OnRowCommand="grdReserve_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%#Eval("ADNO") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Army No">
                                                        <ItemTemplate>
                                                            <%#Eval("ArmyNo") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rank">
                                                        <ItemTemplate>
                                                            <%#Eval("Rank") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%#Eval("Name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Name">
                                                        <ItemTemplate>
                                                            <%#Eval("UnitName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Headquarter">
                                                        <ItemTemplate>
                                                            <%#Eval("HQName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ICard">
                                                        <ItemTemplate>
                                                            <%#Eval("ICard") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <span>
                                                                <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this entry?');" Text="Reverse" />
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No data to display
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <asp:Button runat="server" ID="btnPrintReserve" CssClass="float-right w-25 btn btn btn-primary mt-2" OnClick="btnPrintReserve_Click" Text="Print Reserve"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
    <script type="text/javascript">
        function myFunctionPriority() {
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById("myInputPriority");
            filter = input.value.toUpperCase();
            ul = document.getElementById("myULPriority");
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

        function myFunctionGerenral() {
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById("myInputGerenral");
            filter = input.value.toUpperCase();
            ul = document.getElementById("myULGeneral");
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

        function myFunctionLoad() {
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById("myInputLoad");
            filter = input.value.toUpperCase();
            ul = document.getElementById("myULLoad");
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

        function myFunctionReserve() {
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById("myInputReserve");
            filter = input.value.toUpperCase();
            ul = document.getElementById("myULReserve");
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
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlTransport').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfddlTransportDetails').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlTransport').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfddlTransportDetails').val(id);
            })
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlCatPriority').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCatPriority').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlCatPriority').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCatPriority').val(id);
            })
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlCatReserve').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCatReserve').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlCatReserve').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCatReserve').val(id);
            })
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlCatGeneral').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCatGeneral').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlCatGeneral').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCatGeneral').val(id);

            })
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlCityPriority').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityPriority').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlCityPriority').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityPriority').val(id);

            })
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlCityGeneral').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityGeneral').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlCityGeneral').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityGeneral').val(id);

            })
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlCityReserve').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityReserve').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlCityReserve').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityReserve').val(id);

            })
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_ddlCityLoad').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityLoad').val(id);
            })
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('#ContentPlaceHolder1_ddlCityLoad').change(function () {
                var id = $(this).val();
                $('#ContentPlaceHolder1_hfCityLoad').val(id);

            })
        });
    </script>
    <script>
        function openCity(tabName) {
            var printDiv = document.getElementById("PrintDiv");
            var printDivReseve = document.getElementById("printDivReserve");
            if (tabName.value == "PrintDiv") {
                printDiv.style.display = "block";
                printDivReseve.style.display = "none";
            }
            else {
                printDiv.style.display = "none";
                printDivReseve.style.display = "block";
            }
        }
    </script>
</asp:Content>
