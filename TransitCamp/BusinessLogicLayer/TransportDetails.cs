using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TransportDetails
    {
        public long ID { get; set; }
        public Nullable<long> CityID { get; set; }
        public string TransportDetail { get; set; }
        public string TransportType { get; set; }
        public string CityName { get; set; }
        public string Session { get; set; }
        public Nullable<long> TransportTypeID { get; set; }
        public Nullable<long> TotalNoOfSeats { get; set; }
        public Nullable<long> NoOfSeats { get; set; }
        public Nullable<long> PrioritySeats { get; set; }
        public Nullable<long> Load { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
