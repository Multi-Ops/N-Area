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
    public interface IUserServices
    {
        Int64 Update(Users user);
        int CheckPassword(string password, Int64 id);
        Users GetUserByID(Int64 id);
        int UserExist(string username);
        Int64 Insert(Users user);
        Int32 TotalItems();
        Int32 TotalItemsUserHistory();
        Int32 TotalItemsUserHistory(string armyno);
        List<Users> Paging(Int32 take, Int32 skip);
        void Delete(Int64 ID);
        List<Users> GetSearchResult(String SearchText, int searchtype);
        List<Users> PaggingByIDCardGroupByICard(Int32 take, Int32 skip);
        List<Users> GetSearchResultBySearchText(String SearchText);
        List<Users> PaggingByIDCard(Int32 take, Int32 skip, string icard);
        void Save();
        List<Users> AutoCompleteName(string Name);
        List<Users> AutoCompleteIcard(string ICard);
        List<Users> AutoCompleteArmyNo(string ArmyNo);
        Users GetUserByICard(string Icard);
        Users GetUserByArmyNo(string armyno);
    }
}
