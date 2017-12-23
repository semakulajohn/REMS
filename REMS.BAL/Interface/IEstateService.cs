using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.Models;

namespace REMS.BAL.Interface
{
  public  interface IEstateService
    {
        IEnumerable<Estate> GetAllEstates();
        Estate GetEstate(long estateId);
        long SaveEstate(Estate estate, string userId);
        void MarkAsDeleted(long estateId,string userId);
        int GetCountOfHousesForParticularEstate(long estateId);
        IEnumerable<House> GetHousesForAParticularEstate(long estateId);
       
    }
}
