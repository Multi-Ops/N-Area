using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICharterDetailsServices
    {
        CharterDetails GetByID(Int64 id);
        void Insert(CharterDetails info);
        Int64 Update(CharterDetails info);
        List<CharterDetails> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<CharterDetails> GetSearchResult(String SearchText);
        void Save();
        List<CharterDetails> GetAllCharterNo();
    }
}
