using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDivisionServices
    {
        Division GetDivID(Int64 id);
        void Insert(Division div);
        Int64 Update(Division div);
        List<Division> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Division> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string divname);
        void Save();
        List<Division> GetDivtDetails();
    }
}
