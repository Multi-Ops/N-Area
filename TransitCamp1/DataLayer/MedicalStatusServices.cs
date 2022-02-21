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
    public class MedicalStatusServices : IMedicalStatusServices, IDisposable
    {
        #region Interface

        //create instance of data access context
        private TCContext context;
        public MedicalStatusServices(TCContext context)
        {
            this.context = context;
        }


        public List<MedicalStatus> getmeddetails()
        {
            List<MedicalStatus> list = new List<MedicalStatus>();
            list = (from p in context.medicalstatusmasters
                    select new MedicalStatus
                    {
                        ID = p.ID,
                        MedicalStatusName = p.MedicalStatusName,
                    }).ToList();

            return list;
        }

        //get by id
        public MedicalStatus GetByID(Int64 id)
        {
            MedicalStatus div = new MedicalStatus();
            var getdata = (from p in context.medicalstatusmasters
                           where p.ID == id
                           select new MedicalStatus
                           {
                               ID = p.ID,
                               MedicalStatusName = p.MedicalStatusName
                           }).FirstOrDefault();
            return getdata;
        }

        //Insert
        public void Insert(MedicalStatus info)
        {
            var data = new medicalstatusmaster
            {
                MedicalStatusName = info.MedicalStatusName,
                CreatedOn = info.CreatedOn
            };
            context.medicalstatusmasters.Add(data);
        }

        //update
        public Int64 Update(MedicalStatus info)
        {
            var data = (from p in context.medicalstatusmasters
                        where p.ID == info.ID
                        select p).FirstOrDefault();
            data.MedicalStatusName = info.MedicalStatusName;
            data.UpdatedOn = info.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of from database
        public List<MedicalStatus> Paging(Int32 take, Int32 skip)
        {
            List<MedicalStatus> list = new List<MedicalStatus>();
            list = (from p in context.medicalstatusmasters
                    select new MedicalStatus
                    {
                        ID = p.ID,
                        MedicalStatusName = p.MedicalStatusName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from p in context.medicalstatusmasters
                         select p).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from p in context.medicalstatusmasters
                        where p.ID == ID
                        select p).FirstOrDefault();
            context.medicalstatusmasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list from database
        public List<MedicalStatus> GetSearchResult(String SearchText)
        {
            List<MedicalStatus> list = new List<MedicalStatus>();

            list = (from p in context.medicalstatusmasters
                    where p.MedicalStatusName.Contains(SearchText)
                    select new MedicalStatus
                    {
                        ID = p.ID,
                        MedicalStatusName = p.MedicalStatusName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if Already exist
        public int CheckAlreadyExist(string info)
        {
            var id = context.medicalstatusmasters.FirstOrDefault(u => u.MedicalStatusName == info)?.ID;
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
