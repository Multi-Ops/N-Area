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
    public class ADTypeServices : IADTypeServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public ADTypeServices(TCContext context)
        {
            this.context = context;
        }

        //get ADTypeDetails 
        public List<ADType> GetADTypeDetails()
        {
            List<ADType> list = new List<ADType>();
            list = (from p in context.adtypemasters
                    select new ADType
                    {
                        ID = p.ID,
                        ADTypeName = p.ADTypeName,
                    }).ToList();
            return list;
        }


        //get by id
        public ADType GetByID(Int64 id)
        {
            ADType div = new ADType();
            var getdata = (from p in context.adtypemasters
                           where p.ID == id
                           select new ADType
                           {
                               ID = p.ID,
                               ADTypeName = p.ADTypeName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(ADType info)
        {
            var data = new adtypemaster
            {
                ADTypeName = info.ADTypeName,
                CreatedOn = info.CreatedOn
            };
            context.adtypemasters.Add(data);
        }

        //update
        public Int64 Update(ADType info)
        {
            var data = (from p in context.adtypemasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.ADTypeName = info.ADTypeName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<ADType> Paging(Int32 take, Int32 skip)
        {
            List<ADType> list = new List<ADType>();
            list = (from p in context.adtypemasters
                    select new ADType
                    {
                        ID = p.ID,
                        ADTypeName = p.ADTypeName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.adtypemasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.adtypemasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.adtypemasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<ADType> GetSearchResult(String SearchText)
        {
            List<ADType> list = new List<ADType>();

            list = (from p in context.adtypemasters
                    where p.ADTypeName.Contains(SearchText)
                    select new ADType
                    {
                        ID = p.ID,
                        ADTypeName = p.ADTypeName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.adtypemasters.FirstOrDefault(u => u.ADTypeName == info)?.ID;
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
