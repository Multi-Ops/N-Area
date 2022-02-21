using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IMedicalStatusServices
    {
        MedicalStatus GetByID(Int64 id);
        void Insert(MedicalStatus info);
        Int64 Update(MedicalStatus info);
        List<MedicalStatus> Paging(Int32 take, Int32 skip);
        Int32 TotalItems();
        void Delete(Int64 ID);
        List<MedicalStatus> GetSearchResult(String SearchText);
        int CheckAlreadyExist(string info);
        void Save();
        List<MedicalStatus> getmeddetails();
    }
}
