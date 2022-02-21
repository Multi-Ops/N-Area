using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace DataLayer
{
    public interface ILeaveServices
    {
        Leave GetID(Int64 id);
        void Insert(Leave info);
        Int64 Update(Leave info);
        List<Leave> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Leave> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
    }
}
