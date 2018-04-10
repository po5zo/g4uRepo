using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using g4u.Core.Models;

namespace g4u.Core
{
    public interface IWishlistRepository : IRepository<Wishlist>
    { 
        Task<QueryResult<Wishlist>> GetWishlistAsync(WishlistQuery filter);
    }
}