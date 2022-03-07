<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AddBooking.aspx.cs" Inherits="TransitCamp.Bookings.AddBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <div class="row justify-content-center form-bg-image">
                <div class="col-12 d-flex align-items-center justify-content-center">
                    <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                        <div class="text-center text-md-center mb-4 mt-md-0">
                            <h1 class="mb-0 h3">Add Room</h1>
                        </div>
                        <form action="#">
                            <div class="row">
                                <%--                  <div class="col">
                                    <label for="email">Checkout Date</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtDate" for="ContentPlaceHolder1_hfDate" placeholder="Checkout Date" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfDate"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfDate" Value="" />
                                    </span>
                                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator4" ErrorMessage="Required!" ForeColor="Red" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="col">
                                    <label for="email">Room Selection</label>
                                    <span class="icon-arrange">

                                        <a class="form-control select-room" data-toggle="modal" data-target="#ad">Select Rooms
                                            <%--<asp:TextBox ID="txtSelectRooms" runat="server" Enabled="false"></asp:TextBox>--%></a>

                                        <div class="modal fade" id="ad" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                            <div class="modal-dialog" role="document">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title">AD</h5>
                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <asp:CheckBoxList runat="server" ID="chkSelectRooms"></asp:CheckBoxList>
                                                        <asp:Button class="btn btn-primary ml-2 mr-2 custom" runat="server" ID="btnRoomSelection" Text="Select"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </span>
                                </div>
                            </div>
                            <%--<div class="row mt-4">
                                <div class="col">
                                    <label for="email">Max Count</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtMaxCount" CssClass="form-control border-none" placeholder="Max Count"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtMaxCount" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMaxCount" ValidationExpression='^[1-9]\d*(\.\d+)?$' ErrorMessage="Decimal Value Only" ForeColor="Red" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col">
                                    <label for="email">Room Price</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtRoomPrice" CssClass="form-control border-none" placeholder="Room Price"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtRoomPrice" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtRoomPrice" ValidationExpression='^[1-9]\d*(\.\d+)?$' ErrorMessage="Decimal Value Only" ForeColor="Red" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                            </div>--%>

                            <div class="d-flex justify-content-between align-items-top mb-3">
                                <asp:CheckBox For="email" ID="chkISFly" runat="server" CssClass="CheckBox mt-4" Text="Will Fly" />
                                <asp:CheckBox For="email" ID="chkIsLRC" runat="server" CssClass="CheckBox mt-4" Text="Has LRC" />
                                <%--<asp:CheckBox For="email" ID="chkIsShare" runat="server" CssClass="CheckBox mt-4" Text="Is Share" />--%>

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
    </script>

</asp:Content>
