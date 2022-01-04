using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;


namespace TransitCamp.Routing
{
    public class Routing : System.Web.HttpApplication
    {
        public void RoutingUrls()
        {
            RouteTable.Routes.MapPageRoute("Login", "Login", "~/Login.aspx");
            RouteTable.Routes.MapPageRoute("CreateAccount", "CreateAccount", "~/CreateAccount.aspx");
            RouteTable.Routes.MapPageRoute("Dashboard", "Dashboard", "~/Dashboard.aspx");
            RouteTable.Routes.MapPageRoute("UserList", "UserList", "~/Admin/UserList.aspx");
            RouteTable.Routes.MapPageRoute("AddAirline", "AddAirline", "~/Admin/AddAirline.aspx");
            RouteTable.Routes.MapPageRoute("AirlineList", "AirlineList", "~/Admin/AirlineList.aspx");
            RouteTable.Routes.MapPageRoute("AddCity", "AddCity", "~/Admin/AddCity.aspx");
            RouteTable.Routes.MapPageRoute("CitiesList", "CitiesList", "~/Admin/CitiesList.aspx");
            RouteTable.Routes.MapPageRoute("DivisionList", "DivisionList", "~/Admin/DivisionList.aspx");
            RouteTable.Routes.MapPageRoute("AddDivision", "AddDivision", "~/Admin/AddDivision.aspx");
            RouteTable.Routes.MapPageRoute("HeadquarterList", "HeadquarterList", "~/Admin/HeadquarterList.aspx");
            RouteTable.Routes.MapPageRoute("AddHeadquarter", "AddHeadquarter", "~/Admin/AddHeadquarter.aspx");
            RouteTable.Routes.MapPageRoute("AddRank", "AddRank", "~/Admin/AddRank.aspx");
            RouteTable.Routes.MapPageRoute("RankList", "RankList", "~/Admin/RankList.aspx");
            RouteTable.Routes.MapPageRoute("AddTransport", "AddTransport", "~/Admin/AddTransport.aspx");
            RouteTable.Routes.MapPageRoute("TransportList", "TransportList", "~/Admin/TransportList.aspx");
            RouteTable.Routes.MapPageRoute("AddUnit", "AddUnit", "~/Admin/AddUnit.aspx");
            RouteTable.Routes.MapPageRoute("UnitList", "UnitList", "~/Admin/UnitList.aspx");
            RouteTable.Routes.MapPageRoute("LeaveList", "LeaveList", "~/Admin/LeaveList.aspx");
            RouteTable.Routes.MapPageRoute("AddLeave", "AddLeave", "~/Admin/AddLeave.aspx");
            RouteTable.Routes.MapPageRoute("AddSignature", "AddSignature", "~/Admin/AddSignature.aspx");
            RouteTable.Routes.MapPageRoute("SignatureList", "SignatureList", "~/Admin/SignatureList.aspx");
            RouteTable.Routes.MapPageRoute("MoveList", "MoveList", "~/Admin/MoveList.aspx");
            RouteTable.Routes.MapPageRoute("AddMove", "AddMove", "~/Admin/AddMove.aspx");
            RouteTable.Routes.MapPageRoute("PriorityList", "PriorityList", "~/Admin/PriorityList.aspx");
            RouteTable.Routes.MapPageRoute("AddPriority", "AddPriority", "~/Admin/AddPriority.aspx");
            RouteTable.Routes.MapPageRoute("PriorityStatusList", "PriorityStatusList", "~/Admin/PriorityStatusList.aspx");
            RouteTable.Routes.MapPageRoute("AddPriorityStatus", "AddPriorityStatus", "~/Admin/AddPriorityStatus.aspx");
            RouteTable.Routes.MapPageRoute("CategoryList", "CategoryList", "~/Admin/CategoryList.aspx");
            RouteTable.Routes.MapPageRoute("AddCategory", "AddCategory", "~/Admin/AddCategory.aspx");
            RouteTable.Routes.MapPageRoute("LevelList", "LevelList", "~/Admin/LevelList.aspx");
            RouteTable.Routes.MapPageRoute("AddLevel", "AddLevel", "~/Admin/AddLevel.aspx");
            RouteTable.Routes.MapPageRoute("ADTypeList", "ADTypeList", "~/Admin/ADTypeList.aspx");
            RouteTable.Routes.MapPageRoute("AddADType", "AddADType", "~/Admin/AddADType.aspx");
            RouteTable.Routes.MapPageRoute("AddCamp", "AddCamp", "~/Admin/AddCamp.aspx");
            RouteTable.Routes.MapPageRoute("CampList", "CampList", "~/Admin/CampList.aspx");
            RouteTable.Routes.MapPageRoute("MedicalStatusList", "MedicalStatusList", "~/Admin/MedicalStatusList.aspx");
            RouteTable.Routes.MapPageRoute("AddMedicalStatus", "AddMedicalStatus", "~/Admin/AddMedicalStatus.aspx");
            RouteTable.Routes.MapPageRoute("AddOutLogic", "AddOutLogic", "~/Admin/AddOutLogic.aspx");
            RouteTable.Routes.MapPageRoute("OutLogicList", "OutLogicList", "~/Admin/OutLogicList.aspx");
            RouteTable.Routes.MapPageRoute("CharterDetailsList", "CharterDetailsList", "~/Admin/CharterDetailsList.aspx");
            RouteTable.Routes.MapPageRoute("AddCharterDetails", "AddCharterDetails", "~/Admin/AddCharterDetails.aspx");
            RouteTable.Routes.MapPageRoute("ADEntery", "ADEntery", "~/Admin/ADEntery.aspx");
            RouteTable.Routes.MapPageRoute("ADList", "ADList", "~/Admin/ADList.aspx");
            RouteTable.Routes.MapPageRoute("CharterADList", "CharterADList", "~/Admin/CharterADList.aspx");
            RouteTable.Routes.MapPageRoute("CharterADEntery", "CharterADEntery", "~/Admin/CharterADEntery.aspx");
            RouteTable.Routes.MapPageRoute("ADLeaveEntry", "ADLeaveEntry", "~/Admin/ADLeaveEntry.aspx");
            RouteTable.Routes.MapPageRoute("ADLeaveList", "ADLeaveList", "~/Admin/ADLeaveList.aspx");
            RouteTable.Routes.MapPageRoute("ADTDList", "ADTDList", "~/Admin/ADTDList.aspx");
            RouteTable.Routes.MapPageRoute("ADTDEntry", "ADTDEntry", "~/Admin/ADTDEntry.aspx");
            RouteTable.Routes.MapPageRoute("TransferCourierToCharterList", "TransferCourierToCharterList", "~/Admin/TransferCourierToCharterList.aspx");
            RouteTable.Routes.MapPageRoute("TransferCourierToCharter", "TransferCourierToCharter", "~/Admin/TransferCourierToCharter.aspx");
            RouteTable.Routes.MapPageRoute("FamilyEntry", "FamilyEntry", "~/Admin/FamilyEntry.aspx");
            RouteTable.Routes.MapPageRoute("FamilyList", "FamilyList", "~/Admin/FamilyList.aspx");
            RouteTable.Routes.MapPageRoute("TransportDetailList", "TransportDetailList", "~/Admin/TransportDetailList.aspx");
            RouteTable.Routes.MapPageRoute("TransportDetails", "TransportDetails", "~/Admin/TransportDetails.aspx");
            RouteTable.Routes.MapPageRoute("Manifest", "Manifest", "~/Admin/Manifest.aspx");
            RouteTable.Routes.MapPageRoute("ManifestList", "ManifestList", "~/Admin/ManifestList.aspx");
            RouteTable.Routes.MapPageRoute("ADListPriorityWise", "ADListPriorityWise", "~/Admin/ADListPriorityWise.aspx");
            RouteTable.Routes.MapPageRoute("ADListOnTempHold", "ADListOnTempHold", "~/Admin/ADListOnTempHold.aspx");
            RouteTable.Routes.MapPageRoute("ADListReserve", "ADListReserve", "~/Admin/ADListReserve.aspx");
            RouteTable.Routes.MapPageRoute("ADOnHoldStatusList", "ADOnHoldStatusList", "~/Admin/ADOnHoldStatusList.aspx");
            RouteTable.Routes.MapPageRoute("UserHistory", "UserHistory", "~/Admin/UserHistory.aspx");
            RouteTable.Routes.MapPageRoute("UserHistoryDetails", "UserHistoryDetails", "~/Admin/UserHistoryDetails.aspx");
            RouteTable.Routes.MapPageRoute("ADIsLoadList", "ADIsLoadList", "~/Admin/ADIsLoadList.aspx");
            RouteTable.Routes.MapPageRoute("AllManifests", "AllManifests", "~/Admin/AllManifests.aspx");
            RouteTable.Routes.MapPageRoute("ManifestDetails", "ManifestDetails", "~/ManifestDetails.aspx");
            RouteTable.Routes.MapPageRoute("AllReserveManifests", "AllReserveManifests", "~/Admin/AllReserveManifests.aspx");
            RouteTable.Routes.MapPageRoute("ReserveManifestDetails", "ReserveManifestDetails", "~/Admin/ReserveManifestDetails.aspx");

            //Reports
            RouteTable.Routes.MapPageRoute("ManifestReport", "ManifestReport", "~/Reports/ManifestReport.aspx");
            RouteTable.Routes.MapPageRoute("ManifestCombineReport", "ManifestCombineReport", "~/Reports/ManifestCombineReport.aspx");
            RouteTable.Routes.MapPageRoute("ADReportToday", "ADReportToday", "~/Reports/ADReportToday.aspx");
            RouteTable.Routes.MapPageRoute("ADReportDateWise", "ADReportDateWise", "~/Reports/ADReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("HoldingReport", "HoldingReport", "~/Reports/HoldingReport.aspx");
            RouteTable.Routes.MapPageRoute("ADReportMoveType", "ADReportMoveType", "~/Reports/ADReportMoveType.aspx");
            RouteTable.Routes.MapPageRoute("CityDateWise", "CityDateWise", "~/Reports/CityDateWise.aspx");
            RouteTable.Routes.MapPageRoute("RedGreenStatus", "RedGreenStatus", "~/Reports/RedGreenStatus.aspx");
            RouteTable.Routes.MapPageRoute("UnitWiseReport", "UnitWiseReport", "~/Reports/UnitWiseReport.aspx");
            RouteTable.Routes.MapPageRoute("DivisonWiseReport", "DivisonWiseReport", "~/Reports/DivisonWiseReport.aspx");
            RouteTable.Routes.MapPageRoute("HQWiseReport", "HQWiseReport", "~/Reports/HQWiseReport.aspx");
            RouteTable.Routes.MapPageRoute("DepartureDateWise", "DepartureDateWise", "~/Reports/DepartureDateWise.aspx");
            RouteTable.Routes.MapPageRoute("DepartureCityWise", "DepartureCityWise", "~/Reports/DepartureCityWise.aspx");
            RouteTable.Routes.MapPageRoute("DepartureTransportWise", "DepartureTransportWise", "~/Reports/DepartureTransportWise.aspx");
            RouteTable.Routes.MapPageRoute("DepartureMoveType", "DepartureMoveType", "~/Reports/DepartureMoveType.aspx");
            RouteTable.Routes.MapPageRoute("DepartureUnitWiseReport", "DepartureUnitWiseReport", "~/Reports/DepartureUnitWiseReport.aspx");
            RouteTable.Routes.MapPageRoute("DeepartureDivisionReport", "DeepartureDivisionReport", "~/Reports/DeepartureDivisionReport.aspx");
            RouteTable.Routes.MapPageRoute("DepartureHQWise", "DepartureHQWise", "~/Reports/DepartureHQWise.aspx");
            RouteTable.Routes.MapPageRoute("MedicalStatusReport", "MedicalStatusReport", "~/Reports/MedicalStatusReport.aspx");
            RouteTable.Routes.MapPageRoute("FlyReport", "FlyReport", "~/Reports/FlyReport.aspx");
            RouteTable.Routes.MapPageRoute("CancellationReport", "CancellationReport", "~/Reports/CancellationReport.aspx");
            RouteTable.Routes.MapPageRoute("DepartureDateWiseDaily", "DepartureDateWiseDaily", "~/Reports/DepartureDateWiseDaily.aspx");
            RouteTable.Routes.MapPageRoute("DepartureCityWiseDateWise", "DepartureCityWiseDateWise", "~/Reports/DepartureCityWiseDateWise.aspx");
            RouteTable.Routes.MapPageRoute("DepartureTransportWiseDateWise", "DepartureTransportWiseDateWise", "~/Reports/DepartureTransportWiseDateWise.aspx");
            RouteTable.Routes.MapPageRoute("DepartureMoveTypeDateWise", "DepartureMoveTypeDateWise", "~/Reports/DepartureMoveTypeDateWise.aspx");
            RouteTable.Routes.MapPageRoute("DepartureUnitWiseReportDateWise", "DepartureUnitWiseReportDateWise", "~/Reports/DepartureUnitWiseReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("DeepartureDivisionReportDateWise", "DeepartureDivisionReportDateWise", "~/Reports/DeepartureDivisionReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("DepartureHQWiseDateWise", "DepartureHQWiseDateWise", "~/Reports/DepartureHQWiseDateWise.aspx");
            RouteTable.Routes.MapPageRoute("ADReportMoveTypeDateWise", "ADReportMoveTypeDateWise", "~/Reports/ADReportMoveTypeDateWise.aspx");
            RouteTable.Routes.MapPageRoute("ArrivalCityDateWise", "ArrivalCityDateWise", "~/Reports/ArrivalCityDateWise.aspx");
            RouteTable.Routes.MapPageRoute("RedGreenStatusDateWise", "RedGreenStatusDateWise", "~/Reports/RedGreenStatusDateWise.aspx");
            RouteTable.Routes.MapPageRoute("UnitWiseReportDateWise", "UnitWiseReportDateWise", "~/Reports/UnitWiseReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("DivisonWiseReportDateWise", "DivisonWiseReportDateWise", "~/Reports/DivisonWiseReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("HQWiseReportDateWise", "HQWiseReportDateWise", "~/Reports/HQWiseReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("MedicalStatusReportDateWise", "MedicalStatusReportDateWise", "~/Reports/MedicalStatusReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("CancellationReportDateWise", "CancellationReportDateWise", "~/Reports/CancellationReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("FlyReportDateWise", "FlyReportDateWise", "~/Reports/FlyReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("HoldStatusReportDateWise", "HoldStatusReportDateWise", "~/Reports/HoldStatusReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("OnHoldingReport", "OnHoldingReport", "~/Reports/OnHoldingReport.aspx");
            RouteTable.Routes.MapPageRoute("WaitingReport", "WaitingReport", "~/Reports/WaitingReport.aspx");
            RouteTable.Routes.MapPageRoute("WaitingReportDateWise", "WaitingReportDateWise", "~/Reports/WaitingReportDateWise.aspx");
            RouteTable.Routes.MapPageRoute("ReserveManifestReport", "ReserveManifestReport", "~/Reports/ReserveManifestReport.aspx");
            RouteTable.Routes.MapPageRoute("FormationWiseSummary", "FormationWiseSummary", "~/Reports/FormationWiseSummary.aspx");
            RouteTable.Routes.MapPageRoute("FormationWiseSummaryDateRange", "FormationWiseSummaryDateRange", "~/Reports/FormationWiseSummaryDateRange.aspx");
            RouteTable.Routes.MapPageRoute("FormationWiseSummaryDateRangeDeparture", "FormationWiseSummaryDateRangeDeparture", "~/Reports/FormationWiseSummaryDateRangeDeparture.aspx");
            RouteTable.Routes.MapPageRoute("FormationWiseSummaryDeparture", "FormationWiseSummaryDeparture", "~/Reports/FormationWiseSummaryDeparture.aspx");
            RouteTable.Routes.MapPageRoute("OnHoldingDateRangeReport", "OnHoldingDateRangeReport", "~/Reports/OnHoldingDateRangeReport.aspx");
            RouteTable.Routes.MapPageRoute("ADFormationWiseSummary", "ADFormationWiseSummary", "~/Reports/ADFormationWiseSummary.aspx");
            RouteTable.Routes.MapPageRoute("ADDateRange", "ADDateRange", "~/Reports/ADDateRange.aspx");
            RouteTable.Routes.MapPageRoute("DepartureUnitWiseSummary", "DepartureUnitWiseSummary", "~/Reports/DepartureUnitWiseSummary.aspx");
            RouteTable.Routes.MapPageRoute("DepartureUnitWiseSummaryDateRange", "DepartureUnitWiseSummaryDateRange", "~/Reports/DepartureUnitWiseSummaryDateRange.aspx");
            RouteTable.Routes.MapPageRoute("ADUnitWiseDateRange", "ADUnitWiseDateRange", "~/Reports/ADUnitWiseDateRange.aspx");
            RouteTable.Routes.MapPageRoute("ArrivalUnitWiseSummaryDateRange", "ArrivalUnitWiseSummaryDateRange", "~/Reports/ArrivalUnitWiseSummaryDateRange.aspx");
            RouteTable.Routes.MapPageRoute("ArrivalUnitWiseSummary", "ArrivalUnitWiseSummary", "~/Reports/ArrivalUnitWiseSummary.aspx");
            RouteTable.Routes.MapPageRoute("RationReport", "RationReport", "~/Reports/RationReport.aspx");
            RouteTable.Routes.MapPageRoute("WaitingDateWise", "WaitingDateWise", "~/Reports/WaitingDateWise.aspx");
            RouteTable.Routes.MapPageRoute("WaitingReportFMNWise", "WaitingReportFMNWise", "~/Reports/WaitingReportFMNWise.aspx");

            //Bookings
            RouteTable.Routes.MapPageRoute("BlockList", "BlockList", "~/Bookings/BlockList.aspx");
            RouteTable.Routes.MapPageRoute("AddBlock", "AddBlock", "~/Bookings/AddBlock.aspx");
            RouteTable.Routes.MapPageRoute("AddBillAttribute", "AddBillAttribute", "~/Bookings/AddBillAttribute.aspx");
            RouteTable.Routes.MapPageRoute("AddBooking", "AddBooking", "~/Bookings/AddBooking.aspx");
            RouteTable.Routes.MapPageRoute("BillAttributeList", "BillAttributeList", "~/Bookings/BillAttributeList.aspx");
            RouteTable.Routes.MapPageRoute("BookingList", "BookingList", "~/Bookings/BookingList.aspx");
            RouteTable.Routes.MapPageRoute("RoomList", "RoomList", "~/Bookings/RoomList.aspx");
            RouteTable.Routes.MapPageRoute("AddRoom", "AddRoom", "~/Bookings/AddRoom.aspx");
        }
    }
}