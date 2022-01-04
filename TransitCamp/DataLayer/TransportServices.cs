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
    public class TransportServices : ITransportServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public TransportServices(TCContext context)
        {
            this.context = context;
        }

        //get details
        public List<Transport> GetDetails()
        {
            List<Transport> list = new List<Transport>();
            list = (from p in context.transportmasters
                    select new Transport
                    {
                        ID = p.ID,
                        TransportName = p.TransportName
                    }).ToList();
            return list;
        }

        //get by id
        public Transport GetID(Int64 id)
        {
            Transport div = new Transport();
            var getdata = (from p in context.transportmasters
                           where p.ID == id
                           select new Transport
                           {
                               TransportName = p.TransportName,
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Transport info)
        {
            var data = new transportmaster
            {
                TransportName = info.TransportName,
                CreatedOn = info.CreatedOn
            };
            context.transportmasters.Add(data);
        }

        //update
        public Int64 Update(Transport info)
        {
            var data = (from p in context.transportmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.TransportName = info.TransportName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Transport> Paging(Int32 take, Int32 skip)
        {
            List<Transport> list = new List<Transport>();
            list = (from p in context.transportmasters
                    select new Transport
                    {
                        ID = p.ID,
                        TransportName = p.TransportName
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.transportmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.transportmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.transportmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Transport> GetSearchResult(String SearchText)
        {
            List<Transport> list = new List<Transport>();

            list = (from p in context.transportmasters
                    where p.TransportName.Contains(SearchText)
                    select new Transport
                    {
                        ID = p.ID,
                        TransportName = p.TransportName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.transportmasters.FirstOrDefault(u => u.TransportName == info)?.ID;
            if (id == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(id);
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
