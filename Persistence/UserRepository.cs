using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using g4u.Core;
using g4u.Core.Models;
using g4u.Extensions;
using Microsoft.EntityFrameworkCore;

namespace g4u.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly g4uDbContext context;
        public UserRepository(g4uDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<QueryResult<User>> GetUser(UserQuery queryObj)
        {
            var result = new QueryResult<User>();

            var query = context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObj.AuthSub))
            {
                query = query.Where(u => u.AuthSub == queryObj.AuthSub);
            }

            var columnsMap = new Dictionary<string, Expression<Func<User, object>>>()
            {
                ["email"] = u => u.Email,
                ["roles"] = u => u.Roles,
                ["authSub"] = u => u.AuthSub
            };

            query = query.ApplyOrdering(queryObj, columnsMap);
            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();
            
            return result;
        }
    }
}