using DrinkFinder.MvcClient.Services;
using Geocoding;
using Geocoding.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Polly;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace DrinkFinder.MvcClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = "mvcclient";
                    options.AccessDeniedPath = "/Home/Login";
                    options.LoginPath = "/Home/Login";
                    options.LogoutPath = "/Home/Logout";

                    options.Events.OnSigningOut = async e =>
                    {
                        await e.HttpContext.RevokeUserRefreshTokenAsync();
                    };
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = Configuration["Endpoints:AuthServer"];

                    options.ClientId = "DrinkFinder.MVC";
                    options.ClientSecret = "3CAEA85E-763B-43C3-B487-C5DBA4364A93";

                    // code flow + PKCE (PKCE is turned on by default)
                    options.ResponseType = "code";
                    options.UsePkce = true;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("roles");
                    options.Scope.Add("establishment.read");
                    options.Scope.Add("establishment.write");
                    options.Scope.Add("news.read");
                    options.Scope.Add("news.write");
                    options.Scope.Add("offline_access");

                    // not mapped by default
                    options.ClaimActions.MapJsonKey("role", "role");

                    // keeps id_token smaller
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("role", "Admin"));
                options.AddPolicy("Manager", policy => policy.RequireClaim("role", "Manager", "Admin"));
                options.AddPolicy("Member", policy => policy.RequireClaim("role", "Member", "Manager", "Admin"));
            });

            // adds user and client access token management
            services.AddAccessTokenManagement()
                    .ConfigureBackchannelHttpClient()
                    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(3)
                    }));

            // registers a typed HTTP client with token management support
            services.AddHttpClient<IEstablishmentService, EstablishmentService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Endpoints:Api"]);
            })
                .AddUserAccessTokenHandler();

            services.AddScoped<IGeocoder>(sp => new GoogleGeocoder { ApiKey = Configuration["ApiKeys:GoogleGeocoding"] });
            services.AddScoped<IGeocodingService, GeocodingService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
