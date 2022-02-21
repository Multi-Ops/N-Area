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
    public class ManifestServices : IManifestServices, IDisposable
    {
        #region Interface
        //create instance of data access context
        private TCContext context;
        public ManifestServices(TCContext context)
        {
            this.context = context;
        }

        public List<Manifest> GetManifestWithManifestNo(string ManifestNO)
        {
            List<Manifest> manifest = new List<Manifest>();
            manifest = (from p in context.manifestmasters
                        join a in context.admasters on p.ADID equals a.ID
                        where p.MenifestNo == ManifestNO
                        select new Manifest
                        {
                            ID = p.ID,
                            MenifestNo = p.MenifestNo,
                            ADID = p.ADID,
                            TransportDetailID = p.TransportDetailID,
                            CategoryID = p.CategoryID,
                            ManifestDate = p.ManifestDate,
                            CityID = p.CityID,
                            Session = p.Session,
                            ADNO = p.ADNO,
                            ArmyNo = p.ArmyNo,
                            RankID = p.RankID,
                            Name = p.Name,
                            UnitID = p.UnitID,
                            ICard = p.ICard,
                            HQID = p.HQID,
                            TransportDetails = p.TransportDetails,
                            IsReserve = a.IsReserve,
                            CreatedOn = p.CreatedOn,
                            UpdatedOn = p.UpdatedOn,
                        }).ToList();
            return manifest;
        }

        public List<Manifest> GetManifestWithManifestNo(string ManifestNO, int tid)
        {
            List<Manifest> manifest = new List<Manifest>();
            manifest = (from p in context.manifestmasters
                        join a in context.admasters on p.ADID equals a.ID
                        where p.MenifestNo == ManifestNO && p.TransportDetailID == tid
                        select new Manifest
                        {
                            ID = p.ID,
                            MenifestNo = p.MenifestNo,
                            ADID = p.ADID,
                            TransportDetailID = p.TransportDetailID,
                            CategoryID = p.CategoryID,
                            ManifestDate = p.ManifestDate,
                            CityID = p.CityID,
                            Session = p.Session,
                            ADNO = p.ADNO,
                            ArmyNo = p.ArmyNo,
                            RankID = p.RankID,
                            Name = p.Name,
                            UnitID = p.UnitID,
                            ICard = p.ICard,
                            HQID = p.HQID,
                            TransportDetails = p.TransportDetails,
                            IsReserve = a.IsReserve,
                            CreatedOn = p.CreatedOn,
                            UpdatedOn = p.UpdatedOn,
                        }).ToList();
            return manifest;
        }

        public List<Manifest> GetManifestWithManifestNo(string ManifestNO, DateTime date)
        {
            List<Manifest> manifest = new List<Manifest>();
            manifest = (from p in context.manifestmasters
                        join a in context.admasters on p.ADID equals a.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        where p.MenifestNo == ManifestNO && p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year
                        select new Manifest
                        {
                            ID = p.ID,
                            MenifestNo = p.MenifestNo,
                            ADID = p.ADID,
                            TransportDetailID = p.TransportDetailID,
                            CategoryID = p.CategoryID,
                            ManifestDate = p.ManifestDate,
                            CityID = p.CityID,
                            Session = p.Session,
                            ADNO = p.ADNO,
                            ArmyNo = p.ArmyNo,
                            RankID = p.RankID,
                            Name = p.Name,
                            UnitID = p.UnitID,
                            ICard = p.ICard,
                            HQID = p.HQID,
                            TransportDetails = p.TransportDetails,
                            IsReserve = a.IsReserve,
                            CreatedOn = p.CreatedOn,
                            UpdatedOn = p.UpdatedOn,
                            CityName = c.CityName
                        }).ToList();
            return manifest;
        }

        //get by ADid
        public Int64 GetByADID(Int64 id)
        {
            var getdata = (from p in context.manifestmasters
                           where p.ADID == id
                           select p.ID).FirstOrDefault();
            return Convert.ToInt64(getdata);
        }

        //get by TransportID
        public object GetByTransportIDDateWise(Int64 id)
        {
            DateTime today = DateTime.Now;
            var getdata = (from p in context.manifestmasters
                           where p.TransportDetailID == id && p.ManifestDate.Value.Day == today.Day && p.ManifestDate.Value.Month == today.Month && p.ManifestDate.Value.Year == today.Year
                           select p).FirstOrDefault();
            if (getdata != null)
                return getdata.MenifestNo;
            else
                return null;
        }

        //get count category wise data
        public object GetManifestADByCategoryID(Int64 id, string manifestno, int cid)
        {
            var getdata = (from p in context.manifestmasters
                           where p.CategoryID == id && p.MenifestNo == manifestno && p.CityID == cid
                           select p).Count();
            return getdata;
        }

        //get count Load data
        public object GetManifestADByLoad(string manifestno, int cid)
        {
            var getdata = (from p in context.manifestmasters
                           join a in context.admasters on p.ADID equals a.ID
                           where p.MenifestNo == manifestno && a.IsLoad == true && p.CityID == cid
                           select p).Count();
            return getdata;
        }

        //get by ADid
        public Int64 CheckManifestExist(string ManifestNo)
        {
            var getdata = (from p in context.manifestmasters
                           where p.MenifestNo == ManifestNo
                           select p.ID).FirstOrDefault();
            return Convert.ToInt64(getdata);
        }

        //get by for report by cat ID
        public Manifest GetManifestForReport(string ManifestNo)
        {
            var data = (from p in context.manifestmasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo
                        select new Manifest
                        {
                            ID = p.ID,
                            TransportDetails = p.TransportDetails,
                            TransportDetailID = p.TransportDetailID,
                            ManifestDate = p.ManifestDate,
                            CityName = c.CityName,
                            CategoryName = c1.CategoryName,
                            MenifestNo = p.MenifestNo,
                        }).FirstOrDefault();
            return data;
        }

        //get by for report by cat ID from cancel table by date
        public Manifest GetManifestForReportByDate(string ManifestNo, DateTime date, int cid)
        {
            DateTime nextDate = date.AddDays(1);
            var data = (from p in context.manifestmasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo && p.CityID == cid && (p.ManifestDate >= date && p.ManifestDate < nextDate)
                        select new Manifest
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

        //get by for report by cat ID from cancel table by date
        public Manifest GetManifestForReportByDateRange(string ManifestNo, DateTime fromDate, DateTime toDate, int cid)
        {
            var data = (from p in context.manifestmasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo && p.CityID == cid && (p.ManifestDate >= fromDate && p.ManifestDate <= toDate)
                        select new Manifest
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

        //get by for report by cat ID 3
        public Manifest GetManifestForReportbycat2(string ManifestNo)
        {
            var data = (from p in context.manifestmasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo && a.CategoryID == 3
                        select new Manifest
                        {
                            ID = p.ID,
                            TransportDetails = p.TransportDetails,
                            ManifestDate = p.ManifestDate,
                            CityName = c.CityName,
                            CategoryName = c1.CategoryName,
                        }).FirstOrDefault();
            return data;
        }

        //get by for report by cat ID 5
        public Manifest GetManifestForReportbycat5(string ManifestNo)
        {
            var data = (from p in context.manifestmasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        where p.MenifestNo == ManifestNo && a.CategoryID == 5
                        select new Manifest
                        {
                            ID = p.ID,
                            TransportDetails = p.TransportDetails,
                            ManifestDate = p.ManifestDate,
                            CityName = c.CityName,
                            CategoryName = c1.CategoryName,
                        }).FirstOrDefault();
            return data;
        }

        //get by Transport id
        public Manifest GetByTransportID(Int64 id)
        {
            var getdata = (from p in context.manifestmasters
                           join r in context.rankmasters on p.RankID equals r.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           where p.TransportDetailID == id
                           select new Manifest
                           {
                               ID = p.ID,
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

        public Manifest GetByTransportID(Int64 id, Int64 cityID)
        {
            var getdata = (from p in context.manifestmasters
                           join r in context.rankmasters on p.RankID equals r.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           where p.TransportDetailID == id && p.CityID == cityID
                           select new Manifest
                           {
                               ID = p.ID,
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

        //get Manifest Number by descending order
        public Manifest GetManifestNumberByDes()
        {
            var getdata = (from p in context.manifestmasters
                           orderby p.MenifestNo descending
                           select new Manifest
                           {
                               ID = p.ID,
                               MenifestNo = p.MenifestNo,
                           }).FirstOrDefault();
            return getdata;
        }

        public Manifest GetManifestNumberByDes(DateTime date)
        {
            var getdata = (from p in context.manifestmasters
                           join t in context.transportdetails on p.TransportDetailID equals t.ID
                           join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                           orderby p.MenifestNo descending
                           where t.Date.Value.Day == date.Day && t.Date.Value.Year == date.Year && t.Date.Value.Month == date.Month
                           select new Manifest
                           {
                               ID = p.ID,
                               MenifestNo = p.MenifestNo,
                               TransportTypeName = t1.TransportName,
                           }).FirstOrDefault();
            return getdata;
        }

        public Manifest GetManifestNumberByDesCityWise(DateTime date, int cityid)
        {
            var getdata = (from p in context.manifestmasters
                           join t in context.transportdetails on p.TransportDetailID equals t.ID
                           join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                           orderby p.MenifestNo descending
                           where t.Date.Value.Day == date.Day && t.Date.Value.Year == date.Year && t.Date.Value.Month == date.Month && p.CityID == cityid
                           select new Manifest
                           {
                               ID = p.ID,
                               MenifestNo = p.MenifestNo,
                               TransportTypeName = t1.TransportName,
                           }).FirstOrDefault();
            return getdata;
        }

        public Manifest GetManifestNumberByDes(Int64 cityId)
        {
            var getdata = (from p in context.manifestmasters
                           orderby p.MenifestNo descending
                           where p.CityID == cityId
                           select new Manifest
                           {
                               ID = p.ID,
                               MenifestNo = p.MenifestNo,
                           }).FirstOrDefault();
            return getdata;
        }

        //get by id
        public Manifest GetByID(Int64 id)
        {
            var getdata = (from p in context.manifestmasters
                           join r in context.rankmasters on p.RankID equals r.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           where p.ID == id
                           select new Manifest
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
                               CategoryID = p.CategoryID,
                               TransportDetails = p.TransportDetails,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn,
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public Int64 Insert(Manifest info)
        {

            var data = new manifestmaster
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
            context.manifestmasters.Add(data);
            return data.ID;
        }

        //update
        public Int64 Update(Manifest info)
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

        //Get By Departure Transport from database for reporting
        public List<ADEntery> DepartureTransportTypeReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.manifestmasters
                    join td in context.transportdetails on p.TransportDetailID equals td.ID
                    join t in context.transportmasters on td.TransportTypeID equals t.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join c in context.citymasters on a.CityID equals c.ID
                    join c1 in context.categorymasters on a.CategoryID equals c1.ID
                    join r in context.rankmasters on a.RankID equals r.ID
                    join u in context.unitmasters on a.UnitID equals u.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    join h in context.hqmasters on a.HQID equals h.ID
                    join m in context.movemasters on a.MoveID equals m.ID
                    join at in context.adtypemasters on a.AdTypeID equals at.ID
                    where t.ID == id && p.ManifestDate >= fromdate && p.ManifestDate <= todate && a.IsManifest == true
                    select new ADEntery
                    {
                        ID = a.ID,
                        CityName = c.CityName,
                        ADNO = a.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = a.ICard,
                        ArmyNo = a.ArmyNo,
                        RankName = r.Rank,
                        Name = a.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = a.Authority,
                        Session = a.Session,
                        BP = a.BP,
                        Date = a.Date,
                        IsTempHold = a.IsTempHold,
                        DocumentUrl = a.DocumentUrl,
                        IsPriority = a.IsPriority,
                        IsReserve = a.IsReserve,
                        IsLoad = a.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = at.ID,
                        ADTypeName = at.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureTransportTypeReport(DateTime fromdate, DateTime todate, Int64 id, int cID)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.manifestmasters
                        join td in context.transportdetails on p.TransportDetailID equals td.ID
                        join t in context.transportmasters on td.TransportTypeID equals t.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c in context.citymasters on a.CityID equals c.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        join r in context.rankmasters on a.RankID equals r.ID
                        join u in context.unitmasters on a.UnitID equals u.ID
                        join d in context.divmasters on a.DivID equals d.ID
                        join h in context.hqmasters on a.HQID equals h.ID
                        join m in context.movemasters on a.MoveID equals m.ID
                        join at in context.adtypemasters on a.AdTypeID equals at.ID
                        where p.ManifestDate >= fromdate && p.ManifestDate <= todate && a.IsManifest == true && p.CityID == cID
                        select new ADEntery
                        {
                            ID = a.ID,
                            CityName = c.CityName,
                            ADNO = a.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = a.ICard,
                            ArmyNo = a.ArmyNo,
                            RankName = r.Rank,
                            Name = a.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = a.Authority,
                            Session = a.Session,
                            BP = a.BP,
                            Date = a.Date,
                            IsTempHold = a.IsTempHold,
                            DocumentUrl = a.DocumentUrl,
                            IsPriority = a.IsPriority,
                            IsReserve = a.IsReserve,
                            IsLoad = a.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = at.ID,
                            ADTypeName = at.ADTypeName,
                            TransportName = t.TransportName,
                            ArrDate = a.Date,
                            DepDate = p.ManifestDate,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.manifestmasters
                        join td in context.transportdetails on p.TransportDetailID equals td.ID
                        join t in context.transportmasters on td.TransportTypeID equals t.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c in context.citymasters on a.CityID equals c.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        join r in context.rankmasters on a.RankID equals r.ID
                        join u in context.unitmasters on a.UnitID equals u.ID
                        join d in context.divmasters on a.DivID equals d.ID
                        join h in context.hqmasters on a.HQID equals h.ID
                        join m in context.movemasters on a.MoveID equals m.ID
                        join at in context.adtypemasters on a.AdTypeID equals at.ID
                        where t.ID == id && p.ManifestDate >= fromdate && p.ManifestDate <= todate && a.IsManifest == true && p.CityID == cID
                        select new ADEntery
                        {
                            ID = a.ID,
                            CityName = c.CityName,
                            ADNO = a.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = a.ICard,
                            ArmyNo = a.ArmyNo,
                            RankName = r.Rank,
                            Name = a.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = a.Authority,
                            Session = a.Session,
                            BP = a.BP,
                            Date = a.Date,
                            IsTempHold = a.IsTempHold,
                            DocumentUrl = a.DocumentUrl,
                            IsPriority = a.IsPriority,
                            IsReserve = a.IsReserve,
                            IsLoad = a.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = at.ID,
                            ADTypeName = at.ADTypeName,
                            ArrDate = a.Date,
                            DepDate = p.ManifestDate,
                            FMN = d.DivName
                        }).ToList();
            }
            return list;
        }

        //Get By Departure Transport from database for reporting
        public List<ADEntery> DepartureTransportTypeReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.manifestmasters
                    join td in context.transportdetails on p.TransportDetailID equals td.ID
                    join t in context.transportmasters on td.TransportTypeID equals t.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join c in context.citymasters on a.CityID equals c.ID
                    join c1 in context.categorymasters on a.CategoryID equals c1.ID
                    join r in context.rankmasters on a.RankID equals r.ID
                    join u in context.unitmasters on a.UnitID equals u.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    join h in context.hqmasters on a.HQID equals h.ID
                    join m in context.movemasters on a.MoveID equals m.ID
                    join at in context.adtypemasters on a.AdTypeID equals at.ID
                    where t.ID == id && a.IsManifest == true && p.ManifestDate >= date && p.ManifestDate < nextdate
                    select new ADEntery
                    {
                        ID = a.ID,
                        CityName = c.CityName,
                        ADNO = a.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = a.ICard,
                        ArmyNo = a.ArmyNo,
                        RankName = r.Rank,
                        Name = a.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = a.Authority,
                        Session = a.Session,
                        BP = a.BP,
                        Date = a.Date,
                        IsTempHold = a.IsTempHold,
                        DocumentUrl = a.DocumentUrl,
                        IsPriority = a.IsPriority,
                        IsReserve = a.IsReserve,
                        IsLoad = a.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = at.ID,
                        ADTypeName = at.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureTransportTypeReportID(Int64 id, DateTime date, int cID)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {

                list = (from p in context.manifestmasters
                        join td in context.transportdetails on p.TransportDetailID equals td.ID
                        join t in context.transportmasters on td.TransportTypeID equals t.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c in context.citymasters on a.CityID equals c.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        join r in context.rankmasters on a.RankID equals r.ID
                        join u in context.unitmasters on a.UnitID equals u.ID
                        join d in context.divmasters on a.DivID equals d.ID
                        join h in context.hqmasters on a.HQID equals h.ID
                        join m in context.movemasters on a.MoveID equals m.ID
                        join at in context.adtypemasters on a.AdTypeID equals at.ID
                        where p.CityID == cID && a.IsManifest == true && p.ManifestDate >= date && p.ManifestDate < nextdate
                        select new ADEntery
                        {
                            ID = a.ID,
                            CityName = c.CityName,
                            ADNO = a.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = a.ICard,
                            ArmyNo = a.ArmyNo,
                            RankName = r.Rank,
                            Name = a.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = a.Authority,
                            Session = a.Session,
                            BP = a.BP,
                            Date = a.Date,
                            IsTempHold = a.IsTempHold,
                            DocumentUrl = a.DocumentUrl,
                            IsPriority = a.IsPriority,
                            IsReserve = a.IsReserve,
                            IsLoad = a.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = at.ID,
                            ADTypeName = at.ADTypeName,
                            TransportName = t.TransportName,
                            ArrDate = a.Date,
                            DepDate = p.ManifestDate,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {

                list = (from p in context.manifestmasters
                        join td in context.transportdetails on p.TransportDetailID equals td.ID
                        join t in context.transportmasters on td.TransportTypeID equals t.ID
                        join a in context.admasters on p.ADID equals a.ID
                        join c in context.citymasters on a.CityID equals c.ID
                        join c1 in context.categorymasters on a.CategoryID equals c1.ID
                        join r in context.rankmasters on a.RankID equals r.ID
                        join u in context.unitmasters on a.UnitID equals u.ID
                        join d in context.divmasters on a.DivID equals d.ID
                        join h in context.hqmasters on a.HQID equals h.ID
                        join m in context.movemasters on a.MoveID equals m.ID
                        join at in context.adtypemasters on a.AdTypeID equals at.ID
                        where t.ID == id && a.IsManifest == true && p.ManifestDate >= date && p.ManifestDate < nextdate && p.CityID == cID
                        select new ADEntery
                        {
                            ID = a.ID,
                            CityName = c.CityName,
                            ADNO = a.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = a.ICard,
                            ArmyNo = a.ArmyNo,
                            RankName = r.Rank,
                            Name = a.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = a.Authority,
                            Session = a.Session,
                            BP = a.BP,
                            Date = a.Date,
                            IsTempHold = a.IsTempHold,
                            DocumentUrl = a.DocumentUrl,
                            IsPriority = a.IsPriority,
                            IsReserve = a.IsReserve,
                            IsLoad = a.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = at.ID,
                            ADTypeName = at.ADTypeName,
                            ArrDate = a.Date,
                            DepDate = p.ManifestDate,
                            FMN = d.DivName
                        }).ToList();
            }

            return list;
        }

        //Get List from database
        public List<Manifest> Paging(Int32 take, Int32 skip, Int64 ID)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    where p.TransportDetailID == ID
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get List from database
        public List<Manifest> ManifestListing(Int64 ID)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    where p.TransportDetailID == ID
                    orderby a.ID ascending
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        IsReserve = a.IsReserve
                    }).ToList();
            return list;
        }

        //Get List from database by manifest filter with Cat
        public List<Manifest> PagingManifest(string Manifestno, Int64 CatID)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join m in context.movemasters on a.MoveID equals m.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    where p.MenifestNo == Manifestno && a.CategoryID == CatID
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,
                        CategoryID = x.ID,
                        TransportDetailID = t.ID,
                        CityID = c.ID,
                        TransportDetails = p.TransportDetails,
                        FMN = d.DivName,
                        MoveName = m.MoveName,
                        Date = a.Date,
                        IsPriority = a.IsPriority,
                        BP = a.BP,
                        IsLoad = a.IsLoad
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get List from database by manifest filter without Cat
        public List<Manifest> PagingManifest(string Manifestno)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join m in context.movemasters on a.MoveID equals m.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    where p.MenifestNo == Manifestno
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,
                        CategoryID = x.ID,
                        TransportDetailID = t.ID,
                        CityID = c.ID,
                        FMN = d.DivName,
                        MoveName = m.MoveName,
                        Date = a.Date,
                        IsPriority = a.IsPriority,
                        BP = a.BP,
                        IsReserve = a.IsReserve,
                        IsLoad = a.IsLoad,

                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        public List<Manifest> PagingManifest(string Manifestno, DateTime date, int cityid)
        {
            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join m in context.movemasters on a.MoveID equals m.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    where p.MenifestNo == Manifestno && (p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year) && p.CityID == cityid
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,
                        CategoryID = x.ID,
                        TransportDetailID = t.ID,
                        CityID = c.ID,
                        FMN = d.DivName,
                        MoveName = m.MoveName,
                        Date = a.Date,
                        IsPriority = a.IsPriority,
                        BP = a.BP,
                        IsReserve = a.IsReserve,
                        IsLoad = a.IsLoad,

                    }).OrderBy(x => x.ID).ToList();
            return list;
        }

        //Get List from database by manifest filter
        public List<Manifest> PagingManifestcat1(string Manifestno)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    where p.MenifestNo == Manifestno && a.CategoryID == 3
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,

                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get List from database by manifest filter
        public List<Manifest> PagingManifestcat2(string Manifestno)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    where p.MenifestNo == Manifestno && a.CategoryID == 5
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,

                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get List from database all manifests
        public List<Manifest> PagingAll(Int32 take, Int32 skip)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get List from database all manifests
        public List<Manifest> PagingAll(Int32 take, Int32 skip, string manifestno)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    where p.MenifestNo == manifestno
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,
                        CategoryID = x.ID,
                        TransportDetailID = t.ID,
                        CityID = c.ID,
                        FMN = d.DivName,
                        IsPriority = a.IsPriority,
                        BP = a.BP,
                        IsReserve = a.IsReserve,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        public List<Manifest> PagingAll(Int32 take, Int32 skip, string manifestno, DateTime date)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    where p.MenifestNo == manifestno && (p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year)
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,
                        CategoryID = x.ID,
                        TransportDetailID = t.ID,
                        CityID = c.ID,
                        FMN = d.DivName,
                        IsPriority = a.IsPriority,
                        BP = a.BP,
                        IsReserve = a.IsReserve,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        public List<Manifest> PagingAll(Int32 take, Int32 skip, string manifestno, DateTime date, int cityid)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    where p.MenifestNo == manifestno && (p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year) && p.CityID == cityid
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,
                        CategoryID = x.ID,
                        TransportDetailID = t.ID,
                        CityID = c.ID,
                        FMN = d.DivName,
                        IsPriority = a.IsPriority,
                        BP = a.BP,
                        IsReserve = a.IsReserve,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get List from database all manifests
        public List<Manifest> SearchResultsManifestDetails(string searchtext, string manifestno)
        {

            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join a in context.admasters on p.ADID equals a.ID
                    join x in context.categorymasters on a.CategoryID equals x.ID
                    join d in context.divmasters on a.DivID equals d.ID

                    where ((p.MenifestNo == manifestno) && (p.ArmyNo.Contains(searchtext) || r.Rank.Contains(searchtext) || p.Name.Contains(searchtext) || u.UnitName.Contains(searchtext)))
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
                        CategoryName = x.CategoryName,
                        CategoryID = x.ID,
                        TransportDetailID = t.ID,
                        CityID = c.ID,
                        FMN = d.DivName,
                        IsPriority = a.IsPriority,
                        BP = a.BP,
                        IsReserve = a.IsReserve,
                    }).ToList();
            return list;
        }

        //Get AD Entry No
        public List<ADEntery> ADEntryNo()
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }
        //Get AD Entry No
        public List<Manifest> GetManifest(Int32 take, Int32 skip)
        {

            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    group p by p.MenifestNo into g
                    orderby g.Key descending
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).Skip(skip).Take(take).ToList();
            return list;
        }

        public List<Manifest> GetManifest(Int32 take, Int32 skip, DateTime date)
        {

            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    join a in context.admasters on p.ADID equals a.ID
                    where p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year && a.IsReserve == false
                    group p by p.MenifestNo into g
                    orderby g.Key descending
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).Skip(skip).Take(take).ToList();
            return list;
        }

        public List<Manifest> GetManifestReserve(Int32 take, Int32 skip, DateTime date)
        {

            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    join a in context.admasters on p.ADID equals a.ID
                    where p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year && a.IsReserve == true
                    group p by p.MenifestNo into g
                    orderby g.Key descending
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get AD Entry No
        public List<Manifest> GetManifestSearch(string manifestno)
        {

            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    where p.MenifestNo.Contains(manifestno)
                    group p by p.MenifestNo into g
                    orderby g.Key descending
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //search by date
        public List<Manifest> GetManifestSearch(DateTime date)
        {

            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    where p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year
                    group p by p.MenifestNo into g
                    orderby g.Key descending
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //Get Manifest NO
        public List<Manifest> GetManifest()
        {
            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    group p by p.MenifestNo into g
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //Get Manifest NO
        public List<Manifest> GetManifestDateWise(DateTime date, int cid)
        {
            DateTime nextday = date.AddDays(1);
            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    where p.ManifestDate >= date && p.ManifestDate < nextday && p.CityID == cid
                    group p by p.MenifestNo into g
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //Get Manifest NO
        public List<Manifest> GetManifestDateWiseRange(DateTime fromDate, DateTime toDate, int cid)
        {
            List<Manifest> list = new List<Manifest>();
            list = (from p in context.manifestmasters
                    where p.ManifestDate >= fromDate && p.ManifestDate <= toDate && p.CityID == cid
                    group p by p.MenifestNo into g
                    select new Manifest
                    {
                        MenifestNo = g.Key
                    }).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.manifestmasters
                         select p).Count();

            return count;
        }

        public Int32 TotalItemsManifestDetails()
        {
            var count = (from p in context.manifestmasters
                         join a in context.admasters on p.ADID equals a.ID
                         where a.IsReserve == false
                         select p).Count();

            return count;
        }
        public Int32 TotalItemsReserveManifestDetails()
        {
            var count = (from p in context.manifestmasters
                         join a in context.admasters on p.ADID equals a.ID
                         where a.IsReserve == true
                         select p).Count();

            return count;
        }

        public Int32 TotalItems(string manifestNo)
        {
            var count = (from p in context.manifestmasters
                         where p.MenifestNo == manifestNo
                         select p).Count();

            return count;
        }

        public Int32 TotalItemsManifestDetails(string manifestNo)
        {
            var count = (from p in context.manifestmasters
                         join a in context.admasters on p.ADID equals a.ID
                         where p.MenifestNo == manifestNo && a.IsReserve == false
                         select p).Count();

            return count;
        }

        public Int32 TotalItemsReserveManifestDetails(string manifestNo)
        {
            var count = (from p in context.manifestmasters
                         join a in context.admasters on p.ADID equals a.ID
                         where p.MenifestNo == manifestNo && a.IsReserve == true
                         select p).Count();

            return count;
        }


        public Int32 TotalItems(DateTime date)
        {
            var count = (from p in context.manifestmasters
                         join a in context.admasters on p.ADID equals a.ID
                         where p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year && a.IsReserve == false
                         select p).Count();

            return count;
        }

        public Int32 TotalItemsReserve(DateTime date)
        {
            var count = (from p in context.manifestmasters
                         join a in context.admasters on p.ADID equals a.ID
                         where p.ManifestDate.Value.Day == date.Day && p.ManifestDate.Value.Month == date.Month && p.ManifestDate.Value.Year == date.Year && a.IsReserve == true
                         select p).Count();

            return count;
        }

        //Get total number of records present in DataBase for manifest
        public Int32 TotalItemsTransportID(Int64 id)
        {
            var count = (from p in context.manifestmasters
                         where p.TransportDetailID == id
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.manifestmasters
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.manifestmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Manifest> GetSearchResult(String SearchText)
        {
            List<Manifest> list = new List<Manifest>();

            list = (from p in context.manifestmasters
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    select new Manifest
                    {
                        ID = p.ID,
                        Session = p.Session,
                        ManifestDate = p.ManifestDate,
                        MenifestNo = p.MenifestNo,
                        CityName = c.CityName,
                        TransportTypeName = t1.TransportName,
                        RankID = p.RankID,
                        ArmyNo = p.ArmyNo,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        UnitID = p.UnitID,
                        ICard = p.ICard,
                        HQID = p.HQID,
                        HQName = h.HQName,
                        Rank = r.Rank,
                        UnitName = u.UnitName,
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
