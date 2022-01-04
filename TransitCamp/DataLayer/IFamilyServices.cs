using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IFamilyServices
    {
        FamilyEntry GetByID(Int64 id);
        void Insert(FamilyEntry info);
        Int64 Update(FamilyEntry info);
        List<FamilyEntry> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<FamilyEntry> GetSearchResult(String SearchText);
        void Save();
    }
}
