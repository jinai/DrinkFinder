using AutoMapper;
using DrinkFinder.Api.Filters;
using DrinkFinder.Api.Services;
using DrinkFinder.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;

namespace DrinkFinder.Api
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
            // Keep the property name for validation error messages
            ValidatorOptions.Global.DisplayNameResolver = ValidatorOptions.Global.PropertyNameResolver;

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = Configuration["AuthServer:BaseEndpoint"];
                        options.RequireHttpsMetadata = true;
                        options.Audience = "drinkfinder.api";

                        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("Manager", policy => policy.RequireClaim(ClaimTypes.Role, "Manager", "Admin"));
                options.AddPolicy("Member", policy => policy.RequireClaim(ClaimTypes.Role, "Member", "Manager", "Admin"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddInfrastructure(Configuration);
            services.AddScoped<IEstablishmentService, EstablishmentService>();
            services.AddScoped<INewsService, NewsService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DrinkFinder API",
                    Description = "DrinkFinder API for domain entities, written in .NET Core 3.1",
                    Contact = new OpenApiContact
                    {
                        Name = "Gauthier L.",
                        Url = new Uri("https://github.com/jinai/DrinkFinder")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under GPLv3",
                        Url = new Uri("https://github.com/jinai/DrinkFinder/blob/dev/LICENSE.txt")
                    }
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(Configuration["AuthServer:AuthorizeEndpoint"]),
                            TokenUrl = new Uri(Configuration["AuthServer:TokenEndpoint"]),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "Standard OpenId scope" },
                                { "profile", "Standard OpenId scope" },
                                { "email", "Standard OpenId scope" },
                                { "roles", "Your roles" },
                                { "establishment.read", "Read-only access to your establishments" },
                                { "establishment.write", "Write access to your establishments" },
                                { "news.read", "Read-only access to your news" },
                                { "news.write", "Write access to your news" }
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
                options.CustomSchemaIds(type =>
                {
                    return type.Name.EndsWith("dto", StringComparison.OrdinalIgnoreCase) ? type.Name.Replace("dto", string.Empty, StringComparison.OrdinalIgnoreCase) : type.Name;
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            })
                .AddSwaggerGenNewtonsoftSupport(); // Needed to display enums as strings
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "DrinkFinder API v1");
                options.OAuthClientId("DrinkFinder.Swagger");
                options.OAuthAppName("Swagger UI");
                options.OAuthUsePkce();
            });
        }
    }
}
