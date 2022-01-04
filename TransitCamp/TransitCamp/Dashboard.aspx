<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TransitCamp.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bold {
            font-size: 18px;
            font-weight: 700;
        }

        .paragraph {
            background: #83e283;
            padding: 1px 1px 1px 5px;
            margin-bottom: 5px;
        }

        #ContentPlaceHolder1_btnBackup {
            color: #2e3650 !important;
            font-size: 20px !important;
            font-weight: bold !important;
        }

        .close {
            border: none;
            background: #fff;
        }

        .modal-content {
            width: 50% !important;
        }

        .custom {
            width: 90px;
        }

        .card-para {
            font-size: 12px;
            font-weight: 700;
        }

        .card-title {
            font-size: 18px;
            font-weight: bold;
        }

        .custom-height {
            height: 190px !important
        }

        .modal-content {
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-2">
        <div class="card-columns text-black float-left w-50 pr-4 mb-4">
            <div class="card mt-2 mb-3 custom-height">
                <div class="card-body overflow-auto">
                    <h4 class="card-title mb-2">Total AD's Left In Camp</h4>
                    <asp:Repeater runat="server" ID="rptAdsLeft">
                        <ItemTemplate>
                            <div class="transport-details">
                                <p class="paragraph">
                                    <span class="bold">On Hold&nbsp;</span>
                                    :&nbsp;<label><%#Eval("OnTempHold") %></label>&nbsp; 
                            <span class="bold">Cancel AD&nbsp;</span>
                                    :&nbsp;<label><%#Eval("OnHoldStatus") %></label>&nbsp; 
                            <span class="bold">TOTAL&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Total") %></label>&nbsp; 
                            <span class="bold">Actual ADs Left&nbsp;</span>&nbsp;
                                    :&nbsp;<label><%#Eval("ADsLeftAfterExcluding") %></label>
                                </p>
                            </div>
                            <div class="transport-details">
                                <p class="paragraph">
                                    <span class="bold">Officers&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Officer") %></label>&nbsp;   
                            <span class="bold">JCO's&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Jco") %></label>&nbsp;  
                            <span class="bold">Others&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Other") %></label>&nbsp;
                            <span class="bold">TOTAL&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Total") %></label>
                                </p>
                            </div>
                            <div class="transport-details">
                                <p class="paragraph">
                                    <span class="bold">On Hold&nbsp;</span>
                                    :&nbsp;<label><%#Eval("OnTempHold") %></label>&nbsp; 
                            <span class="bold">Cancel AD&nbsp;</span>
                                    :&nbsp;<label><%#Eval("OnHoldStatus") %></label>&nbsp; 
                            <span class="bold">Priority&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Priority") %></label>&nbsp;
                            <span class="bold">Reserve&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Reserve") %></label>&nbsp;
                            <span class="bold">Load&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Load") %></label>&nbsp;
                            <span class="bold">Rest&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Rest") %></label>&nbsp;
                            <span class="bold">TOTAL&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Total") %></label>
                                </p>
                            </div>
                            <div class="transport-details">
                                <p class="paragraph">
                                    <span class="bold">LVE&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Lve") %></label>&nbsp; 
                            <span class="bold">TD/RTU&nbsp;</span>
                                    :&nbsp;<label><%#Eval("td") %></label>&nbsp;
                            <span class="bold">Posting&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Posting") %></label>&nbsp;
                            <span class="bold">ADV Party&nbsp;</span>
                                    :&nbsp;<label><%#Eval("ADVParty") %></label>&nbsp;
                            <span class="bold">Other&nbsp;</span>
                                    :&nbsp;<label><%#Eval("MoveOther") %></label>&nbsp;
                            <span class="bold">TOTAL&nbsp;</span>
                                    :&nbsp;<label><%#Eval("Total") %></label>
                                </p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="card-columns text-black float-left w-50 pr-4 mb-4">
            <div class="card mt-2 mb-3 custom-height">
                <div class="card-body overflow-auto">
                    <div>
                        <h4 class="card-title mb-2">Today's Ready To Departure Transportation Summary</h4>
                        <asp:Repeater runat="server" ID="rptSummary">
                            <ItemTemplate>
                                <div class="transport-details">
                                    <p class="paragraph">
                                        <span class="bold">Transport Details&nbsp;</span>
                                        :&nbsp;<label><%#Eval("TransportDetail") %></label>&nbsp;   
                            <span class="bold">City&nbsp;</span>
                                        :&nbsp;<label><%#Eval("City") %></label>&nbsp;  
                            <span class="bold">OFFICERS&nbsp;</span>
                                        :&nbsp;<label><%#Eval("NoOfOfficers") %></label>&nbsp;     
                            <span class="bold">JCOs&nbsp;</span>
                                        :&nbsp;<label><%#Eval("NoOfJCOs") %></label>&nbsp;
                            <span class="bold">OTHERS&nbsp;</span>
                                        :&nbsp;<label><%#Eval("NoOfOthers") %></label>&nbsp;
                            <span class="bold">TOTAL&nbsp;</span>
                                        :&nbsp;<label><%#Eval("Total") %></label>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>
            </div>
        </div>

        <div class="w-100 float-left">
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#ad">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">AD</h3>
                            <p class="card-para">
                                Manage and View AD Entry.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#transport">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">Transport Details</h3>
                            <p class="card-para">
                                Manage and View Transport.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#manifest">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">Manifest</h3>
                            <p class="card-para">
                                Manage and View Manifest.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#rank">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">Rank</h3>
                            <p class="card-para">
                                Add/Remove and View Ranks.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#airline">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">Airline</h3>
                            <p class="card-para">
                                Manage and View Airlines.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#cities">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">Cities</h3>
                            <p class="card-para">
                                Add/Remove and View Cities.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#division">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">Divison</h3>
                            <p class="card-para">
                                Manage and View Divison.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="card-columns text-black float-left w-25 pr-4 mb-4">
                <a data-toggle="modal" data-target="#hq">
                    <div class="card mt-2 mb-3">
                        <div class="card-body">
                            <h3 class="card-title">Headquarter</h3>
                            <p class="card-para">
                                Manage and View HQ.
                            </p>
                        </div>
                    </div>
                </a>
            </div>
            <center>
                <asp:Button runat="server" ID="btnBackup" CssClass="form-control mb-4 btnbackup" Width="200px" Text="BackUP" OnClick="btnBackup_Click" />
            </center>
        </div>


        <!-- Modal -->
        <div class="modal fade" id="ad" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">AD</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'ADEntery'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'ADList'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

        <!-- Modal Transport-->
        <div class="modal fade" id="transport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Transport</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'TransportDetails'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'TransportDetailList'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

        <!-- Modal Manifest-->
        <div class="modal fade" id="manifest" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Manifest</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'Manifest'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'AllManifests'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

        <!-- Modal Rank-->
        <div class="modal fade" id="rank" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Rank</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'AddRank'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'RankList'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

        <!-- Modal airline-->
        <div class="modal fade" id="airline" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Air Line</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'AddAirline'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'AirlineList'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

        <!-- Modal cities-->
        <div class="modal fade" id="cities" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Cities</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'AddCity'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'CitiesList'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

        <!-- Modal division-->
        <div class="modal fade" id="division" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Division</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'AddDivision'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'DivisionList'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

        <!-- Modal HQ-->
        <div class="modal fade" id="hq" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Head Quarter</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <button type="button" class="btn btn-primary ml-2 mr-2 custom" onclick="location.href = 'AddHeadquarter'">Add</button>
                        <button type="button" class="btn btn-primary ml-2 custom" onclick="location.href = 'HeadquarterList'">Search</button>
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
</asp:Content>
