using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ReviewsSystem.DAL
{
    public class ReviewsSystemContextFactory : IDesignTimeDbContextFactory<ReviewsSystemContext>
    {
        public ReviewsSystemContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ReviewsSystem.WebAPI"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReviewsSystemContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new(optionsBuilder.Options);
        }
    }
}
