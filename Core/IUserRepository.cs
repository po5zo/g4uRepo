using System.Threading.Tasks;
using g4u.Core.Models;

namespace g4u.Core
{
    public interface IUserRepository : IRepository<User>
    {
         Task<QueryResult<User>> GetUser(UserQuery filter);
    }
}