using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IADTypeServices
    {
        ADType GetByID(Int64 id);
        void Insert(ADType info);
        Int64 Update(ADType info);
        List<ADType> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<ADType> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<ADType> GetADTypeDetails();
    }
}
