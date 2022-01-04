using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPriorityStatusServices
    {
        PriorityStatus GetByID(Int64 id);
        void Insert(PriorityStatus info);
        Int64 Update(PriorityStatus info);
        List<PriorityStatus> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<PriorityStatus> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<PriorityStatus> GetPriorityStatusDetails();
    }
}
