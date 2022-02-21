using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ADLeaveEntry
    {
        public long ID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<long> LavelID { get; set; }
        public string ICard { get; set; }
        public string ArmyNo { get; set; }
        public Nullable<long> RankID { get; set; }
        public string LevelName { get; set; }
        public string CityName { get; set; }
        public string Rank { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public string HQName { get; set; }
        public string MoveName { get; set; }
        public string PriorityName { get; set; }
        public Nullable<long> HQID { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<long> MoveID { get; set; }
        public Nullable<long> PriorityID { get; set; }
        public Nullable<System.DateTime> OutDate { get; set; }
        public string Session { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
