using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICampServices
    {
        Camp GetByID(Int64 id);
        void Insert(Camp info);
        Int64 Update(Camp info);
        List<Camp> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Camp> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Camp> GetCampDetails();
    }
}
