using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.DAL.Configurations
{
    public class WorkOutConfiguration : IEntityTypeConfiguration<WorkOut>
    {
        public void Configure(EntityTypeBuilder<WorkOut> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Description)
                .IsRequired();

            builder.Property(w => w.TrainingDuration)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(w => w.Price)
                .IsRequired();

            builder.HasMany(w => w.Reviews)
                .WithOne(r => r.WorkOut)
                .HasForeignKey(r => r.WorkOutId);
        }
    }
}
