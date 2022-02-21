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
    public class TransferCourierToCharterServices : ITransferCourierToCharterServices, IDisposable
    {
        #region Interface
        //create instance of data access context
        private TCContext context;
        public TransferCourierToCharterServices(TCContext context)
        {
            this.context = context;
        }

        //get by id
        public TransferCourierToCharter GetByID(Int64 id)
        {
            var getdata = (from p in context.transfercouriermasters
                           where p.ID == id
                           select new TransferCourierToCharter
                           {
                               CityID = p.CityID,
                               CategoryID = p.CategoryID,
                               ICardNo = p.ICardNo,
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
                               PriorityStatusID = p.PriorityStatusID,
                               SeatNo = p.SeatNo,
                               Date = p.Date,
                               TransferDate = p.TransferDate,
                               CharterNo = p.CharterNo,
                               FlightDate = p.FlightDate
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(TransferCourierToCharter info)
        {

            var data = new transfercouriermaster
            {
                CityID = info.CityID,
                CategoryID = info.CategoryID,
                ICardNo = info.ICardNo,
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
                PriorityStatusID = info.PriorityStatusID,
                Date = info.Date,
                SeatNo = info.SeatNo,
                CharterNo = info.CharterNo,
                FlightDate = info.FlightDate,
                TransferDate = info.TransferDate,
                CreatedOn = info.CreatedOn
            };
            context.transfercouriermasters.Add(data);
        }

        //update
        public Int64 Update(TransferCourierToCharter info)
        {
            var data = (from p in context.transfercouriermasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CategoryID = info.CategoryID;
            data.CityID = info.CityID;
            data.Date = info.Date;
            data.Session = info.Session;
            data.ICardNo = info.ICardNo;
            data.ArmyNo = info.ArmyNo;
            data.RankID = info.RankID;
            data.Name = info.Name;
            data.UnitID = info.UnitID;
            data.DivID = info.DivID;
            data.HQID = info.HQID;
            data.Authority = info.Authority;
            data.MoveID = info.MoveID;
            data.PriorityID = info.PriorityID;
            data.PriorityStatusID = info.PriorityStatusID;
            data.SeatNo = info.SeatNo;
            data.TransferDate = info.TransferDate;
            data.FlightDate = info.FlightDate;
            data.CharterNo = info.CharterNo;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List from database
        public List<TransferCourierToCharter> Paging(Int32 take, Int32 skip)
        {

            List<TransferCourierToCharter> list = new List<TransferCourierToCharter>();

            list = (from p in context.transfercouriermasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    join ps in context.prioritystatusmasters on p.PriorityStatusID equals ps.ID
                    select new TransferCourierToCharter
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        CategoryName = c1.CategoryName,
                        ICardNo = p.ICardNo,
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
                        PStatusName = ps.PStatusName,
                        SeatNo = p.SeatNo,
                        Date = p.Date,
                        TransferDate = p.TransferDate,
                        FlightDate = p.FlightDate,
                        CharterNo = p.CharterNo,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.transfercouriermasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.transfercouriermasters
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.transfercouriermasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<TransferCourierToCharter> GetSearchResult(String SearchText)
        {
            List<TransferCourierToCharter> list = new List<TransferCourierToCharter>();

            list = (from p in context.transfercouriermasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join p1 in context.prioritymasters on p.PriorityID equals p1.ID
                    join ps in context.prioritystatusmasters on p.PriorityStatusID equals ps.ID
                    where p.Name.Contains(SearchText)
                    select new TransferCourierToCharter
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        CategoryName = c1.CategoryName,
                        ICardNo = p.ICardNo,
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
                        PStatusName = ps.PStatusName,
                        SeatNo = p.SeatNo,
                        Date = p.Date,
                        TransferDate = p.TransferDate,
                        FlightDate = p.FlightDate,
                        CharterNo = p.CharterNo,
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
