using System.Threading.Tasks;
using g4u.Core.Models;

namespace g4u.Core
{
    public interface IProductRepository : IRepository<Product>
    {
         Task<QueryResult<Product>> GetProductsAsync(ProductQuery filter);
    }
}