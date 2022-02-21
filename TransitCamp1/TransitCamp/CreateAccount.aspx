<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="TransitCamp.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>

        <!-- Section -->
        <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
            <div class="container">
                <div class="row justify-content-center form-bg-image">
                    <div class="col-12 d-flex align-items-center justify-content-center">
                        <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                            <div class="text-center text-md-center mb-4 mt-md-0">
                                <h1 class="mb-0 h3">Create an account</h1>
                            </div>
                            <form action="#">
                                <div class="row">
                                    <div class="col">
                                        <label for="email">Name</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control border-none" placeholder="Name"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="rqfName" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtName" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">User Name</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control border-none" placeholder="User Name"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtUserName" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col">
                                        <label for="email">ID Card Number</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtCardNumber" CssClass="form-control border-none" placeholder="ID Card Number"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtCardNumber" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">Army Number</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtArmyNumber" CssClass="form-control border-none" placeholder="Army Number"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtArmyNumber" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col">
                                        <label for="email">Rank</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtRank" CssClass="form-control border-none" placeholder="Rank"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtRank" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">Regiment</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtRegiment" CssClass="form-control border-none" placeholder="Regiment"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtRegiment" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <%--                                <div class="row mt-4">
                                    <div class="col">
                                        <label for="email">Password</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control border-none" placeholder="Password"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtPassword" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <label for="email">Confirm Password</label>
                                        <span class="icon-arrange">
                                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control border-none" placeholder="Confirm Password"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="requiredgroup" runat="server" Display="Dynamic" ErrorMessage="Required Field!" ControlToValidate="txtConfirmPassword" ForeColor="Red" CssClass="required-filed-validator"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="rqfComparePassword" runat="server" ForeColor="Red" ControlToCompare="txtPassword" ErrorMessage="Password not match!" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                                    </div>
                                </div>--%>
                                <div class="d-flex justify-content-between align-items-top mb-3">
                                    <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                </div>
                                <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-primary" Text="Register" runat="server" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnUpdate" Visible="false" CssClass="btn btn-block btn-primary" Text="Update" Enabled="false" runat="server" OnClick="btnUpdate_Click" />
                            </form>

                            <div class="d-flex justify-content-center align-items-center mt-4">
                                <span class="font-weight-normal">Already have an account?
                                   
                                    <a href="Login" class="font-weight-bold">Login here</a>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
</asp:Content>


