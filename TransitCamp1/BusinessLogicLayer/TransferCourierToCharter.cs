using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TransferCourierToCharter
    {
        public long ID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Session { get; set; }
        public string ICardNo { get; set; }
        public string ArmyNo { get; set; }
        public string CategoryName { get; set; }
        public string CityName { get; set; }
        public string RankName { get; set; }
        public string Rank { get; set; }
        public string DivName { get; set; }
        public string MoveName { get; set; }
        public string HQName { get; set; }
        public string UnitName { get; set; }
        public string PStatusName { get; set; }
        public string PriorityName { get; set; }
        public Nullable<long> RankID { get; set; }
        public string Name { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<long> DivID { get; set; }
        public Nullable<long> HQID { get; set; }
        public string Authority { get; set; }
        public Nullable<long> MoveID { get; set; }
        public Nullable<long> PriorityID { get; set; }
        public Nullable<long> PriorityStatusID { get; set; }
        public Nullable<long> SeatNo { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public Nullable<System.DateTime> FlightDate { get; set; }
        public String CharterNo { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
