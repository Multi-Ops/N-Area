using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Signature
    {
        public long ID { get; set; }
        public string SignatureName { get; set; }
        public string HeadQuarterName { get; set; }
        public string RankName { get; set; }
        public string UnitName { get; set; }
        public string DivName { get; set; }
        public long RankID { get; set; }
        public long HQID { get; set; }
        public long UnitID { get; set; }
        public long DivID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
