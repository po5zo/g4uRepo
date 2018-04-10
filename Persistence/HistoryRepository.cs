using g4u.Core;
using g4u.Core.Models;

namespace g4u.Persistence
{
    public class HistoryRepository : Repository<History>, IHistoryRepository
    {
        private readonly g4uDbContext context;
        public HistoryRepository(g4uDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}