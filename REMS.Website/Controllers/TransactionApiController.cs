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
    public class TransactionApiController : ApiController
    {
        private ITransactionService _transactionService;
        private IUserService _userService;
        ILog logger = log4net.LogManager.GetLogger(typeof(TransactionApiController));
        private string userId = string.Empty;

        public TransactionApiController()
        {
        }

        public TransactionApiController(ITransactionService transactionService, IUserService userService)
        {
            this._transactionService = transactionService;
            this._userService = userService;
            userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
        }

        [HttpGet]
        [ActionName("GetTransaction")]
        public Transaction GetTransaction(long transactionId)
        {
            return _transactionService.GetTransaction(transactionId);
        }

        [HttpGet]
        [ActionName("GetAllTransactions")]
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactionService.GetAllTransactions();
        }



        [HttpGet]
        [ActionName("Delete")]
        public void DeleteTransaction(long transactionId)
        {
            _transactionService.MarkAsDeleted(transactionId, userId);
        }



        [HttpPost]
        [ActionName("Save")]
        public long Save(Transaction model)
        {

            var transactionId = _transactionService.SaveTransaction(model, userId);
            return transactionId;
        }


    }
}
