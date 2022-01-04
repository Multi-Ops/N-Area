<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ADListOnTempHold.aspx.cs" Inherits="TransitCamp.Admin.ADListOnTempHold" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4">
        <div class="row">
            <div class="table-container">

                <div class="table-wrp">
                    <h3 class="mt-5">Cancel AD</h3>

                    <div class="float-left pt-4 pl-1"><a href="ADEntery"><b>Add</b></a></div>
                    <div class="float-left pt-4 pl-4"><a href="ADList"><b>AD List</b></a></div>
                    <div class="float-left pt-4 pl-4"><a href="ADListReserve"><b>Reserve</b></a></div>
                    <div class="float-left pt-4 pl-4"><a href="ADListPriorityWise"><b>Priority</b></a></div>
                    <div class="float-left pt-4 pl-4"><a href="ADListOnTempHold"><b>Cancel AD List</b></a></div>
                    <div class="float-left pt-4 pl-4"><a href="ADOnHoldStatusList"><b>On Hold Status</b></a></div>
                    <div class="float-left pt-4 pl-4"><a href="ADIsLoadList"><b>Load</b></a></div>


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
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        S No.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AD No">
                                    <ItemTemplate>
                                        <%#Eval("ADNO") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ArmyNo">
                                    <ItemTemplate>
                                        <%#Eval("ArmyNo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate>
                                        <%#Eval("RankName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%#Eval("Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ICard">
                                    <ItemTemplate>
                                        <%#Eval("ICard") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Session">
                                    <ItemTemplate>
                                        <%#Eval("Session") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Headquarter">
                                    <ItemTemplate>
                                        <%#Eval("HQName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <%#Eval("CategoryName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <%#Eval("CityName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <%#Eval("OnTemHoldRemark") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <%#Eval("Date") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%--                             <span>
                                            <asp:Button ID="EditButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Edit" Text="Edit" />
                                        </span>--%>
                                        <%--                  <span>
                                            <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this entry?');" Text="Delete" />
                                        </span>--%>
                                        <span>
                                            <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Cancel" Text="Cancel" />
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

