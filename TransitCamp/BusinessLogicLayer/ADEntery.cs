using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ADEntery
    {
        public long ID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string DocumentUrl { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Session { get; set; }
        public string StateName { get; set; }
        public long SessionID { get; set; }
        public string ADNO { get; set; }
        public string ICard { get; set; }
        public string ArmyNo { get; set; }
        public Nullable<long> RankID { get; set; }
        public Nullable<long> MedicalStatusID { get; set; }
        public string Name { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<long> DivID { get; set; }
        public Nullable<long> BrigadeID { get; set; }
        public Nullable<long> HQID { get; set; }
        public string Authority { get; set; }
        public string OnTemHoldRemark { get; set; }
        public string OnHoldRemark { get; set; }
        public Nullable<long> MoveID { get; set; }
        public Nullable<long> PriorityID { get; set; }
        public Nullable<long> AdTypeID { get; set; }
        public string BP { get; set; }
        public string MedicalStatusName { get; set; }
        public string CityName { get; set; }
        public string CategoryName { get; set; }
        public string RankName { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public string DivName { get; set; }
        public string HQName { get; set; }
        public string MoveName { get; set; }
        public string PriorityName { get; set; }
        public string ADTypeName { get; set; }
        public string TransportName { get; set; }
        public string UnitName { get; set; }
        public Nullable<bool> IsTempHold { get; set; }
        public Nullable<bool> IsOnHold { get; set; }
        public Nullable<bool> IsLoad { get; set; }
        public Nullable<long> BookingId { get; set; }
        public Nullable<bool> IsLRC { get; set; }
        public Nullable<bool> IsFly { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string ReferenceN0 { get; set; }
        public Nullable<bool> IsReserve { get; set; }
        public Nullable<bool> IsManifest { get; set; }
        public Nullable<bool> IsPriority { get; set; }
        public string FMN { get; set; }
        public Int32 SNO { get; set; }
        public Nullable<System.DateTime> LeaveFromDate { get; set; }
        public Nullable<DateTime> LeaveToDate { get; set; }
        public Nullable<DateTime> DepDate { get; set; }
        public Nullable<DateTime> ArrDate { get; set; }
        public Nullable<DateTime> MDate { get; set; }
        public Nullable<int> LeaveNoOfDays { get; set; }
        public Nullable<int> NoOfAbsentDays { get; set; }
    }
}
