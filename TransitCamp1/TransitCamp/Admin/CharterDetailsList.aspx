<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CharterDetailsList.aspx.cs" Inherits="TransitCamp.Admin.CharterDetailsList" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4">
        <div class="row">
            <div class="table-container">

                <div class="table-wrp">
                    <h3 class="mt-5">Charter Detail List</h3>

                    <div class="w-25 float-left pt-4 pl-1"><a href="AddCharterDetails"><b>Add</b></a></div>

                    <div class="float-right w-25 pr-1">
                        <div class="form-outline">
                            <asp:TextBox ID="txtSearch" ReadOnly="true" runat="server" CssClass="form-control form_datetime" placeholder="Search" data-date-format="dd-mm-yyyy" data-link-field="ContentPlaceHolder1_hfsearchdate"></asp:TextBox>
                            <i class="fas fa-search float-right" runat="server" onclick="btnSearchIcon_Click"></i>
                            <asp:HiddenField ID="hfsearchdate" runat="server" Value="" />
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary float-right fas fa-search" OnClick="btnSearch_Click" />
                        </div>
                    </div>

                    <div class="table-responsive table-grid float-left w-100">

                        <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdUser" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="grdUser_RowDeleting" OnRowCommand="grdUser_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Airline">
                                    <ItemTemplate>
                                        <%#Eval("AirLineName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Charter No">
                                    <ItemTemplate>
                                        <%#Eval("CharterNo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flight No">
                                    <ItemTemplate>
                                        <%#Eval("FlightNo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City">
                                    <ItemTemplate>
                                        <%#Eval("FromCity") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To City">
                                    <ItemTemplate>
                                        <%#Eval("ToCity") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chartered Date">
                                    <ItemTemplate>
                                        <%#Eval("CharteredDate") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FN/AN">
                                    <ItemTemplate>
                                        <%#Eval("Session") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Of Seats">
                                    <ItemTemplate>
                                        <%#Eval("NumberOfSeats") %>
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
    <script type="text/javascript">
        $(function () {
            $('.form_datetime').datetimepicker({
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
