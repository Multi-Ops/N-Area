using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataAccessLayer;
using BusinessLayer;

namespace DataLayer
{
    public interface IAirlineServices
    {
        Airline GetAirlineByID(Int64 id);
        void Insert(Airline airline);
        Int64 Update(Airline airline);
        List<Airline> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        void Save();
        List<Airline> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string airlinename);
        List<Airline> GetAirlineDetails();
    }
}
