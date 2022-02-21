using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BookingServices : IBookingServices
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public BookingServices(TCContext context)
        {
            this.context = context;
        }

        #region Blocks
        //get details
        public List<BllBlock> GetDetails()
        {
            List<BllBlock> list = new List<BllBlock>();
            list = (from p in context.blockmasters
                    select new BllBlock
                    {
                        Id = p.Id,
                        BlockName = p.BlockName,
                        DefaultPrice = p.DefaultPrice,
                        MaxRoomAvailable = p.MaxRoomAvailable,
                    }).ToList();
            return list;
        }

        //get by id
        public BllBlock GetID(Int64 id)
        {
            BllBlock div = new BllBlock();
            var getdata = (from p in context.blockmasters
                           where p.Id == id
                           select new BllBlock
                           {
                               Id = p.Id,
                               BlockName = p.BlockName,
                               DefaultPrice = p.DefaultPrice,
                               MaxRoomAvailable = p.MaxRoomAvailable,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(BllBlock info)
        {
            var data = new blockmaster
            {
                BlockName = info.BlockName,
                DefaultPrice = info.DefaultPrice,
                MaxRoomAvailable = info.MaxRoomAvailable,
                CreatedOn = info.CreatedOn,
            };
            context.blockmasters.Add(data);
        }

        //update
        public Int64 Update(BllBlock info)
        {
            var data = (from p in context.blockmasters
                        where p.Id == info.Id
                        select p).FirstOrDefault();
            data.BlockName = info.BlockName;
            data.DefaultPrice = info.DefaultPrice;
            data.MaxRoomAvailable = info.MaxRoomAvailable;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.Id;
        }

        //Get List of from database
        public List<BllBlock> Paging(Int32 take, Int32 skip)
        {
            List<BllBlock> list = new List<BllBlock>();
            list = (from p in context.blockmasters
                    select new BllBlock
                    {
                        Id = p.Id,
                        BlockName = p.BlockName,
                        DefaultPrice = p.DefaultPrice,
                        MaxRoomAvailable = p.MaxRoomAvailable
                    }).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.blockmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.blockmasters
                        where p.Id == ID
                        select p).FirstOrDefault();
            context.blockmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<BllBlock> GetSearchResult(String SearchText)
        {
            List<BllBlock> list = new List<BllBlock>();

            list = (from p in context.blockmasters
                    where p.BlockName.Contains(SearchText)
                    select new BllBlock
                    {
                        Id = p.Id,
                        BlockName = p.BlockName,
                        DefaultPrice = p.DefaultPrice,
                    }).OrderByDescending(x => x.Id).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.blockmasters.FirstOrDefault(u => u.BlockName == info)?.Id;
            if (id == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(id);
            }
        }
        #endregion

        #region Rooms
        //get details
        public List<BllRoom> GetDetailsRoom()
        {
            List<BllRoom> list = new List<BllRoom>();
            list = (from p in context.roommasters
                    where p.IsFull == false
                    select new BllRoom
                    {
                        Id = p.Id,
                        RoomName = p.RoomName,
                        MaxRoomCap = p.MaxRoomCap,
                        RoomPrice = p.RoomPrice,
                        IsShare = p.IsShare,
                        IsFull = p.IsFull,
                        BlockId = p.BlockId,
                        IsBillShare = p.IsBillShare,

                    }).ToList();
            return list;
        }

        //get by id
        public BllRoom GetIDRoom(Int64 id)
        {
            BllRoom div = new BllRoom();
            var getdata = (from p in context.roommasters
                           where p.Id == id
                           select new BllRoom
                           {
                               Id = p.Id,
                               RoomName = p.RoomName,
                               MaxRoomCap = p.MaxRoomCap,
                               RoomPrice = p.RoomPrice,
                               IsShare = p.IsShare,
                               BlockId = p.BlockId,
                               IsFull = p.IsFull,
                               IsBillShare = p.IsBillShare,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void InsertRoom(BllRoom info)
        {
            var data = new roommaster
            {
                RoomName = info.RoomName,
                MaxRoomCap = info.MaxRoomCap,
                RoomPrice = info.RoomPrice,
                IsShare = info.IsShare,
                BlockId = info.BlockId,
                IsFull = info.IsFull,
                IsBillShare = info.IsBillShare,
                CreatedOn = info.CreatedOn,
            };
            context.roommasters.Add(data);
        }

        //update
        public Int64 UpdateRoom(BllRoom info)
        {
            var data = (from p in context.roommasters
                        where p.Id == info.Id
                        select p).FirstOrDefault();
            data.RoomName = info.RoomName;
            data.MaxRoomCap = info.MaxRoomCap;
            data.RoomPrice = info.RoomPrice;
            data.IsShare = info.IsShare;
            data.UpdatedOn = info.UpdatedOn;
            data.BlockId = info.BlockId;
            data.IsFull = info.IsFull;
            data.IsBillShare = info.IsBillShare;
            context.Entry(data).State = EntityState.Modified;
            return data.Id;
        }

        //Get List of from database
        public List<BllRoom> PagingRoom(Int32 take, Int32 skip)
        {
            List<BllRoom> list = new List<BllRoom>();
            list = (from p in context.roommasters
                    select new BllRoom
                    {
                        Id = p.Id,
                        RoomName = p.RoomName,
                        MaxRoomCap = p.MaxRoomCap,
                        RoomPrice = p.RoomPrice,
                        IsShare = p.IsShare,
                        IsBillShare = p.IsBillShare,
                        IsFull = p.IsFull,
                        BlockId = p.BlockId
                    }).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItemsRoom()
        {
            var count = (from p in context.roommasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void DeleteRoom(Int64 ID)
        {
            var data = (from p in context.roommasters
                        where p.Id == ID
                        select p).FirstOrDefault();
            context.roommasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<BllRoom> GetSearchResultRoom(String SearchText)
        {
            List<BllRoom> list = new List<BllRoom>();

            list = (from p in context.roommasters
                    where p.RoomName.Contains(SearchText)
                    select new BllRoom
                    {
                        Id = p.Id,
                        RoomName = p.RoomName,
                        MaxRoomCap = p.MaxRoomCap,
                        RoomPrice = p.RoomPrice,
                        IsShare = p.IsShare,
                        IsBillShare = p.IsBillShare,
                        IsFull = p.IsFull,
                        BlockId = p.BlockId
                    }).OrderByDescending(x => x.Id).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExistRoom(string info)
        {
            var id = context.roommasters.FirstOrDefault(u => u.RoomName == info)?.Id;
            if (id == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(id);
            }
        }
        #endregion

        #region Billing Attributes
        //get details
        public List<BLLBillAttribute> GetDetailsBill()
        {
            List<BLLBillAttribute> list = new List<BLLBillAttribute>();
            list = (from p in context.billattributes
                    select new BLLBillAttribute
                    {
                        Id = p.Id,
                        ADID = p.ADID,
                        Name = p.Name,
                        LRCPrice = p.LRCPrice,
                        NonLRCPrice = p.NonLRCPrice
                    }).ToList();
            return list;
        }

        //get by id
        public BLLBillAttribute GetIDBill(Int64 id)
        {
            BLLBillAttribute div = new BLLBillAttribute();
            var getdata = (from p in context.billattributes
                           where p.Id == id
                           select new BLLBillAttribute
                           {
                               Id = p.Id,
                               ADID = p.ADID,
                               Name = p.Name,
                               LRCPrice = p.LRCPrice,
                               NonLRCPrice = p.NonLRCPrice,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void InsertBill(BLLBillAttribute info)
        {
            var data = new billattribute
            {
                Name = info.Name,
                ADID = info.ADID,
                LRCPrice = info.LRCPrice,
                NonLRCPrice = info.NonLRCPrice,
                CreatedOn = info.CreatedOn,
            };
            context.billattributes.Add(data);
        }

        //update
        public Int64 UpdateBill(BLLBillAttribute info)
        {
            var data = (from p in context.billattributes
                        where p.Id == info.Id
                        select p).FirstOrDefault();
            data.Name = info.Name;
            data.ADID = info.ADID;
            data.LRCPrice = info.LRCPrice;
            data.NonLRCPrice = info.NonLRCPrice;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.Id;
        }

        //Get List of from database
        public List<BLLBillAttribute> PagingBill(Int32 take, Int32 skip)
        {
            List<BLLBillAttribute> list = new List<BLLBillAttribute>();
            list = (from p in context.billattributes
                    select new BLLBillAttribute
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ADID = p.ADID,
                        LRCPrice = p.LRCPrice,
                        NonLRCPrice = p.NonLRCPrice,
                    }).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItemsBill()
        {
            var count = (from p in context.billattributes
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void DeleteBill(Int64 ID)
        {
            var data = (from p in context.billattributes
                        where p.Id == ID
                        select p).FirstOrDefault();
            context.billattributes.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<BLLBillAttribute> GetSearchResultBill(String SearchText)
        {
            List<BLLBillAttribute> list = new List<BLLBillAttribute>();

            list = (from p in context.billattributes
                    where p.Name.Contains(SearchText)
                    select new BLLBillAttribute
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ADID = p.ADID,
                        LRCPrice = p.LRCPrice,
                        NonLRCPrice = p.NonLRCPrice
                    }).OrderByDescending(x => x.Id).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExistBill(string info)
        {
            var id = context.billattributes.FirstOrDefault(u => u.Name == info)?.Id;
            if (id == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(id);
            }
        }
        #endregion

        #region Bookings
        //get details
        public List<Booking> GetDetailsBooking()
        {
            List<Booking> list = new List<Booking>();
            list = (from p in context.bookings
                    select new Booking
                    {
                        Id = p.Id,
                        BlockId = p.BlockId,
                        ADId = p.ADId,
                        RoomId = p.RoomId,
                    }).ToList();
            return list;
        }

        //get by id
        public Booking GetIDBooking(Int64 id)
        {
            Booking div = new Booking();
            var getdata = (from p in context.bookings
                           where p.Id == id
                           select new Booking
                           {
                               Id = p.Id,
                               RoomId = p.RoomId,
                               ADId = p.ADId,
                               BlockId = p.BlockId,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void InsertBooking(Booking info)
        {
            var data = new booking
            {
                RoomId = info.RoomId,
                BlockId = info.BlockId,
                ADId = info.ADId,
                CreatedOn = info.CreatedOn,
            };
            context.bookings.Add(data);
        }

        //update
        public Int64 UpdateBooking(Booking info)
        {
            var data = (from p in context.bookings
                        where p.Id == info.Id
                        select p).FirstOrDefault();
            data.RoomId = info.RoomId;
            data.ADId = info.ADId;
            data.BlockId = info.BlockId;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.Id;
        }

        //Get List of from database
        public List<Booking> PagingBooking(Int32 take, Int32 skip)
        {
            List<Booking> list = new List<Booking>();
            list = (from p in context.bookings
                    select new Booking
                    {
                        Id = p.Id,
                        BlockId = p.BlockId,
                        RoomId = p.RoomId,
                        ADId = p.ADId
                    }).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItemsBooking()
        {
            var count = (from p in context.bookings
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void DeleteBooking(Int64 ID)
        {
            var data = (from p in context.bookings
                        where p.Id == ID
                        select p).FirstOrDefault();
            context.bookings.Remove(data);
            context.SaveChanges();
        }

        #endregion

        //save context
        public void Save()
        {
            context.SaveChanges();
        }

        #endregion

        #region IDisposable Interface Implementation
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
