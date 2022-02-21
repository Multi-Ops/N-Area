<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="PrepareBill.aspx.cs" Inherits="TransitCamp.Bookings.PrepareBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_PrintDiv {
            float: left !important;
            width: 100% !important;
        }

        input#ContentPlaceHolder1_chkShare {
            width: 20px !important;
            height: 20px !important;
            float: left;
            margin: 0 5px 0px 0;
        }

        #ContentPlaceHolder1_grdUser tr:last-child {
            color: red;
            font-weight: bolder;
        }

        table {
            page-break-inside: auto
        }

        tr {
            page-break-inside: avoid;
            page-break-after: auto
        }

        thead {
            display: table-header-group
        }

        tfoot {
            display: table-footer-group
        }

        .h-handle {
            margin: 0 auto;
            width: 40%
        }

        .l-sec, .r-sec {
            color: #262b41;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4">
        <div class="row">
            <div id="PrintDiv" runat="server">
                <div class="table-container">

                    <div class="table-wrp">
                        <div class="table-responsive table-grid float-left w-100">
                            <h4 class="h-handle">Transit Facility Offrs : 'N Area'</h4>
                            <div class="heading-details pad">
                                <div class="l-sec pl-4 w-50 float-left">
                                    Bill Number:-
                                    <label runat="server" id="lblBillNo"></label>
                                </div>
                                <div class="r-sec pl-4 w-50 float-left">
                                    <div class="float-right pr-4">
                                        Date:-
                                <label runat="server" id="lblDate"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="heading-details">
                                <div class="l-sec pl-4 w-50 float-left">
                                    Name:-
                                    <label runat="server" id="lblName"></label>
                                </div>
                                <div class="r-sec pl-4 w-50 float-left">
                                    <div class="float-right pr-4">
                                        Rank:-
                                <label runat="server" id="lblRank"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="heading-details">
                                <div class="l-sec pl-4 w-50 float-left">
                                    Unit:-
                                    <label runat="server" id="lblUnit"></label>
                                </div>
                                <div class="r-sec pl-4 w-50 float-left">
                                    <div class="float-right pr-4">
                                        Room:-
                                <label runat="server" id="lblRoom"></label>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView runat="server" CssClass="table table-striped table-grid-one" ID="grdUser" AutoGenerateColumns="true">
                                <%--<Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%#Eval("BlockName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Default Price">
                                    <ItemTemplate>
                                        <%#Eval("DefaultPrice") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Max Room Available">
                                    <ItemTemplate>
                                        <%#Eval("MaxRoomAvailable") %>
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

                            </Columns>--%>
                                <EmptyDataTemplate>
                                    No data to display
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="dec">
                        <h6>Received payment cash/cheque no. ____________________________ for Rs. ____________________________ Date ____________________________ 20 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ____________________________
                        </h6>
                        <h4>For outstation cheques, a bank commission of Rs. 30 must be added.
                        </h4>
                    </div>
                </div>

                <div>
                    <br />
                    <div class="cf"></div>
                </div>
            </div>
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn float-left btn-block btn-primary" OnClientClick="if(confirm('Are you sure you want to Print this data?')==true) { return PrintPanel();} else{}" />
        </div>
        <div class="row justify-content-center form-bg-image">
            <div id="Electricity" class="col-12 d-flex align-items-center justify-content-center" runat="server">
                <div class="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                    <div class="text-center text-md-center mb-4 mt-md-0">
                        <h1 class="mb-0 h3">Electricity Price</h1>
                    </div>

                    <div class="row">
                        <div class="col">
                            <label for="email">Unit Rate</label>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtUnitPrice" placeholder="Price Per Unit"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfUnitPrice" ControlToValidate="txtUnitPrice" Display="Dynamic" ErrorMessage="Required!" ForeColor="Red" CssClass="pt-2 pl-2"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                                ErrorMessage="Integer/Decimal" ForeColor="Red" CssClass="pt-2 pl-2 w-100"
                                ControlToValidate="txtUnitPrice" Display="Dynamic" />
                        </div>
                        <div class="col">
                            <label for="email">Units Consumed</label>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtUnitConsumed" placeholder="Units Consumed"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtUnitConsumed" Display="Dynamic" ErrorMessage="Required!" ForeColor="Red" CssClass="pt-2 pl-2"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                                ErrorMessage="Integer/Decimal" ForeColor="Red" CssClass="pt-2 pl-2 w-100"
                                ControlToValidate="txtUnitConsumed" Display="Dynamic" />
                        </div>
                        <div class="col d-none">
                            <label for="email" class="w-100 pb-2">Share Status</label>
                            <asp:CheckBox runat="server" Text="Is Shared" ID="chkShare" />
                        </div>
                    </div>
                    <div class="d-flex justify-content-between align-items-top mb-3">
                    </div>
                    <asp:Button CssClass="btn btn-block btn-primary" runat="server" ID="btnFinalBill" OnClick="btnFinalBill_Click" Text="Final Bill" />
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
    </script>
</asp:Content>
