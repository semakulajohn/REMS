using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.Models;

namespace REMS.BAL.Interface
{
  public  interface ITransactionService
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransaction(long transactionId);
        long SaveTransaction(Transaction transaction, string userId);
        void MarkAsDeleted(long transactionId, string userId);
        IEnumerable<Transaction> GetAllTransactionsForParticularTenant(long tenantId);
        
    }
}
