using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.Models;

namespace REMS.BAL.Interface
{
  public  interface ITenantService
    {
      IEnumerable<Tenant> GetAllTenants();
      Tenant GetTenant(long tenantId);
      long SaveTenant(Tenant tenant, string userId);
      void MarkAsDeleted(long tenantId, string userId);
      IEnumerable<Transaction> GetTransactionsForTenant(long tenantId);
      IEnumerable<Tenant> GetAllTenantsForParticularHouse(long houseId);
        
        
    
    }
}
