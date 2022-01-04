using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace DataLayer
{
    public interface IADLeaveServices
    {
        ADLeaveEntry GetByID(Int64 id);
        void Insert(ADLeaveEntry info);
        Int64 Update(ADLeaveEntry info);
        List<ADLeaveEntry> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<ADLeaveEntry> GetSearchResult(String SearchText);
        void Save();
    }
}
