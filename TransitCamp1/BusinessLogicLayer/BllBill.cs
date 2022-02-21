using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BllBill
    {
        public long Id { get; set; }
        public bool IsLRC { get; set; }
        public long ADID { get; set; }
        public long RoomID { get; set; }
        public string Particulars { get; set; }
        public double Price { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
