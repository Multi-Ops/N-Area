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
    public class CharterADServices : ICharterADServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public CharterADServices(TCContext context)
        {
            this.context = context;
        }

        //get by id
        public CharterADEntery GetByID(Int64 id)
        {
            var getdata = (from p in context.charteradmasters
                           where p.ID == id
                           select new CharterADEntery
                           {
                               CityID = p.CityID,
                               CategoryID = p.CategoryID,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankID = p.RankID,
                               Name = p.Name,
                               UnitID = p.UnitID,
                               DivID = p.DivID,
                               HQID = p.HQID,
                               Session = p.Session,
                               MoveID = p.MoveID,
                               PriorityID = p.PriorityID,
                               Date = p.Date,
                               SeatNo = p.SeatNo,
                               PriorityStatusID = p.PriorityStatusID,
                               CampID = p.CampID,
                               FlightDate = p.FlightDate,
                               CharterNO = p.CharterNo
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(CharterADEntery info)
        {

            var data = new charteradmaster
            {
                CityID = info.CityID,
                CategoryID = info.CategoryID,
                ICard = info.ICard,
                ArmyNo = info.ArmyNo,
                RankID = info.RankID,
                Name = info.Name,
                UnitID = info.UnitID,
                DivID = info.DivID,
                HQID = info.HQID,
                Session = info.Session,
                MoveID = info.MoveID,
                PriorityID = info.PriorityID,
                Date = info.Date,
                FlightDate = info.FlightDate,
                PriorityStatusID = info.PriorityStatusID,
                SeatNo = info.SeatNo,
                CampID = info.CampID,
                CharterNo = info.CharterNO,
                CreatedOn = info.CreatedOn
            };
            context.charteradmasters.Add(data);
        }

        //update
        public Int64 Update(CharterADEntery info)
        {
            var data = (from p in context.charteradmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CategoryID = info.CategoryID;
            data.CityID = info.CityID;
            data.Date = info.Date;
            data.Session = info.Session;
            data.ICard = info.ICard;
            data.ArmyNo = info.ArmyNo;
            data.RankID = info.RankID;
            data.Name = info.Name;
            data.UnitID = info.UnitID;
            data.DivID = info.DivID;
            data.HQID = info.HQID;
            data.MoveID = info.MoveID;
            data.PriorityID = info.PriorityID;
            data.FlightDate = info.FlightDate;
            data.SeatNo = info.SeatNo;
            data.CharterNo = info.CharterNO;
            data.PriorityStatusID = info.PriorityStatusID;
            data.CampID = info.CampID;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List from database
        public List<CharterADEntery> Paging(Int32 take, Int32 skip)
        {

            List<CharterADEntery> list = new List<CharterADEntery>();

            list = (from p in context.charteradmasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    join ps in context.prioritystatusmasters on p.PriorityStatusID equals ps.ID
                    join ca in context.campmasters on p.CampID equals ca.ID
                    select new CharterADEntery
                    {
                        ID = p.ID,
                        CategoryName = c1.CategoryName,
                        CityName = c.CityName,
                        Date = p.Date,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        MoveName = m.MoveName,
                        PriorityName = p1.PriorityName,
                        PriorityStatusName = ps.PStatusName,
                        CampName = ca.CampName,
                        FlightDate = p.FlightDate,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        Name = p.Name,
                        Session = p.Session,
                        SeatNo = p.SeatNo
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.charteradmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.charteradmasters
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.charteradmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<CharterADEntery> GetSearchResult(String SearchText)
        {
            List<CharterADEntery> list = new List<CharterADEntery>();

            list = (from p in context.charteradmasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    join ps in context.prioritystatusmasters on p.PriorityStatusID equals ps.ID
                    join ca in context.campmasters on p.CampID equals ca.ID
                    where p.Name.Contains(SearchText)
                    select new CharterADEntery
                    {
                        ID = p.ID,
                        CategoryName = c1.CategoryName,
                        CityName = c.CityName,
                        Date = p.Date,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        MoveName = m.MoveName,
                        PriorityName = p1.PriorityName,
                        PriorityStatusName = ps.PStatusName,
                        CampName = ca.CampName,
                        FlightDate = p.FlightDate,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        Name = p.Name,
                        Session = p.Session,
                        SeatNo = p.SeatNo
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
