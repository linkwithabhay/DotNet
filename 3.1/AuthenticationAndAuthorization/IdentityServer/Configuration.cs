using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "random.claim.scope",
                    UserClaims = { "random.claim" }
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                // Add UserClaims directly with the scope
                new ApiResource("ApiOne", new string[] { "another.random.claim" }), // it does not work. WHY?
                new ApiResource("ApiTwo")
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                // Add UserClaims directly with the scope
                new ApiScope("ApiOne", new string[] { "another.random.claim" }), // it works
                new ApiScope("ApiTwo", new string[] { "another.random.claim" })
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ApiOne" }
                },
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44368/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44368/Home/Index" },
                    AllowedScopes = {
                        "ApiOne",
                        "ApiTwo",
                        IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServerConstants.StandardScopes.Profile,
                        "random.claim.scope"
                    },
                    RequireConsent = false,
                    
                    // Puts all the claims in id_token
                    // AlwaysIncludeUserClaimsInIdToken = true
                    
                    // for refresh_token
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "client_id_js",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:44357/home/signin" },
                    PostLogoutRedirectUris = { "https://localhost:44357/Home/Index" },
                    AllowedCorsOrigins = { "https://localhost:44357" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "ApiOne",
                        "random.claim.scope"
                    },
                    AccessTokenLifetime = 1, // token expires after specified seconds
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                }
            };
    }
}
