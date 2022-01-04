<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RationReport.aspx.cs" Inherits="TransitCamp.Reports.RationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tbtblnc td {
            border: 1px solid;
            padding: 2px 5px 2px 5px;
            text-align: right;
        }

        .balanceC\/F {
            width: 35%;
            margin: 0 auto;
        }

        .tblCF td {
            border: 1px solid;
            padding: 2px 15px 2px 15px;
            text-align: right;
        }

        .left-text {
            text-align: left !important
        }

        div#ContentPlaceHolder1_PrintDiv {
            width: 92% !important;
            margin: 0 auto;
        }

        body table {
            color: #000 !important;
        }

        body {
            color: #000 !important;
        }

        .c-left {
            padding: 0px 2px 0px 2px;
        }

        .custom {
            width: 100px !important;
            margin: 0 10px 0px 0px !important;
        }

        .custom-position {
            width: 18% !important;
            margin: 2px 12px 0 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mt-2 w-50 float-right">
        <asp:Button runat="server" CssClass="float-left form-control custom-position btn float-right btn-block btn-primary" ID="btnSearchAD" Text="Search" OnClick="btnSearchAD_Click"></asp:Button>
        <asp:DropDownList runat="server" CssClass="float-left form-control float-right custom-position" ID="ddlCities"></asp:DropDownList>
    </div>

    <hr class="w-100" />
    <div id="PrintDiv" runat="server">
        <div class="head-text">
            <div class="align">
                <asp:Label runat="server" ID="lblCamp"></asp:Label>
                <br />
                Daily Summary As On :-
                <asp:Label runat="server" ID="lblSummaryDate"></asp:Label>

            </div>
        </div>
        <br />
        <div class="main">
            <div class="c-left w-50 float-left">
                <div runat="server" id="divTblSummary">
                    <table>
                        <%--header--%>
                        <tr class="total">
                            <td class="left-text">Category</td>
                            <td class="left-text">Balance B/F</td>
                            <td>
                                <table class="sub-tbl">
                                    <thead>
                                        <p class="left-text">New Arrival</p>
                                    </thead>
                                    <tr>
                                        <td class="left-text">FN</td>
                                        <td class="left-text">AN</td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <thead>
                                        <p class="left-text">Departure</p>
                                    </thead>
                                    <tr>
                                        <td class="left-text">FN</td>
                                        <td class="left-text">AN</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--officer--%>
                        <tr class="total">
                            <td class="left-text">Officers</td>
                            <td>
                                <asp:Label runat="server" ID="lblBFTotalOffc"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="newArrivalOFFCFN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="newArrivalOFFCAN"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="departureOFFCFN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="departureOFFCAN"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--jco's--%>
                        <tr class="total">
                            <td class="left-text">JCOs</td>
                            <td>
                                <asp:Label runat="server" ID="lblBFTotalJCO"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="newArrivalJCOFN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="newArrivalJCOAN"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="departureJCOFN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="departureJCOAN"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--others--%>
                        <tr class="total">
                            <td class="left-text">Others</td>
                            <td>
                                <asp:Label runat="server" ID="lblBFTotalOthers"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="newArrivalORFN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="newArrivalORCAN"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="departureORCFN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="departureORCAN"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--Total--%>
                        <tr class="total">
                            <td class="left-text">Total</td>
                            <td>
                                <asp:Label runat="server" ID="lblBFTotal"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblArrivalFNTotal"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblArrivalANTotal"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepFNTotal"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepANTotal"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="para">
                        <p>Balance C/F</p>
                    </div>
                    <div class="balanceC/F">
                        <table class="tblCF">
                            <tr>
                                <td class="left-text">Officers</td>
                                <td>
                                    <asp:Label runat="server" ID="lblofcCF"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="left-text">JCO</td>
                                <td>
                                    <asp:Label runat="server" ID="lbljcoCF"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="left-text">Others</td>
                                <td>
                                    <asp:Label runat="server" ID="lblorCF"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="left-text">Total</td>
                                <td>
                                    <asp:Label runat="server" ID="lbltotalCF"></asp:Label></td>
                            </tr>
                        </table>
                    </div>

                    <p class="mt-3 dep-det">
                        Departure Detail Of Transients As On:-
                        <asp:Label runat="server" ID="lblDepDate"></asp:Label>
                    </p>
                    <table class="mt-3">
                        <%--header--%>
                        <tr class="total">
                            <td class="left-text">Category</td>
                            <td class="left-text">Balance B/F</td>
                            <td>
                                <table class="sub-tbl">
                                    <thead>
                                        <p class="left-text">New Arrival</p>
                                    </thead>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <thead>
                                        <p class="left-text">Departure</p>
                                    </thead>
                                    <tr>
                                        <td class="left-text">Air</td>
                                        <td class="left-text">Bus</td>
                                        <td class="left-text">Train</td>
                                        <td class="left-text">Cancel</td>
                                        <td class="left-text">Balance</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--officer--%>
                        <tr class="total">
                            <td class="left-text">Officers</td>
                            <td>
                                <asp:Label runat="server" ID="lblDepoffcBF"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDepoffcBFNewArrival"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepoffcBFAir"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepoffcBFBus"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepoffcBFTrain"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepoffcBFLve"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBlnOffc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--jco's--%>
                        <tr class="total">
                            <td class="left-text">Officers</td>
                            <td>
                                <asp:Label runat="server" ID="lblBFJCO"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblBFJCONewArrival"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblBFJCODepAir"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBFJCODepBus"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBFJCODepTrain"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBFJCODepLVE"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblBFJCODepBalance"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--others--%>
                        <tr class="total">
                            <td class="left-text">Others</td>
                            <td>
                                <asp:Label runat="server" ID="lblORBF"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblORBFNewArrival"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblORBFAir"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblORBFBus"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblORBFTrain"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblORBFLVE"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblORBFBalance"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%--Total--%>
                        <tr class="total pt-1">
                            <td class="left-text">Total</td>
                            <td>
                                <asp:Label runat="server" ID="lblDepTotalBF"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDepTotalBFNewArrival"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepTotalBFAir"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepTotalBFBus"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepTotalBFTrain"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepTotalBFLVE"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDepTotalBFBalance"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <p>
                        Balance:-
                        <asp:Label runat="server" ID="lblBalanceDep"></asp:Label>
                    </p>
                    <br />

                </div>
                <table>
                </table>
            </div>

            <div class="c-left w-50 float-left">
                <div runat="server" id="div1">
                    <p class="final">
                        (<asp:Label runat="server" ID="lblCity"></asp:Label>) Ration STR AS ON
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                    </p>
                    <table>
                        <%--header--%>
                        <tr class="total">
                            <td class="left-text">BFF STR
                            </td>
                            <td class="left-text">
                                <asp:Label runat="server" ID="blcBF"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr class="total">
                            <td class="left-text">80%
                            </td>
                            <td class="left-text">ARR
                            </td>
                            <td class="left-text">FN</td>
                            <td>
                                <asp:Label runat="server" ID="lblstrArrFN"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblstrArrFNFinal"></asp:Label></td>
                        </tr>

                        <tr class="total">
                            <td class="left-text">40%
                            </td>
                            <td class="left-text">ARR
                            </td>
                            <td class="left-text">AN</td>
                            <td>
                                <asp:Label runat="server" ID="lblstrArrAN"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblstrArrANFinal"></asp:Label></td>
                        </tr>

                        <tr class="total">
                            <td class="left-text">20%
                            </td>
                            <td class="left-text">DEP
                            </td>
                            <td class="left-text">FN</td>
                            <td>
                                <asp:Label runat="server" ID="lblstrDepFN"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblstrDepFNFinal"></asp:Label></td>
                        </tr>

                        <tr class="total">
                            <td class="left-text">60%
                            </td>
                            <td class="left-text">DEP
                            </td>
                            <td class="left-text">AN</td>
                            <td>
                                <asp:Label runat="server" ID="lblstrDepAN"></asp:Label></td>
                            <td>
                                <asp:Label runat="server" ID="lblstrDepANFinal"></asp:Label></td>
                        </tr>

                        <tr class="total">
                            <td class="left-text">F/STR</td>
                            <td class="left-text">
                                <asp:Label runat="server" ID="lblFstr"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <div class="signature">
                        <div class="sign-h">
                            <h5>SIG OF AD CLK ___________________</h5>
                            <h5>SIG OF SM  _____________________</h5>
                            <h5>SIG OF CO  _____________________</h5>
                        </div>
                        <div class="pLeft">
                            <p>
                                Unit:-
                        <asp:Label runat="server" ID="lblUnit"></asp:Label>
                            </p>
                            <p>
                                Station:-
                    <asp:Label runat="server" ID="lblStation"></asp:Label>
                            </p>
                            <p>
                                Date :-
                    <asp:Label runat="server" ID="lblDepFinalDate"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr class="w-100" />
    <div class="w-100">
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-block btn-primary mb-3 float-right custom" OnClientClick="if(confirm('Are you sure you want to Print this data?')==true) { return PrintPanel();} else{}" />
        <div class="cf"></div>
    </div>
    <hr class="w-100" />
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


        $('.fromdate').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        });

        $('.todateAD').datetimepicker({
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        });

    </script>
</asp:Content>
