using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.Models;
using REMS.DAL.Concrete;
using REMS.DAL.Interface;
using REMS.EF.UnitOfWork;
using REMS.DTO;
using log4net;

namespace REMS.DAL.Concrete
{
 public   class HouseDataService : DataServiceBase,IHouseDataService
    {
     ILog logger = log4net.LogManager.GetLogger(typeof(HouseDataService));

       public HouseDataService(IUnitOfWork<REMSEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<House> GetAllHouses()
        {
            return this.UnitOfWork.Get<House>().AsQueryable();
        }

        public House GetHouse(long houseId)
        {
            return this.UnitOfWork.Get<House>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.HouseId == houseId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new house or updates an already existing house.
        /// </summary>
        /// <param name="house">House to be saved or updated.</param>
        /// <param name="houseId">HouseId of the house creating or updating</param>
        /// <returns>houseId</returns>
        public long SaveHouse(HouseDTO houseDTO, string userId)
        {
            long houseId = 0;

            if (houseDTO.HouseId == 0)
            {

                var house = new House()
                {
                    Number = houseDTO.Number,
                    Amount = houseDTO.Amount,
                    EstateId = houseDTO.EstateId,
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,

        
                };

                this.UnitOfWork.Get<House>().AddNew(house);
                this.UnitOfWork.SaveChanges();
                houseId = house.HouseId;
                return houseId;
            }

            else
            {
                var result = this.UnitOfWork.Get<House>().AsQueryable()
                    .FirstOrDefault(e => e.HouseId == houseDTO.HouseId);
                if (result != null)
                {
                    result.Number = houseDTO.Number;
                    result.UpdatedBy = userId;
                    result.Amount = houseDTO.Amount;
                    result.EstateId = houseDTO.EstateId;
                    result.Timestamp = DateTime.Now;
                    result.Deleted = houseDTO.Deleted;
                    result.DeletedBy = houseDTO.DeletedBy;
                    result.DeletedOn = houseDTO.DeletedOn;

                    this.UnitOfWork.Get<House>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return houseDTO.HouseId;
            }
            return houseId;
        }

        public bool MarkAsDeleted(long Id, string userId)
        {
            bool IsDeleted = false;
            if (Id != null)
            {
                var estate = (from n in this.UnitOfWork.Get<Estate>().AsQueryable()
                              where n.EstateId == Id
                              select n
                           ).FirstOrDefault();
                if (estate != null)
                {
                    estate.DeletedOn = DateTime.Now;
                    estate.Deleted = true;
                    estate.DeletedBy = userId;
                    this.UnitOfWork.Get<Estate>().Update(estate);
                    this.UnitOfWork.SaveChanges();
                }


                IsDeleted = true;
            }
            else
            {
                IsDeleted = false;
            }

            return IsDeleted;

        }

        public int GetCountOfHousesForParticularEstate(long estateId)
        {
            int houseCount = 0;
            var estateHouses = this.UnitOfWork.Get<House>().AsQueryable()
                                .Where (h => h.EstateId == estateId);
            if (estateHouses != null)
            {
                 houseCount = estateHouses.Count();
                 return houseCount;
            }
            else
            {
                return houseCount;
            }
            
        }

        public IEnumerable<House> GetAllHousesForAParticularEstate(long estateId)
        {
            
            var estateHouses = this.UnitOfWork.Get<House>().AsQueryable()
                                .Where(h => h.EstateId == estateId);
            return estateHouses;

        }

    }
}
