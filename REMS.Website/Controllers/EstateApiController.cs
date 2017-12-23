using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using REMS.BAL.Interface;
using log4net;
using REMS.Models;

namespace REMS.Website.Controllers
{
    public class EstateApiController : ApiController
    {
            private IEstateService _estateService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(EstateApiController));
            private string userId = string.Empty;

            public EstateApiController()
            {
            }

            public EstateApiController(IEstateService estateService,IUserService userService)
            {
                this._estateService = estateService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetEstate")]
            public Estate GetEstate(long estateId)
            {
                return _estateService.GetEstate(estateId);
            }

            [HttpGet]
            [ActionName("GetAllEstates")]
            public IEnumerable<Estate> GetAllEstates()
            {
                return _estateService.GetAllEstates();
            }

        [HttpGet]
        [ActionName("GetAllEstateHouses")]
            public IEnumerable<House> GetAllEstateHouses(long estateId)
            {
                return _estateService.GetHousesForAParticularEstate(estateId);
            }
          

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteEstate(long estateId)
            {
                _estateService.MarkAsDeleted(estateId, userId);
            }

           [HttpGet]
           [ActionName("GetEstateHouseCount")]
            public int GetEstateHouseCount(long estateId)
            {
                return _estateService.GetCountOfHousesForParticularEstate(estateId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Estate model)
            {

                var estateId = _estateService.SaveEstate(model, userId);
                return estateId;
            }

            
    }
}
