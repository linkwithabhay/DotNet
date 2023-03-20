using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "MvcClientCookie";
                config.DefaultChallengeScheme = "OIDC";
            })
                .AddCookie("MvcClientCookie")
                .AddOpenIdConnect("OIDC", config =>
                {
                    config.Authority = "https://localhost:44378/";
                    config.ClientId = "client_id_mvc";
                    config.ClientSecret = "client_secret_mvc";
                    config.SaveTokens = true;
                    config.ResponseType = "code";
                    config.SignedOutCallbackPath = "/Home/Index";

                    // configure cookie claim mapping
                    config.ClaimActions.DeleteClaims("amr", "s_hash"); // no use of these claims to me
                    config.ClaimActions.MapUniqueJsonKey("ClaimUniqueKey", "random.claim");

                    // two trips to load claims into the cookie
                    // but keeps the id_token smaller
                    config.GetClaimsFromUserInfoEndpoint = true;

                    // configure scopes
                    config.Scope.Clear();
                    config.Scope.Add("openid");
                    config.Scope.Add("random.claim.scope");
                    config.Scope.Add("ApiOne");
                    config.Scope.Add("ApiTwo");
                    // for refresh_token
                    config.Scope.Add("offline_access");
                });

            services.AddHttpClient();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
