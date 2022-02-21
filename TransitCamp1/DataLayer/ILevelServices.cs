using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ILevelServices
    {
        Level GetByID(Int64 id);
        void Insert(Level info);
        Int64 Update(Level info);
        List<Level> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Level> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Level> GetLevel();
    }
}
