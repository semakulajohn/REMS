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
  public  class TransactionDataService :DataServiceBase,ITransactionDataService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(TransactionDataService));

       public TransactionDataService(IUnitOfWork<REMSEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return this.UnitOfWork.Get<Transaction>().AsQueryable();
        }

        public Transaction GetTransaction(long transactionId)
        {
            return this.UnitOfWork.Get<Transaction>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.TransactionId == transactionId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new tenant transaction or updates an already existing transaction.
        /// </summary>
        /// <param name="transaction">Transaction to be saved or updated.</param>
        /// <param name="transactionId">TransactionId of the transaction creating or updating</param>
        /// <returns>transactionId</returns>
        public long SaveTransaction(TransactionDTO transactionDTO, string userId)
        {
            long transactionId = 0;

            if (transactionDTO.TransactionId == 0)
            {

                var transaction = new Transaction()
                {
                    Amount = transactionDTO.Amount,
                    HouseId = transactionDTO.HouseId,
                    ReceiptNumber = transactionDTO.ReceiptNumber,
                    TenantId = transactionDTO.TenantId,
                    ToDate = transactionDTO.ToDate,
                    FromDate = transactionDTO.FromDate,
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,


                };

                this.UnitOfWork.Get<Transaction>().AddNew(transaction);
                this.UnitOfWork.SaveChanges();
                transactionId = transaction.TransactionId;
                return transactionId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Transaction>().AsQueryable()
                    .FirstOrDefault(e => e.TransactionId == transactionDTO.TransactionId);
                if (result != null)
                {
                    result.Amount = transactionDTO.Amount;
                    result.UpdatedBy = userId;
                    result.HouseId = transactionDTO.HouseId;
                    result.ReceiptNumber = transactionDTO.ReceiptNumber;
                    result.TenantId = transactionDTO.TenantId;
                    result.FromDate = transactionDTO.FromDate;
                    result.ToDate = transactionDTO.ToDate;
                    result.Timestamp = DateTime.Now;
                    result.Deleted = transactionDTO.Deleted;
                    result.DeletedBy = transactionDTO.DeletedBy;
                    result.DeletedOn = transactionDTO.DeletedOn;

                    this.UnitOfWork.Get<Transaction>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return transactionDTO.TransactionId;
            }
            return transactionId;
        }

        public bool MarkAsDeleted(long Id, string userId)
        {
            bool IsDeleted = false;
            if (Id != null)
            {
                var transaction = (from n in this.UnitOfWork.Get<Transaction>().AsQueryable()
                              where n.TransactionId == Id
                              select n
                           ).FirstOrDefault();
                if (transaction != null)
                {
                    transaction.DeletedOn = DateTime.Now;
                    transaction.Deleted = true;
                    transaction.DeletedBy = userId;
                    this.UnitOfWork.Get<Transaction>().Update(transaction);
                    this.UnitOfWork.SaveChanges();
                }


                IsDeleted = true;
            }
            else
            {
                IsDeleted = false;
            }

            return IsDeleted;

        }

        public IEnumerable<Transaction> GetAllTransactionsForParticularTenant(long tenantId)
        {
            
            var transactions = this.UnitOfWork.Get<Transaction>().AsQueryable()
                                .Where(h => h.TenantId == tenantId);
            
            return transactions;
            
        }

        public long GetHouseIdForParticularTenant(long tenantId)
        {
            long houseId = 0;
           var result = this.UnitOfWork.Get<Tenant>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.TenantId == tenantId &&
                    c.Deleted == false
                );
           if (result != null)
           {
               houseId = result.HouseId;
               return houseId;
           }
           return houseId;
        }

        public long GetEstateIdForParticularHouse(long houseId)
        {
            long estateId = 0;
            var result = this.UnitOfWork.Get<House>().AsQueryable()
                  .FirstOrDefault(c =>
                     c.HouseId == houseId &&
                     c.Deleted == false
                 );
            if (result != null)
            {
                estateId = result.EstateId;
                return estateId;
            }
            return estateId;
        }
      
    }
}
