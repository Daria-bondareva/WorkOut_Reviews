using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReviewsSystem.DAL.Configurations;
using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Seeding;

namespace ReviewsSystem.DAL
{
    public class ReviewsSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WorkOut> WorkOuts { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ReviewsSystemContext(DbContextOptions<ReviewsSystemContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new WorkOutConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public void SeedData(int userCount, int workoutCount, int reviewCount)
        {
            var users = UserSeeder.GenerateUsers(userCount);
            var workouts = WorkOutSeeder.GenerateWorkOuts(workoutCount);
            var reviews = ReviewSeeder.GenerateReviews(reviewCount, users, workouts);

            this.Users.AddRange(users);
            this.WorkOuts.AddRange(workouts);
            this.Reviews.AddRange(reviews);

            this.SaveChanges();
        }
    }
}
