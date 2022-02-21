using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICancelServices
    {
        Cancel GetByID(Int64 id);
        Cancel IndividualGetByID(Int64 id);
        Int64 Insert(Cancel info);
        Int32 TotalItems();
        Int64 IndivialInsert(Cancel info);
        Int64 Update(Cancel info);
        Int64 IndividualUpdate(Cancel info);
        void Delete(Int64 ID);
        void IndividualDelete(Int64 ID);
        void Save();
        void IndividualSave();
        List<Cancel> GetManifest();
        List<Cancel> GetManifestByDate(DateTime date, int cid);
        List<Cancel> GetManifestByDateRange(DateTime fromDate, DateTime toDate, int cid);
        Cancel GetManifestForReport(string ManifestNo);
        Cancel GetManifestForReportByDate(string ManifestNo, DateTime date, int cid);
        Cancel GetManifestForReportByDateRange(string ManifestNo, DateTime fromDate, DateTime toDate, int cid);
        object GetManifestADByCategoryID(Int64 id, string manifestno, int cid);
        object GetManifestADByLoad(string manifestno, int cid);
    }
}
