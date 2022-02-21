using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BllRoom
    {
        public long Id { get; set; }
        public Nullable<bool> IsBillShare { get; set; }
        public Nullable<long> BlockId { get; set; }
        public string RoomName { get; set; }
        public int MaxRoomCap { get; set; }
        public double RoomPrice { get; set; }
        public bool IsShare { get; set; }
        public Nullable<bool> IsFull { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
