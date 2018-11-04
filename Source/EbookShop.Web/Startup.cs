using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EbookShop.DataAccess;
using AutoMapper;
using EbookShop.Models;
using Microsoft.AspNetCore.Identity;
using EbookShop.Services.Mapping;
using EbookShop.Services;

namespace EbookShop.Web
{
    public class Startup
    {

        private const string ConnectionString = @"Data Source=localhost\SQLExpress;Initial Catalog=EbookShopDb;Integrated Security=True;Pooling=False";
  
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x=> x.AddProfile<MappingProfile>());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configure DB Context. Add EbookShop context to dependency injection container and set database provider and also connection string. 
            services.AddDbContext<EbookShopContext>(options=>options.UseSqlServer(ConnectionString));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<EbookShopContext>()
                .AddDefaultTokenProviders();
                

            services.AddTransient<IRegistrationService, RegistrationService>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
     
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
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
