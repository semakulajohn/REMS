using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.DTO;
using REMS.BAL.Interface;
using REMS.DAL.Interface;
using REMS.Models;
using REMS.Helpers;
using log4net;

namespace REMS.BAL.Concrete
{
   public class TenantService : ITenantService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(TenantService));
        private ITenantDataService _dataService;
        private IUserService _userService;
        private ITransactionService _transactionService;
        

        public TenantService(ITenantDataService dataService,IUserService userService,ITransactionService transactionService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionService = transactionService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Tenant GetTenant(long tenantId)
        {
            var result = this._dataService.GetTenant(tenantId);
            return MapEFToModel(result);
        }

        public IEnumerable<Transaction> GetTransactionsForTenant(long tenantId)
        {
            var tenantTransactions = this._transactionService.GetAllTransactionsForParticularTenant(tenantId);
            return tenantTransactions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tenant> GetAllTenants()
        {
            var results = this._dataService.GetAllTenants();
            return MapEFToModel(results);
        }


        public IEnumerable<Tenant> GetAllTenantsForParticularHouse(long houseId)
        {
            var results = this._dataService.GetAllTenantsForParticularHouse(houseId);
            return MapEFToModel(results);
        }

        public long SaveTenant(Tenant tenant, string userId)
        {
            var tenantDTO = new DTO.TenantDTO()
            {
                TenantId = tenant.TenantId,
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                HouseId = tenant.HouseId,
                Email = tenant.Email,
                MobileNumber = tenant.MobileNumber,

            };

           var tenantId = this._dataService.SaveTenant(tenantDTO, userId);

           return tenantId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long tenantId, string userId)
        {
            _dataService.MarkAsDeleted(tenantId, userId);
        }


        #region Mapping Methods

        private IEnumerable<Tenant> MapEFToModel(IEnumerable<EF.Models.Tenant> data)
        {
            var list = new List<Tenant>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps tenant EF object to tenant Model Object and
        /// returns the tenant model object.
        /// </summary>
        /// <param name="result">EF tenant object to be mapped.</param>
        /// <returns>tenant Model Object.</returns>
        public Tenant MapEFToModel(EF.Models.Tenant data)
        {

            var tenant = new Tenant()
            {
                TenantId = data.TenantId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                MobileNumber = data.MobileNumber,
                HouseId = data.HouseId,
                CreatedOn = data.CreatedOn,
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),


            };
            return tenant;
        }

        #endregion

    }
}
