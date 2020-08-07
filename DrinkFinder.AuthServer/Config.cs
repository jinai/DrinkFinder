using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace DrinkFinder.AuthServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Your roles",
                    UserClaims = { JwtClaimTypes.Role },
                    Required = true
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope
                {
                    Name = "establishment.read",
                    DisplayName = "Read-only access to your establishments",
                },
                new ApiScope
                {
                    Name = "establishment.write",
                    DisplayName = "Write access to your establishments",
                },
                new ApiScope
                {
                    Name = "news.read",
                    DisplayName = "Read-only access to your news",
                },
                new ApiScope
                {
                    Name = "news.write",
                    DisplayName = "Write access to your news",
                },
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("drinkfinder.api", "DrinkFinder API")
                {
                    Scopes = { "establishment.read", "establishment.write", "news.read", "news.write" },
                    UserClaims = { JwtClaimTypes.Role }
                },
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "DrinkFinder.MVC",
                    ClientName = "DrinkFinder MVC Client",
                    ClientSecrets = { new Secret("3CAEA85E-763B-43C3-B487-C5DBA4364A93".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RequireConsent = true,

                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "establishment.read",
                        "establishment.write",
                        "news.read",
                        "news.write"
                    }
                }
            };
        }
    }
}