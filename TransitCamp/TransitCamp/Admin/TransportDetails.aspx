<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="TransportDetails.aspx.cs" Inherits="TransitCamp.Admin.TransportDetails" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <div class="row justify-content-center form-bg-image">
                <div class="col-12 d-flex align-items-center justify-content-center">
                    <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-600">
                        <div class="text-center text-md-center mb-4 mt-md-0">
                            <h1 class="mb-0 h3">Add Transport Details</h1>
                            <a class="mt-2" href="TransportDetailList">
                                <h7>Transport List</h7>
                            </a>
                        </div>
                        <form action="#">
                            <asp:ScriptManager ID="scriptmanager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="upTransportDetails" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col">
                                            <label for="email">Date</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" ID="txtDate" for="ContentPlaceHolder1_hfDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfDate"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hfDate" Value="" />
                                            </span>
                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator1" ErrorMessage="Required!" ForeColor="Red" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col">
                                            <label for="email">FN/AN</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlSession" CssClass="form-control border-none">
                                                    <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="FN"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="AN"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="row mt-4">
                                        <div class="col">
                                            <label for="email">Transport Type</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlTransportType" CssClass="form-control border-none"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="col">
                                            <label for="email">Transport Name</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtTransportDetails_TextChanged" ID="txtTransportDetails" CssClass="form-control border-none" placeholder="Transport Details"></asp:TextBox>
                                            </span>
                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="rfv" ErrorMessage="Required!" ForeColor="Red" ControlToValidate="txtTransportDetails"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row mt-4">
                                        <div class="col">
                                            <label for="email">City</label>
                                            <span class="icon-arrange">
                                                <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control border-none"></asp:DropDownList>
                                            </span>
                                        </div>

                                        <div class="col">
                                            <label for="email">Total No Of Seats</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtTotalNoOfSeats_TextChanged" ID="txtTotalNoOfSeats" CssClass="form-control border-none" placeholder="Total No Of Seats"></asp:TextBox>
                                            </span>
                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator2" ErrorMessage="Required!" ForeColor="Red" ControlToValidate="txtTotalNoOfSeats"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                ControlToValidate="txtTotalNoOfSeats" runat="server"
                                                ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                                ValidationExpression="\d+">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="row mt-4">
                                        <div class="col">
                                            <label for="email">Priority Seats</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtPrioritySeats_TextChanged" Display="Dynamic" ID="txtPrioritySeats" CssClass="form-control border-none" placeholder="Priority Seats"></asp:TextBox>
                                            </span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                ControlToValidate="txtPrioritySeats" runat="server"
                                                ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                                ValidationExpression="\d+">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col">
                                            <label for="email">Load</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox Display="Dynamic" runat="server" ID="txtLoad" CssClass="form-control border-none" placeholder="Load Seats" AutoPostBack="true" OnTextChanged="txtLoad_TextChanged"></asp:TextBox>
                                            </span>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                                ControlToValidate="txtNoOfSeats" runat="server"
                                                ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                                ValidationExpression="\d+">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col">
                                            <label for="email">No Of Seats</label>
                                            <span class="icon-arrange">
                                                <asp:TextBox Display="Dynamic" runat="server" Enabled="false" ID="txtNoOfSeats" CssClass="form-control border-none" placeholder="No Of Seats"></asp:TextBox>
                                            </span>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                ControlToValidate="txtNoOfSeats" runat="server"
                                                ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                                ValidationExpression="\d+">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="d-flex justify-content-between align-items-top mb-3">
                                        <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btnSave" CssClass="btn btn-block btn-primary" Text="Save" runat="server" OnClick="btnSave_Click" />
                            <asp:Button ID="btnUpdate" CssClass="btn btn-block btn-primary" Text="Update" runat="server" Visible="false" OnClick="btnUpdate_Click" />
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
            var date = new Date();
            date.setDate(date.getDate() + 1);

            $('.form_datetime').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                startDate: new Date(),
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            var date = new Date();
            date.setDate(date.getDate() + 1);

            $('.form_datetime').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
                startDate: new Date(),
            });
        });

        jQuery(function ($) {
            var focusedElementSelector = "";
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_beginRequest(function (source, args) {
                var fe = document.activeElement;
                focusedElementSelector = "";

                if (fe != null) {
                    if (fe.id) {
                        focusedElementSelector = "#" + fe.id;
                    } else {
                        // Handle Chosen Js Plugin
                        var $chzn = $(fe).closest('.chosen-container[id]');
                        if ($chzn.size() > 0) {
                            focusedElementSelector = '#' + $chzn.attr('id') + ' input[type=text]';
                        }
                    }
                }
            });

            prm.add_endRequest(function (source, args) {
                if (focusedElementSelector) {
                    $(focusedElementSelector).focus();
                }
            });
        });

    </script>
</asp:Content>
