using DrinkFinder.AuthServer.Data;
using DrinkFinder.AuthServer.Models;
using DrinkFinder.Common.Constants;
using DrinkFinder.Common.Enums;
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
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<DrinkFinderIdentityContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DrinkFinderIdentityContext>();
                    context.Database.Migrate();

                    // Seeding roles
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


                    // Seeding users
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    // Admins
                    var adminUser = userMgr.FindByNameAsync("admin").Result;
                    if (adminUser == null)
                    {
                        adminUser = new ApplicationUser
                        {
                            Id = Guid.Parse("53bb86b4-78dc-4227-b0c3-41468094aab0"),
                            UserName = "admin",
                            Email = "Admin@DrinkFinder.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "Nimda",
                            Gender = Gender.Male,
                            IsProfessional = true,
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(adminUser, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(adminUser, UserRoles.Admin).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("User Admin created");
                    }
                    else
                    {
                        Log.Debug("User Admin already exists");
                    }

                    // Managers
                    var tony = userMgr.FindByNameAsync("tony").Result;
                    if (tony == null)
                    {
                        tony = new ApplicationUser
                        {
                            Id = Guid.Parse("0ec2fdc1-b2dd-4f8a-801f-ad2dc34bd746"),
                            UserName = "tony",
                            Email = "TonyStark@email.com",
                            EmailConfirmed = true,
                            FirstName = "Tony",
                            LastName = "Stark",
                            Gender = Gender.Male,
                            IsProfessional = true,
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(tony, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(tony, UserRoles.Manager).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("User Tony created");
                    }
                    else
                    {
                        Log.Debug("User Tony already exists");
                    }

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
                            Gender = Gender.Female,
                            IsProfessional = true,
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(alice, UserRoles.Manager).Result;
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

                    var john = userMgr.FindByNameAsync("john").Result;
                    if (john == null)
                    {
                        john = new ApplicationUser
                        {
                            Id = Guid.Parse("e5a49b11-1b6d-441b-ab1d-d9349c93c9e4"),
                            UserName = "john",
                            Email = "JohnDoe@email.com",
                            EmailConfirmed = true,
                            FirstName = "John",
                            LastName = "Doe",
                            Gender = Gender.Male,
                            IsProfessional = true,
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(john, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(john, UserRoles.Manager).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("User John created");
                    }
                    else
                    {
                        Log.Debug("User John already exists");
                    }

                    var jane = userMgr.FindByNameAsync("jane").Result;
                    if (jane == null)
                    {
                        jane = new ApplicationUser
                        {
                            Id = Guid.Parse("19c567cc-4b13-4904-b3ba-11303d02b019"),
                            UserName = "jane",
                            Email = "JaneDoe@email.com",
                            EmailConfirmed = true,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Gender = Gender.Female,
                            IsProfessional = true,
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(jane, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(jane, UserRoles.Manager).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("User Jane created");
                    }
                    else
                    {
                        Log.Debug("User Jane already exists");
                    }

                    // Members
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
                            Gender = Gender.Male,
                            IsProfessional = true,
                            RegistrationDate = DateTimeOffset.Now
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddToRoleAsync(bob, UserRoles.Member).Result;
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
                            Gender = Gender.Male,
                            IsProfessional = false,
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
