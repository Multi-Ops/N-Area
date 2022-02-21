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
    public class CancelServices : ICancelServices, IDisposable
    {

        #region Interface
        //create instance of data access context
        private TCContext context;
        public CancelServices(TCContext context)
        {
            this.context = context;
        }

        //get by id
        public Cancel GetByID(Int64 id)
        {
            var getdata = (from p in context.cancels
                           join r in context.rankmasters on p.RankID equals r.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           where p.ID == id
                           select new Cancel
                           {
                               TransportDetailID = p.TransportDetailID,
                               ManifestDate = p.ManifestDate,
                               MenifestNo = p.MenifestNo,
                               CityID = p.CityID,
                               Session = p.Session,
                               ADNO = p.ADNO,
                               ArmyNo = p.ArmyNo,
                               RankID = p.RankID,
                               Name = p.Name,
                               UnitID = p.UnitID,
                               ICard = p.ICard,
                               HQID = p.HQID,
                               HQName = h.HQName,
                               Rank = r.Rank,
                               UnitName = u.UnitName,
                               ADID = p.ADID,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn,
                           }).FirstOrDefault();
            return getdata;
        }

        //get by id
        public Cancel IndividualGetByID(Int64 id)
        {
            var getdata = (from p in context.cancelindividuals
                           join r in context.rankmasters on p.RankID equals r.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           where p.ID == id
                           select new Cancel
                           {
                               TransportDetailID = p.TransportDetailID,
                               ManifestDate = p.ManifestDate,
                               MenifestNo = p.MenifestNo,
                               CityID = p.CityID,
                               Session = p.Session,
                               ADNO = p.ADNO,
                               ArmyNo = p.ArmyNo,
                               RankID = p.RankID,
                               Name = p.Name,
                               UnitID = p.UnitID,
                               ICard = p.ICard,
                               HQID = p.HQID,
                               HQName = h.HQName,
                               Rank = r.Rank,
                               UnitName = u.UnitName,
                               ADID = p.ADID,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn,
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public Int64 Insert(Cancel info)
        {

            var data = new cancel
            {
                CityID = info.CityID,
                Session = info.Session,
                TransportDetailID = info.TransportDetailID,
                ManifestDate = info.ManifestDate,
                MenifestNo = info.MenifestNo,
                ArmyNo = info.ArmyNo,
                ADNO = info.ADNO,
                RankID = info.RankID,
                Name = info.Name,
                UnitID = info.UnitID,
                ICard = info.ICard,
                HQID = info.HQID,
                ADID = info.ADID,
                TransportDetails = info.TransportDetails,
                CreatedOn = info.CreatedOn,
                CategoryID = info.CategoryID,
            };
            context.cancels.Add(data);
            return data.ID;
        }

        //Insert Individal Cancel
        public Int64 IndivialInsert(Cancel info)
        {
            var data = new cancelindividual
            {
                CityID = info.CityID,
                Session = info.Session,
                TransportDetailID = info.TransportDetailID,
                ManifestDate = info.ManifestDate,
                MenifestNo = info.MenifestNo,
                ArmyNo = info.ArmyNo,
                ADNO = info.ADNO,
                RankID = info.RankID,
                Name = info.Name,
                UnitID = info.UnitID,
                ICard = info.ICard,
                HQID = info.HQID,
                ADID = info.ADID,
                TransportDetails = info.TransportDetails,
                CreatedOn = info.CreatedOn,
                CategoryID = info.CategoryID,
            };
            context.cancelindividuals.Add(data);
            return data.ID;
        }

        //update
        public Int64 Update(Cancel info)
        {
            var data = (from p in context.manifestmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CityID = info.CityID;
            data.Session = info.Session;
            data.ManifestDate = info.ManifestDate;
            data.TransportDetailID = info.TransportDetailID;
            data.MenifestNo = info.MenifestNo;
            data.ADNO = info.ADNO;
            data.ArmyNo = info.ArmyNo;
            data.ICard = info.ICard;
            data.Name = info.Name;
            data.UnitID = info.UnitID;
            data.HQID = info.HQID;
            data.ADID = info.ADID;
            data.RankID = info.RankID;
            data.TransportDetails = info.TransportDetails;
            data.CategoryID = info.CategoryID;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //update Individual Cancel
        public Int64 IndividualUpdate(Cancel info)
        {
            var data = (from p in context.cancelindividuals
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CityID = info.CityID;
            data.Session = info.Session;
            data.ManifestDate = info.ManifestDate;
            data.TransportDetailID = info.TransportDetailID;
            data.MenifestNo = info.MenifestNo;
            data.ADNO = info.ADNO;
            data.ArmyNo = info.ArmyNo;
            data.ICard = info.ICard;
            data.Name = info.Name;
            data.UnitID = info.UnitID;
            data.HQID = info.HQID;
            data.ADID = info.ADID;
            data.RankID = info.RankID;
            data.TransportDetails = info.TransportDetails;
            data.CategoryID = info.CategoryID;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.cancels
                         select p).Count();

            return count;
        }

        //Get total number of records present in DataBase Cancel Individual
        public Int32 IndividualTotalItems()
        {
            var count = (from p in context.cancelindividuals
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.cancels
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.cancels.Remove(data);
            context.SaveChanges();
        }

        //Delete from Database by ID
        public void IndividualDelete(Int64 ID)
        {
            var data = (from p in context.cancelindividuals
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.cancelindividuals.Remove(data);
            context.SaveChanges();
        }

        //save context
        public void Save()
        {
            context.SaveChanges();
        }
        public void IndividualSave()
        {
            context.SaveChanges();
        }

        //Get Manifest NO from cancel table 
        public List<Cancel> GetManifest()
        {
            List<Cancel> list = new List<Cancel>();
            list = (from p in context.cancels
                    group p by p.MenifestNo into g
                    select new Cancel
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //Get Manifest NO from cancel table 
        public List<Cancel> GetManifestByDate(DateTime date, int cid)
        {
            DateTime nextday = date.AddDays(1);
            List<Cancel> list = new List<Cancel>();
            list = (from p in context.cancels
                    where p.CreatedOn >= date && p.CreatedOn < nextday && p.CityID == cid
                    group p by p.MenifestNo into g
                    select new Cancel
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //Get Manifest NO from cancel table by date range
        public List<Cancel> GetManifestByDateRange(DateTime fromDate, DateTime toDate, int cid)
        {
            List<Cancel> list = new List<Cancel>();
            list = (from p in context.cancels
                    where p.CreatedOn >= fromDate && p.CreatedOn <= toDate && p.CityID == cid
                    group p by p.MenifestNo into g
                    select new Cancel
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //get by for report by cat ID from cancel table
        public Cancel GetManifestForReport(string ManifestNo)
        {
            var data = (from p in context.cancels
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo
                        select new Cancel
                        {
                            ID = p.ID,
                            TransportDetails = p.TransportDetails,
                            TransportDetailID = p.TransportDetailID,
                            ManifestDate = p.ManifestDate,
                            CityName = c.CityName,
                            CategoryName = c1.CategoryName,
                            MenifestNo = p.MenifestNo,
                            CreatedOn = p.CreatedOn,
                        }).FirstOrDefault();
            return data;
        }

        //get by for report by cat ID from cancel table by date
        public Cancel GetManifestForReportByDate(string ManifestNo, DateTime date, int cid)
        {
            DateTime nextDate = date.AddDays(1);
            var data = (from p in context.cancels
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo && (p.CreatedOn >= date && p.CreatedOn < nextDate) && p.CityID == cid
                        select new Cancel
                        {
                            ID = p.ID,
                            TransportDetails = p.TransportDetails,
                            TransportDetailID = p.TransportDetailID,
                            ManifestDate = p.ManifestDate,
                            CityName = c.CityName,
                            CategoryName = c1.CategoryName,
                            MenifestNo = p.MenifestNo,
                            CreatedOn = p.CreatedOn
                        }).FirstOrDefault();
            return data;
        }

        //get by for report by cat ID from cancel table by date range
        public Cancel GetManifestForReportByDateRange(string ManifestNo, DateTime fromDate, DateTime toDate, int cid)
        {
            var data = (from p in context.cancels
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo && (p.CreatedOn >= fromDate && p.CreatedOn <= toDate) && p.CityID == cid
                        select new Cancel
                        {
                            ID = p.ID,
                            TransportDetails = p.TransportDetails,
                            TransportDetailID = p.TransportDetailID,
                            ManifestDate = p.ManifestDate,
                            CityName = c.CityName,
                            CategoryName = c1.CategoryName,
                            MenifestNo = p.MenifestNo,
                            CreatedOn = p.CreatedOn
                        }).FirstOrDefault();
            return data;
        }

        //get count category wise data from cancel table
        public object GetManifestADByCategoryID(Int64 id, string manifestno, int cid)
        {
            var getdata = (from p in context.cancels
                           where p.CategoryID == id && p.MenifestNo == manifestno && p.CityID == cid
                           select p).Count();
            return getdata;
        }

        //get count Load data from cancel table
        public object GetManifestADByLoad(string manifestno, int cid)
        {
            var getdata = (from p in context.manifestmasters
                           join a in context.admasters on p.ADID equals a.ID
                           where p.MenifestNo == manifestno && a.IsLoad == true && p.CityID == cid
                           select p).Count();
            return getdata;
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
