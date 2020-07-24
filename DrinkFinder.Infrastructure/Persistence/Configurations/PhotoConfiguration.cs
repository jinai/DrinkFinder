using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photo");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Location)
                .IsUnique();

            builder.Property<Guid>("EstablishmentId");

            builder.HasOne(p => p.Establishment)
                .WithMany(e => e.Photos)
                .HasForeignKey("EstablishmentId");
        }
    }
}
