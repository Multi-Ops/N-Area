using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICityServices
    {
        City GetCityID(Int64 id);
        City GetCityIDNoJoin(Int64 id);
        void Insert(City city);
        Int64 Update(City city);
        List<City> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<City> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string cityname);
        void Save();
        List<City> GetCityDetails();
    }
}
