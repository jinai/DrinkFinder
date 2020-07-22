using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");
            builder.HasKey(n => n.Id);

            builder.Property<Guid>("EstablishmentId");

            builder.HasOne(n => n.Establishment)
                .WithMany(e => e.News)
                .HasForeignKey("EstablishmentId");
        }
    }
}
