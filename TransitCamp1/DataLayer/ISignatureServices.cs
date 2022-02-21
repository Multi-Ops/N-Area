using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ISignatureServices
    {
        Signature GetByID(Int64 id);
        void Insert(Signature info);
        Int64 Update(Signature info);
        List<Signature> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Signature> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        Signature GetDetails();
    }
}
