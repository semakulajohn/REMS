using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.DTO;
using REMS.EF.Models;

namespace REMS.DAL.Interface
{
  public  interface IUserDataService
    {
        AspNetUser GetLoggedInUser(string userId);
        bool UserExists(string finder);
        AspNetUser SaveUser(AspNetUserDTO user, string userId);
        bool MarkAsDeleted(string Id);
        AspNetUser GetAspNetUser(string Id);
        IEnumerable<AspNetRole> GetAllRoles();
        AspNetRole GetAspNetRole(string roleId);
        IEnumerable<AspNetUser> GetAspNetUsers();
        void CreateAspNetUserRolesRecord(string userId, string roleId);
       
    }
}
