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
    public class OutLogicServices : IOutLogicServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public OutLogicServices(TCContext context)
        {
            this.context = context;
        }

        //get OutlogicDetails
        public List<OutLogic> getOLDetails()
        {
            List<OutLogic> list = new List<OutLogic>();
            list = (from p in context.outlogicmasters
                    select new OutLogic
                    {
                        ID = p.ID,
                        OutLogicName = p.OutLogicName
                    }).ToList();
            return list;
        }

        //get by id
        public OutLogic GetByID(Int64 id)
        {
            OutLogic div = new OutLogic();
            var getdata = (from p in context.outlogicmasters
                           where p.ID == id
                           select new OutLogic
                           {
                               ID = p.ID,
                               OutLogicName = p.OutLogicName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(OutLogic info)
        {
            var data = new outlogicmaster
            {
                OutLogicName = info.OutLogicName,
                CreatedOn = info.CreatedOn
            };
            context.outlogicmasters.Add(data);
        }

        //update
        public Int64 Update(OutLogic info)
        {
            var data = (from p in context.outlogicmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.OutLogicName = info.OutLogicName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<OutLogic> Paging(Int32 take, Int32 skip)
        {
            List<OutLogic> list = new List<OutLogic>();
            list = (from p in context.outlogicmasters
                    select new OutLogic
                    {
                        ID = p.ID,
                        OutLogicName = p.OutLogicName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.outlogicmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.outlogicmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.outlogicmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<OutLogic> GetSearchResult(String SearchText)
        {
            List<OutLogic> list = new List<OutLogic>();

            list = (from p in context.outlogicmasters
                    where p.OutLogicName.Contains(SearchText)
                    select new OutLogic
                    {
                        ID = p.ID,
                        OutLogicName = p.OutLogicName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.outlogicmasters.FirstOrDefault(u => u.OutLogicName == info)?.ID;
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
