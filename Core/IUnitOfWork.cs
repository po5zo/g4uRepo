using System.Threading.Tasks;

namespace g4u.Core
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}