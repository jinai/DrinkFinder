using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.HasMany<Establishment>(u => u.Establishments)
                .WithOne(e => e.Manager)
                .HasForeignKey("ManagerId")
                .IsRequired();

            builder.HasMany<News>(u => u.News)
                .WithOne(n => n.Publisher)
                .HasForeignKey("PublisherId")
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
