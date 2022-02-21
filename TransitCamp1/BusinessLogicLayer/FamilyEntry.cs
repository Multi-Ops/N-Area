using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FamilyEntry
    {
        public long ID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Session { get; set; }
        public string FamilyName { get; set; }
        public string CityName { get; set; }
        public string CategoryName { get; set; }
        public string IDCard { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string PriorityNo { get; set; }
        public string Relation { get; set; }
        public string NoOfLuggae { get; set; }
        public string RankName { get; set; }
        public string UnitName { get; set; }
        public string DivName { get; set; }
        public string MoveName { get; set; }
        public string PriorityName { get; set; }
        public string HQName { get; set; }
        public string ArmyNo { get; set; }
        public Nullable<long> RankID { get; set; }
        public string Name { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<long> DivID { get; set; }
        public Nullable<long> HQID { get; set; }
        public string Authority { get; set; }
        public Nullable<long> MoveID { get; set; }
        public Nullable<long> PriorityID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
