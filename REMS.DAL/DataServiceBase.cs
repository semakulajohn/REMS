using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.UnitOfWork;
using REMS.EF.Context;
using REMS.EF.Models;

namespace REMS.DAL
{
  public  class DataServiceBase
    {
        
        private IUnitOfWork<REMSEntities> _unitOfwork;

        protected IUnitOfWork<REMSEntities> UnitOfWork { get { return this._unitOfwork; } }

        public DataServiceBase(IUnitOfWork<REMSEntities> unitOfWork)
        {
            this._unitOfwork = unitOfWork;
        }
    }
}
