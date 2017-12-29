using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using REMS.BAL.Interface;
using log4net;
using REMS.Models;

namespace REMS.Website.Controllers
{
    public class TenantApiController : ApiController
    {
          private ITenantService _tenantService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(TenantApiController));
            private string userId = string.Empty;

            public TenantApiController()
            {
            }

            public TenantApiController(ITenantService tenantService,IUserService userService)
            {
                this._tenantService = tenantService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetTenant")]
            public Tenant GetTenant(long tenantId)
            {
                return _tenantService.GetTenant(tenantId);
            }

            [HttpGet]
            [ActionName("GetAllTenants")]
            public IEnumerable<Tenant> GetAllTenants()
            {
                return _tenantService.GetAllTenants();
            }
            [HttpGet]
            [ActionName("GetAllTenantsForParticularHouse")]
            public IEnumerable<Tenant> GetAllTenantsForParticularHouse(long houseId)
            {
                return _tenantService.GetAllTenantsForParticularHouse(houseId);
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteTenant(long tenantId)
            {
                _tenantService.MarkAsDeleted(tenantId, userId);
            }
         
            [HttpGet]
            [ActionName("GetTransactionsForParticularTenant")]
            public IEnumerable<Transaction> GetTransactionsForParticularTenant(long tenantId)
            {
                return _tenantService.GetTransactionsForTenant(tenantId);
            }


            [HttpPost]
            [ActionName("Save")]
            public long Save(Tenant model)
            {

                var tenantId = _tenantService.SaveTenant(model, userId);
                return tenantId;
            }

    }
}
