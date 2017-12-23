using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMS.DTO
{
  public  class EstateDTO
    {
        public long EstateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfHouses { get; set; }
        public string Location { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    }
}
