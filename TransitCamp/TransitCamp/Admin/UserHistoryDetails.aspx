<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UserHistoryDetails.aspx.cs" Inherits="TransitCamp.Admin.UserHistoryDetails" %>

<%@ Register Src="~/UserControl/Paging.ascx" TagPrefix="uc1" TagName="Paging" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        i.fas.fa-search {
            top: -28px !important;
            left: -20px !important;
        }

        #ContentPlaceHolder1_btnPrint {
            float: right !important;
            width: 10% !important;
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
                            <div id="PrintDiv" runat="server">
                                <h3 class="mt-5">User History Details For
                                <asp:Label runat="server" ID="lblName"></asp:Label></h3>
                                <div class="table-responsive table-grid float-left w-100">

                                    <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdUser" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDeleting="grdUser_RowDeleting" OnRowCommand="grdUser_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S. No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AD">
                                                <ItemTemplate>
                                                    <%#Eval("ADNO") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Army No">
                                                <ItemTemplate>
                                                    <%#Eval("ArmyNumber") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank">
                                                <ItemTemplate>
                                                    <%#Eval("Rank") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <%#Eval("Name") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <%#Eval("UnitName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID Card">
                                                <ItemTemplate>
                                                    <%#Eval("IDCardNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting Date">
                                                <ItemTemplate>
                                                    <%#Eval("CreatedOn","{0:dd/MM/yyyy}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FN/AN">
                                                <ItemTemplate>
                                                    <%#Eval("Session") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Headquarter">
                                                <ItemTemplate>
                                                    <%#Eval("HQName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <%#Eval("DDate","{0:dd/MM/yyyy}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FN/AN">
                                                <ItemTemplate>
                                                    <%#Eval("DSession") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <%#Eval("FlightNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No data to display
                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                </div>
                            </div>
                            <div class="float-left pl-1">
                                <uc1:Paging runat="server" ID="Paging" />
                            </div>
                        </div>
                    </div>
                    <div>
                        <br />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-block btn-primary" OnClientClick="if(confirm('Are you sure you want to Print this data?')==true) { return PrintPanel();} else{}" />
                        <div class="cf"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
    <script>
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintDiv.ClientID %>");
            var printWindow = window.open("", '_blank');
            printWindow.document.write('<html><head><title></title><link type="text/css" href="/Content/css/Print.css" rel="stylesheet">');
            printWindow.document.write('</head><body class="print-body"><center class="Print-Center"><div style="top: 2%; left: 70%;"></div>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</center></body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
