using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.Models;
using REMS.DTO;

namespace REMS.DAL.Interface
{
  public  interface ITenantDataService
    {
        IEnumerable<Tenant> GetAllTenants();
        Tenant GetTenant(long tenantId);
        long SaveTenant(TenantDTO tenant, string userId);
        bool MarkAsDeleted(long tenantId, string userId);
    }
}
