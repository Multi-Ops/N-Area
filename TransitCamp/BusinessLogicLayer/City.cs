using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class City
    {
        public long ID { get; set; }
        public long OutLogicID { get; set; }
        public long MedicalStatusID { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string MedicalStatusName { get; set; }
        public string OutLogicName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
