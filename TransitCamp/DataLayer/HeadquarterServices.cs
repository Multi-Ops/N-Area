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
    public class HeadquarterServices : IHeadquarterServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public HeadquarterServices(TCContext context)
        {
            this.context = context;
        }

        //Get Headquarter details
        public List<Headquarter> GetHQDetails()
        {
            List<Headquarter> list = new List<Headquarter>();

            list = (from p in context.hqmasters
                    select new Headquarter
                    {
                        ID = p.ID,
                        HQName = p.HQName,
                    }).ToList();

            return list;
        }

        //get HQ by id
        public Headquarter GetID(Int64 id)
        {
            Headquarter div = new Headquarter();
            var gethq = (from h in context.hqmasters
                         where h.ID == id
                         select new Headquarter
                         {
                             HQName = h.HQName
                         }).FirstOrDefault();
            return gethq;
        }

        //Insert HQ
        public void Insert(Headquarter hq)
        {
            var hqmaster = new hqmaster
            {
                HQName = hq.HQName,
                CreatedOn = hq.CreatedOn
            };
            context.hqmasters.Add(hqmaster);
        }

        //update HQ
        public Int64 Update(Headquarter hq)
        {
            var data = (from h in context.hqmasters
                        where h.ID == hq.ID
                        select h).FirstOrDefault();
            data.HQName = hq.HQName;
            data.UpdatedOn = hq.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of HQ from database
        public List<Headquarter> Paging(Int32 take, Int32 skip)
        {
            List<Headquarter> list = new List<Headquarter>();
            list = (from h in context.hqmasters
                    select new Headquarter
                    {
                        ID = h.ID,
                        HQName = h.HQName
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from h in context.hqmasters
                         select h).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from h in context.hqmasters
                        where h.ID == ID
                        select h).FirstOrDefault();
            context.hqmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list of HQ from database
        public List<Headquarter> GetSearchResult(String SearchText)
        {
            List<Headquarter> list = new List<Headquarter>();

            list = (from h in context.hqmasters
                    where h.HQName.Contains(SearchText)
                    select new Headquarter
                    {
                        ID = h.ID,
                        HQName = h.HQName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if HQ Name Already exist
        public int CheckAlreadyExist(string hqname)
        {
            var hqid = context.hqmasters.FirstOrDefault(u => u.HQName == hqname)?.ID;
            if (hqid == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(hqid);
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
