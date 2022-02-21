using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLLBillAttribute
    {
        public long Id { get; set; }
        public long ADID { get; set; }
        public string Name { get; set; }
        public double LRCPrice { get; set; }
        public double NonLRCPrice { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
