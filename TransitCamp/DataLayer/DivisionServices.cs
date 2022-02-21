using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using BusinessLayer;

namespace DataLayer
{
    public class DivisionServices : IDivisionServices, IDisposable
    {
        #region Interface

        #region Division

        //create instance of data access context
        private TCContext context;
        public DivisionServices(TCContext context)
        {
            this.context = context;
        }

        //get Division details
        public List<Division> GetDivtDetails()
        {
            List<Division> list = new List<Division>();

            list = (from p in context.divmasters
                    select new Division
                    {
                        ID = p.ID,
                        DivName = p.DivName,
                    }).ToList();

            return list;
        }

        //get division by id
        public Division GetDivID(Int64 id)
        {
            Division div = new Division();
            var getdiv = (from d in context.divmasters
                          join h in context.hqmasters on d.HQID equals h.ID
                          where d.ID == id
                          select new Division
                          {
                              DivName = d.DivName,
                              HeadquarterName = h.HQName,
                              HQID = h.ID
                          }).FirstOrDefault();
            return getdiv;
        }

        //Insert Division
        public void Insert(Division div)
        {
            var divmaster = new divmaster
            {
                DivName = div.DivName,
                HQID = div.HQID,
                CreatedOn = div.CreatedOn
            };
            context.divmasters.Add(divmaster);
        }

        //update division
        public Int64 Update(Division div)
        {
            var data = (from d in context.divmasters
                        where d.ID == div.ID
                        select d).FirstOrDefault();
            data.DivName = div.DivName;
            data.HQID = div.HQID;
            data.UpdatedOn = div.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of division from database
        public List<Division> Paging(Int32 take, Int32 skip)
        {
            List<Division> list = new List<Division>();
            list = (from d in context.divmasters
                    join h in context.hqmasters on d.HQID equals h.ID
                    select new Division
                    {
                        ID = d.ID,
                        DivName = d.DivName,
                        HeadquarterName = h.HQName
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from d in context.divmasters
                         select d).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from d in context.divmasters
                        where d.ID == ID
                        select d).FirstOrDefault();
            context.divmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list of divisions from database
        public List<Division> GetSearchResult(String SearchText)
        {
            List<Division> list = new List<Division>();

            list = (from d in context.divmasters
                    join h in context.hqmasters on d.HQID equals h.ID
                    where d.DivName.Contains(SearchText)
                    select new Division
                    {
                        ID = d.ID,
                        DivName = d.DivName,
                        HeadquarterName = h.HQName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Divison Name Already exist
        public int CheckAlreadyExist(string divname)
        {
            var divid = context.divmasters.FirstOrDefault(u => u.DivName == divname)?.ID;
            if (divid == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(divid);
            }
        }


        #endregion

        #region Brigarde

        //get Division details
        public List<Brigade> GetBrigDetails()
        {
            List<Brigade> list = new List<Brigade>();

            list = (from p in context.brigadematers
                    select new Brigade
                    {
                        Id = p.Id,
                        Name = p.Name,
                    }).ToList();

            return list;
        }

        //get division by id
        public Brigade GetBrigID(Int64 id)
        {
            Brigade div = new Brigade();
            var getdiv = (from p in context.brigadematers
                          where p.Id == id
                          select new Brigade
                          {
                              Name = p.Name,
                          }).FirstOrDefault();
            return getdiv;
        }

        //Insert Division
        public void InsertBrig(Brigade info)
        {
            var res = new brigademater
            {
                Name = info.Name,
                CreatedOn = info.CreatedOn
            };
            context.brigadematers.Add(res);
        }

        //update division
        public Int64 UpdateBrig(Brigade info)
        {
            var data = (from p in context.brigadematers
                        where p.Id == info.Id
                        select p).FirstOrDefault();
            data.Name = info.Name;
            data.UpdatedOn = info.UpdatedOn;

            context.Entry(data).State = EntityState.Modified;
            return data.Id;
        }

        //Get List of division from database
        public List<Brigade> PagingBrig(Int32 take, Int32 skip)
        {
            List<Brigade> list = new List<Brigade>();
            list = (from p in context.brigadematers
                    select new Brigade
                    {
                        Id = p.Id,
                        Name = p.Name,
                    }).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItemsBrig()
        {
            var count = (from p in context.brigadematers
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void DeleteBrig(Int64 ID)
        {
            var data = (from p in context.brigadematers
                        where p.Id == ID
                        select p).FirstOrDefault();
            context.brigadematers.Remove(data);
            context.SaveChanges();
        }

        //Get searched list of divisions from database
        public List<Brigade> GetSearchResultBrig(String SearchText)
        {
            List<Brigade> list = new List<Brigade>();

            list = (from d in context.brigadematers
                    where d.Name.Contains(SearchText)
                    select new Brigade
                    {
                        Id = d.Id,
                        Name = d.Name,
                    }).OrderByDescending(x => x.Id).ToList();

            return list;
        }

        //check if Divison Name Already exist
        public int CheckAlreadyExistBrig(string divname)
        {
            var divid = context.brigadematers.FirstOrDefault(u => u.Name == divname)?.Id;
            if (divid == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(divid);
            }
        }
        #endregion

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
