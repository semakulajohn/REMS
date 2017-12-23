using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.Models;

namespace REMS.BAL.Interface
{
 public   interface IHouseService
    {
        IEnumerable<House> GetAllHouses();
        House GetHouse(long houseId);
        long SaveHouse(House house, string userId);
        void MarkAsDeleted(long houseId, string userId);
        IEnumerable<House> GetAllHousesForAParticularEstate(long estateId);
        
    }
}
