using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class EstablishmentConfiguration : IEntityTypeConfiguration<Establishment>
    {
        public void Configure(EntityTypeBuilder<Establishment> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Establishment");
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.ShortCode)
                .IsUnique();

            builder.HasIndex(e => e.VatNumber)
                .IsUnique();

            builder.HasIndex(e => e.UserId);

            builder.OwnsOne(e => e.Address);
            builder.OwnsOne(e => e.Socials);
            builder.OwnsOne(e => e.ContactInfo);

            builder.HasMany(e => e.BusinessHours)
                .WithOne(bh => bh.Establishment)
                .HasForeignKey(bh => bh.EstablishmentId)
                .IsRequired();

            builder.HasMany(e => e.Pictures)
                .WithOne(p => p.Establishment)
                .HasForeignKey(p => p.EstablishmentId)
                .IsRequired();

            builder.HasMany(e => e.News)
                .WithOne(n => n.Establishment)
                .HasForeignKey(n => n.EstablishmentId)
                .IsRequired();
        }
    }
}
