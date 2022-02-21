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
    public class MoveServices : IMoveServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public MoveServices(TCContext context)
        {
            this.context = context;
        }

        //get move details 
        public List<Move> GetMoveDetails()
        {
            List<Move> list = new List<Move>();
            list = (from p in context.movemasters
                    select new Move
                    {
                        ID = p.ID,
                        MoveName = p.MoveName,
                    }).ToList();
            return list;
        }

        //get by id
        public Move GetByID(Int64 id)
        {
            Move div = new Move();
            var getdata = (from p in context.movemasters
                           join c in context.levelmasters on p.CategoryID equals c.ID
                           where p.ID == id
                           select new Move
                           {
                               ID = p.ID,
                               MoveName = p.MoveName,
                               CategoryID = c.ID,
                               LevelName = c.LevelName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Move info)
        {
            var data = new movemaster
            {
                MoveName = info.MoveName,
                CategoryID = info.CategoryID,
                CreatedOn = info.CreatedOn
            };
            context.movemasters.Add(data);
        }

        //update
        public Int64 Update(Move info)
        {
            var data = (from p in context.movemasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CategoryID = info.CategoryID;
            data.MoveName = info.MoveName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Move> Paging(Int32 take, Int32 skip)
        {
            List<Move> list = new List<Move>();
            list = (from p in context.movemasters
                    join c in context.levelmasters on p.CategoryID equals c.ID
                    select new Move
                    {
                        ID = p.ID,
                        MoveName = p.MoveName,
                        CategoryID = c.ID,
                        LevelName = c.LevelName
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.movemasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.movemasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.movemasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Move> GetSearchResult(String SearchText)
        {
            List<Move> list = new List<Move>();

            list = (from p in context.movemasters
                    join c in context.levelmasters on p.CategoryID equals c.ID
                    where p.MoveName.Contains(SearchText)
                    select new Move
                    {
                        ID = p.ID,
                        MoveName = p.MoveName,
                        CategoryID = c.ID,
                        LevelName = c.LevelName
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.movemasters.FirstOrDefault(u => u.MoveName == info)?.ID;
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
