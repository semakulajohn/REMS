using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMS.Models
{
  public  class Transaction
    {
        public long TransactionId { get; set; }
        public double Amount { get; set; }
        public string ReceiptNumber { get; set; }
        public long HouseId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public long TenantId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string TenantName { get; set; }
        public string HouseNumber { get; set; }
    
    }
}
