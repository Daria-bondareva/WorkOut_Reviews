using Bogus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Interfaces;

namespace ReviewsSystem.DAL.Seeding
{
    public  class UserSeeder
    {
        public static List<User> GenerateUsers(int count)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.IndexFaker + 1)
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password());

            return userFaker.Generate(count);
        }
    }
}
