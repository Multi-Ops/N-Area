using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICategoryServices
    {
        Category GetByID(Int64 id);
        void Insert(Category info);
        Int64 Update(Category info);
        List<Category> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<Category> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<Category> details();
    }
}
