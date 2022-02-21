using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IBookingServices
    {
        List<BllBlock> GetDetails();
        BllBlock GetID(Int64 id);
        void Insert(BllBlock info);
        Int64 Update(BllBlock info);
        List<BllBlock> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<BllBlock> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);

        //room
        List<BllRoom> GetDetailsRoom();
        BllRoom GetIDRoom(Int64 id);
        void InsertRoom(BllRoom info);
        Int64 UpdateRoom(BllRoom info);
        List<BllRoom> PagingRoom(Int32 take, Int32 skip);
        Int32 TotalItemsRoom();
        void DeleteRoom(Int64 ID);
        List<BllRoom> GetSearchResultRoom(String SearchText);
        int CheckAlreadyExistRoom(string info);

        //Billing Attributres
        List<BLLBillAttribute> GetDetailsBill();
        BLLBillAttribute GetIDBill(Int64 id);
        void InsertBill(BLLBillAttribute info);
        Int64 UpdateBill(BLLBillAttribute info);
        List<BLLBillAttribute> PagingBill(Int32 take, Int32 skip);
        Int32 TotalItemsBill();
        void DeleteBill(Int64 ID);
        List<BLLBillAttribute> GetSearchResultBill(String SearchText);
        int CheckAlreadyExistBill(string info);

        //Bookings Attributres
        List<Booking> GetDetailsBooking();
        Booking GetIDBooking(Int64 id);
        Booking GetBookingByADID(Int64 id);
        void InsertBooking(Booking info);
        Int64 UpdateBooking(Booking info);
        List<Booking> PagingBooking(Int32 take, Int32 skip);
        Int32 TotalItemsBooking();
        void DeleteBooking(Int64 ID);

        void Save();
    }
}
