using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IHeadquarterServices
    {
        Headquarter GetID(Int64 id);
        void Insert(Headquarter hq);
        Int64 Update(Headquarter hq);
        List<Headquarter> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Headquarter> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string hqname);
        void Save();
        List<Headquarter> GetHQDetails();
    }
}
