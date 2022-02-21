using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITransportServices
    {
        Transport GetID(Int64 id);
        void Insert(Transport info);
        Int64 Update(Transport info);
        List<Transport> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Transport> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Transport> GetDetails();
    }
}
