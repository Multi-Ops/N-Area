using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public interface IUnitServices
    {
        Unit GetID(Int64 id);
        void Insert(Unit info);
        Int64 Update(Unit info);
        List<Unit> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Unit> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Unit> GetUnitDetails();
        Unit GetDIVHQByUnitID(Int64 UnitID);
    }
}
