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
    
    public partial class booking
    {
        public long Id { get; set; }
        public long BlockId { get; set; }
        public string CommonID { get; set; }
        public long ADId { get; set; }
        public Nullable<int> RoomId { get; set; }
        public long CreatedOn { get; set; }
        public long UpdatedOn { get; set; }
    }
}