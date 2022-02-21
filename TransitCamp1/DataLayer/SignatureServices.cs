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
    public class SignatureServices : ISignatureServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public SignatureServices(TCContext context)
        {
            this.context = context;
        }

        //get details
        public Signature GetDetails()
        {
            Signature div = new Signature();
            var getdata = (from p in context.signaturedetailmasters
                           join r in context.rankmasters on p.RankID equals r.ID
                           orderby p.ID descending
                           select new Signature
                           {
                               SignatureName = p.SignatureName,
                               RankName = r.Rank,
                               RankID = r.ID,
                           }).FirstOrDefault();
            return getdata;
        }

        //get by id
        public Signature GetByID(Int64 id)
        {
            Signature div = new Signature();
            var getdata = (from p in context.signaturedetailmasters
                           join h in context.hqmasters on p.HQID equals h.ID
                           join r in context.rankmasters on p.RankID equals r.ID
                           join d in context.divmasters on p.DivID equals d.ID
                           join u in context.unitmasters on p.UnitID equals u.ID
                           where p.ID == id
                           select new Signature
                           {
                               SignatureName = p.SignatureName,
                               HeadQuarterName = h.HQName,
                               RankName = r.Rank,
                               UnitName = u.UnitName,
                               DivName = d.DivName,
                               RankID = r.ID,
                               HQID = h.ID,
                               DivID = d.ID,
                               UnitID = u.ID
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(Signature info)
        {
            var data = new signaturedetailmaster
            {
                SignatureName = info.SignatureName,
                RankID = info.RankID,
                HQID = info.HQID,
                UnitID = info.UnitID,
                DivID = info.DivID,
                CreatedOn = info.CreatedOn
            };
            context.signaturedetailmasters.Add(data);
        }

        //update
        public Int64 Update(Signature info)
        {
            var data = (from p in context.signaturedetailmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.SignatureName = info.SignatureName;
            data.RankID = info.RankID;
            data.HQID = info.HQID;
            data.UnitID = info.UnitID;
            data.DivID = info.DivID;
            data.CreatedOn = info.CreatedOn;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<Signature> Paging(Int32 take, Int32 skip)
        {
            List<Signature> list = new List<Signature>();
            list = (from p in context.signaturedetailmasters
                    join h in context.hqmasters on p.HQID equals h.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    select new Signature
                    {
                        ID = p.ID,
                        SignatureName = p.SignatureName,
                        HeadQuarterName = h.HQName,
                        RankName = r.Rank,
                        UnitName = u.UnitName,
                        DivName = d.DivName
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.signaturedetailmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.signaturedetailmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.signaturedetailmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<Signature> GetSearchResult(String SearchText)
        {
            List<Signature> list = new List<Signature>();

            list = (from p in context.signaturedetailmasters
                    join h in context.hqmasters on p.HQID equals h.ID
                    join r in context.rankmasters on p.RankID equals r.ID
                    join d in context.divmasters on p.DivID equals d.ID
                    join u in context.unitmasters on p.UnitID equals u.ID
                    where p.SignatureName.Contains(SearchText)
                    select new Signature
                    {
                        ID = p.ID,
                        SignatureName = p.SignatureName,
                        HeadQuarterName = h.HQName,
                        RankName = r.Rank,
                        UnitName = u.UnitName,
                        DivName = d.DivName
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.signaturedetailmasters.FirstOrDefault(u => u.SignatureName == info)?.ID;
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
