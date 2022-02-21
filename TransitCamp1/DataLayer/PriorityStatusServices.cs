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
    public class PriorityStatusServices : IPriorityStatusServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public PriorityStatusServices(TCContext context)
        {
            this.context = context;
        }

        //get Priority Status Details
        public List<PriorityStatus> GetPriorityStatusDetails()
        {
            List<PriorityStatus> list = new List<PriorityStatus>();
            list = (from p in context.prioritystatusmasters
                    select new PriorityStatus
                    {
                        ID = p.ID,
                        PStatusName = p.PStatusName,
                    }).ToList();
            return list;
        }

        //get by id
        public PriorityStatus GetByID(Int64 id)
        {
            PriorityStatus div = new PriorityStatus();
            var getdata = (from p in context.prioritystatusmasters
                           where p.ID == id
                           select new PriorityStatus
                           {
                               ID = p.ID,
                               PStatusName = p.PStatusName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(PriorityStatus info)
        {
            var data = new prioritystatusmaster
            {
                PStatusName = info.PStatusName,
                CreatedOn = info.CreatedOn
            };
            context.prioritystatusmasters.Add(data);
        }

        //update
        public Int64 Update(PriorityStatus info)
        {
            var data = (from p in context.prioritystatusmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.PStatusName = info.PStatusName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<PriorityStatus> Paging(Int32 take, Int32 skip)
        {
            List<PriorityStatus> list = new List<PriorityStatus>();
            list = (from p in context.prioritystatusmasters
                    select new PriorityStatus
                    {
                        ID = p.ID,
                        PStatusName = p.PStatusName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.prioritystatusmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.prioritystatusmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.prioritystatusmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<PriorityStatus> GetSearchResult(String SearchText)
        {
            List<PriorityStatus> list = new List<PriorityStatus>();

            list = (from p in context.prioritystatusmasters
                    where p.PStatusName.Contains(SearchText)
                    select new PriorityStatus
                    {
                        ID = p.ID,
                        PStatusName = p.PStatusName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.prioritystatusmasters.FirstOrDefault(u => u.PStatusName == info)?.ID;
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
