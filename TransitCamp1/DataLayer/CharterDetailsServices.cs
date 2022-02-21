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
    public class CharterDetailsServices : ICharterDetailsServices, IDisposable
    {
        #region Interface

        //create instance of data access context

        private TCContext context;
        public CharterDetailsServices(TCContext context)
        {
            this.context = context;
        }

        //get all Charter No
        public List<CharterDetails> GetAllCharterNo()
        {

            List<CharterDetails> list = new List<CharterDetails>();

            list = (from p in context.charterdetailsmasters
                    select new CharterDetails
                    {
                        ID = p.ID,
                        CharterNo = p.CharterNo
                    }).ToList();
            return list;
        }

        //get by id
        public CharterDetails GetByID(Int64 id)
        {
            var getdata = (from p in context.charterdetailsmasters
                           join a in context.airlinemasters on p.AirLineID equals a.ID
                           where p.ID == id
                           select new CharterDetails
                           {
                               AirLineID = a.ID,
                               AirlineName = a.AirlineName,
                               CharteredDate = p.CharteredDate,
                               CharterNo = p.CharterNo,
                               FlightNo = p.FlightNo,
                               NumberOfSeats = p.NumberOfSeats,
                               ToCityID = p.ToCityID,
                               FromCityID = p.FromCityID,
                               FromCity = p.FromCity,
                               ToCity = p.ToCity,
                               Session = p.Session
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(CharterDetails info)
        {

            var data = new charterdetailsmaster
            {
                AirLineID = info.AirLineID,
                FromCity = info.FromCity,
                FromCityID = info.FromCityID,
                ToCity = info.ToCity,
                ToCityID = info.ToCityID,
                CharteredDate = info.CharteredDate,
                CharterNo = info.CharterNo,
                FlightNo = info.FlightNo,
                NumberOfSeats = info.NumberOfSeats,
                Session = info.Session,
                CreatedOn = info.CreatedOn
            };
            context.charterdetailsmasters.Add(data);
        }

        //update
        public Int64 Update(CharterDetails info)
        {
            var data = (from p in context.charterdetailsmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.AirLineID = info.AirLineID;
            data.ToCityID = info.ToCityID;
            data.ToCity = info.ToCity;
            data.FromCity = info.FromCity;
            data.FromCityID = info.FromCityID;
            data.CharteredDate = info.CharteredDate;
            data.CharterNo = info.CharterNo;
            data.FlightNo = info.FlightNo;
            data.NumberOfSeats = info.NumberOfSeats;
            data.Session = info.Session;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List from database
        public List<CharterDetails> Paging(Int32 take, Int32 skip)
        {

            List<CharterDetails> list = new List<CharterDetails>();

            list = (from p in context.charterdetailsmasters
                    join a in context.airlinemasters on p.AirLineID equals a.ID
                    select new CharterDetails
                    {
                        ID = p.ID,
                        AirLineID = p.AirLineID,
                        ToCityID = p.ToCityID,
                        FromCityID = p.FromCityID,
                        AirlineName = a.AirlineName,
                        CharteredDate = p.CharteredDate,
                        CharterNo = p.CharterNo,
                        FlightNo = p.FlightNo,
                        NumberOfSeats = p.NumberOfSeats,
                        FromCity = p.FromCity,
                        ToCity = p.ToCity,
                        Session = p.Session
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.charterdetailsmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.charterdetailsmasters
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.charterdetailsmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<CharterDetails> GetSearchResult(String SearchText)
        {
            List<CharterDetails> list = new List<CharterDetails>();

            DateTime searchdate = Convert.ToDateTime(SearchText);

            list = (from p in context.charterdetailsmasters
                    join a in context.airlinemasters on p.AirLineID equals a.ID
                    where p.CharteredDate.Value.Day == searchdate.Day && p.CharteredDate.Value.Month == searchdate.Month && p.CharteredDate.Value.Year == searchdate.Year
                    select new CharterDetails
                    {
                        ID = p.ID,
                        AirLineID = a.ID,
                        AirlineName = a.AirlineName,
                        CharteredDate = p.CharteredDate,
                        CharterNo = p.CharterNo,
                        FlightNo = p.FlightNo,
                        NumberOfSeats = p.NumberOfSeats,
                        FromCity = p.FromCity,
                        ToCity = p.ToCity,
                        Session = p.Session,
                    }).OrderByDescending(x => x.ID).ToList();
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
