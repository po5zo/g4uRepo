using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using g4u.Core.Models;

namespace g4u.Core
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
         Task<TEntity> Get(int id);
         Task<IEnumerable<TEntity>> GetAll();
         void Add(TEntity newentity);
         void Remove(TEntity entity);
         Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}