using DrinkFinder.Infrastructure.Persistence.Abstract;
using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Context
{
    public class DrinkFinderContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DrinkFinderContext(DbContextOptions<DrinkFinderContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastEditedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
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