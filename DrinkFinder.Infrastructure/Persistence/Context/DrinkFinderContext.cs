using DrinkFinder.Common.Interfaces;
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
            foreach (var entry in ChangeTracker.Entries<IEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AddedDate = DateTime.Now;
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

            builder.Entity<Establishment>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<News>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Photo>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Timetable>().HasQueryFilter(e => !e.IsDeleted);
        }

        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<News> News { get; set; }
    }
}