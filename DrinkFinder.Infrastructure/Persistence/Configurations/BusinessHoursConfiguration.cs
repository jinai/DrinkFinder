using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class BusinessHoursConfiguration : IEntityTypeConfiguration<BusinessHours>
    {
        public void Configure(EntityTypeBuilder<BusinessHours> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("BusinessHours");
            builder.HasKey(t => t.Id);
        }
    }
}
