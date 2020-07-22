using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class EstablishmentConfiguration : IEntityTypeConfiguration<Establishment>
    {
        public void Configure(EntityTypeBuilder<Establishment> builder)
        {
            builder.ToTable("Establishment");
            builder.HasKey(e => e.Id);

            builder.OwnsOne(e => e.Address);
            builder.OwnsOne(e => e.Socials);
            builder.OwnsOne(e => e.ContactInfo);

            builder.HasOne<Timetable>(e => e.Timetable)
                .WithOne(t => t.Establishment)
                .HasForeignKey<Timetable>("EstablishmentId");
        }
    }
}
