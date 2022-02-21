using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IMoveServices
    {
        Move GetByID(Int64 id);
        void Insert(Move info);
        Int64 Update(Move info);
        List<Move> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Move> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Move> GetMoveDetails();
    }
}
