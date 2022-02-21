<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="TransitCamp.Admin.UserList" %>

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
                            <h3 class="mt-5">User List</h3>

                            <div class="w-25 float-left pt-4 pl-1"><a href="CreateAccount"><b>Add</b></a></div>
                            <div class="float-right custom-w pr-1">
                                <div class="form-outline float-right">
                                    <div class="dis">
                                        <asp:DropDownList runat="server" ID="ddlsearch" CssClass="form-control float-left display-inine">
                                            <asp:ListItem Value="0" Text="--Select To Search--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Name Wise"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Army No Wise"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Icard Wise"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="dis">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search"></asp:TextBox>
                                        <i class="fas fa-search float-right" runat="server" style="font-size: 12px" onclick="btnSearchIcon_Click"></i>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary float-right customeditbtn" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive table-grid float-left w-100">

                                <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdUser" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="grdUser_RowDeleting" OnRowCommand="grdUser_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <%#Eval("Name") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID Card">
                                            <ItemTemplate>
                                                <%#Eval("IDCardNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Army Number">
                                            <ItemTemplate>
                                                <%#Eval("ArmyNumber") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank">
                                            <ItemTemplate>
                                                <%#Eval("Rank") %>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
</asp:Content>
