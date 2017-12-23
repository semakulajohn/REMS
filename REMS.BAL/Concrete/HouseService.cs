using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.DTO;
using REMS.BAL.Interface;
using REMS.DAL.Interface;
using REMS.Models;
using REMS.Helpers;
using log4net;

namespace REMS.BAL.Concrete
{
 public   class HouseService : IHouseService
    {
     ILog logger = log4net.LogManager.GetLogger(typeof(HouseService));
        private IHouseDataService _dataService;
        private IUserService _userService;
        

        public HouseService(IHouseDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public House GetHouse(long houseId)
        {
            var result = this._dataService.GetHouse(houseId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<House> GetAllHouses()
        {
            var results = this._dataService.GetAllHouses();
            return MapEFToModel(results);
        }


        public IEnumerable<House> GetAllHousesForAParticularEstate(long estateId)
        {
            var results = this._dataService.GetAllHousesForAParticularEstate(estateId);
            return MapEFToModel(results);
        }
       
        public long SaveHouse(House house, string userId)
        {
            var houseDTO = new DTO.HouseDTO()
            {
                HouseId = house.HouseId,
                Number = house.Number,
                Amount = house.Amount,
                EstateId = house.EstateId,    

            };

           var houseId = this._dataService.SaveHouse(houseDTO, userId);

           return houseId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long houseId, string userId)
        {
            _dataService.MarkAsDeleted(houseId, userId);
        }

       
        #region Mapping Methods

        private IEnumerable<House> MapEFToModel(IEnumerable<EF.Models.House> data)
        {
            var list = new List<House>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps house EF object to house Model Object and
        /// returns the house model object.
        /// </summary>
        /// <param name="result">EF house object to be mapped.</param>
        /// <returns>house Model Object.</returns>
        public House MapEFToModel(EF.Models.House data)
        {
            var estateName = string.Empty;
            if (data.EstateId != 0)
            {
                estateName = data.Estate.Name;
            }

            var house = new House()
            {
                HouseId = data.HouseId,
                EstateId = data.EstateId,
                Number = data.Number,
                Amount = data.Amount,
                CreatedOn = data.CreatedOn,
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
                EstateName = estateName,


            };
            return house;
        }
       

        #endregion
    }
}
