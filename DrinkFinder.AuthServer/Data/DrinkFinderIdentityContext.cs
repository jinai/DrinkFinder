using DrinkFinder.AuthServer.Models;
using DrinkFinder.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DrinkFinder.AuthServer.Data
{
    public class DrinkFinderIdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DrinkFinderIdentityContext() { }

        public DrinkFinderIdentityContext(DbContextOptions<DrinkFinderIdentityContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=DrinkFinder;Trusted_Connection=True;MultipleActiveResultSets=true",
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Identity));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(Schemas.Identity);
            base.OnModelCreating(builder);
        }
    }
}
