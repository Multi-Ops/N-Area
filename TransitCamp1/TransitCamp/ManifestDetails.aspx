<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ManifestDetails.aspx.cs" Inherits="TransitCamp.ManifestDetails" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-custo {
            background: transparent;
            border: none;
            color: #f40000;
            font-size: 16px;
            text-transform: uppercase;
            margin: -1px 0px 0px 5px;
            font-weight: bolder;
        }

        .btn-custom {
            background: transparent;
            border: none;
            text-transform: uppercase;
            color: #262B40;
            float: left;
            padding: 0px 0px 0px 10px;
            font-weight: bolder;
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
                            <h3 class="mt-5">Manifest Details</h3>

                            <div class="float-left pt-4 pl-1"><a href="Manifest"><b>Add</b></a></div>
                            <div class="float-left pt-4 pl-1">
                                <asp:Button runat="server" Text="Print" CssClass="btn-custom" ID="btnPrint" OnClick="btnPrint_Click" />
                            </div>
                            <div class="float-left pt-4 pl-1">
                                <asp:Button runat="server" Text="Reverse Manifest" CssClass="btn-custo" ID="btnReverseManifest" OnClick="btnReverseManifest_Click" OnClientClick="return confirm('Do you want to delete this entry?');" />
                            </div>
                            <div class="float-right w-25 pr-1">
                                <div class="form-outline">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search"></asp:TextBox>
                                    <i class="fas fa-search float-right" runat="server" onclick="btnSearchIcon_Click"></i>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary float-right fas fa-search" OnClick="btnSearch_Click" />
                                </div>
                            </div>

                            <div class="table-responsive table-grid float-left w-100">

                                <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdManifest" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="grdManifest_RowDeleting" OnRowCommand="grdManifest_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Army No">
                                            <ItemTemplate>
                                                <%# Eval("ArmyNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Rank">
                                            <ItemTemplate>
                                                <%#Eval("Rank") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblOfficer_Dependent" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Name">
                                            <ItemTemplate>
                                                <%#Eval("Name") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblJCO_Dependent" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Unit">
                                            <ItemTemplate>
                                                <%#Eval("UnitName") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblOR_Dependent" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="ADNO">
                                            <ItemTemplate>
                                                <%#Eval("ADNO") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FMN">
                                            <ItemTemplate>
                                                <%#Eval("FMN") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="REMARKS">
                                            <ItemTemplate>
                                                <%# Convert.ToBoolean(Eval("IsPriority")) ? "Priority" : "Normal" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-primary grid-btn" CommandName="Reverse" Text="Reverse" />
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

