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
    public class ADServices : IADServices, IDisposable
    {
        #region Interface
        //create instance of data access context
        private TCContext context;
        public ADServices(TCContext context)
        {
            this.context = context;
        }

        //get all ads for dasboard summary
        public List<ADEntery> GetAllADLeft()
        {
            List<ADEntery> data = new List<ADEntery>();

            data = (from p in context.admasters
                    join c in context.categorymasters on p.CategoryID equals c.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    where p.IsManifest == false && p.IsOnHold == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CategoryID = p.CategoryID,
                        CategoryName = c.CategoryName,
                        IsTempHold = p.IsTempHold,
                        IsPriority = p.IsPriority,
                        IsLoad = p.IsLoad,
                        IsReserve = p.IsReserve,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        IsOnHold = p.IsOnHold
                    }).ToList();
            return data;
        }

        public List<ADEntery> GetAllADLeftModify()
        {
            List<ADEntery> data = new List<ADEntery>();

            data = (from p in context.admasters
                    join c in context.categorymasters on p.CategoryID equals c.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    where p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CategoryID = p.CategoryID,
                        CategoryName = c.CategoryName,
                        IsTempHold = p.IsTempHold,
                        IsPriority = p.IsPriority,
                        IsLoad = p.IsLoad,
                        IsReserve = p.IsReserve,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        IsOnHold = p.IsOnHold
                    }).ToList();
            return data;
        }

        public List<ADEntery> GetAllADLeft(Int64 cityID)
        {
            List<ADEntery> data = new List<ADEntery>();

            data = (from p in context.admasters
                    join c in context.categorymasters on p.CategoryID equals c.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    where p.IsManifest == false && p.IsOnHold == false && p.CityID == cityID
                    select new ADEntery
                    {
                        ID = p.ID,
                        CategoryID = p.CategoryID,
                        CategoryName = c.CategoryName,
                        IsTempHold = p.IsTempHold,
                        IsPriority = p.IsPriority,
                        IsLoad = p.IsLoad,
                        IsReserve = p.IsReserve,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        IsOnHold = p.IsOnHold

                    }).ToList();
            return data;
        }

        //get ADNO for insert and update
        public ADEntery GetADNO(Int64 catId, Int64 city, string cityname)
        {
            ADEntery result = new ADEntery();
            result = (from p in context.admasters
                      orderby p.ID descending
                      where p.CategoryID == catId && p.CityID == city && p.ADNO.Contains(cityname)
                      select new ADEntery
                      {
                          CategoryID = p.CategoryID,
                          CityID = p.CityID,
                          ADNO = p.ADNO,
                      }).FirstOrDefault();
            return result;
        }

        //get ADNO for insert and update
        public ADEntery GetADNO(string cityname, int catid)
        {
            ADEntery result = new ADEntery();
            result = (from p in context.admasters
                      orderby p.ID descending
                      where p.ADNO.Contains(cityname) && p.CategoryID == catid
                      select new ADEntery
                      {
                          CategoryID = p.CategoryID,
                          CityID = p.CityID,
                          ADNO = p.ADNO,
                      }).FirstOrDefault();
            return result;
        }

        public ADEntery GetADNO(Int64 catId, Int64 city)
        {
            ADEntery result = new ADEntery();
            result = (from p in context.admasters
                      orderby p.ID descending
                      where p.CategoryID == catId && p.CityID == city
                      select new ADEntery
                      {
                          CategoryID = p.CategoryID,
                          CityID = p.CityID,
                          ADNO = p.ADNO,
                      }).FirstOrDefault();
            return result;
        }

        public string GetADNO()
        {
            var result = (from p in context.admasters
                          orderby p.ID descending
                          select p.ADNO).FirstOrDefault();
            return result;
        }

        public List<ADEntery> GetADDetails()
        {
            List<ADEntery> lsResult = new List<ADEntery>();
            lsResult = (from p in context.admasters
                        join c in context.categorymasters on p.CategoryID equals c.ID
                        join c1 in context.citymasters on p.CityID equals c1.ID
                        orderby p.ID descending
                        select new ADEntery
                        {
                            ID = p.ID,
                            ADNO = p.ADNO,
                            CategoryName = c.CategoryName,
                            CityName = c1.CityName,
                        }).ToList();
            return lsResult;
        }

        //get list PresentDay
        public List<ADEntery> GetByDate(DateTime today)
        {
            DateTime nextdate = today.AddDays(1);
            List<ADEntery> getdate = new List<ADEntery>();
            getdate = (from p in context.admasters
                       join c in context.citymasters on p.CityID equals c.ID
                       join d in context.divmasters on p.DivID equals d.ID
                       join r in context.rankmasters on p.RankID equals r.ID
                       join u in context.unitmasters on p.UnitID equals u.ID
                       join m in context.movemasters on p.MoveID equals m.ID
                       join ca in context.categorymasters on p.CategoryID equals ca.ID
                       where p.Date >= today && p.Date < nextdate
                       select new ADEntery
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
                           Authority = p.Authority,
                           Session = p.Session,
                           MoveID = p.MoveID,
                           PriorityID = p.PriorityID,
                           AdTypeID = p.AdTypeID,
                           BP = p.BP,
                           Date = p.Date,
                           IsOnHold = p.IsOnHold,
                           IsTempHold = p.IsTempHold,
                           IsPriority = p.IsPriority,
                           IsManifest = p.IsManifest,
                           IsReserve = p.IsReserve,
                           CreatedOn = p.CreatedOn,
                           UpdatedOn = p.UpdatedOn,
                           ADNO = p.ADNO,
                           FMN = d.DivName,
                           LeaveToDate = p.LeaveToDate,
                           LeaveFromDate = p.LeaveFromDate,
                           LeaveNoOfDays = p.LeaveNoOfDays,
                           MedicalStatusID = c.MedicalStatusID,
                           StateName = c.StateName,
                           NoOfAbsentDays = p.NoOfAbsentDays,
                           CategoryName = ca.CategoryName,
                           IsLoad = p.IsLoad,
                           UnitName = u.UnitName,
                           RankName = r.Rank,
                           MoveName = m.MoveName,
                       }).ToList();
            return getdate;
        }

        public List<ADEntery> GetByDate(DateTime today, int cid)
        {
            DateTime nextdate = today.AddDays(1);
            List<ADEntery> getdate = new List<ADEntery>();
            getdate = (from p in context.admasters
                       join c in context.citymasters on p.CityID equals c.ID
                       join r in context.rankmasters on p.RankID equals r.ID
                       join d in context.divmasters on p.DivID equals d.ID
                       join u in context.unitmasters on p.UnitID equals u.ID
                       join m in context.movemasters on p.MoveID equals m.ID
                       join ca in context.categorymasters on p.CategoryID equals ca.ID
                       where p.Date >= today && p.Date < nextdate && p.CityID == cid
                       select new ADEntery
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
                           Authority = p.Authority,
                           Session = p.Session,
                           MoveID = p.MoveID,
                           PriorityID = p.PriorityID,
                           AdTypeID = p.AdTypeID,
                           BP = p.BP,
                           Date = p.Date,
                           IsOnHold = p.IsOnHold,
                           IsTempHold = p.IsTempHold,
                           IsPriority = p.IsPriority,
                           IsManifest = p.IsManifest,
                           IsReserve = p.IsReserve,
                           CreatedOn = p.CreatedOn,
                           UpdatedOn = p.UpdatedOn,
                           ADNO = p.ADNO,
                           FMN = d.DivName,
                           LeaveToDate = p.LeaveToDate,
                           LeaveFromDate = p.LeaveFromDate,
                           LeaveNoOfDays = p.LeaveNoOfDays,
                           MedicalStatusID = c.MedicalStatusID,
                           StateName = c.StateName,
                           NoOfAbsentDays = p.NoOfAbsentDays,
                           CategoryName = ca.CategoryName,
                           IsLoad = p.IsLoad,
                           UnitName = u.UnitName,
                           RankName = r.Rank,
                           MoveName = m.MoveName,
                       }).ToList();
            return getdate;
        }

        //get list PresentDay
        public List<ADEntery> GetByDateForHoldingReport(DateTime date)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> getdate = new List<ADEntery>();
            getdate = (from p in context.admasters
                       join c in context.citymasters on p.CityID equals c.ID
                       join c1 in context.categorymasters on p.CategoryID equals c1.ID
                       join r in context.rankmasters on p.RankID equals r.ID
                       join u in context.unitmasters on p.UnitID equals u.ID
                       join d in context.divmasters on p.DivID equals d.ID
                       join h in context.hqmasters on p.HQID equals h.ID
                       join m in context.movemasters on p.MoveID equals m.ID
                       where p.Date >= date && p.Date < nextdate && p.IsTempHold == true && p.IsManifest == false
                       select new ADEntery
                       {
                           ID = p.ID,
                           CityName = c.CityName,
                           ADNO = p.ADNO,
                           CategoryName = c1.CategoryName,
                           ICard = p.ICard,
                           ArmyNo = p.ArmyNo,
                           RankName = r.Rank,
                           Name = p.Name,
                           UnitName = u.UnitName,
                           DivName = d.DivName,
                           HQName = h.HQName,
                           Authority = p.Authority,
                           Session = p.Session,
                           BP = p.BP,
                           Date = p.Date,
                           IsTempHold = p.IsTempHold,
                           DocumentUrl = p.DocumentUrl,
                           IsPriority = p.IsPriority,
                           IsReserve = p.IsReserve,
                           IsLoad = p.IsLoad,
                           MoveID = m.ID,
                           MoveName = m.MoveName,
                       }).ToList();
            return getdate;
        }

        public List<ADEntery> GetByDateForHoldingReport(DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> getdate = new List<ADEntery>();
            getdate = (from p in context.admasters
                       join c in context.citymasters on p.CityID equals c.ID
                       join c1 in context.categorymasters on p.CategoryID equals c1.ID
                       join r in context.rankmasters on p.RankID equals r.ID
                       join u in context.unitmasters on p.UnitID equals u.ID
                       join d in context.divmasters on p.DivID equals d.ID
                       join h in context.hqmasters on p.HQID equals h.ID
                       join m in context.movemasters on p.MoveID equals m.ID
                       where p.UpdatedOn >= date && p.UpdatedOn < nextdate && p.IsTempHold == true && p.IsManifest == false && p.CityID == cid
                       select new ADEntery
                       {
                           ID = p.ID,
                           CityName = c.CityName,
                           ADNO = p.ADNO,
                           CategoryName = c1.CategoryName,
                           ICard = p.ICard,
                           ArmyNo = p.ArmyNo,
                           RankName = r.Rank,
                           Name = p.Name,
                           UnitName = u.UnitName,
                           DivName = d.DivName,
                           HQName = h.HQName,
                           Authority = p.Authority,
                           Session = p.Session,
                           BP = p.BP,
                           Date = p.Date,
                           IsTempHold = p.IsTempHold,
                           DocumentUrl = p.DocumentUrl,
                           IsPriority = p.IsPriority,
                           IsReserve = p.IsReserve,
                           IsLoad = p.IsLoad,
                           MoveID = m.ID,
                           MoveName = m.MoveName,
                           FMN = d.DivName,
                           OnTemHoldRemark = p.OnTempHoldRemark,
                           UpdatedOn = p.UpdatedOn,
                       }).ToList();
            return getdate;
        }

        public List<ADEntery> GetByDateForOnHoldingReport(DateTime date, int cid = 0)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> getdate = new List<ADEntery>();

            if (cid == 0)
            {
                getdate = (from p in context.admasters
                           join c in context.citymasters on p.CityID equals c.ID
                           join c1 in context.categorymasters on p.CategoryID equals c1.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join m in context.movemasters on p.MoveID equals m.ID
                           where p.UpdatedOn >= date && p.UpdatedOn < nextdate && p.IsOnHold == true && p.IsManifest == false
                           select new ADEntery
                           {
                               ID = p.ID,
                               CityName = c.CityName,
                               ADNO = p.ADNO,
                               CategoryName = c1.CategoryName,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankName = r.Rank,
                               Name = p.Name,
                               UnitName = u.UnitName,
                               DivName = d.DivName,
                               HQName = h.HQName,
                               Authority = p.Authority,
                               Session = p.Session,
                               BP = p.BP,
                               Date = p.Date,
                               IsTempHold = p.IsTempHold,
                               DocumentUrl = p.DocumentUrl,
                               IsPriority = p.IsPriority,
                               IsReserve = p.IsReserve,
                               IsLoad = p.IsLoad,
                               MoveID = m.ID,
                               MoveName = m.MoveName,
                               OnHoldRemark = p.OnHoldRemark,
                               UpdatedOn = p.UpdatedOn,
                               FMN = d.DivName
                           }).ToList();
            }
            else
            {
                getdate = (from p in context.admasters
                           join c in context.citymasters on p.CityID equals c.ID
                           join c1 in context.categorymasters on p.CategoryID equals c1.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join m in context.movemasters on p.MoveID equals m.ID
                           where p.UpdatedOn >= date && p.UpdatedOn < nextdate && p.IsOnHold == true && p.IsManifest == false && p.CityID == cid
                           select new ADEntery
                           {
                               ID = p.ID,
                               CityName = c.CityName,
                               ADNO = p.ADNO,
                               CategoryName = c1.CategoryName,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankName = r.Rank,
                               Name = p.Name,
                               UnitName = u.UnitName,
                               DivName = d.DivName,
                               HQName = h.HQName,
                               Authority = p.Authority,
                               Session = p.Session,
                               BP = p.BP,
                               Date = p.Date,
                               IsTempHold = p.IsTempHold,
                               DocumentUrl = p.DocumentUrl,
                               IsPriority = p.IsPriority,
                               IsReserve = p.IsReserve,
                               IsLoad = p.IsLoad,
                               MoveID = m.ID,
                               MoveName = m.MoveName,
                               OnHoldRemark = p.OnHoldRemark,
                               UpdatedOn = p.UpdatedOn,
                               FMN = d.DivName
                           }).ToList();
            }

            return getdate;
        }

        //get list PresentDay
        public List<ADEntery> GetByDateForHoldingReportDateRange(DateTime fromdate, DateTime todate, int cid = 0)
        {
            List<ADEntery> getdate = new List<ADEntery>();

            if (cid == 0)
            {
                getdate = (from p in context.admasters
                           join c in context.citymasters on p.CityID equals c.ID
                           join c1 in context.categorymasters on p.CategoryID equals c1.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join m in context.movemasters on p.MoveID equals m.ID
                           where p.Date >= fromdate && p.Date <= todate && p.IsTempHold == true && p.IsManifest == false
                           select new ADEntery
                           {
                               ID = p.ID,
                               CityName = c.CityName,
                               ADNO = p.ADNO,
                               CategoryName = c1.CategoryName,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankName = r.Rank,
                               Name = p.Name,
                               UnitName = u.UnitName,
                               DivName = d.DivName,
                               HQName = h.HQName,
                               Authority = p.Authority,
                               Session = p.Session,
                               BP = p.BP,
                               Date = p.Date,
                               IsTempHold = p.IsTempHold,
                               DocumentUrl = p.DocumentUrl,
                               IsPriority = p.IsPriority,
                               IsReserve = p.IsReserve,
                               IsLoad = p.IsLoad,
                               MoveID = m.ID,
                               MoveName = m.MoveName,
                               OnTemHoldRemark = p.OnTempHoldRemark,
                               FMN = d.DivName,
                               UpdatedOn = p.UpdatedOn
                           }).ToList();
            }
            else
            {
                getdate = (from p in context.admasters
                           join c in context.citymasters on p.CityID equals c.ID
                           join c1 in context.categorymasters on p.CategoryID equals c1.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join m in context.movemasters on p.MoveID equals m.ID
                           where p.Date >= fromdate && p.Date <= todate && p.IsTempHold == true && p.IsManifest == false && p.CityID == cid
                           select new ADEntery
                           {
                               ID = p.ID,
                               CityName = c.CityName,
                               ADNO = p.ADNO,
                               CategoryName = c1.CategoryName,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankName = r.Rank,
                               Name = p.Name,
                               UnitName = u.UnitName,
                               DivName = d.DivName,
                               HQName = h.HQName,
                               Authority = p.Authority,
                               Session = p.Session,
                               BP = p.BP,
                               Date = p.Date,
                               IsTempHold = p.IsTempHold,
                               DocumentUrl = p.DocumentUrl,
                               IsPriority = p.IsPriority,
                               IsReserve = p.IsReserve,
                               IsLoad = p.IsLoad,
                               MoveID = m.ID,
                               MoveName = m.MoveName,
                               OnTemHoldRemark = p.OnTempHoldRemark,
                               FMN = d.DivName,
                               UpdatedOn = p.UpdatedOn

                           }).ToList();
            }
            return getdate;
        }

        public List<ADEntery> GetByDateForOnHoldingReportDateRange(DateTime fromdate, DateTime todate, int cid = 0)
        {
            List<ADEntery> getdate = new List<ADEntery>();

            if (cid == 0)
            {
                getdate = (from p in context.admasters
                           join c in context.citymasters on p.CityID equals c.ID
                           join c1 in context.categorymasters on p.CategoryID equals c1.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join m in context.movemasters on p.MoveID equals m.ID
                           where p.Date >= fromdate && p.Date <= todate && p.IsOnHold == true && p.IsManifest == false
                           select new ADEntery
                           {
                               ID = p.ID,
                               CityName = c.CityName,
                               ADNO = p.ADNO,
                               CategoryName = c1.CategoryName,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankName = r.Rank,
                               Name = p.Name,
                               UnitName = u.UnitName,
                               DivName = d.DivName,
                               HQName = h.HQName,
                               Authority = p.Authority,
                               Session = p.Session,
                               BP = p.BP,
                               Date = p.Date,
                               IsTempHold = p.IsTempHold,
                               DocumentUrl = p.DocumentUrl,
                               IsPriority = p.IsPriority,
                               IsReserve = p.IsReserve,
                               IsLoad = p.IsLoad,
                               MoveID = m.ID,
                               MoveName = m.MoveName,
                               OnTemHoldRemark = p.OnTempHoldRemark,
                               FMN = d.DivName,
                               OnHoldRemark = p.OnHoldRemark,
                               UpdatedOn = p.UpdatedOn
                           }).ToList();
            }
            else
            {
                getdate = (from p in context.admasters
                           join c in context.citymasters on p.CityID equals c.ID
                           join c1 in context.categorymasters on p.CategoryID equals c1.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           join h in context.hqmasters on p.HQID equals h.ID
                           join m in context.movemasters on p.MoveID equals m.ID
                           where p.Date >= fromdate && p.Date <= todate && p.IsOnHold == true && p.IsManifest == false && p.CityID == cid
                           select new ADEntery
                           {
                               ID = p.ID,
                               CityName = c.CityName,
                               ADNO = p.ADNO,
                               CategoryName = c1.CategoryName,
                               ICard = p.ICard,
                               ArmyNo = p.ArmyNo,
                               RankName = r.Rank,
                               Name = p.Name,
                               UnitName = u.UnitName,
                               DivName = d.DivName,
                               HQName = h.HQName,
                               Authority = p.Authority,
                               Session = p.Session,
                               BP = p.BP,
                               Date = p.Date,
                               IsTempHold = p.IsTempHold,
                               DocumentUrl = p.DocumentUrl,
                               IsPriority = p.IsPriority,
                               IsReserve = p.IsReserve,
                               IsLoad = p.IsLoad,
                               MoveID = m.ID,
                               MoveName = m.MoveName,
                               OnTemHoldRemark = p.OnTempHoldRemark,
                               FMN = d.DivName,
                               OnHoldRemark = p.OnHoldRemark,
                               UpdatedOn = p.UpdatedOn
                           }).ToList();
            }
            return getdate;
        }


        //get list by date
        public List<ADEntery> GetByDate(DateTime datefrom, DateTime dateto)
        {
            List<ADEntery> getdate = new List<ADEntery>();
            getdate = (from p in context.admasters
                       join c in context.citymasters on p.CityID equals c.ID
                       join d in context.divmasters on p.DivID equals d.ID
                       join ca in context.categorymasters on p.CategoryID equals ca.ID
                       join m in context.movemasters on p.MoveID equals m.ID
                       join r in context.rankmasters on p.RankID equals r.ID
                       join u in context.unitmasters on p.UnitID equals u.ID
                       where p.Date >= datefrom && p.Date <= dateto
                       select new ADEntery
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
                           Authority = p.Authority,
                           Session = p.Session,
                           MoveID = p.MoveID,
                           PriorityID = p.PriorityID,
                           AdTypeID = p.AdTypeID,
                           BP = p.BP,
                           Date = p.Date,
                           IsOnHold = p.IsOnHold,
                           IsTempHold = p.IsTempHold,
                           IsPriority = p.IsPriority,
                           IsManifest = p.IsManifest,
                           IsReserve = p.IsReserve,
                           CreatedOn = p.CreatedOn,
                           UpdatedOn = p.UpdatedOn,
                           ADNO = p.ADNO,
                           FMN = d.DivName,
                           LeaveToDate = p.LeaveToDate,
                           LeaveFromDate = p.LeaveFromDate,
                           LeaveNoOfDays = p.LeaveNoOfDays,
                           MedicalStatusID = c.MedicalStatusID,
                           StateName = c.StateName,
                           NoOfAbsentDays = p.NoOfAbsentDays,
                           CategoryName = ca.CategoryName,
                           UnitName = u.UnitName,
                           MoveName = m.MoveName,
                           RankName = r.Rank,
                       }).ToList();
            return getdate;
        }

        public List<ADEntery> GetByDate(DateTime datefrom, DateTime dateto, int cid)
        {
            List<ADEntery> getdate = new List<ADEntery>();
            getdate = (from p in context.admasters
                       join c in context.citymasters on p.CityID equals c.ID
                       join d in context.divmasters on p.DivID equals d.ID
                       join ca in context.categorymasters on p.CategoryID equals ca.ID
                       join m in context.movemasters on p.MoveID equals m.ID
                       join r in context.rankmasters on p.RankID equals r.ID
                       join u in context.unitmasters on p.UnitID equals u.ID
                       where p.Date >= datefrom && p.Date <= dateto && p.CityID == cid
                       select new ADEntery
                       {
                           ID = p.ID,
                           CityID = p.CityID,
                           CategoryID = p.CategoryID,
                           ICard = p.ICard,
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
                           AdTypeID = p.AdTypeID,
                           BP = p.BP,
                           Date = p.Date,
                           IsOnHold = p.IsOnHold,
                           IsTempHold = p.IsTempHold,
                           IsPriority = p.IsPriority,
                           IsManifest = p.IsManifest,
                           IsReserve = p.IsReserve,
                           CreatedOn = p.CreatedOn,
                           UpdatedOn = p.UpdatedOn,
                           ADNO = p.ADNO,
                           FMN = d.DivName,
                           LeaveToDate = p.LeaveToDate,
                           LeaveFromDate = p.LeaveFromDate,
                           LeaveNoOfDays = p.LeaveNoOfDays,
                           MedicalStatusID = c.MedicalStatusID,
                           StateName = c.StateName,
                           NoOfAbsentDays = p.NoOfAbsentDays,
                           CategoryName = ca.CategoryName,
                           UnitName = u.UnitName,
                           MoveName = m.MoveName,
                           RankName = r.Rank,
                       }).ToList();
            return getdate;
        }

        //get by id
        public ADEntery GetByID(Int64 id)
        {
            var getdata = (from p in context.admasters
                           join c in context.citymasters on p.CityID equals c.ID
                           join m in context.movemasters on p.MoveID equals m.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           where p.ID == id
                           select new ADEntery
                           {
                               ID = p.ID,
                               CityID = p.CityID,
                               CategoryID = p.CategoryID,
                               ICard = p.ICard,
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
                               AdTypeID = p.AdTypeID,
                               BP = p.BP,
                               Date = p.Date,
                               IsOnHold = p.IsOnHold,
                               IsTempHold = p.IsTempHold,
                               IsPriority = p.IsPriority,
                               IsManifest = p.IsManifest,
                               IsReserve = p.IsReserve,
                               CreatedOn = p.CreatedOn,
                               UpdatedOn = p.UpdatedOn,
                               ADNO = p.ADNO,
                               FMN = d.DivName,
                               LeaveToDate = p.LeaveToDate,
                               LeaveFromDate = p.LeaveFromDate,
                               LeaveNoOfDays = p.LeaveNoOfDays,
                               MedicalStatusID = c.MedicalStatusID,
                               StateName = c.StateName,
                               NoOfAbsentDays = p.NoOfAbsentDays,
                               OnTemHoldRemark = p.OnTempHoldRemark,
                               OnHoldRemark = p.OnHoldRemark,
                               IsLoad = p.IsLoad,
                               UnitName = u.UnitName,
                               MoveName = m.MoveName,
                               RankName = r.Rank,
                               CheckOutDate = p.CheckOutDate,
                               IsFly = p.IsFly,
                               IsLRC = p.IsLRC,
                               BookingId = p.BookingId
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public Int64 Insert(ADEntery info)
        {
            try
            {
                var data = new admaster
                {
                    CityID = info.CityID,
                    ADNO = info.ADNO,
                    DocumentUrl = info.DocumentUrl,
                    CategoryID = info.CategoryID,
                    ICard = info.ICard,
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
                    AdTypeID = info.AdTypeID,
                    Date = info.Date,
                    BP = info.BP,
                    IsTempHold = info.IsTempHold,
                    IsOnHold = info.IsOnHold,
                    IsPriority = info.IsPriority,
                    IsReserve = info.IsReserve,
                    IsManifest = info.IsManifest,
                    FMN = info.FMN,
                    LeaveNoOfDays = info.LeaveNoOfDays,
                    LeaveFromDate = info.LeaveFromDate,
                    LeaveToDate = info.LeaveToDate,
                    NoOfAbsentDays = info.NoOfAbsentDays,
                    IsLoad = info.IsLoad,
                    //IsFly = info.IsFly,
                    OnHoldRemark = info.OnHoldRemark,
                    OnTempHoldRemark = info.OnTemHoldRemark,
                    IsFly = info.IsFly,
                    IsLRC = info.IsLRC,
                    CheckOutDate = info.CheckOutDate,
                    CreatedOn = info.CreatedOn
                };
                context.admasters.Add(data);
                Save();
                return data.ID;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
            }
            return 0;
        }

        //update
        public Int64 Update(ADEntery info)
        {
            var data = (from p in context.admasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.CategoryID = info.CategoryID;
            data.ADNO = info.ADNO;
            data.CityID = info.CityID;
            data.DocumentUrl = info.DocumentUrl;
            data.Date = info.Date;
            data.Session = info.Session;
            data.ICard = info.ICard;
            data.ArmyNo = info.ArmyNo;
            data.RankID = info.RankID;
            data.Name = info.Name;
            data.UnitID = info.UnitID;
            data.DivID = info.DivID;
            data.HQID = info.HQID;
            data.Authority = info.Authority;
            data.MoveID = info.MoveID;
            data.PriorityID = info.PriorityID;
            data.AdTypeID = info.AdTypeID;
            data.BP = info.BP;
            data.IsOnHold = info.IsOnHold;
            data.IsTempHold = info.IsTempHold;
            data.IsPriority = info.IsPriority;
            data.IsManifest = info.IsManifest;
            data.UpdatedOn = info.UpdatedOn;
            data.IsOnHold = info.IsOnHold;
            data.LeaveFromDate = info.LeaveFromDate;
            data.LeaveToDate = info.LeaveToDate;
            data.LeaveNoOfDays = info.LeaveNoOfDays;
            data.FMN = info.FMN;
            data.IsReserve = info.IsReserve;
            data.NoOfAbsentDays = info.NoOfAbsentDays;
            data.OnTempHoldRemark = info.OnTemHoldRemark;
            data.OnHoldRemark = info.OnHoldRemark;
            data.IsLoad = info.IsLoad;
            data.OnHoldRemark = info.OnHoldRemark;
            data.OnTempHoldRemark = info.OnTemHoldRemark;
            data.IsLRC = info.IsLRC;
            data.IsFly = info.IsFly;
            data.CheckOutDate = info.CheckOutDate;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }


        //Get Pagging By Reserve from database
        public List<ADEntery> PagingReserve(Int32 take, Int32 skip)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where p.IsReserve == true && p.IsOnHold == false && p.IsTempHold == false && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get Pagging By On Hold from database
        public List<ADEntery> PagingOnHoldFinal(Int32 take, Int32 skip)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where p.IsOnHold == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        OnHoldRemark = p.OnHoldRemark
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get Pagging By Load from database
        public List<ADEntery> PagingLoad(Int32 take, Int32 skip)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where p.IsLoad == true && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get Pagging By Priority from database
        public List<ADEntery> PagingPriority(Int32 take, Int32 skip)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where p.IsPriority == true && p.IsOnHold == false && p.IsTempHold == false && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get Pagging By Onhold from database
        public List<ADEntery> PagingOnHold(Int32 take, Int32 skip)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where p.IsOnHold == false && p.IsTempHold == true && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        OnTemHoldRemark = p.OnTempHoldRemark
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get Pagging By Onhold from database for reporting
        public List<ADEntery> PagingOnHold()
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    where p.IsOnHold == false && p.IsTempHold == true && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        OnTemHoldRemark = p.OnTempHoldRemark,
                        OnHoldRemark = p.OnHoldRemark,
                        FMN = d.DivName
                    }).ToList();
            return list;
        }

        public List<ADEntery> PagingOnHolding()
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    where p.IsOnHold == true && p.IsTempHold == false && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        OnTemHoldRemark = p.OnTempHoldRemark
                    }).ToList();
            return list;
        }

        //Get Pagging By Move Type from database for reporting
        public List<ADEntery> MoveTypeWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.MoveID == id && p.Date >= fromdate && p.Date <= todate && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> MoveTypeWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= fromdate && p.Date <= todate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.MoveID == id && p.Date >= fromdate && p.Date <= todate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }

            return list;
        }

        //Get Pagging By Move Type ID from database for reporting
        public List<ADEntery> MoveTypeWiseReportByID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.MoveID == id && p.IsManifest == false && p.Date >= date && p.Date < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName
                    }).ToList();
            return list;
        }

        public List<ADEntery> MoveTypeWiseReportByID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.MoveID == id && p.Date >= date && p.Date < nextdate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            return list;
        }

        //Get Pagging By Departure Move Type from database for reporting
        public List<ADEntery> DepartureMoveTypeWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.MoveID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureMoveTypeWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cID)
        {

            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cID && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            ArrDate = p.Date,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.MoveID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true && p.CityID == cID
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            ArrDate = p.Date,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }

            return list;
        }

        //Get Pagging By Departure Move Type ID from database for reporting
        public List<ADEntery> DepartureMoveTypeWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.MoveID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName,
                        ArrDate = p.Date,
                        DepDate = mf.ManifestDate
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureMoveTypeWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            ArrDate = p.Date,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.MoveID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            ArrDate = p.Date,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }
        }

        //Get Pagging By City from database for reporting
        public List<ADEntery> CityWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.Date >= fromdate && p.Date <= todate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == id && p.Date >= fromdate && p.Date <= todate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            return list;
        }

        //Get Pagging By City from database for reporting
        public List<ADEntery> CityWiseReportdeparture(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.CityID == id && p.Date >= fromdate && p.Date <= todate && p.IsManifest == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        //Get Pagging By City ID from database for reporting
        //public List<ADEntery> CityWiseReportID(Int64 id, DateTime date)
        //{
        //    DateTime nextdate = date.AddDays(1);
        //    List<ADEntery> list = new List<ADEntery>();

        //    list = (from p in context.admasters
        //            join c in context.citymasters on p.CityID equals c.ID
        //            join c1 in context.categorymasters on p.CategoryID equals c1.ID
        //            join r in context.rankmasters on p.RankID equals r.ID
        //            join u in context.unitmasters on p.UnitID equals u.ID
        //            join d in context.divmasters on p.DivID equals d.ID
        //            join h in context.hqmasters on p.HQID equals h.ID
        //            join m in context.movemasters on p.MoveID equals m.ID
        //            join a in context.adtypemasters on p.AdTypeID equals a.ID
        //            where p.CityID == id && p.IsManifest == false && p.Date >= date && p.Date < nextdate
        //            select new ADEntery
        //            {
        //                ID = p.ID,
        //                CityName = c.CityName,
        //                ADNO = p.ADNO,
        //                CategoryName = c1.CategoryName,
        //                ICard = p.ICard,
        //                ArmyNo = p.ArmyNo,
        //                RankName = r.Rank,
        //                Name = p.Name,
        //                UnitName = u.UnitName,
        //                DivName = d.DivName,
        //                HQName = h.HQName,
        //                Authority = p.Authority,
        //                Session = p.Session,
        //                BP = p.BP,
        //                Date = p.Date,
        //                IsTempHold = p.IsTempHold,
        //                DocumentUrl = p.DocumentUrl,
        //                IsPriority = p.IsPriority,
        //                IsReserve = p.IsReserve,
        //                IsLoad = p.IsLoad,
        //                MoveID = m.ID,
        //                MoveName = m.MoveName,
        //                AdTypeID = a.ID,
        //                ADTypeName = a.ADTypeName,
        //                DivID = d.ID
        //            }).ToList();
        //    return list;
        //}

        public List<ADEntery> CityWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            DivID = d.ID,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == id && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            DivID = d.ID,
                            FMN = d.DivName
                        }).ToList();
            }

            return list;
        }

        public List<ADEntery> CityWiseReportIDALL(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            DivID = d.ID,
                            IsManifest = p.IsManifest
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == id && p.IsManifest == false && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            DivID = d.ID
                        }).ToList();
            }

            return list;
        }

        public List<ADEntery> CityWiseReportIDDeparture(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.CityID == id && p.IsManifest == true && p.Date >= date && p.Date < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        DivID = d.ID
                    }).ToList();
            return list;
        }

        //Get Green/Red Status from database for reporting
        public List<ADEntery> StatusWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();


            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= fromdate && p.Date <= todate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.AdTypeID == id && p.Date >= fromdate && p.Date <= todate && p.IsManifest == false && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                        }).ToList();
            }

            return list;
        }

        //Get Green/Red Status By from database for reporting
        public List<ADEntery> StatusWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.AdTypeID == id && p.Date >= date && p.Date < nextdate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            return list;
        }

        //Get By Unit from database for reporting
        public List<ADEntery> UnitWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= fromdate && p.Date <= todate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.UnitID == id && p.Date >= fromdate && p.Date <= todate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            return list;
        }

        //Get By UnitID from database for reporting
        public List<ADEntery> UnitWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.UnitID == id && p.IsManifest == false && p.Date >= date && p.Date < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> UnitWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.UnitID == id && p.Date >= date && p.Date < nextdate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            return list;
        }

        //Get By Departure Unit from database for reporting
        public List<ADEntery> DepartureUnitWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.UnitID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureUnitWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.UnitID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }
        }

        //Get By Departure UnitID from database for reporting
        public List<ADEntery> DepartureUnitWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.UnitID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate <= nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName,
                        DepDate = mf.ManifestDate
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureUnitWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate <= nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate <= nextdate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }
        }

        //Get By Departure City from database for reporting
        public List<ADEntery> DepartureCityWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                        }).ToList();
                return list;
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                        }).ToList();
                return list;
            }
        }

        //Get By Departure City ID from database for reporting
        public List<ADEntery> DepartureCityWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            DepDate = mf.ManifestDate,
                            FMN = d.DivName,
                            ArrDate = p.Date
                        }).ToList();
                return list;
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            DepDate = mf.ManifestDate,
                            FMN = d.DivName,
                            ArrDate = p.Date
                        }).ToList();
                return list;
            }
        }

        //Get By Division from database for reporting
        public List<ADEntery> DivisionWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.DivID == id && p.Date >= fromdate && p.Date <= todate && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> DivisionWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= fromdate && p.Date <= todate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.DivID == id && p.Date >= fromdate && p.Date <= todate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            return list;
        }

        //Get By DivisionID from database for reporting
        public List<ADEntery> DivisionWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.DivID == id && p.IsManifest == false && p.Date >= date && p.Date < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName
                    }).ToList();
            return list;
        }

        public List<ADEntery> DivisionWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.DivID == id && p.Date >= date && p.Date < nextdate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            return list;
        }

        //Get By HQ from database for reporting
        public List<ADEntery> HQWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.HQID == id && p.Date >= fromdate && p.Date <= todate && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> HQWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= fromdate && p.Date <= todate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.HQID == id && p.Date >= fromdate && p.Date <= todate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
            }

            return list;
        }

        //Get By MedicalStatus from database for reporting
        public List<ADEntery> MedicalStatusWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {
            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join me in context.medicalstatusmasters on c.MedicalStatusID equals me.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= fromdate && p.Date <= todate && p.IsManifest == false
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            MedicalStatusName = me.MedicalStatusName

                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join me in context.medicalstatusmasters on c.MedicalStatusID equals me.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where c.MedicalStatusID == id && p.Date >= fromdate && p.Date <= todate && p.IsManifest == false && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            MedicalStatusName = me.MedicalStatusName
                        }).ToList();
            }
            return list;
        }

        //Get By MedicalStatus from database for reporting
        public List<ADEntery> MedicalStatusWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join ms in context.medicalstatusmasters on c.MedicalStatusID equals ms.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where c.ID == cid && p.IsManifest == false && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            MedicalStatusName = ms.MedicalStatusName
                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join ms in context.medicalstatusmasters on c.MedicalStatusID equals ms.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where c.MedicalStatusID == id && p.IsManifest == false && p.Date >= date && p.Date < nextdate && c.ID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            MedicalStatusName = ms.MedicalStatusName
                        }).ToList();
            }
            return list;
        }

        //Get By HQID from database for reporting
        public List<ADEntery> HQWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.HQID == id && p.IsManifest == false && p.Date >= date && p.Date < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName
                    }).ToList();
            return list;
        }

        public List<ADEntery> HQWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);
            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.Date >= date && p.Date < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }
            else
            {
                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.HQID == id && p.Date >= date && p.Date < nextdate && p.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName

                        }).ToList();
            }

            return list;
        }

        //Get By DepartureHQ from database for reporting
        public List<ADEntery> DepartureWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.HQID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }
        public List<ADEntery> DepartureWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.HQID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true && mf.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }

            return list;
        }

        //Get By DepartureHQ from database for reporting
        public List<ADEntery> DepartureWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.HQID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName,
                        DepDate = mf.ManifestDate
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.HQID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate && mf.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }

            return list;
        }

        //Get By Departure Division from database for reporting
        public List<ADEntery> DepartureDivisionWiseReport(DateTime fromdate, DateTime todate, Int64 id)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.DivID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureDivisionWiseReport(DateTime fromdate, DateTime todate, Int64 id, int cid)
        {

            List<ADEntery> list = new List<ADEntery>();
            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.DivID == id && mf.ManifestDate >= fromdate && mf.ManifestDate <= todate && p.IsManifest == true && mf.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
            }
            return list;
        }

        //Get By Departure Division from database for reporting
        public List<ADEntery> DepartureDivisionWiseReportID(Int64 id, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.DivID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName,
                        DepDate = mf.ManifestDate
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureDivisionWiseReportID(Int64 id, DateTime date, int cid)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            if (id == 0)
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cid && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }
            else
            {
                list = (from mf in context.manifestmasters
                        join p in context.admasters on mf.ADID equals p.ID
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.DivID == id && p.IsManifest == true && mf.ManifestDate >= date && mf.ManifestDate < nextdate && mf.CityID == cid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName,
                            DepDate = mf.ManifestDate
                        }).ToList();
                return list;
            }

        }

        public List<ADEntery> WaitingListReport(Int64 cityId, DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.CityID == cityId && p.IsManifest == false && p.IsTempHold == false && p.Date >= date && p.Date < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> WaitingListReportMod(Int64 cityId, DateTime date)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.CityID == cityId && p.IsManifest == false && p.IsTempHold == false && (p.Date.Value.Day == date.Date.Day && p.Date.Value.Month == date.Month && p.Date.Value.Month == date.Month)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> WaitingListReportMod(DateTime date)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.IsManifest == false && p.IsTempHold == false && (p.Date.Value.Day == date.Date.Day && p.Date.Value.Month == date.Month && p.Date.Value.Month == date.Month)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> WaitingListReport(Int64 cityId, int unitid, DateTime date)
        {
            List<ADEntery> list = new List<ADEntery>();

            if (unitid == 0)
            {

                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cityId && p.IsManifest == false && p.IsTempHold == false && p.Date <= date
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
                return list;
            }
            else
            {

                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cityId && p.IsManifest == false && p.IsTempHold == false && p.Date <= date && p.UnitID == unitid
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
                return list;
            }

        }

        public List<ADEntery> WaitingListReportFMN(Int64 cityId, int divId, DateTime date)
        {
            List<ADEntery> list = new List<ADEntery>();

            if (divId == 0)
            {

                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cityId && p.IsManifest == false && p.IsTempHold == false && p.Date <= date
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
                return list;
            }
            else
            {

                list = (from p in context.admasters
                        join c in context.citymasters on p.CityID equals c.ID
                        join c1 in context.categorymasters on p.CategoryID equals c1.ID
                        join r in context.rankmasters on p.RankID equals r.ID
                        join u in context.unitmasters on p.UnitID equals u.ID
                        join d in context.divmasters on p.DivID equals d.ID
                        join h in context.hqmasters on p.HQID equals h.ID
                        join m in context.movemasters on p.MoveID equals m.ID
                        join a in context.adtypemasters on p.AdTypeID equals a.ID
                        where p.CityID == cityId && p.IsManifest == false && p.IsTempHold == false && p.Date <= date && p.DivID == divId
                        select new ADEntery
                        {
                            ID = p.ID,
                            CityName = c.CityName,
                            ADNO = p.ADNO,
                            CategoryName = c1.CategoryName,
                            ICard = p.ICard,
                            ArmyNo = p.ArmyNo,
                            RankName = r.Rank,
                            Name = p.Name,
                            UnitName = u.UnitName,
                            DivName = d.DivName,
                            HQName = h.HQName,
                            Authority = p.Authority,
                            Session = p.Session,
                            BP = p.BP,
                            Date = p.Date,
                            IsTempHold = p.IsTempHold,
                            DocumentUrl = p.DocumentUrl,
                            IsPriority = p.IsPriority,
                            IsReserve = p.IsReserve,
                            IsLoad = p.IsLoad,
                            MoveID = m.ID,
                            MoveName = m.MoveName,
                            AdTypeID = a.ID,
                            ADTypeName = a.ADTypeName,
                            FMN = d.DivName
                        }).ToList();
                return list;
            }

        }

        public List<ADEntery> WaitingListReportMod(int cityID, DateTime date)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.CityID == cityID && p.IsManifest == false && p.IsTempHold == false && p.Date <= date
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName
                    }).ToList();
            return list;
        }

        public List<ADEntery> WaitingListReport(Int64 cityId, DateTime fromdate, DateTime toDate)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.CityID == cityId && p.IsManifest == false && p.IsTempHold == false && p.Date >= fromdate && p.Date <= toDate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName,
                        OnHoldRemark = p.OnHoldRemark,
                        UpdatedOn = p.UpdatedOn
                    }).ToList();
            return list;
        }

        //Get By Departure Date Wise from database for reporting
        public List<ADEntery> DepartureDateWiseReport(DateTime fromdate, DateTime todate)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from m1 in context.manifestmasters
                    join p in context.admasters on m1.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.IsManifest == true && m1.ManifestDate >= fromdate && m1.ManifestDate <= todate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureDateWiseReport(DateTime fromdate, DateTime todate, Int64 cityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from m1 in context.manifestmasters
                    join p in context.admasters on m1.ADID equals p.ID
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    join m in context.movemasters on p.MoveID equals m.ID
                    join a in context.adtypemasters on p.AdTypeID equals a.ID
                    where p.IsManifest == true && m1.ManifestDate >= fromdate && m1.ManifestDate <= todate && p.CityID == cityID
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a.ID,
                        ADTypeName = a.ADTypeName,
                        FMN = d.DivName,
                        DepDate = m1.ManifestDate
                    }).ToList();
            return list;
        }

        //Get By Departure Date Wise from database for reporting (Single Date)
        public List<ADEntery> DepartureDateWiseReport(DateTime date)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.manifestmasters
                    join a in context.admasters on p.ADID equals a.ID
                    join c in context.citymasters on a.CityID equals c.ID
                    join c1 in context.categorymasters on a.CategoryID equals c1.ID
                    join r in context.rankmasters on a.RankID equals r.ID
                    join u in context.unitmasters on a.UnitID equals u.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    join h in context.hqmasters on a.HQID equals h.ID
                    join m in context.movemasters on a.MoveID equals m.ID
                    join a1 in context.adtypemasters on a.AdTypeID equals a1.ID
                    where a.IsManifest == true && p.ManifestDate >= date && p.ManifestDate < nextdate
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = a.Authority,
                        Session = p.Session,
                        BP = a.BP,
                        Date = a.Date,
                        IsTempHold = a.IsTempHold,
                        DocumentUrl = a.DocumentUrl,
                        IsPriority = a.IsPriority,
                        IsReserve = a.IsReserve,
                        IsLoad = a.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a1.ID,
                        ADTypeName = a1.ADTypeName,
                        ArrDate = a.Date,
                        FMN = d.DivName,
                        DepDate = p.ManifestDate
                    }).ToList();
            return list;
        }

        public List<ADEntery> DepartureDateWiseReport(DateTime date, int cityID)
        {
            DateTime nextdate = date.AddDays(1);

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.manifestmasters
                    join a in context.admasters on p.ADID equals a.ID
                    join c in context.citymasters on a.CityID equals c.ID
                    join c1 in context.categorymasters on a.CategoryID equals c1.ID
                    join r in context.rankmasters on a.RankID equals r.ID
                    join u in context.unitmasters on a.UnitID equals u.ID
                    join d in context.divmasters on a.DivID equals d.ID
                    join h in context.hqmasters on a.HQID equals h.ID
                    join m in context.movemasters on a.MoveID equals m.ID
                    join a1 in context.adtypemasters on a.AdTypeID equals a1.ID
                    where a.IsManifest == true && p.ManifestDate >= date && p.ManifestDate < nextdate && a.CityID == cityID
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = a.Authority,
                        Session = p.Session,
                        BP = a.BP,
                        Date = a.Date,
                        IsTempHold = a.IsTempHold,
                        DocumentUrl = a.DocumentUrl,
                        IsPriority = a.IsPriority,
                        IsReserve = a.IsReserve,
                        IsLoad = a.IsLoad,
                        MoveID = m.ID,
                        MoveName = m.MoveName,
                        AdTypeID = a1.ID,
                        ADTypeName = a1.ADTypeName,
                        DepDate = p.ManifestDate,
                        FMN = d.DivName
                    }).ToList();
            return list;
        }

        //Get List from database
        public List<ADEntery> Paging(Int32 take, Int32 skip)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where p.IsOnHold == false && p.IsReserve == false && p.IsTempHold == false && p.IsPriority == false && p.IsManifest == false && p.IsLoad == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        CheckOutDate = p.CheckOutDate
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get AD Entry No Priority Wise
        public List<ADEntery> ADEntryNoPriority()
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == true && p.IsTempHold == false && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        public List<ADEntery> ADEntryNoPriority(Int64 cityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == true && p.IsTempHold == false && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == false && p.CityID == cityID
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No Priority City Wise
        public List<ADEntery> ADEntryNoPriorityCity(Int64 CityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == true && p.IsTempHold == false && p.IsManifest == false && p.CityID == CityID && p.IsOnHold == false && p.IsLoad == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No General City Wise
        public List<ADEntery> ADEntryNoGeneralCity(Int64 CityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == false && p.IsTempHold == false && p.IsReserve != true && p.IsManifest == false && p.CityID == CityID && p.IsOnHold == false && p.IsLoad == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No Reserve City Wise
        public List<ADEntery> ADEntryNoReserveCity(Int64 CityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsReserve == true && p.IsTempHold == false && p.IsManifest == false && p.CityID == CityID && p.IsOnHold == false && p.IsLoad == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No Load City Wise
        public List<ADEntery> ADEntryNoLoadCity(Int64 CityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsReserve == false && p.IsTempHold == false && p.IsManifest == false && p.CityID == CityID && p.IsOnHold == false && p.IsLoad == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No Priority CatID Wise
        public List<ADEntery> ADEntryNoPriorityCat(Int64 CatID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == true && p.IsTempHold == false && p.IsManifest == false && p.CategoryID == CatID && p.IsOnHold == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No General Wise
        public List<ADEntery> ADEntryNoGeneral()
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == false && p.IsTempHold == false && p.IsReserve != true && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        public List<ADEntery> ADEntryNoGeneralCityWise(Int64 cityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == false && p.IsTempHold == false && p.IsReserve != true && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == false && p.CityID == cityID
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No General Cat Wise
        public List<ADEntery> ADEntryNoGeneralCat(Int64 CatID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsPriority == false && p.IsTempHold == false && p.IsReserve != true && p.IsManifest == false && p.CategoryID == CatID && p.IsOnHold == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No Reserve Wise
        public List<ADEntery> ADEntryNoReserve()
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsReserve == true && p.IsTempHold == false && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        public List<ADEntery> ADEntryNoReserveCityWise(Int64 cityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsReserve == true && p.IsTempHold == false && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == false && p.CityID == cityID
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No Load Wise
        public List<ADEntery> ADEntryNoLoad()
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsReserve == false && p.IsTempHold == false && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == true
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        public List<ADEntery> ADEntryNoLoad(Int64 cityID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsReserve == false && p.IsTempHold == false && p.IsManifest == false && p.IsOnHold == false && p.IsLoad == true && p.CityID == cityID
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No Reserve Cat Wise
        public List<ADEntery> ADEntryNoReserveCat(Int64 CatID)
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsReserve == true && p.IsTempHold == false && p.IsManifest == false && p.CategoryID == CatID && p.IsOnHold == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                    }).ToList();
            return list;
        }

        //Get AD Entry No
        public List<ADEntery> ADEntryNo()
        {

            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    where p.IsOnHold == false && p.IsManifest == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        Name = p.Name,
                        IsTempHold = p.IsTempHold
                    }).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.admasters
                         where p.IsPriority == false && p.IsOnHold == false && p.IsReserve == false && p.IsTempHold == false && p.IsManifest == false && p.IsLoad == false
                         select p).Count();

            return count;
        }

        //Get total number of priority records present in DataBase
        public Int32 TotalItemsPriority()
        {
            var count = (from p in context.admasters
                         where p.IsPriority == true && p.IsOnHold == false && p.IsReserve == false && p.IsTempHold == false
                         select p).Count();

            return count;
        }

        //Get total number of on hold status records present in DataBase
        public Int32 TotalItemsOnHoldStatus()
        {
            var count = (from p in context.admasters
                         where p.IsPriority == false && p.IsOnHold == true && p.IsReserve == false && p.IsTempHold == false
                         select p).Count();

            return count;
        }

        //Get total number of on hold status records present in DataBase
        public Int32 TotalItemsLoad()
        {
            var count = (from p in context.admasters
                         where p.IsPriority == false && p.IsOnHold == false && p.IsReserve == false && p.IsTempHold == false && p.IsLoad == true
                         select p).Count();

            return count;
        }

        //Get total number of on temp hold records present in DataBase
        public Int32 TotalItemsOnTemHold()
        {
            var count = (from p in context.admasters
                         where p.IsPriority == false && p.IsOnHold == false && p.IsReserve == false && p.IsTempHold == true
                         select p).Count();

            return count;
        }

        //Get total number of on temp hold records present in DataBase
        public Int32 TotalItemsOnReserve()
        {
            var count = (from p in context.admasters
                         where p.IsPriority == false && p.IsOnHold == false && p.IsReserve == true && p.IsTempHold == false
                         select p).Count();

            return count;
        }

        //Get total number of on hold status records present in DataBase
        public Int32 TotalItemsOnHold()
        {
            var count = (from p in context.admasters
                         where p.IsOnHold == true
                         select p).Count();

            return count;
        }

        //Delete reserve from AD by ID
        public void Reserve(Int64 ID)
        {
            var data = (from p in context.admasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            data.IsReserve = true;
            data.IsPriority = false;
            context.Entry(data).State = EntityState.Modified;
        }

        //Delete load from AD by ID 
        public void Load(Int64 ID)
        {
            var data = (from p in context.admasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            data.IsLoad = true;
            data.IsPriority = false;
            context.Entry(data).State = EntityState.Modified;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.admasters
                        where p.ID == ID
                        select p).FirstOrDefault();

            context.admasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<ADEntery> GetSearchResult(String SearchText)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where (p.ADNO.Contains(SearchText) || p.ArmyNo.Contains(SearchText) || p.ICard.Contains(SearchText) || p.Name.Contains(SearchText) || h.HQName.Contains(SearchText) || u.UnitName.Contains(SearchText) || r.Rank.Contains(SearchText) || c1.CategoryName.Contains(SearchText) || c.CityName.Contains(SearchText)) && (p.IsOnHold == false && p.IsManifest == false && p.IsReserve == false && p.IsPriority == false && p.IsTempHold == false && p.IsLoad == false)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get searched list from database Reserve
        public List<ADEntery> GetSearchResultReserve(String SearchText)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where (p.ADNO.Contains(SearchText) || p.ArmyNo.Contains(SearchText) || p.ICard.Contains(SearchText) || p.Name.Contains(SearchText) || h.HQName.Contains(SearchText) || u.UnitName.Contains(SearchText) || r.Rank.Contains(SearchText) || c1.CategoryName.Contains(SearchText) || c.CityName.Contains(SearchText)) && (p.IsReserve == true && p.IsTempHold == false && p.IsOnHold == false && p.IsManifest == false)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get searched list from database Load
        public List<ADEntery> GetSearchResultLoad(String SearchText)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where (p.ADNO.Contains(SearchText) || p.ArmyNo.Contains(SearchText) || p.ICard.Contains(SearchText) || p.Name.Contains(SearchText) || h.HQName.Contains(SearchText) || u.UnitName.Contains(SearchText) || r.Rank.Contains(SearchText) || c1.CategoryName.Contains(SearchText) || c.CityName.Contains(SearchText)) && (p.IsReserve == false && p.IsTempHold == false && p.IsOnHold == false && p.IsManifest == false && p.IsLoad == true)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get searched list from database IsTempHold
        public List<ADEntery> GetSearchResultTempHold(String SearchText)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where (p.ADNO.Contains(SearchText) || p.ArmyNo.Contains(SearchText) || p.ICard.Contains(SearchText) || p.Name.Contains(SearchText) || h.HQName.Contains(SearchText) || u.UnitName.Contains(SearchText) || r.Rank.Contains(SearchText) || c1.CategoryName.Contains(SearchText) || c.CityName.Contains(SearchText)) && (p.IsOnHold == false && p.IsTempHold == true && p.IsManifest == false)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        OnTemHoldRemark = p.OnTempHoldRemark
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get searched list from database IsTempHold
        public List<ADEntery> GetSearchResultOnHoldStatus(String SearchText)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where (p.ADNO.Contains(SearchText) || p.ArmyNo.Contains(SearchText) || p.ICard.Contains(SearchText) || p.Name.Contains(SearchText) || h.HQName.Contains(SearchText) || u.UnitName.Contains(SearchText) || r.Rank.Contains(SearchText) || c1.CategoryName.Contains(SearchText) || c.CityName.Contains(SearchText)) && (p.IsOnHold == true)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        OnHoldRemark = p.OnHoldRemark
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //Get searched list from database Priority
        public List<ADEntery> GetSearchResultPriority(String SearchText)
        {
            List<ADEntery> list = new List<ADEntery>();

            list = (from p in context.admasters
                    join c in context.citymasters on p.CityID equals c.ID
                    join c1 in context.categorymasters on p.CategoryID equals c1.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join h in context.hqmasters on p.HQID equals h.ID
                    where (p.ADNO.Contains(SearchText) || p.ArmyNo.Contains(SearchText) || p.ICard.Contains(SearchText) || p.Name.Contains(SearchText) || h.HQName.Contains(SearchText) || u.UnitName.Contains(SearchText) || r.Rank.Contains(SearchText) || c1.CategoryName.Contains(SearchText) || c.CityName.Contains(SearchText)) && (p.IsPriority == true && p.IsTempHold == false && p.IsOnHold == false && p.IsManifest == false)
                    select new ADEntery
                    {
                        ID = p.ID,
                        CityName = c.CityName,
                        ADNO = p.ADNO,
                        CategoryName = c1.CategoryName,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        RankName = r.Rank,
                        Name = p.Name,
                        UnitName = u.UnitName,
                        DivName = d.DivName,
                        HQName = h.HQName,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        //save context
        public void Save()
        {
            context.SaveChanges();
        }

        //ration reports
        public List<ADEntery> GetAllADsForRation(int cityid)
        {
            List<ADEntery> list = new List<ADEntery>();
            list = (from p in context.admasters
                    join c in context.categorymasters on p.CategoryID equals c.ID
                    where p.CityID == cityid && p.IsTempHold == false
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        Name = p.Name,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        CategoryName = c.CategoryName,
                        IsManifest = p.IsManifest
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        public List<ADEntery> GetAllADsForRationBF(int cityid)
        {
            DateTime pDate = DateTime.Now;
            List<ADEntery> list = new List<ADEntery>();
            list = (from mf in context.manifestmasters
                    join p in context.admasters on mf.ADID equals p.ID
                    join c in context.categorymasters on p.CategoryID equals c.ID
                    where p.CityID == cityid && p.IsTempHold == false && p.IsManifest == true && (mf.ManifestDate.Value.Day == pDate.Day && mf.ManifestDate.Value.Month == pDate.Month && mf.ManifestDate.Value.Year == pDate.Year)
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        Name = p.Name,
                        Authority = p.Authority,
                        Session = p.Session,
                        BP = p.BP,
                        Date = p.Date,
                        IsTempHold = p.IsTempHold,
                        DocumentUrl = p.DocumentUrl,
                        IsPriority = p.IsPriority,
                        IsReserve = p.IsReserve,
                        IsLoad = p.IsLoad,
                        CategoryName = c.CategoryName,
                        IsManifest = p.IsManifest
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
        }

        public List<ADEntery> GetAllADsForRationDep(int cityid)
        {
            List<ADEntery> list = new List<ADEntery>();
            list = (from p in context.manifestmasters
                    join a in context.admasters on p.ADID equals a.ID
                    join c in context.categorymasters on p.CategoryID equals c.ID
                    join t in context.transportdetails on p.TransportDetailID equals t.ID
                    join t1 in context.transportmasters on t.TransportTypeID equals t1.ID
                    where p.CityID == cityid
                    select new ADEntery
                    {
                        ID = p.ID,
                        ADNO = p.ADNO,
                        ICard = p.ICard,
                        ArmyNo = p.ArmyNo,
                        Name = p.Name,
                        Authority = a.Authority,
                        Session = p.Session,
                        BP = a.BP,
                        Date = a.Date,
                        IsTempHold = a.IsTempHold,
                        DocumentUrl = a.DocumentUrl,
                        IsPriority = a.IsPriority,
                        IsReserve = a.IsReserve,
                        IsLoad = a.IsLoad,
                        CategoryName = c.CategoryName,
                        MDate = p.ManifestDate,
                        IsManifest = a.IsManifest,
                        TransportName = t1.TransportName
                    }).OrderByDescending(x => x.ID).ToList();
            return list;
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
