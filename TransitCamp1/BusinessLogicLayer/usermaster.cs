using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Users
    {
        public long ID { get; set; }
        public long UserRoleD { get; set; }
        public string IDCardNo { get; set; }
        public string ArmyNumber { get; set; }
        public string Rank { get; set; }
        public string DIVName { get; set; }
        public string HQName { get; set; }
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
        public string Regiment { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string ADNO { get; set; }
        public string TransportDetails { get; set; }
        public string Move { get; set; }
        public string Session { get; set; }
        public string DSession { get; set; }
        public string DDate { get; set; }
        public string FlightNo { get; set; }
        public Nullable<System.DateTime> TransportDate { get; set; }
        public Nullable<bool> IsManifest { get; set; }
        public Nullable<bool> IsOnHoldStatus { get; set; }
        public Nullable<bool> IsTempHold { get; set; }
        public Nullable<bool> IsPriority { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> UserRoleID { get; set; }
        public Nullable<long> ADID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<long> RankID { get; set; }
        public Nullable<long> DivID { get; set; }
        public Nullable<long> HQID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<long> UnitID { get; set; }
    }
}
