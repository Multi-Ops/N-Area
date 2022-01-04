<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AddCity.aspx.cs" Inherits="TransitCamp.Admin.AddCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <div class="row justify-content-center form-bg-image">
                <div class="col-12 d-flex align-items-center justify-content-center">
                    <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                        <div class="text-center text-md-center mb-4 mt-md-0">
                            <h1 class="mb-0 h3">Add City</h1>
                        </div>
                        <form action="#">
                            <div class="row">
                                <div class="col">
                                    <label for="email">City Name</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtCName" CssClass="form-control border-none" placeholder="City Name"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="rqfName" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtCName" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col">
                                    <label for="email">State Name</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtState" CssClass="form-control border-none" placeholder="State Name"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtState" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col">
                                    <label for="email">Medical Status</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList ID="ddlMedstat" runat="server" CssClass="form-control border-none"></asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col">
                                    <label for="email">Out Logic</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList ID="ddlOL" runat="server" CssClass="form-control border-none"></asp:DropDownList>
                                    </span>
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
