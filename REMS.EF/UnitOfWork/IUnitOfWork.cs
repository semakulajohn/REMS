using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REMS.EF.Context;
using REMS.EF.Repository;

namespace REMS.EF.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : IDbContext
    {
        IRepository<TEntity> Get<TEntity>() where TEntity : class;
        int SaveChanges();
    }

}
