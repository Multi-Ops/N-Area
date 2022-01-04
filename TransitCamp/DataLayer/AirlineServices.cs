using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using BusinessLayer;

namespace DataLayer
{
    public class AirlineServices : IAirlineServices, IDisposable
    {

        #region Interface

        //create instance of data access context
        private TCContext context;
        public AirlineServices(TCContext context)
        {
            this.context = context;
        }

        //get airline details
        public List<Airline> GetAirlineDetails()
        {
            List<Airline> list = new List<Airline>();

            list = (from p in context.airlinemasters
                    select new Airline
                    {
                        ID = p.ID,
                        AirlineName = p.AirlineName,
                    }).ToList();

            return list;
        }

        //get airline by id
        public Airline GetAirlineByID(Int64 id)
        {
            Airline airline = new Airline();
            var getairline = (from a in context.airlinemasters
                              where a.ID == id
                              select new Airline
                              {
                                  AirlineName = a.AirlineName,
                                  Type = a.Type,
                              }).FirstOrDefault();
            return getairline;
        }


        //Insert airline
        public void Insert(Airline airline)
        {
            var airlinemaster = new airlinemaster
            {
                AirlineName = airline.AirlineName,
                Type = airline.Type,
                CreatedOn = airline.CreatedOn
            };
            context.airlinemasters.Add(airlinemaster);
        }

        //update airline
        public Int64 Update(Airline airline)
        {
            var data = (from a in context.airlinemasters
                        where a.ID == airline.ID
                        select a).FirstOrDefault();
            data.AirlineName = airline.AirlineName;
            data.Type = airline.Type;
            data.UpdatedOn = airline.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of airlines from database
        public List<Airline> Paging(Int32 take, Int32 skip)
        {
            List<Airline> list = new List<Airline>();
            list = (from a in context.airlinemasters
                    select new Airline
                    {
                        ID = a.ID,
                        AirlineName = a.AirlineName,
                        Type = a.Type
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from a in context.airlinemasters
                         select a).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from a in context.airlinemasters
                        where a.ID == ID
                        select a).FirstOrDefault();
            context.airlinemasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list of airlines from database
        public List<Airline> GetSearchResult(String SearchText)
        {
            List<Airline> list = new List<Airline>();

            list = (from a in context.airlinemasters
                    where a.AirlineName.Contains(SearchText)
                    select new Airline
                    {
                        ID = a.ID,
                        AirlineName = a.AirlineName,
                        Type = a.Type
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Airline Name Already exist
        public int CheckAlreadyExist(string airlinename)
        {
            var airlineid = context.airlinemasters.FirstOrDefault(u => u.AirlineName == airlinename)?.ID;
            if (airlineid == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(airlineid);
            }
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
