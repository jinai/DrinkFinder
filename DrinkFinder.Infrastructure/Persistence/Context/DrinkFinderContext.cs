using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace DrinkFinder.Infrastructure.Persistence.Context
{
    public class DrinkFinderContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DrinkFinderContext(DbContextOptions<DrinkFinderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}