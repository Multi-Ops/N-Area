using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITransportDetailsServices
    {
        void GetID(Int64 id);
        void Insert(TransportDetails info);
        Int64 InsertAndGetID(TransportDetails info);
        Int64 Update(TransportDetails info);
        List<TransportDetails> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<TransportDetails> GetSearchResult(String SearchText);
        TransportDetails CheckAlreadyExist(string info, DateTime date);
        void Save();
        TransportDetails GetDetailsByID(Int64 ID, int cid);
        TransportDetails GetDetailsByID(Int64 ID);
        TransportDetails GetDetailsByID(Int64 ID, Int64 cityID);
        List<TransportDetails> GetDetails();
        Int32 TotalItemsToday();
    }
}
