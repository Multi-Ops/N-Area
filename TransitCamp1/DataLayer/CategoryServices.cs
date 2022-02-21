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
    public class CategoryServices : ICategoryServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public CategoryServices(TCContext context)
        {
            this.context = context;
        }

        //getdetails
        public List<Category> details()
        {
            List<Category> list = new List<Category>();

            list = (from p in context.categorymasters
                    select new Category
                    {
                        ID = p.ID,
                        CategoryName = p.CategoryName,
                    }).ToList();

            return list;
        }

        //get by id
        public Category GetByID(Int64 id)
        {
            Category div = new Category();
            var getdata = (from p in context.categorymasters
                           where p.ID == id
                           select new Category
                           {
                               ID = p.ID,
                               CategoryName = p.CategoryName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Category info)
        {
            var data = new categorymaster
            {
                CategoryName = info.CategoryName,
                CreatedOn = info.CreatedOn
            };
            context.categorymasters.Add(data);
        }

        //update
        public Int64 Update(Category info)
        {
            var data = (from p in context.categorymasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CategoryName = info.CategoryName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Category> Paging(Int32 take, Int32 skip)
        {
            List<Category> list = new List<Category>();
            list = (from p in context.categorymasters
                    select new Category
                    {
                        ID = p.ID,
                        CategoryName = p.CategoryName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.categorymasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.categorymasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.categorymasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Category> GetSearchResult(String SearchText)
        {
            List<Category> list = new List<Category>();

            list = (from p in context.categorymasters
                    where p.CategoryName.Contains(SearchText)
                    select new Category
                    {
                        ID = p.ID,
                        CategoryName = p.CategoryName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.categorymasters.FirstOrDefault(u => u.CategoryName == info)?.ID;
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
