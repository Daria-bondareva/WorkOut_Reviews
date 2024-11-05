using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsSystem.DAL.Entities;

namespace ReviewsSystem.DAL.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(500); 

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.HasCheckConstraint("CK_Review_Rating", "[Rating] >= 1 AND [Rating] <= 5");

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.WorkOut)
                .WithMany(w => w.Reviews)
                .HasForeignKey(r => r.WorkOutId);

        }
    }
}
