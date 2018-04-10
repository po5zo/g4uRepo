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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly g4uDbContext context;
        public ProductRepository(g4uDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<QueryResult<Product>> GetProductsAsync(ProductQuery queryObj)
        {
            var result = new QueryResult<Product>();

            var query = context.Products.AsQueryable();
            var wishlist = context.Wishlists.AsQueryable();

            if (queryObj.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == queryObj.CategoryId.Value);

            if (queryObj.PlatformId.HasValue)
                query = query.Where(p => p.PlatformId == queryObj.PlatformId);
            
            if (queryObj.PlatformId.HasValue && queryObj.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == queryObj.CategoryId.Value && p.PlatformId == queryObj.PlatformId.Value);

            if (!string.IsNullOrWhiteSpace(queryObj.AuthSub) && !queryObj.IsForWishlist)
                query = query.Where(p => p.AuthSub == queryObj.AuthSub);

            if (queryObj.IsForWishlist && !string.IsNullOrWhiteSpace(queryObj.AuthSub))
                query = query.Where(p => wishlist.Where(w => w.AuthSub == queryObj.AuthSub).Select(x => x.ProductId).Contains(p.Id));

            var columnsMap = new Dictionary<string, Expression<Func<Product, object>>>()
            {
                ["name"] = p => p.Name,
                ["sellOrBuy"] = p => p.SellOrBuy,
                ["ageLimit"] = p => p.AgeLimit,
                ["description"] = p => p.Description,
                ["releaseDate"] = p => p.ReleaseDate,
                ["price"] = p => p.Price,
                ["authSub"] = p => p.AuthSub
            };

            query = query.ApplyOrdering(queryObj, columnsMap);
            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();
            
            return result;
        }
    }
}