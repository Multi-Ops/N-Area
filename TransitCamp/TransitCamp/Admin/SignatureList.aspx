﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="SignatureList.aspx.cs" Inherits="TransitCamp.Admin.SignatureList" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4">
        <div class="row">
            <div class="table-container">

                <div class="table-wrp">
                    <h3 class="mt-5">Signature List</h3>

                    <div class="w-25 float-left pt-4 pl-1"><a href="AddSignature"><b>Add</b></a></div>

                    <div class="float-right w-25 pr-1">
                        <div class="form-outline">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search"></asp:TextBox>
                            <i class="fas fa-search float-right" runat="server" onclick="btnSearchIcon_Click"></i>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary float-right fas fa-search" OnClick="btnSearch_Click" />
                        </div>
                    </div>

                    <div class="table-responsive table-grid float-left w-100">

                        <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdUser" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="grdUser_RowDeleting" OnRowCommand="grdUser_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%#Eval("SignatureName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Headquarter">
                                    <ItemTemplate>
                                        <%#Eval("HeadQuarterName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate>
                                        <%#Eval("RankName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <%#Eval("UnitName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division">
                                    <ItemTemplate>
                                        <%#Eval("DivName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <span>
                                            <asp:Button ID="EditButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Edit" Text="Edit" />
                                        </span>
                                        <span>
                                            <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this entry?');" Text="Delete" />
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
</asp:Content>
