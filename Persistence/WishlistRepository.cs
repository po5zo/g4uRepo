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
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        private readonly g4uDbContext context;
        public WishlistRepository(g4uDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<QueryResult<Wishlist>> GetWishlistAsync(WishlistQuery filter)
        {
            var result = new QueryResult<Wishlist>();

            var query = context.Wishlists.AsQueryable();

            if (filter.ProductId.HasValue)
                query = query.Where(p => p.ProductId == filter.ProductId.Value);

            if(!string.IsNullOrWhiteSpace(filter.AuthSub))
                query = query.Where(p => p.AuthSub == filter.AuthSub);

            if (!string.IsNullOrWhiteSpace(filter.AuthSub) && filter.ProductId.HasValue)
                query = query.Where(w => w.AuthSub == filter.AuthSub && w.ProductId == filter.ProductId);
            
            var columnsMap = new Dictionary<string, Expression<Func<Wishlist, object>>>()
            {
                ["productId"] = w => w.ProductId,
                ["authSub"] = w => w.AuthSub
            };

            query = query.ApplyOrdering(filter, columnsMap);
            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(filter);
            result.Items = await query.ToListAsync();

            return result;
        }
    }
}