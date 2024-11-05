using Bogus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Interfaces;


namespace ReviewsSystem.DAL.Seeding
{
    public  class WorkOutSeeder
    {
        public static List<WorkOut> GenerateWorkOuts(int count)
        {
            var workoutFaker = new Faker<WorkOut>()
                .RuleFor(w => w.Id, f => f.IndexFaker + 1)
                .RuleFor(w => w.Name, f => f.Commerce.ProductName())
                .RuleFor(w => w.Description, f => f.Lorem.Paragraph())
                .RuleFor(w => w.TypeId, f => f.Random.Int(1, 5))
                .RuleFor(w => w.CategoryId, f => f.Random.Int(1, 5))
                .RuleFor(w => w.TrainingDuration, f => $"{f.Random.Int(30, 120)} minutes")
                .RuleFor(w => w.Price, f => f.Random.Decimal(10, 100));

            return workoutFaker.Generate(count);
        }
    }
}
