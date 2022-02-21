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
    public class ADTDServices : IADTDServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public ADTDServices(TCContext context)
        {
            this.context = context;
        }

        //get by id
        public ADTDEntry GetByID(Int64 id)
        {
            var getdata = (from p in context.adtdmasters
                           where p.ID == id
                           select new ADTDEntry
                           {
                               LavelID = p.LavelID,
                               CityID = p.CityID,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankID = p.RankID,
                               Name = p.Name,
                               UnitID = p.UnitID,
                               HQID = p.HQID,
                               Session = p.Session,
                               MoveID = p.MoveID,
                               PriorityID = p.PriorityID,
                               OutDate = p.OutDate,
                               Remark = p.Remark
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(ADTDEntry info)
        {

            var data = new adtdmaster
            {
                LavelID = info.LavelID,
                CityID = info.CityID,
                ICard = info.ICard,
                ArmyNo = info.ArmyNo,
                RankID = info.RankID,
                Name = info.Name,
                UnitID = info.UnitID,
                HQID = info.HQID,
                Session = info.Session,
                MoveID = info.MoveID,
                PriorityID = info.PriorityID,
                OutDate = info.OutDate,
                Remark = info.Remark,
                CreatedOn = info.CreatedOn
            };
            context.adtdmasters.Add(data);
        }

        //update
        public Int64 Update(ADTDEntry info)
        {
            var data = (from p in context.adtdmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.LavelID = info.LavelID;
            data.CityID = info.CityID;
            data.Session = info.Session;
            data.ICard = info.ICard;
            data.ArmyNo = info.ArmyNo;
            data.RankID = info.RankID;
            data.Name = info.Name;
            data.UnitID = info.UnitID;
            data.HQID = info.HQID;
            data.MoveID = info.MoveID;
            data.PriorityID = info.PriorityID;
            data.OutDate = info.OutDate;
            data.Remark = info.Remark;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List from database
        public List<ADTDEntry> Paging(Int32 take, Int32 skip)
        {

            List<ADTDEntry> list = new List<ADTDEntry>();

            list = (from p in context.adtdmasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join l in context.levelmasters on p.LavelID equals l.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    select new ADTDEntry
                    {
                        ID = p.ID,
                        LevelName = l.LevelName,
                        CityName = c.CityName,
                        OutDate = p.OutDate,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        HQName = h.HQName,
                        MoveName = m.MoveName,
                        PriorityName = p1.PriorityName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        Name = p.Name,
                        Session = p.Session,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.adtdmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.adtdmasters
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.adtdmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<ADTDEntry> GetSearchResult(String SearchText)
        {
            List<ADTDEntry> list = new List<ADTDEntry>();

            list = (from p in context.adtdmasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join l in context.levelmasters on p.LavelID equals l.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    where p.Name.Contains(SearchText)
                    select new ADTDEntry
                    {
                        ID = p.ID,
                        LevelName = l.LevelName,
                        CityName = c.CityName,
                        OutDate = p.OutDate,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        HQName = h.HQName,
                        MoveName = m.MoveName,
                        PriorityName = p1.PriorityName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        Name = p.Name,
                        Session = p.Session,
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
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
