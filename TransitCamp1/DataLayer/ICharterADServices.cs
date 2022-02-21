using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICharterADServices
    {
        CharterADEntery GetByID(Int64 id);
        void Insert(CharterADEntery info);
        Int64 Update(CharterADEntery info);
        List<CharterADEntery> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<CharterADEntery> GetSearchResult(String SearchText);
        void Save();
    }
}
