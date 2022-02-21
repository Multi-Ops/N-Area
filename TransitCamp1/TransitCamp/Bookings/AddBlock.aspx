<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AddBlock.aspx.cs" Inherits="TransitCamp.Bookings.AddBlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <div class="row justify-content-center form-bg-image">
                <div class="col-12 d-flex align-items-center justify-content-center">
                    <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                        <div class="text-center text-md-center mb-4 mt-md-0">
                            <h1 class="mb-0 h3">Add Block</h1>
                        </div>
                        <form action="#">
                            <div class="row">
                                <div class="col">
                                    <label for="email">Block</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtBlockName" CssClass="form-control border-none" placeholder="Block Name"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="rqfName" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtBlockName" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col">
                                    <label for="email">Default Price</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtDefaultPrice" CssClass="form-control border-none" placeholder="Default Price"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtDefaultPrice" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="reDefaultPrice" ControlToValidate="txtDefaultPrice" ValidationExpression='^[1-9]\d*(\.\d+)?$' ErrorMessage="Decimal/Int Value Only" ForeColor="Red" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>

                            </div>
                            <div class="row mt-4">
                                <div class="col">
                                    <label for="email">Max Room Availability</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtMaxRoomAvailable" CssClass="form-control border-none" placeholder="Max Room Available"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtMaxRoomAvailable" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMaxRoomAvailable" ValidationExpression='^[1-9]\d*(\.\d+)?$' ErrorMessage="Decimal Value Only" ForeColor="Red" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>
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
</asp:Content>
