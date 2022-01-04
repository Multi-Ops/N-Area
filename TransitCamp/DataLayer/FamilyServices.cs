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
    public class FamilyServices : IFamilyServices, IDisposable
    {
        #region Interface
        //create instance of data access context
        private TCContext context;
        public FamilyServices(TCContext context)
        {
            this.context = context;
        }

        //get by id
        public FamilyEntry GetByID(Int64 id)
        {
            var getdata = (from p in context.familymasters
                           where p.ID == id
                           select new FamilyEntry
                           {
                               CityID = p.CityID,
                               CategoryID = p.CategoryID,
                               IDCard = p.IDCard,
                               ArmyNo = p.ArmyNo,
                               RankID = p.RankID,
                               Name = p.Name,
                               UnitID = p.UnitID,
                               DivID = p.DivID,
                               HQID = p.HQID,
                               Authority = p.Authority,
                               Session = p.Session,
                               MoveID = p.MoveID,
                               PriorityID = p.PriorityID,
                               FamilyName = p.FamilyName,
                               Age = p.Age,
                               Date = p.Date,
                               Sex = p.Sex,
                               PriorityNo = p.PriorityNo,
                               Relation = p.Relation,
                               NoOfLuggae = p.NoOfLuggae
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(FamilyEntry info)
        {

            var data = new familymaster
            {
                CityID = info.CityID,
                CategoryID = info.CategoryID,
                IDCard = info.IDCard,
                ArmyNo = info.ArmyNo,
                RankID = info.RankID,
                Name = info.Name,
                UnitID = info.UnitID,
                DivID = info.DivID,
                HQID = info.HQID,
                Authority = info.Authority,
                Session = info.Session,
                MoveID = info.MoveID,
                PriorityID = info.PriorityID,
                FamilyName = info.FamilyName,
                Date = info.Date,
                Age = info.Age,
                Sex = info.Sex,
                PriorityNo = info.PriorityNo,
                Relation = info.Relation,
                NoOfLuggae = info.NoOfLuggae,
                CreatedOn = info.CreatedOn
            };
            context.familymasters.Add(data);
        }

        //update
        public Int64 Update(FamilyEntry info)
        {
            var data = (from p in context.familymasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CategoryID = info.CategoryID;
            data.CityID = info.CityID;
            data.Date = info.Date;
            data.Session = info.Session;
            data.IDCard = info.IDCard;
            data.ArmyNo = info.ArmyNo;
            data.RankID = info.RankID;
            data.Name = info.Name;
            data.UnitID = info.UnitID;
            data.DivID = info.DivID;
            data.HQID = info.HQID;
            data.Authority = info.Authority;
            data.MoveID = info.MoveID;
            data.PriorityID = info.PriorityID;
            data.Age = info.Age;
            data.FamilyName = info.FamilyName;
            data.Relation = info.Relation;
            data.NoOfLuggae = info.NoOfLuggae;
            data.Sex = info.Sex;
            data.PriorityNo = info.PriorityNo;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List from database
        public List<FamilyEntry> Paging(Int32 take, Int32 skip)
        {

            List<FamilyEntry> list = new List<FamilyEntry>();

            list = (from p in context.familymasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    select new FamilyEntry
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        CategoryName = c1.CategoryName,
                        IDCard = p.IDCard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        MoveName = m.MoveName,
                        PriorityName = p1.PriorityName,
                        Age = p.Age,
                        Sex = p.Sex,
                        Date = p.Date,
                        PriorityNo = p.PriorityNo,
                        NoOfLuggae = p.NoOfLuggae,
                        FamilyName = p.FamilyName,
                        Relation = p.Relation
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.familymasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.familymasters
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.familymasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<FamilyEntry> GetSearchResult(String SearchText)
        {
            List<FamilyEntry> list = new List<FamilyEntry>();

            list = (from p in context.familymasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    where p.Name.Contains(SearchText)
                    select new FamilyEntry
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        CategoryName = c1.CategoryName,
                        IDCard = p.IDCard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        MoveName = m.MoveName,
                        PriorityName = p1.PriorityName,
                        Age = p.Age,
                        Sex = p.Sex,
                        Date = p.Date,
                        PriorityNo = p.PriorityNo,
                        NoOfLuggae = p.NoOfLuggae,
                        FamilyName = p.FamilyName,
                        Relation = p.Relation
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
