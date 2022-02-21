<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManifestReport.aspx.cs" Inherits="TransitCamp.Reports.ManifestReport" MasterPageFile="~/SiteMaster.Master" %>

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
        <div class="">
            <div class="row">
                <div class="table-container">
                    <div class="table-wrp">
                        <div id="PrintDiv" runat="server">
                            <div class="head-text">
                                <div class="Centre">
                                    (LEH)<br />
                                    MANIFEST NO :
                                    <asp:Label runat="server" ID="lblManifestNo"></asp:Label><br />
                                    <br />
                                </div>
                                <div class="align">
                                    MANIFEST OF TRANSIENTS MOV BY CHARTER FROM
                                        <asp:Label runat="server" ID="lblCamp"></asp:Label>
                                    (CHANDIGARH -
                                    <asp:Label runat="server" ID="lblCity"></asp:Label>
                                    )
                                </div>
                                <br />
                                <div class="align1">
                                    FROM FLT NO
                                    <asp:Label runat="server" ID="lblTransportDetails"></asp:Label>
                                    :
                                    <asp:Label runat="server" ID="lblDate"></asp:Label>
                                </div>
                                <br />
                                <div class="cat">
                                    CATEGORY :
                                        <asp:Label runat="server" ID="lblcat"></asp:Label>
                                </div>
                                <br />
                                <br />
                            </div>
                            <div class="table-responsive table-grid">
                                <asp:GridView runat="server" ShowFooter="true" CssClass="table table-striped table-grid-one grid" ID="grdManifestReport" AutoGenerateColumns="false">
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

                                        <%--                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Move Type">
                                            <ItemTemplate>
                                                <%#Eval("MoveName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="I Card">
                                            <ItemTemplate>
                                                <%# Eval("ICard") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--                      <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Reporting Date">
                                            <ItemTemplate>
                                                <%#Eval("Date", "{0:dd/MM/yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <%--                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FN/AN">
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
            <br />
            <div class="row">
                <div class="table-container">
                    <div class="table-wrp">
                        <div id="Print2" runat="server">
                            <div class="head-text">
                                <div class="Centre">
                                    MANIFEST NO :
                                    <asp:Label runat="server" ID="lblManifestNo1"></asp:Label><br />
                                    <br />
                                </div>
                                <div class="align">
                                    MANIFEST OF TRANSIENTS MOV BY CHARTER FROM
                                        <asp:Label runat="server" ID="lblCamp1"></asp:Label>
                                    (CHANDIGARH -
                                    <asp:Label runat="server" ID="lblCity1"></asp:Label>
                                    )
                                </div>
                                <br />
                                <div class="align1">
                                    FROM FLT NO
                                    <asp:Label runat="server" ID="lblTransportDetails1"></asp:Label>
                                    :
                                    <asp:Label runat="server" ID="lblDate1"></asp:Label>
                                </div>
                                <br />
                                <div class="cat">
                                    CATEGORY :
                                        <asp:Label runat="server" ID="lblCat1"></asp:Label>
                                </div>
                                <br />
                                <br />
                            </div>
                            <div class="table-responsive table-grid">
                                <asp:GridView runat="server" ShowFooter="true" CssClass="table table-striped table-grid-one grid" ID="grdCat1" AutoGenerateColumns="false">
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

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Move Type">
                                            <ItemTemplate>
                                                <%#Eval("MoveName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="I Card">
                                            <ItemTemplate>
                                                <%# Eval("ICard") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Reporting Date">
                                            <ItemTemplate>
                                                <%#Eval("Date", "{0:dd/MM/yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FN/AN">
                                            <ItemTemplate>
                                                <%#Eval("Session") %>
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
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data to display
                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </div>
                            <div class="declare">
                                Certified that the baggage of passenger/Army load list in the manifest have been checked by the undersigned and there is no such item the baggage or on the person which can be used sabotage/hijacking of aircraft
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
                            <asp:Label runat="server" ID="lblRptRank1"></asp:Label>
                                    <br />
                                    <span class="signature-details">Name</span> :
                            <asp:Label runat="server" ID="lblRptName1"></asp:Label>
                                    <br />
                                    <span class="signature-details">Date</span> :
                            <asp:Label runat="server" ID="lblRptDate1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div>
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="Print" CssClass="btn btn-block btn-primary" OnClientClick="if(confirm('Are you sure you want to Print this data?')==true) { return PrintPanel1();} else{}" />
                            <div class="cf"></div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="table-container">
                    <div class="table-wrp">
                        <div id="Print3" runat="server">
                            <div class="head-text">
                                <div class="Centre">
                                    MANIFEST NO :
                                    <asp:Label runat="server" ID="lblManifestNo2"></asp:Label><br />
                                    <br />
                                </div>
                                <div class="align">
                                    MANIFEST OF TRANSIENTS MOV BY CHARTER FROM 
                                        <asp:Label runat="server" ID="lblCamp2"></asp:Label>
                                    (CHANDIGARH -
                                    <asp:Label runat="server" ID="lblCity2"></asp:Label>
                                    )
                                </div>
                                <br />
                                <div class="align1">
                                    FROM FLT NO
                                    <asp:Label runat="server" ID="lblTransportDetails2"></asp:Label>
                                    :
                                    <asp:Label runat="server" ID="lblDate2"></asp:Label>
                                </div>
                                <br />
                                <div class="cat">
                                    CATEGORY :
                                        <asp:Label runat="server" ID="lblCat3"></asp:Label>
                                </div>
                                <br />
                                <br />
                            </div>
                            <div class="table-responsive table-grid">
                                <asp:GridView runat="server" ShowFooter="true" CssClass="table table-striped table-grid-one grid" ID="grdCat2" AutoGenerateColumns="false">
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

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Move Type">
                                            <ItemTemplate>
                                                <%#Eval("MoveName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="I Card">
                                            <ItemTemplate>
                                                <%# Eval("ICard") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Reporting Date">
                                            <ItemTemplate>
                                                <%#Eval("Date", "{0:dd/MM/yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FN/AN">
                                            <ItemTemplate>
                                                <%#Eval("Session") %>
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
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No data to display
                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </div>
                            <div class="declare">
                                Certified that the baggage of passenger/Army load list in the manifest have been checked by the undersigned and there is no such item the baggage or on the person which can be used sabotage/hijacking of aircraft
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
                            <asp:Label runat="server" ID="lblRptRank2"></asp:Label>
                                    <br />
                                    <span class="signature-details">Name</span> :
                            <asp:Label runat="server" ID="lblRptName2"></asp:Label>
                                    <br />
                                    <span class="signature-details">Date</span> :
                            <asp:Label runat="server" ID="lblRptDate2"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div>
                            <br />
                            <asp:Button ID="Button2" runat="server" Text="Print" CssClass="btn btn-block btn-primary" OnClientClick="if(confirm('Are you sure you want to Print this data?')==true) { return PrintPanel2();} else{}" />
                            <div class="cf"></div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnPrintCombine" runat="server" Text="Print Combine" CssClass="btn float-right btn-block btn-primary" OnClick="btnPrintCombine_Click" />
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
        function PrintPanel1() {
            var panel = document.getElementById("<%=Print2.ClientID %>");
            var printWindow = window.open("", '_blank');
            printWindow.document.write('<html><head><link type="text/css" href="/Content/css/Print.css" rel="stylesheet"><title></title>');
            printWindow.document.write('</head><body class="print-body"><center class="Print-Center"><div style="top: 2%; left: 70%;"></div>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</center></body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        function PrintPanel2() {
            var panel = document.getElementById("<%=Print3.ClientID %>");
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
