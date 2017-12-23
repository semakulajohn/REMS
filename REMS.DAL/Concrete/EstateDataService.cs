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
  public  class EstateDataService : DataServiceBase,IEstateDataService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(EstateDataService));

       public EstateDataService(IUnitOfWork<REMSEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Estate> GetAllEstates()
        {
            return this.UnitOfWork.Get<Estate>().AsQueryable();
        }

        public Estate GetEstate(long estateId)
        {
            return this.UnitOfWork.Get<Estate>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.EstateId == estateId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new estate or updates an already existing estate.
        /// </summary>
        /// <param name="estate">Estate to be saved or updated.</param>
        /// <param name="estateId">EsateId of the estate creating or updating</param>
        /// <returns>estateId</returns>
        public long SaveEstate(EstateDTO estateDTO, string userId)
        {
            long estateId = 0;

            if (estateDTO.EstateId == 0)
            {
           
                var estate = new Estate()
                {
                    Name = estateDTO.Name,
                    NumberOfHouses = estateDTO.NumberOfHouses,
                    Description = estateDTO.Description,
                    Location = estateDTO.Location,
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Estate>().AddNew(estate);
                this.UnitOfWork.SaveChanges();
                estateId = estate.EstateId;
                return estateId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Estate>().AsQueryable()
                    .FirstOrDefault(e => e.EstateId == estateDTO.EstateId);
                if (result != null)
                {
                    result.Name = estateDTO.Name;
                    result.UpdatedBy = userId;
                    result.NumberOfHouses = estateDTO.NumberOfHouses;
                    result.Description = estateDTO.Description;
                    result.Location = estateDTO.Location;
                    result.Timestamp = DateTime.Now;
                    result.Deleted = estateDTO.Deleted;
                    result.DeletedBy = estateDTO.DeletedBy;
                    result.DeletedOn = estateDTO.DeletedOn;
 
                    this.UnitOfWork.Get<Estate>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return estateDTO.EstateId;
            }
            return estateId;
        }

        public bool MarkAsDeleted(long Id,string userId)
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

      
     
    }
}
