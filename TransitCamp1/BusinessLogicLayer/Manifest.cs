using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Manifest
    {
        public long ID { get; set; }
        public long ADID { get; set; }
        public string TransportDetails { get; set; }
        public long TransportDetailID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<System.DateTime> ManifestDate { get; set; }
        public string Session { get; set; }
        public string MenifestNo { get; set; }
        public string CityName { get; set; }
        public string TransportTypeName { get; set; }
        public string BP { get; set; }
        public string FMN { get; set; }
        public string MoveName { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> TotalNoOfSeats { get; set; }
        public Nullable<long> NoOfSeats { get; set; }
        public Nullable<long> PrioritySeats { get; set; }
        public string ADNO { get; set; }
        public Nullable<bool> IsPriority { get; set; }
        public string ArmyNo { get; set; }
        public Nullable<long> RankID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string Name { get; set; }
        public Nullable<long> UnitID { get; set; }
        public string ICard { get; set; }
        public string Rank { get; set; }
        public string UnitName { get; set; }
        public Nullable<bool> IsReserve { get; set; }
        public Nullable<bool> IsLoad { get; set; }
        public string CategoryName { get; set; }
        public string HQName { get; set; }
        public Nullable<long> HQID { get; set; }
    }
}
