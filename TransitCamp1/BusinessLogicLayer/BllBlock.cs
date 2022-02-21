using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BllBlock
    {
        public long Id { get; set; }
        public string BlockName { get; set; }
        public bool IsShare { get; set; }
        public Nullable<double> DefaultPrice { get; set; }
        public Nullable<int> MaxRoomAvailable { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
