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
    public class RankServices : IRankServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public RankServices(TCContext context)
        {
            this.context = context;
        }

        //get Rank details With CategoryID
        public List<Ranks> GetRankDetailsByCatID(Int64 CatID)
        {
            List<Ranks> list = new List<Ranks>();

            list = (from p in context.rankmasters
                    where p.LevelID == CatID
                    select new Ranks
                    {
                        ID = p.ID,
                        Rank = p.Rank,
                    }).ToList();

            return list;
        }

        //get Rank details
        public List<Ranks> GetRankDetails()
        {
            List<Ranks> list = new List<Ranks>();

            list = (from p in context.rankmasters
                    select new Ranks
                    {
                        ID = p.ID,
                        Rank = p.Rank,
                    }).ToList();

            return list;
        }

        //get rank by id
        public Ranks GetID(Int64 id)
        {
            Ranks div = new Ranks();
            var getdata = (from p in context.rankmasters
                           join c in context.categorymasters on p.LevelID equals c.ID
                           where p.ID == id
                           select new Ranks
                           {
                               Rank = p.Rank,
                               LevelID = c.ID,
                               LevelName = c.CategoryName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert rank
        public void Insert(Ranks info)
        {
            var data = new rankmaster
            {
                Rank = info.Rank,
                LevelID = info.LevelID,
                CreatedOn = info.CreatedOn
            };
            context.rankmasters.Add(data);
        }

        //update rank
        public Int64 Update(Ranks info)
        {
            var data = (from p in context.rankmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.Rank = info.Rank;
            data.LevelID = info.LevelID;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of rank from database
        public List<Ranks> Paging(Int32 take, Int32 skip)
        {
            List<Ranks> list = new List<Ranks>();
            list = (from p in context.rankmasters
                    join c in context.categorymasters on p.LevelID equals c.ID
                    select new Ranks
                    {
                        ID = p.ID,
                        LevelID = c.ID,
                        LevelName = c.CategoryName,
                        Rank = p.Rank
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.rankmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.rankmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.rankmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list of rank from database
        public List<Ranks> GetSearchResult(String SearchText)
        {
            List<Ranks> list = new List<Ranks>();

            list = (from p in context.rankmasters
                    join c in context.categorymasters on p.LevelID equals c.ID
                    where p.Rank.Contains(SearchText)
                    select new Ranks
                    {
                        ID = p.ID,
                        Rank = p.Rank,
                        LevelName = c.CategoryName,
                        LevelID = c.ID,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if rank Name Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.rankmasters.FirstOrDefault(u => u.Rank == info)?.ID;
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
