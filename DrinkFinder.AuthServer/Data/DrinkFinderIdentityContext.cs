using DrinkFinder.AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DrinkFinder.AuthServer.Data
{
    public class DrinkFinderIdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DrinkFinderIdentityContext(DbContextOptions<DrinkFinderIdentityContext> options) : base(options) { }
    }
}
