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
    public class PriorityServices : IPriorityServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public PriorityServices(TCContext context)
        {
            this.context = context;
        }

        //get priority detaisl
        public List<Priority> GetPriorityDetails()
        {
            List<Priority> list = new List<Priority>();
            list = (from p in context.prioritymasters
                    select new Priority
                    {
                        ID = p.ID,
                        PriorityName = p.PriorityName,
                    }).ToList();
            return list;
        }

        //get by id
        public Priority GetByID(Int64 id)
        {
            Priority div = new Priority();
            var getdata = (from p in context.prioritymasters
                           where p.ID == id
                           select new Priority
                           {
                               ID = p.ID,
                               PriorityName = p.PriorityName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Priority info)
        {
            var data = new prioritymaster
            {
                PriorityName = info.PriorityName,
                CreatedOn = info.CreatedOn
            };
            context.prioritymasters.Add(data);
        }

        //update
        public Int64 Update(Priority info)
        {
            var data = (from p in context.prioritymasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.PriorityName = info.PriorityName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Priority> Paging(Int32 take, Int32 skip)
        {
            List<Priority> list = new List<Priority>();
            list = (from p in context.prioritymasters
                    select new Priority
                    {
                        ID = p.ID,
                        PriorityName = p.PriorityName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.prioritymasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.prioritymasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.prioritymasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Priority> GetSearchResult(String SearchText)
        {
            List<Priority> list = new List<Priority>();

            list = (from p in context.prioritymasters
                    where p.PriorityName.Contains(SearchText)
                    select new Priority
                    {
                        ID = p.ID,
                        PriorityName = p.PriorityName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.prioritymasters.FirstOrDefault(u => u.PriorityName == info)?.ID;
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
