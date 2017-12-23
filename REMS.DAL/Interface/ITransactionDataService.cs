using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.Models;
using REMS.DTO;

namespace REMS.DAL.Interface
{
  public  interface ITransactionDataService
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransaction(long transactionId);
        long SaveTransaction(TransactionDTO transaction, string userId);
        bool MarkAsDeleted(long transactionId, string userId);
        IEnumerable<Transaction> GetAllTransactionsForParticularTenant(long tenantId);
        long GetHouseIdForParticularTenant(long tenantId);
        long GetEstateIdForParticularHouse(long houseId);
        
    }
}
