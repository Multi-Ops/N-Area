using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITransferCourierToCharterServices
    {
        TransferCourierToCharter GetByID(Int64 id);
        Int64 Update(TransferCourierToCharter info);
        void Insert(TransferCourierToCharter info);
        List<TransferCourierToCharter> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<TransferCourierToCharter> GetSearchResult(String SearchText);
        void Save();
    }
}
