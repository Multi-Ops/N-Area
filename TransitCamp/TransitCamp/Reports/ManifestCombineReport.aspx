<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ManifestCombineReport.aspx.cs" Inherits="TransitCamp.Reports.ManifestCombineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .head-text {
            Font-size: 16px !important;
            text-align: center;
            margin: 12px 0 -24px 0px;
            color: #fff;
            background: #262B40;
            padding: 12px 0 10px 0px;
        }

        .btn {
            width: 8% !important;
            float: right;
            margin: 0 15px 0 0px;
        }

        th, td {
            text-align: center !important;
        }

        .txt-box-area {
            float: right;
            width: 100% !important;
            margin: 10px 0px 0px 0px !important;
            padding: 0px 0px 0px 0px;
        }

        .txt-search {
            width: 18% !important
        }

        .tbtblnc {
            width: 18% !important;
            margin: 5px 0px 0px 5px;
            background: white;
            display: none;
        }

        .sign {
            display: none
        }

        .declare {
            display: none
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="overflow-hidden">
        <div class="row">
            <div class="table-container">
                <div class="table-wrp">
                    <div id="PrintDiv" runat="server">
                        <div class="head-text">
                            <div class="Centre">
                                MANIFEST NO :
                                    <asp:Label runat="server" ID="lblManifestNo"></asp:Label><br />
                                <br />
                            </div>
                            <div class="align">
                                MANIFEST OF TRANSIENTS MOV
                                <asp:Label runat="server" ID="lblTransportTypeName"></asp:Label>
                                FROM
                                        <asp:Label runat="server" ID="lblCamp"></asp:Label>
                                (CHANDIGARH -
                                    <asp:Label runat="server" ID="lblCity"></asp:Label>
                                )
                            </div>
                            <br />
                            <div class="align1">
                                FROM FLT / VEHICLE NO
                                    <asp:Label runat="server" ID="lblTransportDetails"></asp:Label>
                                :
                                    <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </div>
                            <br />
                            <br />
                        </div>
                        <div class="table-responsive table-grid">
                            <asp:GridView runat="server" ShowFooter="true" CssClass="table table-striped table-grid-one grid" ID="grdManifestReport" AutoGenerateColumns="false" OnRowCreated="grd_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="ADNO">
                                        <ItemTemplate>
                                            <%#Eval("ADNO") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Army No">
                                        <ItemTemplate>
                                            <%# Eval("ArmyNo") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Rank">
                                        <ItemTemplate>
                                            <%# Eval("Rank") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Name">
                                        <ItemTemplate>
                                            <%#Eval("Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Unit">
                                        <ItemTemplate>
                                            <%#Eval("UnitName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Move Type">
                                        <ItemTemplate>
                                            <%#Eval("MoveName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="I Card No">
                                        <ItemTemplate>
                                            <%# Eval("ICard") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Reporting Date">
                                        <ItemTemplate>
                                            <%#Eval("Date", "{0:dd/MM/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <%--             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FN/AN">
                                        <ItemTemplate>
                                            <%#Eval("Session") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FMN">
                                        <ItemTemplate>
                                            <%#Eval("FMN") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="REMARKS">
                                        <ItemTemplate>
                                            <%--<%# Convert.ToBoolean(Eval("IsPriority")) ? "Priority" : "Normal" %>--%>
                                            <%# Convert.ToBoolean(Eval("IsPriority")) ? "Priority" : Convert.ToBoolean(Eval("IsLoad")) ? "Load" :  Convert.ToBoolean(Eval("IsReserve")) ? "Reserve" : "Normal" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No data to display
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>

                        <div class="declare">
                            Certified that the baggage of passenger/Army load list in the manifest have been checked by the undersigned and there is no such item the baggage or on the person which can be used sabotage/hijacking of aircraft
                        </div>
                        <div runat="server" id="divTbl">
                            <table class="table tbtblnc">
                                <tr class="total">
                                    <td>Officer</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblOfficer"></asp:Label></td>
                                </tr>

                                <tr class="total">
                                    <td>Jcos</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblJCOs"></asp:Label></td>
                                </tr>
                                <tr class="total">
                                    <td>OR</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblOR"></asp:Label></td>
                                </tr>
                                <tr class="total sum">
                                    <td>Number Of Transients</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblNoOfTrans"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="sign">
                            <span>Sign Of FLT NCO</span>
                            <br />
                            <br />
                            <br />
                            Signature___________________

                        </div>
                        <div class="sign">
                            <span>Sign Of FLT JCO</span>
                            <br />
                            <br />
                            <br />
                            Signature___________________

                        </div>
                        <div class="sign">
                            <span>Sign Of OC</span>
                            <br />
                            <br />
                            <br />
                            Signature___________________
                            <br />
                            <br />
                            <div class="sign-align">
                                <span class="signature-details">Rank</span> :
                            <asp:Label runat="server" ID="lblRptRank"></asp:Label>
                                <br />
                                <span class="signature-details">Name</span> :
                            <asp:Label runat="server" ID="lblRptName"></asp:Label>
                                <br />
                                <span class="signature-details">Date</span> :
                            <asp:Label runat="server" ID="lblRptDate"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div>
                        <br />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-block btn-primary" OnClientClick="if(confirm('Are you sure you want to Print this data?')==true) { return PrintPanel();} else{}" />
                        <div class="cf"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
    <script type="text/javascript">
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
