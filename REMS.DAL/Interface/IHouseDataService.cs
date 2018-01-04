using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.Models;
using REMS.DTO;

namespace REMS.DAL.Interface
{
  public  interface IHouseDataService
    {
        IEnumerable<House> GetAllHouses();
        House GetHouse(long houseId);
        long SaveHouse(HouseDTO house, string userId);
        void MarkAsDeleted(long houseId, string userId);
        int GetCountOfHousesForParticularEstate(long estateId);
        IEnumerable<House> GetAllHousesForAParticularEstate(long estateId);
    }
}
