using Auxiliary.Email;
using Domain.DTO;
using Domain.IDTO;
using Domain.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Claims;
using Repository.Context;
using Repository.Repository;
using Repository.Requirements;
using System;
using System.Reflection;

namespace QuickBuy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            #region Database Connect
            // Connection String - Migration Local - Connect 
            var connectionString = Configuration.GetConnectionString("QuickBuyDB");
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            
            services.AddDbContext<QuickBuyContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)));
            #endregion

            #region Identity
            // Identity
            services.AddIdentity<AccountIDTO, IdentityRole>(options =>
            {
                // Confirmar e-mail login
                options.SignIn.RequireConfirmedEmail = true;

                // Burocracia da senha
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                // Segurança
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            }).AddEntityFrameworkStores<QuickBuyContext>().AddDefaultTokenProviders().AddPasswordValidator<PasswordRequirements<UserDTO>>();
            #endregion

            // Time Validation - Token [3 hours]
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(3));

            #region Scope's
            // Scope's
            services.AddScoped<IUserStore<AccountIDTO>, UserOnlyStore<AccountIDTO, QuickBuyContext>>();
            services.AddScoped<IUserClaimsPrincipalFactory<AccountIDTO>, UserClaimsPrincipalFactory>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentFormRepository, PaymentFormRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Email
            // Email Configuration
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, SendEmail>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
