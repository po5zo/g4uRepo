using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using g4u.Core;
using g4u.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace g4u.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly g4uDbContext context;
        private readonly DbSet<TEntity> _entities;
        public Repository(g4uDbContext context)
        {
            this.context = context;
            _entities = context.Set<TEntity>();
        }
        public void Add(TEntity newentity)
        {
            _entities.Add(newentity);
        }

        public async Task<TEntity> Get(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }
    }
}