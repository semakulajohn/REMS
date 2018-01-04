using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.Models;
using REMS.DTO;

namespace REMS.DAL.Interface
{
  public  interface IEstateDataService
    {
        IEnumerable<Estate> GetAllEstates();
        Estate GetEstate(long estateId);
        long SaveEstate(EstateDTO estate, string userId);
        void MarkAsDeleted(long estateId,string userId);
        
    }
}
