<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="FamilyEntry.aspx.cs" Inherits="TransitCamp.Admin.FamilyEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <div class="container">
            <div class="row justify-content-center form-bg-image">
                <div class="col-12 d-flex align-items-center justify-content-center">
                    <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-800">
                        <div class="text-center text-md-center mb-4 mt-md-0">
                            <h1 class="mb-0 h3">Family Entry</h1>
                        </div>
                        <form action="#">
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
                                <div class="col w-75">
                                    <label for="email">Date</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtDate" for="ContentPlaceHolder1_hfDate" TextMode="DateTime" ReadOnly="true" CssClass="form-control date form_datetime border-none" data-date-format="dd-mm-yyyy - HH:ii p" data-link-field="ContentPlaceHolder1_hfDate"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hfDate" Value="" />
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtDate" runat="server" Display="Dynamic" ID="RequiredFieldValidator5" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col w-25">
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
                                    <label for="email">Family Name</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtFamilyName" CssClass="form-control border-none" placeholder="Family Name"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtFamilyName" runat="server" Display="Dynamic" ID="RequiredFieldValidator6" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col">
                                    <label for="email">Age</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtage" CssClass="form-control border-none" placeholder="Age"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtage" runat="server" Display="Dynamic" ID="RequiredFieldValidator7" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                        ControlToValidate="txtage" runat="server"
                                        ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col">
                                    <label for="email">Sex</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList runat="server" ID="ddlSex" CssClass="form-control border-none">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Male"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col">
                                    <label for="email">Priority No</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtPriorityNo" CssClass="form-control border-none" placeholder="Priority No"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtPriorityNo" runat="server" Display="Dynamic" ID="RequiredFieldValidator9" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col">
                                    <label for="email">Relation</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtRelation" CssClass="form-control border-none" placeholder="Relation"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtRelation" runat="server" Display="Dynamic" ID="RequiredFieldValidator10" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col">
                                    <label for="email">No Of Luggage</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtNoOfLuggae" CssClass="form-control border-none" placeholder="No Of Luggage"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtNoOfLuggae" runat="server" Display="Dynamic" ID="RequiredFieldValidator11" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        ControlToValidate="txtNoOfLuggae" runat="server"
                                        ErrorMessage="Only Numbers allowed" Display="Dynamic" ForeColor="Red"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="col">
                                    <label for="email">I Card No</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtICard" CssClass="form-control border-none" placeholder="I card No"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtICard" runat="server" Display="Dynamic" ID="rfv1" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col">
                                    <label for="email">Army No</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtArmyNo" CssClass="form-control border-none" placeholder="Army No"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtArmyNo" runat="server" Display="Dynamic" ID="RequiredFieldValidator1" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mt-4 mb-3">
                                <div class="col">
                                    <label for="email">Rank</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList runat="server" ID="ddlRank" CssClass="form-control border-none"></asp:DropDownList>
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
                                    <label for="email">Unit</label>
                                    <span class="icon-arrange">
                                        <asp:DropDownList runat="server" ID="ddlUnit" CssClass="form-control border-none"></asp:DropDownList>
                                    </span>
                                </div>
                            </div>
                            <div class="row mt-4 mb-3">
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
                                    <label for="email">Authority</label>
                                    <span class="icon-arrange">
                                        <asp:TextBox runat="server" ID="txtAut" CssClass="form-control border-none" placeholder="Authority"></asp:TextBox>
                                    </span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtAut" runat="server" Display="Dynamic" ID="RequiredFieldValidator3" ForeColor="Red" ErrorMessage="Rquired!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mt-4 mb-3">
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
                            <div class="d-flex justify-content-between align-items-top mb-3 pl-2 pr-2">
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
