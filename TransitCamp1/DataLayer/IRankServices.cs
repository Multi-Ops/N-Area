using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRankServices
    {
        Ranks GetID(Int64 id);
        void Insert(Ranks info);
        Int64 Update(Ranks info);
        List<Ranks> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Ranks> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Ranks> GetRankDetails();
        List<Ranks> GetRankDetailsByCatID(Int64 CatID);
    }
}
