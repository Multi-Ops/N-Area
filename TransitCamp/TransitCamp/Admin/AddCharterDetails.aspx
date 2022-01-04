<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AddCharterDetails.aspx.cs" Inherits="TransitCamp.Admin.AddCharterDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <div class="row justify-content-center form-bg-image">
                <div class="col-12 d-flex align-items-center justify-content-center">
                    <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                        <div class="text-center text-md-center mb-4 mt-md-0">
                            <h1 class="mb-0 h3">Add Charter Details</h1>
                        </div>
                        <form action="#">
                            <div class="row">
                                <div class="col">
                                    <label for="email">Airline Name</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList runat="server" ID="ddlAirline" CssClass="form-control border-none"></asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col">
                                    <label for="email">Charter No</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtCharterNo" CssClass="form-control border-none" placeholder="Charter No"></asp:TextBox>
                                    </span>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col">
                                    <label for="email">From City</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList runat="server" ID="ddlFromCity" CssClass="form-control border-none"></asp:DropDownList>
                                    </span>
                                    <asp:CompareValidator
                                        ID="cv1" Operator="NotEqual" runat="server"
                                        ValidationGroup="Validate" ControlToValidate="ddlFromCity" Display="Dynamic"
                                        ControlToCompare="ddlToCity" ForeColor="Red" ErrorMessage="Cities Should be Different." SetFocusOnError="true">
                                    </asp:CompareValidator>
                                </div>
                                <div class="col">
                                    <label for="email">To City</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList runat="server" ID="ddlToCity" CssClass="form-control border-none"></asp:DropDownList>
                                    </span>
                                    <asp:CompareValidator
                                        ID="CompareValidator1" Operator="NotEqual" runat="server"
                                        ValidationGroup="Validate" ControlToValidate="ddlToCity" Display="Dynamic"
                                        ControlToCompare="ddlFromCity" ForeColor="Red" ErrorMessage="Cities Should be Different." SetFocusOnError="true">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col">
                                    <label for="email">Charter Date</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtCharterDate" for="ContentPlaceHolder1_hfCharterDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfCharterDate"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfCharterDate" Value="" />
                                    </span>
                                </div>
                            </div>
                            <div class="row mt-4 mb-3">
                                <div class="col">
                                    <label for="email">Flight No</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtFlightNo" CssClass="form-control border-none" placeholder="Flight No"></asp:TextBox>
                                    </span>
                                </div>
                                <div class="col">
                                    <label for="email">No Of Seats</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtNoOfSeats" CssClass="form-control border-none" placeholder="No Of Seats"></asp:TextBox>
                                    </span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        ControlToValidate="txtNoOfSeats" runat="server"
                                        ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="d-flex justify-content-between align-items-top mb-3">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
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
            $('.form_datetime').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 1,
            });
        });
    </script>
</asp:Content>
