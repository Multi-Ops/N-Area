<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ADUnitWiseDateRange.aspx.cs" Inherits="TransitCamp.Reports.ADUnitWiseDateRange" %>

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

        .custom-position {
            width: 18% !important;
            margin: 2px 12px 0 0 !important;
        }

        .lblerror {
            color: red;
            float: left;
            width: 94%;
            text-align: right;
            margin: 5px 0px 0px 4px;
            font-size: 18px;
        }

        #ContentPlaceHolder1_PrintDiv {
            float: left !important;
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="overflow-hidden">
        <div class="row">
            <div class="table-container">
                <div class="table-wrp">
                    <div class="mt-2 w-75 float-right">
                        <asp:Button runat="server" CssClass="float-left form-control float-right custom-position btn btn-block btn-primary" ID="btnToday" Text="Date Wise" OnClick="btnToday_Click"></asp:Button>
                        <asp:Button runat="server" CssClass="float-left form-control float-right custom-position btn btn-block btn-primary" ID="btnSearchAD" Text="Search" OnClick="btnSearchAD_Click"></asp:Button>
                        <asp:TextBox runat="server" CssClass="float-left form-control float-right todateAD custom-position" ID="txtTo" placeholder="To" for="ContentPlaceHolder1_hfToDate" ReadOnly="true" data-date-format="dd-mm-yyyy" data-link-field="ContentPlaceHolder1_hfToDate"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfToDate" Value="" />
                        <asp:TextBox runat="server" CssClass="float-left form-control float-right fromdate custom-position" ID="txtFrom" placeholder="From" for="ContentPlaceHolder1_hfFromDate" ReadOnly="true" data-date-format="dd-mm-yyyy" data-link-field="ContentPlaceHolder1_hfFromDate"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfFromDate" Value="" />
                        <asp:DropDownList runat="server" CssClass="float-left form-control float-right custom-position" ID="ddlCity"></asp:DropDownList>
                    </div>
                    <div class="lblerror">
                        <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
                    </div>
                    <div id="PrintDiv" runat="server">
                        <div class="head-text">
                            <div class="align">
                                Unit Wise AD Summary (<asp:Label runat="server" ID="lblCity"></asp:Label>)
                                <br />
                                From:
                                <asp:Label ID="lblFrom" runat="server"></asp:Label>
                                To:
                                <asp:Label runat="server" ID="lblTO"></asp:Label>
                                <br />
                                For
                                <asp:Label runat="server" ID="lblCamp"></asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="table-responsive table-grid">
                            <asp:GridView runat="server" ShowFooter="true" CssClass="table table-striped table-grid-one grid" ID="grd" AutoGenerateColumns="false" OnRowCreated="grd_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%--<%# Container.DataItemIndex + 1 %>--%>
                                            <%#Eval("SNo") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="FMN">
                                        <ItemTemplate>
                                            <%#Eval("Unit") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Officer">
                                        <ItemTemplate>
                                            <%#Eval("Officer") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="JCO">
                                        <ItemTemplate>
                                            <%# Eval("Jco") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Other">
                                        <ItemTemplate>
                                            <%# Eval("Other") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Total">
                                        <ItemTemplate>
                                            <%#Eval("Total") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No data to display
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <%--<div runat="server" id="divTbl">
                            <table class="table tbtblnc">
                                <thead title="Category" class="tbleheading">
                                    <th class="headingtable">Category</th>
                                </thead>
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
                        </div>--%>
                        <%--<div runat="server" id="tblMoveType">
                            <table class="table tbtblnc">
                                <thead title="Move Type" class="tbleheading">
                                    <th class="headingtable">Move Type</th>
                                </thead>
                                <tr class="total">
                                    <td>Lve</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblLve"></asp:Label></td>
                                </tr>

                                <tr class="total">
                                    <td>Td/Rtu</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTD"></asp:Label></td>
                                </tr>
                                <tr class="total">
                                    <td>Posting</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPosting"></asp:Label></td>
                                </tr>
                                <tr class="total">
                                    <td>ADV Party</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblAdvParty"></asp:Label></td>
                                </tr>
                                <tr class="total">
                                    <td>Other</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblOther"></asp:Label></td>
                                </tr>
                                <tr class="total sum">
                                    <td>Total</td>
                                    <td>
                                        <asp:Label runat="server" ID="lblSum"></asp:Label></td>
                                </tr>
                            </table>
                        </div>--%>
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
