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
    public class TransportDetailsServices : ITransportDetailsServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public TransportDetailsServices(TCContext context)
        {
            this.context = context;
        }

        //Get All Transport Details
        public List<TransportDetails> GetDetails()
        {
            DateTime presentdate = DateTime.Now;
            List<TransportDetails> list = new List<TransportDetails>();
            list = (from p in context.transportdetails
                    join d in context.transportmasters on p.TransportTypeID equals d.ID
                    where (p.Date >= presentdate.Date)
                    select new TransportDetails
                    {
                        ID = p.ID,
                        TransportDetail = p.TransportDetails,
                    }).ToList();
            return list;
        }

        //Get Details
        public TransportDetails GetDetailsByID(Int64 ID, int cid)
        {
            var list = (from p in context.transportdetails
                        join d in context.transportmasters on p.TransportTypeID equals d.ID
                        where p.ID == ID && p.CityID == cid
                        select new TransportDetails
                        {
                            ID = p.ID,
                            TransportDetail = p.TransportDetails,
                            TransportTypeID = p.TransportTypeID,
                            TransportType = d.TransportName,
                            TotalNoOfSeats = p.TotalNoOfSeats,
                            NoOfSeats = p.NoOfSeats,
                            CityID = p.CityID,
                            Session = p.Session,
                            Date = p.Date,
                            Load = p.Load,
                            PrioritySeats = p.PrioritySeats,

                        }).FirstOrDefault();
            return list;
        }

        public TransportDetails GetDetailsByID(Int64 ID)
        {
            var list = (from p in context.transportdetails
                        join d in context.transportmasters on p.TransportTypeID equals d.ID
                        where p.ID == ID
                        select new TransportDetails
                        {
                            ID = p.ID,
                            TransportDetail = p.TransportDetails,
                            TransportTypeID = p.TransportTypeID,
                            TransportType = d.TransportName,
                            TotalNoOfSeats = p.TotalNoOfSeats,
                            NoOfSeats = p.NoOfSeats,
                            CityID = p.CityID,
                            Session = p.Session,
                            Date = p.Date,
                            Load = p.Load,
                            PrioritySeats = p.PrioritySeats,

                        }).FirstOrDefault();
            return list;
        }

        public TransportDetails GetDetailsByID(Int64 ID, Int64 cityID)
        {
            var list = (from p in context.transportdetails
                        join d in context.transportmasters on p.TransportTypeID equals d.ID
                        where p.ID == ID && p.CityID == cityID
                        select new TransportDetails
                        {
                            ID = p.ID,
                            TransportDetail = p.TransportDetails,
                            TransportTypeID = p.TransportTypeID,
                            TransportType = d.TransportName,
                            TotalNoOfSeats = p.TotalNoOfSeats,
                            NoOfSeats = p.NoOfSeats,
                            CityID = p.CityID,
                            Session = p.Session,
                            Date = p.Date,
                            Load = p.Load,
                            PrioritySeats = p.PrioritySeats,

                        }).FirstOrDefault();
            return list;
        }

        //get by id
        public void GetID(Int64 id)
        {
            var getdata = (from p in context.transportdetails
                           where p.ID == id
                           select new TransportDetails
                           {
                               ID = p.ID,
                           }).FirstOrDefault();
        }

        //Insert
        public void Insert(TransportDetails info)
        {
            var data = new transportdetail
            {
                TransportDetails = info.TransportDetail,
                Date = info.Date,
                TotalNoOfSeats = info.TotalNoOfSeats,
                NoOfSeats = info.NoOfSeats,
                PrioritySeats = info.PrioritySeats,
                TransportTypeID = info.TransportTypeID,
                CityID = info.CityID,
                Session = info.Session,
                Load = info.Load,
                CreatedOn = info.CreatedOn
            };
            context.transportdetails.Add(data);
        }

        //Insert and get ID
        public Int64 InsertAndGetID(TransportDetails info)
        {
            var data = new transportdetail
            {
                TransportDetails = info.TransportDetail,
                Date = info.Date,
                TotalNoOfSeats = info.TotalNoOfSeats,
                NoOfSeats = info.NoOfSeats,
                PrioritySeats = info.PrioritySeats,
                TransportTypeID = info.TransportTypeID,
                CityID = info.CityID,
                Session = info.Session,
                Load = info.Load,
                CreatedOn = info.CreatedOn
            };
            context.transportdetails.Add(data);
            context.SaveChanges();
            return data.ID;
        }

        //update
        public Int64 Update(TransportDetails info)
        {
            var data = (from p in context.transportdetails
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.TransportDetails = info.TransportDetail;
            data.TransportTypeID = info.TransportTypeID;
            data.TotalNoOfSeats = info.TotalNoOfSeats;
            data.NoOfSeats = info.NoOfSeats;
            data.PrioritySeats = info.PrioritySeats;
            data.Date = info.Date;
            data.CityID = info.CityID;
            data.Session = info.Session;
            data.Load = info.Load;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database for pagging
        public List<TransportDetails> Paging(Int32 take, Int32 skip)
        {
            DateTime Currentdate = DateTime.Now;
            DateTime date = Currentdate.AddDays(1);
            List<TransportDetails> list = new List<TransportDetails>();
            list = (from p in context.transportdetails
                    join d in context.transportmasters on p.TransportTypeID equals d.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    where ((p.Date.Value.Day == date.Day || p.Date.Value.Day == Currentdate.Day) && p.Date.Value.Month == date.Month && p.Date.Value.Year == date.Year)
                    select new TransportDetails
                    {
                        ID = p.ID,
                        TransportDetail = p.TransportDetails,
                        TransportType = d.TransportName,
                        TotalNoOfSeats = p.TotalNoOfSeats,
                        CityName = c.CityName,
                        NoOfSeats = p.NoOfSeats,
                        Session = p.Session,
                        Load = p.Load,
                        Date = p.Date,
                        PrioritySeats = p.PrioritySeats
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.transportdetails
                         select p).Count();

            return count;
        }

        //Get total number of records present in DataBase for present day
        public Int32 TotalItemsToday()
        {
            DateTime today = DateTime.Now;
            var count = (from p in context.transportdetails
                         where p.Date.Value.Day == today.Day && p.Date.Value.Month == today.Month && p.Date.Value.Year == today.Year
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.transportdetails
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.transportdetails.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<TransportDetails> GetSearchResult(String SearchText)
        {
            DateTime date = DateTime.Now.AddDays(1);

            List<TransportDetails> list = new List<TransportDetails>();

            list = (from p in context.transportdetails
                    join d in context.transportmasters on p.TransportTypeID equals d.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    where p.TransportDetails.Contains(SearchText) || d.TransportName.Contains(SearchText)
                    where p.Date.Value.Day == date.Day && p.Date.Value.Month == date.Month && p.Date.Value.Year == date.Year

                    select new TransportDetails
                    {
                        ID = p.ID,
                        TransportDetail = p.TransportDetails,
                        TransportType = d.TransportName,
                        TotalNoOfSeats = p.TotalNoOfSeats,
                        NoOfSeats = p.NoOfSeats,
                        Date = p.Date,
                        Session = p.Session,
                        Load = p.Load,
                        CityName = c.CityName,
                        PrioritySeats = p.PrioritySeats
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public TransportDetails CheckAlreadyExist(string info, DateTime date)
        {
            var list = (from p in context.transportdetails
                        where p.TransportDetails == info && p.Date.Value.Day == date.Day && p.Date.Value.Month == date.Month && p.Date.Value.Year == date.Year
                        select new TransportDetails
                        {
                            ID = p.ID,
                            TransportDetail = p.TransportDetails,
                        }).FirstOrDefault();
            return list;
        }

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
