using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IOutLogicServices
    {
        OutLogic GetByID(Int64 id);
        void Insert(OutLogic info);
        Int64 Update(OutLogic info);
        List<OutLogic> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<OutLogic> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<OutLogic> getOLDetails();
    }
}
