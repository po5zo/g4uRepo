using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace g4u.Persistence
{
    public class g4uDbContextFactory : IDesignTimeDbContextFactory<g4uDbContext>
    {
        public g4uDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<g4uDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=Database3;Integrated Security=True");

            return new g4uDbContext(optionsBuilder.Options);
        }
    }
}