<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UserHistory.aspx.cs" Inherits="TransitCamp.Admin.UserHistory" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        i.fas.fa-search {
            top: -28px !important;
            left: -20px !important;
        }
    </style>
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
                            <h3 class="mt-5">User History List</h3>

                            <div class="float-right custom-w pr-1">
                                <div class="form-outline float-right">
                                    <div class="dis">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search"></asp:TextBox>
                                        <i class="fas fa-search float-right" runat="server" style="font-size: 12px" onclick="btnSearchIcon_Click"></i>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary float-right customeditbtn" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive table-grid float-left w-100">

                                <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdUser" DataKeyNames="ArmyNo" AutoGenerateColumns="false" OnRowDeleting="grdUser_RowDeleting" OnRowCommand="grdUser_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S. No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AD">
                                            <ItemTemplate>
                                                <%#Eval("ADNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Army Number">
                                            <ItemTemplate>
                                                <%#Eval("ArmyNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ICard">
                                            <ItemTemplate>
                                                <%#Eval("ICardNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Button ID="ShowDetails" runat="server" CssClass="btn btn-primary grid-btn" CommandName="ShowDetails" Text="Details" />
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
</asp:Content>
