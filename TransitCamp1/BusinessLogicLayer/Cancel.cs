using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Cancel
    {
        public long ID { get; set; }
        public string TransportDetails { get; set; }
        public long ADID { get; set; }
        public long TransportDetailID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<System.DateTime> ManifestDate { get; set; }
        public string Session { get; set; }
        public string MenifestNo { get; set; }
        public string CityName { get; set; }
        public string CategoryName { get; set; }
        public string ADNO { get; set; }
        public string ArmyNo { get; set; }
        public string HQName { get; set; }
        public string Rank { get; set; }
        public string UnitName { get; set; }
        public Nullable<long> RankID { get; set; }
        public string Name { get; set; }
        public Nullable<long> UnitID { get; set; }
        public string ICard { get; set; }
        public Nullable<long> HQID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
