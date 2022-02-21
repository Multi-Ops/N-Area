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
    public class UnitServices : IUnitServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public UnitServices(TCContext context)
        {
            this.context = context;
        }

        //get Unit for AD
        public Unit GetDIVHQByUnitID(Int64 UnitID)
        {
            var data = (from p in context.unitmasters
                        where p.ID == UnitID
                        select new Unit
                        {
                            ID = p.ID,
                            UnitName = p.UnitName,
                            HQID = p.HQID,
                            DivID = p.DivID,
                            CityID = p.CityID,
                            BrigadeID = p.BrigadeID
                        }).FirstOrDefault();

            return data;
        }

        //get unit details
        public List<Unit> GetUnitDetails()
        {
            List<Unit> list = new List<Unit>();

            list = (from p in context.unitmasters
                    select new Unit
                    {
                        ID = p.ID,
                        UnitName = p.UnitName,
                        CityID = p.CityID,
                        BrigadeID = p.BrigadeID
                    }).ToList();

            return list;
        }

        //get by id
        public Unit GetID(Int64 id)
        {
            Unit div = new Unit();
            var getdata = (from p in context.unitmasters
                           join h in context.hqmasters on p.HQID equals h.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           where p.ID == id
                           select new Unit
                           {
                               UnitName = p.UnitName,
                               HQID = h.ID,
                               DivID = d.ID,
                               HQName = h.HQName,
                               DivisionName = d.DivName,
                               BrigadeID = p.BrigadeID,
                               CityID = p.CityID
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Unit info)
        {
            var data = new unitmaster
            {
                UnitName = info.UnitName,
                HQID = info.HQID,
                DivID = info.DivID,
                CityID = info.CityID,
                CreatedOn = info.CreatedOn,
                BrigadeID = info.BrigadeID
            };
            context.unitmasters.Add(data);
        }

        //update
        public Int64 Update(Unit info)
        {
            var data = (from p in context.unitmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.UnitName = info.UnitName;
            data.HQID = info.HQID;
            data.DivID = info.DivID;
            data.CityID = info.CityID;
            data.UpdatedOn = info.UpdatedOn;
            data.BrigadeID = info.BrigadeID;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Unit> Paging(Int32 take, Int32 skip)
        {
            List<Unit> list = new List<Unit>();
            list = (from p in context.unitmasters
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join b in context.brigadematers on p.BrigadeID equals b.Id
                    select new Unit
                    {
                        ID = p.ID,
                        UnitName = p.UnitName,
                        HQID = h.ID,
                        DivID = d.ID,
                        DivisionName = d.DivName,
                        HQName = h.HQName,
                        CityName = c.CityName,
                        BrigadeID = p.BrigadeID,
                        BName = b.Name
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.unitmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.unitmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.unitmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Unit> GetSearchResult(String SearchText)
        {
            List<Unit> list = new List<Unit>();

            list = (from p in context.unitmasters
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join b in context.brigadematers on p.BrigadeID equals b.Id

                    where p.UnitName.Contains(SearchText)
                    select new Unit
                    {
                        ID = p.ID,
                        UnitName = p.UnitName,
                        HQID = h.ID,
                        DivID = d.ID,
                        DivisionName = d.DivName,
                        HQName = h.HQName,
                        CityName = c.CityName,
                        BrigadeID = p.BrigadeID,
                        BName = b.Name
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.unitmasters.FirstOrDefault(u => u.UnitName == info)?.ID;
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
