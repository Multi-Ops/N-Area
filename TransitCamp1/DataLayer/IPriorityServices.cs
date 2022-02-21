using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPriorityServices
    {
        Priority GetByID(Int64 id);
        void Insert(Priority info);
        Int64 Update(Priority info);
        List<Priority> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Priority> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Priority> GetPriorityDetails();
    }
}

