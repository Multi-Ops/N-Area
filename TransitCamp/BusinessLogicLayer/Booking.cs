using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Booking
    {
        public long Id { get; set; }
        public long BlockId { get; set; }
        public long ADId { get; set; }
        public Nullable<int> RoomId { get; set; }
        public long CreatedOn { get; set; }
        public long UpdatedOn { get; set; }
    }
}
