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
  public  class EstateService :IEstateService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(EstateService));
        private IEstateDataService _dataService;
        private IUserService _userService;
        private IHouseDataService _houseDataService;
        private IHouseService _houseService;
        

        public EstateService(IEstateDataService dataService,IUserService userService,IHouseDataService houseDataService,IHouseService houseService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._houseDataService = houseDataService;
            this._houseService = houseService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estateId"></param>
        /// <returns></returns>
        public Estate GetEstate(long estateId)
        {
            var result = this._dataService.GetEstate(estateId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Estate> GetAllEstates()
        {
            var results = this._dataService.GetAllEstates();
            return MapEFToModel(results);
        } 

       
        public long SaveEstate(Estate estate, string userId)
        {
            var estateDTO = new DTO.EstateDTO()
            {
                EstateId = estate.EstateId,
                Name = estate.Name,
                Description = estate.Description,
                NumberOfHouses = estate.NumberOfHouses,
                Location = estate.Location,    

            };

           var estateId = this._dataService.SaveEstate(estateDTO, userId);

           return estateId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="estateId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long estateId, string userId)
        {
            _dataService.MarkAsDeleted(estateId, userId);
        }

        public int GetCountOfHousesForParticularEstate(long estateId)
        {
            int houseCount = _houseDataService.GetCountOfHousesForParticularEstate(estateId);
            return houseCount;
        }

        public IEnumerable<House> GetHousesForAParticularEstate(long estateId)
        {
            var estateHouses = this._houseService.GetAllHousesForAParticularEstate(estateId);
            return estateHouses;
        }

        #region Mapping Methods

        private IEnumerable<Estate> MapEFToModel(IEnumerable<EF.Models.Estate> data)
        {
            var list = new List<Estate>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps estate EF object to estate Model Object and
        /// returns the estate model object.
        /// </summary>
        /// <param name="result">EF estate object to be mapped.</param>
        /// <returns>estate Model Object.</returns>
        public Estate MapEFToModel(EF.Models.Estate data)
        {
          
            var estate = new Estate()
            {
                EstateId = data.EstateId,
                Name = data.Name,
                Description = data.Description,
                NumberOfHouses = data.NumberOfHouses,
                Location = data.Location,
                CreatedOn = data.CreatedOn,
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return estate;
        }



       #endregion

     }
}
