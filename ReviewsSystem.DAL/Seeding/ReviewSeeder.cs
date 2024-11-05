using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Interfaces;

namespace ReviewsSystem.DAL.Seeding
{
    public class ReviewSeeder
    {
        public static List<Review> GenerateReviews(int count, List<User> users, List<WorkOut> workouts)
        {
            var reviewFaker = new Faker<Review>()
                .RuleFor(r => r.Id, f => f.IndexFaker + 1)
                .RuleFor(r => r.Comment, f => f.Lorem.Sentence())
                .RuleFor(r => r.DatePosted, f => f.Date.Past(1))
                .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
                .RuleFor(r => r.UserId, f => f.PickRandom(users).Id)
                .RuleFor(r => r.WorkOutId, f => f.PickRandom(workouts).Id);

            return reviewFaker.Generate(count);
        }
    }
}
