﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TCContext : DbContext
    {
        public TCContext()
            : base("name=TCContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<adcityinfo> adcityinfoes { get; set; }
        public virtual DbSet<adleavemaster> adleavemasters { get; set; }
        public virtual DbSet<admaster> admasters { get; set; }
        public virtual DbSet<admasterthoise> admasterthoises { get; set; }
        public virtual DbSet<adtdmaster> adtdmasters { get; set; }
        public virtual DbSet<adtypemaster> adtypemasters { get; set; }
        public virtual DbSet<airlinemaster> airlinemasters { get; set; }
        public virtual DbSet<billattribute> billattributes { get; set; }
        public virtual DbSet<blockmaster> blockmasters { get; set; }
        public virtual DbSet<booking> bookings { get; set; }
        public virtual DbSet<campmaster> campmasters { get; set; }
        public virtual DbSet<cancel> cancels { get; set; }
        public virtual DbSet<cancelindividual> cancelindividuals { get; set; }
        public virtual DbSet<cancelthoise> cancelthoises { get; set; }
        public virtual DbSet<categorymaster> categorymasters { get; set; }
        public virtual DbSet<charteradmaster> charteradmasters { get; set; }
        public virtual DbSet<charterdetailsmaster> charterdetailsmasters { get; set; }
        public virtual DbSet<citymaster> citymasters { get; set; }
        public virtual DbSet<divmaster> divmasters { get; set; }
        public virtual DbSet<familymaster> familymasters { get; set; }
        public virtual DbSet<hqmaster> hqmasters { get; set; }
        public virtual DbSet<leavemaster> leavemasters { get; set; }
        public virtual DbSet<levelmaster> levelmasters { get; set; }
        public virtual DbSet<manifestmaster> manifestmasters { get; set; }
        public virtual DbSet<manifestmasterthoise> manifestmasterthoises { get; set; }
        public virtual DbSet<medicalstatusmaster> medicalstatusmasters { get; set; }
        public virtual DbSet<movemaster> movemasters { get; set; }
        public virtual DbSet<moveordertype> moveordertypes { get; set; }
        public virtual DbSet<outlogicmaster> outlogicmasters { get; set; }
        public virtual DbSet<prioritymaster> prioritymasters { get; set; }
        public virtual DbSet<prioritystatusmaster> prioritystatusmasters { get; set; }
        public virtual DbSet<rankmaster> rankmasters { get; set; }
        public virtual DbSet<receiptmaster> receiptmasters { get; set; }
        public virtual DbSet<roommaster> roommasters { get; set; }
        public virtual DbSet<signaturedetailmaster> signaturedetailmasters { get; set; }
        public virtual DbSet<transfercouriermaster> transfercouriermasters { get; set; }
        public virtual DbSet<transportdetail> transportdetails { get; set; }
        public virtual DbSet<transportmaster> transportmasters { get; set; }
        public virtual DbSet<unitmaster> unitmasters { get; set; }
        public virtual DbSet<usermaster> usermasters { get; set; }
        public virtual DbSet<userrolemaster> userrolemasters { get; set; }
    }
}
