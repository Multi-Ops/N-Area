//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class charterdetailsmaster
    {
        public long ID { get; set; }
        public Nullable<long> AirLineID { get; set; }
        public string FromCity { get; set; }
        public Nullable<long> FromCityID { get; set; }
        public Nullable<long> ToCityID { get; set; }
        public string ToCity { get; set; }
        public Nullable<System.DateTime> CharteredDate { get; set; }
        public string Session { get; set; }
        public string CharterNo { get; set; }
        public string FlightNo { get; set; }
        public Nullable<int> NumberOfSeats { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
