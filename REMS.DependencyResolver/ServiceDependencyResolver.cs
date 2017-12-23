using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using REMS.BAL.Interface;
using REMS.BAL.Concrete;
using REMS.DAL.Interface;
using REMS.DAL.Concrete;

namespace REMS.DependencyResolver
{
  public  class ServiceDependencyResolver : NinjectModule
    {
      public override void Load()
      {
          //BAL
          Bind(typeof(IUserService)).To(typeof(UserService));
          Bind(typeof(IEstateService)).To(typeof(EstateService));
          Bind(typeof(IHouseService)).To(typeof(HouseService));
          Bind(typeof(ITransactionService)).To(typeof(TransactionService));
          Bind(typeof(ITenantService)).To(typeof(TenantService));


          //DAL
          Bind(typeof(IUserDataService)).To(typeof(UserDataService));
           Bind(typeof(IEstateDataService)).To(typeof(EstateDataService));
           Bind(typeof(IHouseDataService)).To(typeof(HouseDataService));
           Bind(typeof(ITransactionDataService)).To(typeof(TransactionDataService));
           Bind(typeof(ITenantDataService)).To(typeof(TenantDataService));
      }  
    }
}
