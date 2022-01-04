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
    public class LevelServices : ILevelServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public LevelServices(TCContext context)
        {
            this.context = context;
        }

        //get level details
        public List<Level> GetLevel()
        {
            List<Level> list = new List<Level>();
            list = (from p in context.levelmasters
                    select new Level
                    {
                        ID = p.ID,
                        LevelName = p.LevelName
                    }).ToList();
            return list;
        }

        //get by id
        public Level GetByID(Int64 id)
        {
            Level div = new Level();
            var getdata = (from p in context.levelmasters
                           where p.ID == id
                           select new Level
                           {
                               ID = p.ID,
                               LevelName = p.LevelName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Level info)
        {
            var data = new levelmaster
            {
                LevelName = info.LevelName,
                CreatedOn = info.CreatedOn
            };
            context.levelmasters.Add(data);
        }

        //update
        public Int64 Update(Level info)
        {
            var data = (from p in context.levelmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.LevelName = info.LevelName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Level> Paging(Int32 take, Int32 skip)
        {
            List<Level> list = new List<Level>();
            list = (from p in context.levelmasters
                    select new Level
                    {
                        ID = p.ID,
                        LevelName = p.LevelName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.levelmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.levelmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.levelmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Level> GetSearchResult(String SearchText)
        {
            List<Level> list = new List<Level>();

            list = (from p in context.levelmasters
                    where p.LevelName.Contains(SearchText)
                    select new Level
                    {
                        ID = p.ID,
                        LevelName = p.LevelName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.levelmasters.FirstOrDefault(u => u.LevelName == info)?.ID;
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
