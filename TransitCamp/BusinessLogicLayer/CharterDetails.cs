using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CharterDetails
    {
        public long ID { get; set; }
        public Nullable<long> AirLineID { get; set; }
        public Nullable<long> ADCityInfoID { get; set; }
        public Nullable<long> FromCityID { get; set; }
        public Nullable<long> ToCityID { get; set; }
        public Nullable<System.DateTime> CharteredDate { get; set; }
        public string Session { get; set; }
        public string CharterNo { get; set; }
        public string FlightNo { get; set; }
        public string AirlineName { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public Nullable<int> NumberOfSeats { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
