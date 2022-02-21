<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TransitCamp.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main>
        <!-- Section -->
        <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
            <div class="container">
                <%--<p class="text-center"><a href="../dashboard/dashboard.html" class="text-gray-700"><i class="fas fa-angle-left mr-2"></i>Back to homepage</a></p>--%>
                <div class="row justify-content-center form-bg-image" <%--data-background-lg="/Content/images/illustrations/signin.svg"--%>>
                    <div class="col-12 d-flex align-items-center justify-content-center">
                        <div class="bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                            <div class="text-center text-md-center mb-4 mt-md-0">
                                <h1 class="mb-0 h3">Sign In</h1>
                            </div>
                            <form action="#" class="mt-4">
                                <!-- Form -->
                                <div class="form-group mb-4">
                                    <label for="email">User Name</label>
                                    <div class="input-group">
                                        <span class="input-group-text" id="basic-addon1"><span class="fas fa-user"></span></span>
                                        <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rqfUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>

                                </div>
                                <!-- End of Form -->
                                <div class="form-group">
                                    <!-- Form -->
                                    <div class="form-group mb-4">
                                        <label for="password">Password</label>
                                        <div class="input-group">
                                            <span class="input-group-text" id="basic-addon2"><span class="fas fa-unlock-alt"></span></span>
                                            <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="refPassword" runat="server" Display="Dynamic" ControlToValidate="txtPassword" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                    </div>
                                    <!-- End of Form -->
                                    <div class="d-flex justify-content-between align-items-top mb-3">
                                        <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                    </div>

                                    <div class="d-flex justify-content-between align-items-top mb-3">
                                        <div class="form-check">
                                            <asp:CheckBox runat="server" ID="chkRemember" CssClass="form-check-box mr-2" />
                                            <label class="form-check-label mb-0" for="remember">
                                                Remember me
                                            </label>
                                        </div>
                                        <div><a href="./forgot-password.html" class="small text-right">Lost password?</a></div>
                                    </div>
                                </div>
                                <asp:Button ID="btnlogin" CssClass="btn btn-block btn-primary" Text="Sign In" runat="server" OnClick="btnlogin_Click" />
                            </form>
                            <div class="d-flex justify-content-center align-items-center mt-4">
                                <span class="font-weight-normal">Not registered?
                                   
                                <a href="CreateAccount" class="font-weight-bold">Create account</a>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
