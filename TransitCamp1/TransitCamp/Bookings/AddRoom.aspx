<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="TransitCamp.Bookings.AddRoom" %>

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
                                <div class="col">
                                    <label for="email">Select Block</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList runat="server" ID="ddlblock" CssClass="form-control border-none"></asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col">
                                    <label for="email">Room Name</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtRoomName" CssClass="form-control border-none" placeholder="Room Name"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtRoomName" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="row mt-4">
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
                            </div>

                            <div class="d-flex justify-content-between align-items-top mb-3">
                                <asp:CheckBox For="email" ID="chkShareable" runat="server" CssClass="CheckBox mt-4" Text="Is Shareable" />
                                <asp:CheckBox For="email" ID="chkIsBillShare" runat="server" CssClass="CheckBox mt-4" Text="Is Bill Share" />

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
</asp:Content>

