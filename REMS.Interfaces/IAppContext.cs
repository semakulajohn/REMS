using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.UnitOfWork;

namespace REMS.Interfaces
{
   public interface IAppContext
    {
       ICache Cache { get; set; }
        
    }
}
