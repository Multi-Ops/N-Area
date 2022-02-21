<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AllReserveManifests.aspx.cs" Inherits="TransitCamp.Admin.AllReserveManifests" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4">
        <asp:ScriptManager ID="scriptmanager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upUser" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="table-container">

                        <div class="table-wrp">
                            <h3 class="mt-5">Reserve Manifest List</h3>

                            <div class="float-left pt-4 pl-1 pr-4"><a href="Manifest"><b>Add</b></a></div>
                            <div class="float-left pt-4 pl-1"><a href="AllManifests"><b>Normal Manifests</b></a></div>
                            <div class="float-right w-25 pr-1">
                                <div class="form-outline">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search" Visible="false"></asp:TextBox>
                                    <asp:TextBox runat="server" CssClass="float-left form-control float-right fromdate custom-position" ID="txtFrom" placeholder="Date" for="ContentPlaceHolder1_hfFromDate" ReadOnly="true" data-date-format="dd-mm-yyyy" data-link-field="ContentPlaceHolder1_hfFromDate"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hfFromDate" Value="" />
                                    <i class="fas fa-search float-right" runat="server" onclick="btnSearchIcon_Click"></i>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary float-right fas fa-search" OnClick="btnSearch_Click" />
                                </div>
                            </div>

                            <div class="table-responsive table-grid float-left w-100">

                                <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdManifest" DataKeyNames="ManifestNo" AutoGenerateColumns="false" OnRowDeleting="grdManifest_RowDeleting" OnRowCommand="grdManifest_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Manifest No">
                                            <ItemTemplate>
                                                <a href="ManifestDetails?ManifestNo=<%#Eval("Manifestno") %>"><%#Eval("Manifestno") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manifest Date">
                                            <ItemTemplate>
                                                <%#Eval("ManifestDate") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transport No">
                                            <ItemTemplate>
                                                <%#Eval("TransportNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City">
                                            <ItemTemplate>
                                                <%#Eval("City") %>
                                                <asp:HiddenField ID="hfcid" runat="server" Value='<%# Bind("CityId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Session">
                                            <ItemTemplate>
                                                <%#Eval("Session") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Details" Text="Details" />
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data to display
                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </div>
                            <div class="float-left pl-1">
                                <uc1:Paging runat="server" ID="Paging" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        $(function () {
            $('.fromdate').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0
            });
        });
        prm.add_endRequest(function () {
            $('.fromdate').datetimepicker({
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0
            });
        });
    </script>
</asp:Content>
