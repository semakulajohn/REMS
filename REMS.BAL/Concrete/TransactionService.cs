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
  public  class TransactionService : ITransactionService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(TransactionService));
        private ITransactionDataService _dataService;
        private IUserService _userService;
        

        public TransactionService(ITransactionDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public Transaction GetTransaction(long transactionId)
        {
            var result = this._dataService.GetTransaction(transactionId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Transaction> GetAllTransactions()
        {
            var results = this._dataService.GetAllTransactions();
            return MapEFToModel(results);
        } 

       
        public long SaveTransaction(Transaction transaction, string userId)
        {
            var houseId = this._dataService.GetHouseIdForParticularTenant(transaction.TenantId);
            //if (houseId > 0)
            //{
            //    var estateId = this._dataService.GetEstateIdForParticularHouse(houseId);
            //}
            var transactionDTO = new DTO.TransactionDTO()
            {
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                TenantId= transaction.TenantId,
                HouseId = houseId,
                ReceiptNumber = transaction.ReceiptNumber, 
                FromDate = transaction.FromDate,
                ToDate = transaction.ToDate,

            };

           var transactionId = this._dataService.SaveTransaction(transactionDTO, userId);

           return transactionId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long transactionId, string userId)
        {
            _dataService.MarkAsDeleted(transactionId, userId);
        }
        public IEnumerable<Transaction> GetAllTransactionsForParticularTenant(long tenantId)
        {
            var tenantTransactions = this._dataService.GetAllTransactionsForParticularTenant(tenantId);
            return MapEFToModel(tenantTransactions);
        }


        #region Mapping Methods

        private IEnumerable<Transaction> MapEFToModel(IEnumerable<EF.Models.Transaction> data)
        {
            var list = new List<Transaction>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Transaction EF object to Transaction Model Object and
        /// returns the Transaction model object.
        /// </summary>
        /// <param name="result">EF Transaction object to be mapped.</param>
        /// <returns>Transaction Model Object.</returns>
        public Transaction MapEFToModel(EF.Models.Transaction data)
        {
            var tenantName = string.Empty;
            var  houseNumber= string.Empty;

            if (data.TransactionId != 0)
            {

                if (data.Tenant != null)
                {
                    tenantName = data.Tenant.FirstName + " " + data.Tenant.LastName;
                }
                         

            }
            if (data.TransactionId != 0)
            {
                if (data.House != null)
                {
                    houseNumber = data.House.Number;
                }
            }
            var transaction = new Transaction()
            {
                TransactionId = data.TransactionId,
                Amount = data.Amount,
                ReceiptNumber = data.ReceiptNumber,
                HouseId = data.HouseId,
                TenantId = data.TenantId,
                ToDate =data.ToDate,
                FromDate = Convert.ToDateTime(data.FromDate),
                CreatedOn = Convert.ToDateTime(data.CreatedOn),
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
                TenantName = tenantName,
                HouseNumber = houseNumber,
               

            };
            return transaction;
        }
        #endregion
    }
}
