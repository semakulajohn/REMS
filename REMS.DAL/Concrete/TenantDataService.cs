using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.Models;
using REMS.DAL.Concrete;
using REMS.DAL.Interface;
using REMS.EF.UnitOfWork;
using REMS.DTO;
using log4net;

namespace REMS.DAL.Concrete
{
  public  class TenantDataService :DataServiceBase,ITenantDataService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(TenantDataService));

       public TenantDataService(IUnitOfWork<REMSEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Tenant> GetAllTenants()
        {
            return this.UnitOfWork.Get<Tenant>().AsQueryable();
        }

        public Tenant GetTenant(long tenantId)
        {
            return this.UnitOfWork.Get<Tenant>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.TenantId == tenantId &&
                    c.Deleted == false
                );
        }


        /// <summary>
        /// Saves a new tenant or updates an already existing tenant.
        /// </summary>
        /// <param name="tenant">Tenant to be saved or updated.</param>
        /// <param name="tenantId">TenantId of the tenant creating or updating</param>
        /// <returns>tenantId</returns>
        public long SaveTenant(TenantDTO tenantDTO, string userId)
        {
            long tenantId = 0;

            if (tenantDTO.TenantId == 0)
            {

                var tenant = new Tenant()
                {
                    FirstName =tenantDTO.FirstName,
                    LastName = tenantDTO.LastName,
                    Email = tenantDTO.Email,
                    MobileNumber = tenantDTO.MobileNumber,
                    HouseId = tenantDTO.HouseId,
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,


                };

                this.UnitOfWork.Get<Tenant>().AddNew(tenant);
                this.UnitOfWork.SaveChanges();
                tenantId = tenant.TenantId;
                return tenantId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Tenant>().AsQueryable()
                    .FirstOrDefault(e => e.TenantId == tenantDTO.TenantId);
                if (result != null)
                {
                    result.FirstName =tenantDTO.FirstName;
                    result.LastName = tenantDTO.LastName;
                    result.Email = tenantDTO.Email;
                    result.MobileNumber = tenantDTO.MobileNumber;
                    result.HouseId = tenantDTO.HouseId;
                    result.Timestamp = DateTime.Now;
                    result.Deleted = tenantDTO.Deleted;
                    result.DeletedBy = tenantDTO.DeletedBy;
                    result.DeletedOn = tenantDTO.DeletedOn;

                    this.UnitOfWork.Get<Tenant>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return tenantDTO.TenantId;
            }
            return tenantId;
        }

        public void MarkAsDeleted(long tenantId, string userId)
        {

            using (var dbContext = new REMSEntities())
            {
                dbContext.Mark_Tenant_And_Related_DataAs_Deleted(tenantId, userId);
            }      

        }

        public IEnumerable<Tenant> GetAllTenantsForParticularHouse(long houseId)
        {
            var houseTenants = this.UnitOfWork.Get<Tenant>().AsQueryable()
                .Where(h => h.HouseId == houseId);
            return houseTenants; ;
        }
    }
}
