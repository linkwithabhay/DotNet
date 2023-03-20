using IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(config =>
            {
                //config.UseInMemoryDatabase("Memory");
                config.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                //config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            string migrationAssembly = typeof(Startup).Assembly.GetName().Name;

            //var filePath = Path.Combine(_env.ContentRootPath, "temp/identityserver_cert.pfx");
            //var certificate = new X509Certificate2(filePath, "password");  // Password not secure here

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString, sql =>
                            sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString, sql =>
                            sql.MigrationsAssembly(migrationAssembly));
                })
                //.AddSigningCredential(certificate);
                //.AddInMemoryIdentityResources(Configuration.IdentityResources)
                //.AddInMemoryApiResources(Configuration.ApiResources) // it does not work. WHY?
                //.AddInMemoryApiScopes(Configuration.ApiScopes) // it works instead
                //.AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(config =>
                {
                    config.ClientId = "YOUR_CLIENT_ID";
                    config.ClientSecret = "YOUR_CLIENT_SECRET";
                })
                .AddFacebook(config =>
                {
                    config.AppId = "YOUR_CLIENT_ID";
                    config.AppSecret = "YOUR_CLIENT_SECRET";
                });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // this will do the initial DB population
            InitializeDatabase(app);

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var appContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                appContext.Database.Migrate();

                if (!appContext.Users.Any())
                {
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var user = new IdentityUser("bob");
                    userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                    userManager.AddClaimAsync(user, new Claim("random.claim", "claim.value")).GetAwaiter().GetResult();
                    userManager.AddClaimAsync(user, new Claim("another.random.claim", "another.claim.value"))
                        .GetAwaiter().GetResult();
                }

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                configContext.Database.Migrate();

                if (!configContext.Clients.Any())
                {
                    foreach (var client in Configuration.Clients)
                    {
                        configContext.Clients.Add(client.ToEntity());
                    }
                    configContext.SaveChanges();
                }

                if (!configContext.IdentityResources.Any())
                {
                    foreach (var resource in Configuration.IdentityResources)
                    {
                        configContext.IdentityResources.Add(resource.ToEntity());
                    }
                    configContext.SaveChanges();
                }

                if (!configContext.ApiScopes.Any())
                {
                    foreach (var resource in Configuration.ApiScopes)
                    {
                        configContext.ApiScopes.Add(resource.ToEntity());
                    }
                    configContext.SaveChanges();
                }
            }
        }
    }
}
