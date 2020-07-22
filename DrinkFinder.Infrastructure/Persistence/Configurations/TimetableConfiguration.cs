using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Configurations
{
    public class TimetableConfiguration : IEntityTypeConfiguration<Timetable>
    {
        public void Configure(EntityTypeBuilder<Timetable> builder)
        {
            builder.ToTable("Timetable");
            builder.HasKey(t => t.Id);

            builder.Property<Guid>("EstablishmentId");
        }
    }
}
