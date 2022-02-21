using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using BusinessLayer;

namespace DataLayer
{
    public class CityServices : ICityServices, IDisposable
    {


        #region Interface

        //create instance of data access context
        private TCContext context;
        public CityServices(TCContext context)
        {
            this.context = context;
        }

        //get airline details
        public List<City> GetCityDetails()
        {
            List<City> list = new List<City>();

            list = (from p in context.citymasters
                    select new City
                    {
                        ID = p.ID,
                        CityName = p.CityName,
                    }).ToList();

            return list;
        }

        //get City by id
        public City GetCityID(Int64 id)
        {
            City city = new City();
            var getcity = (from c in context.citymasters
                           join m in context.medicalstatusmasters on c.MedicalStatusID equals m.ID
                           join o in context.outlogicmasters on c.OutLogicID equals o.ID
                           where c.ID == id
                           select new City
                           {
                               CityName = c.CityName,
                               OutLogicID = o.ID,
                               MedicalStatusID = m.ID,
                               OutLogicName = o.OutLogicName,
                               MedicalStatusName = m.MedicalStatusName,
                               StateName = c.StateName,
                           }).FirstOrDefault();
            return getcity;
        }

        public City GetCityIDNoJoin(Int64 id)
        {
            City city = new City();
            var getcity = (from c in context.citymasters
                           where c.ID == id
                           select new City
                           {
                               CityName = c.CityName,
                               StateName = c.StateName,
                           }).FirstOrDefault();
            return getcity;
        }

        //Insert City
        public void Insert(City city)
        {
            var citymaster = new citymaster
            {
                CityName = city.CityName,
                OutLogicID = city.OutLogicID,
                MedicalStatusID = city.MedicalStatusID,
                StateName = city.StateName,
                CreatedOn = city.CreatedOn
            };
            context.citymasters.Add(citymaster);
        }

        //update City
        public Int64 Update(City city)
        {
            var data = (from c in context.citymasters
                        where c.ID == city.ID
                        select c).FirstOrDefault();
            data.CityName = city.CityName;
            data.MedicalStatusID = city.MedicalStatusID;
            data.OutLogicID = city.OutLogicID;
            data.StateName = city.StateName;
            data.UpdatedOn = city.UpdatedOn;
            context.Entry(data).State = EntityState.Modified;
            return data.ID;
        }

        //Get List of Cities from database
        public List<City> Paging(Int32 take, Int32 skip)
        {
            List<City> list = new List<City>();
            list = (from c in context.citymasters
                    join m in context.medicalstatusmasters on c.MedicalStatusID equals m.ID
                    join o in context.outlogicmasters on c.OutLogicID equals o.ID
                    select new City
                    {
                        ID = c.ID,
                        CityName = c.CityName,
                        OutLogicID = o.ID,
                        MedicalStatusID = m.ID,
                        OutLogicName = o.OutLogicName,
                        MedicalStatusName = m.MedicalStatusName,
                        StateName = c.StateName,
                    }).OrderByDescending(x => x.ID).Skip(skip).Take(take).ToList();
            return list;
        }

        //Get total number of records present in DataBase
        public Int32 TotalItems()
        {
            var count = (from a in context.citymasters
                         select a).Count();

            return count;
        }

        //Delete from Database by ID
        public void Delete(Int64 ID)
        {
            var data = (from c in context.citymasters
                        where c.ID == ID
                        select c).FirstOrDefault();
            context.citymasters.Remove(data);
            context.SaveChanges();
        }

        //Get searched list of cities from database
        public List<City> GetSearchResult(String SearchText)
        {
            List<City> list = new List<City>();

            list = (from c in context.citymasters
                    where c.CityName.Contains(SearchText)
                    join m in context.medicalstatusmasters on c.MedicalStatusID equals m.ID
                    join o in context.outlogicmasters on c.OutLogicID equals o.ID
                    select new City
                    {
                        ID = c.ID,
                        CityName = c.CityName,
                        OutLogicID = o.ID,
                        MedicalStatusID = m.ID,
                        OutLogicName = o.OutLogicName,
                        MedicalStatusName = m.MedicalStatusName,
                        StateName = c.StateName,
                    }).OrderByDescending(x => x.ID).ToList();

            return list;
        }

        //check if city Name Already exist
        public int CheckAlreadyExist(string cityname)
        {
            var cityid = context.citymasters.FirstOrDefault(u => u.CityName == cityname)?.ID;
            if (cityid == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(cityid);
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
