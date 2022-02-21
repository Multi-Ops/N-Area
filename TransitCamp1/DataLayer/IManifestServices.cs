using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IManifestServices
    {
        Manifest GetByID(Int64 id);
        Int64 Insert(Manifest info);
        Int64 Update(Manifest info);
        List<Manifest> Paging(Int32 take, Int32 skip, Int64 ID);
        List<Manifest> ManifestListing(Int64 ID);
        List<Manifest> PagingManifest(string Manifestno, Int64 CatID);
        List<Manifest> PagingManifest(string Manifestno);
        List<Manifest> PagingManifest(string Manifestno, DateTime date, int cityid);
        List<ADEntery> ADEntryNo();
        Int32 TotalItems();
        Int32 TotalItemsManifestDetails();
        Int32 TotalItemsReserveManifestDetails();
        Int32 TotalItems(string manifestNo);
        Int32 TotalItemsManifestDetails(string manifestNo);
        Int32 TotalItemsReserveManifestDetails(string manifestNo);
        Int32 TotalItems(DateTime date);
        Int32 TotalItemsReserve(DateTime date);
        Int32 TotalItemsTransportID(Int64 id);
        void Delete(Int64 ID);
        List<Manifest> GetSearchResult(String SearchText);
        void Save();
        Int64 GetByADID(Int64 id);
        List<Manifest> GetManifest();
        List<Manifest> GetManifestDateWise(DateTime date, int cid);
        List<Manifest> GetManifestDateWiseRange(DateTime fromDate, DateTime toDate, int cid);
        List<Manifest> PagingAll(Int32 take, Int32 skip);
        Manifest GetByTransportID(Int64 id);
        Manifest GetByTransportID(Int64 id, Int64 cityID);
        Manifest GetManifestNumberByDes();
        Manifest GetManifestNumberByDes(DateTime date);
        Manifest GetManifestNumberByDesCityWise(DateTime date, int cityid);
        Manifest GetManifestNumberByDes(Int64 cityId);
        Int64 CheckManifestExist(string ManifestNo);
        List<Manifest> GetManifestWithManifestNo(string ManifestNO);
        List<Manifest> GetManifestWithManifestNo(string ManifestNO, int tid);
        List<Manifest> GetManifestWithManifestNo(string ManifestNO, DateTime date);
        Manifest GetManifestForReport(string ManifestNo);
        Manifest GetManifestForReportByDate(string ManifestNo, DateTime date, int cid);
        Manifest GetManifestForReportByDateRange(string ManifestNo, DateTime fromDate, DateTime toDate, int cid);
        Manifest GetManifestForReportbycat2(string ManifestNo);
        Manifest GetManifestForReportbycat5(string ManifestNo);
        List<Manifest> PagingManifestcat1(string Manifestno);
        List<Manifest> PagingManifestcat2(string Manifestno);
        object GetByTransportIDDateWise(Int64 id);
        List<Manifest> GetManifest(Int32 take, Int32 skip);
        List<Manifest> GetManifest(Int32 take, Int32 skip, DateTime date);
        List<Manifest> GetManifestReserve(Int32 take, Int32 skip, DateTime date);
        List<Manifest> PagingAll(Int32 take, Int32 skip, string manifestno);
        List<Manifest> PagingAll(Int32 take, Int32 skip, string manifestno, DateTime date);
        List<Manifest> PagingAll(Int32 take, Int32 skip, string manifestno, DateTime date, int cityid);
        List<Manifest> GetManifestSearch(string manifestno);
        List<Manifest> GetManifestSearch(DateTime date);
        List<Manifest> SearchResultsManifestDetails(string searchtext, string manifestno);
        object GetManifestADByCategoryID(Int64 id, string manifestno, int cid);
        List<ADEntery> DepartureTransportTypeReport(DateTime fromdate, DateTime todate, Int64 id);
        List<ADEntery> DepartureTransportTypeReport(DateTime fromdate, DateTime todate, Int64 id, int cID);
        List<ADEntery> DepartureTransportTypeReportID(Int64 id, DateTime date);
        List<ADEntery> DepartureTransportTypeReportID(Int64 id, DateTime date, int cID);
        object GetManifestADByLoad(string manifestno, int cid);
    }
}
