using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMS.DTO
{
  public  class HouseDTO
    {
        public long HouseId { get; set; }
        public string Number { get; set; }
        public double Amount { get; set; }
        public long EstateId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    }
}
