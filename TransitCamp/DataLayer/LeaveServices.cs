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
    public class LeaveServices : ILeaveServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public LeaveServices(TCContext context)
        {
            this.context = context;
        }

        //get by id
        public Leave GetID(Int64 id)
        {
            Leave div = new Leave();
            var getdata = (from p in context.leavemasters
                           where p.ID == id
                           select new Leave
                           {
                               LeaveType = p.LeaveType,
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Leave info)
        {
            var data = new leavemaster
            {
                LeaveType = info.LeaveType,
                CreatedOn = info.CreatedOn
            };
            context.leavemasters.Add(data);
        }

        //update
        public Int64 Update(Leave info)
        {
            var data = (from p in context.leavemasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.LeaveType = info.LeaveType;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Leave> Paging(Int32 take, Int32 skip)
        {
            List<Leave> list = new List<Leave>();
            list = (from p in context.leavemasters
                    select new Leave
                    {
                        ID = p.ID,
                        LeaveType = p.LeaveType
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.leavemasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.leavemasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.leavemasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Leave> GetSearchResult(String SearchText)
        {
            List<Leave> list = new List<Leave>();

            list = (from p in context.leavemasters
                    where p.LeaveType.Contains(SearchText)
                    select new Leave
                    {
                        ID = p.ID,
                        LeaveType = p.LeaveType,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.leavemasters.FirstOrDefault(u => u.LeaveType == info)?.ID;
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
