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
    public class HouseApiController : ApiController
    {
          private IHouseService _houseService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(HouseApiController));
            private string userId = string.Empty;

            public HouseApiController()
            {
            }

            public HouseApiController(IHouseService houseService,IUserService userService)
            {
                this._houseService = houseService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetHouse")]
            public House GetHouse(long houseId)
            {
                return _houseService.GetHouse(houseId);
            }

            [HttpGet]
            [ActionName("GetAllHouses")]
            public IEnumerable<House> GetAllHouses()
            {
                return _houseService.GetAllHouses();
            }

          

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteHouse(long houseId)
            {
                _houseService.MarkAsDeleted(houseId, userId);
            }



            [HttpPost]
            [ActionName("Save")]
            public long Save(House model)
            {

                var houseId = _houseService.SaveHouse(model, userId);
                return houseId;
            }

    }
}
