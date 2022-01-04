using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IADTDServices
    {
        ADTDEntry GetByID(Int64 id);
        void Insert(ADTDEntry info);
        Int64 Update(ADTDEntry info);
        List<ADTDEntry> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<ADTDEntry> GetSearchResult(String SearchText);
        void Save();
    }
}
