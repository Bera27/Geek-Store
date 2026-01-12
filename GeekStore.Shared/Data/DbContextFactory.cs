using Microsoft.EntityFrameworkCore.Design;

namespace GeekStore.Shared.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<GeekStoreDataContext>
    {
        public GeekStoreDataContext CreateDbContext(string[] args)
        {
            var DbPath = Path.Combine(Directory.GetCurrentDirectory(), "migrator.db3");
            return new GeekStoreDataContext(DbPath);
        }
    }
}
