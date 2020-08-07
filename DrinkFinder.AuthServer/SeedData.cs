using DrinkFinder.AuthServer.Data;
using DrinkFinder.AuthServer.Models;
using DrinkFinder.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;

namespace DrinkFinder.AuthServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<DrinkFinderIdentityContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Identity));
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<DrinkFinderIdentityContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<DrinkFinderIdentityContext>();
                    context.Database.Migrate();

                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                    var admin = roleMgr.FindByNameAsync(UserRoles.Admin).Result;
                    if (admin == null)
                    {
                        admin = new IdentityRole<Guid>
                        {
                            Name = UserRoles.Admin
                        };
                        var result = roleMgr.CreateAsync(admin).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug($"Role {UserRoles.Admin} created");
                    }
                    else
                    {
                        Log.Debug($"Role {UserRoles.Admin} already exists");
                    }

                    var manager = roleMgr.FindByNameAsync(UserRoles.Manager).Result;
                    if (manager == null)
                    {
                        manager = new IdentityRole<Guid>
                        {
                            Name = UserRoles.Manager
                        };
                        var result = roleMgr.CreateAsync(manager).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug($"Role {UserRoles.Manager} created");
                    }
                    else
                    {
                        Log.Debug($"Role {UserRoles.Manager} already exists");
                    }

                    var member = roleMgr.FindByNameAsync(UserRoles.Member).Result;
                    if (member == null)
                    {
                        member = new IdentityRole<Guid>
                        {
                            Name = UserRoles.Member
                        };
                        var result = roleMgr.CreateAsync(member).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug($"Role {UserRoles.Member} created");
                    }
                    else
                    {
                        Log.Debug($"Role {UserRoles.Member} already exists");
                    }

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null)
                    {
                        alice = new ApplicationUser
                        {
                            Id = Guid.Parse("9c59fbb6-c669-447e-9b2b-0a64d2a5f8f6"),
                            UserName = "alice",
                            Email = "AliceSmith@email.com",
                            EmailConfirmed = true,
                            FirstName = "Alice",
                            LastName = "Smith",
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(alice, UserRoles.Admin).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("User Alice created");
                    }
                    else
                    {
                        Log.Debug("User Alice already exists");
                    }

                    var bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null)
                    {
                        bob = new ApplicationUser
                        {
                            Id = Guid.Parse("ba7c7c61-52dd-4d23-a703-a1d31702bf33"),
                            UserName = "bob",
                            Email = "BobSmith@email.com",
                            EmailConfirmed = true,
                            FirstName = "Bob",
                            LastName = "Smith",
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(bob, UserRoles.Manager).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("User Bob created");
                    }
                    else
                    {
                        Log.Debug("User Bob already exists");
                    }

                    var charlie = userMgr.FindByNameAsync("charlie").Result;
                    if (charlie == null)
                    {
                        charlie = new ApplicationUser
                        {
                            Id = Guid.Parse("497991f4-da7f-45ae-b885-04b74b6a3a90"),
                            UserName = "charlie",
                            Email = "CharlieSmith@email.com",
                            EmailConfirmed = true,
                            FirstName = "Charlie",
                            LastName = "Smith",
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(charlie, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(charlie, UserRoles.Member).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("User Charlie created");
                    }
                    else
                    {
                        Log.Debug("User Charlie already exists");
                    }
                }
            }
        }
    }
}
