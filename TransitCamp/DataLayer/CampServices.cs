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
    public class CampServices : ICampServices, IDisposable
    {

        #region Interface

        //create instance of data access context
        private TCContext context;
        public CampServices(TCContext context)
        {
            this.context = context;
        }

        //get camp details 
        public List<Camp> GetCampDetails()
        {
            List<Camp> list = new List<Camp>();

            list = (from p in context.campmasters
                    select new Camp
                    {
                        ID = p.ID,
                        CampName = p.CampName,
                    }).ToList();

            return list;
        }

        //get by id
        public Camp GetByID(Int64 id)
        {
            Camp div = new Camp();
            var getdata = (from p in context.campmasters
                           where p.ID == id
                           select new Camp
                           {
                               ID = p.ID,
                               CampName = p.CampName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Camp info)
        {
            var data = new campmaster
            {
                CampName = info.CampName,
                CreatedOn = info.CreatedOn
            };
            context.campmasters.Add(data);
        }

        //update
        public Int64 Update(Camp info)
        {
            var data = (from p in context.campmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CampName = info.CampName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Camp> Paging(Int32 take, Int32 skip)
        {
            List<Camp> list = new List<Camp>();
            list = (from p in context.campmasters
                    select new Camp
                    {
                        ID = p.ID,
                        CampName = p.CampName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.campmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.campmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.campmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Camp> GetSearchResult(String SearchText)
        {
            List<Camp> list = new List<Camp>();

            list = (from p in context.campmasters
                    where p.CampName.Contains(SearchText)
                    select new Camp
                    {
                        ID = p.ID,
                        CampName = p.CampName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.campmasters.FirstOrDefault(u => u.CampName == info)?.ID;
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
