using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.Models;



namespace REMS.BAL.Interface
{
  public  interface IUserService
    {
        AspNetUser GetLoggedInUser(string userId);
        bool UserExists(string finder);
        AspNetUser SaveUser(AspNetUser user, string userId);
        string GetUserFullName(EF.Models.AspNetUser aspNetUser);
        //void SendWelcomeEmail(string emailAddress, string firstName);
        bool MarkAsDeleted(string Id);
        AspNetUser GetAspNetUser(string Id);
        IEnumerable<AspNetRole> GetAllRoles();
        IEnumerable<AspNetUser> GetAllAspNetUsers();
        AspNetRole GetAspNetRole(string roleId);
        IEnumerable<AspNetUser> GetAllManagers();
        IEnumerable<AspNetUser> GetAllAdmins();
    }
}
