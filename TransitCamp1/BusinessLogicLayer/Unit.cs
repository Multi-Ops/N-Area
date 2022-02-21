using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Unit
    {
        public long ID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<long> HQID { get; set; }
        public Nullable<long> DivID { get; set; }
        public string UnitName { get; set; }
        public string DivisionName { get; set; }
        public string HQName { get; set; }
        public string CityName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
