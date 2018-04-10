using System.Threading.Tasks;
using g4u.Core;

namespace g4u.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly g4uDbContext context;
        public UnitOfWork(g4uDbContext context)
        {
            this.context = context;
        }
        
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}