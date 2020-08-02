using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Picture");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Location)
                .IsUnique();
        }
    }
}
