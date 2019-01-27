using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using EbookShop.Services.Helpers;
using FluentValidation.AspNetCore;
using EbookShop.Services.Dtos;
using EbookShop.Services.Validation;
using FluentValidation;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MediatR;
using System.Reflection;
using EbookShop.Services.Infrastructure;
using MediatR.Pipeline;

namespace EbookShop.Web
{
    public class Startup
    {


        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            // Configure DB Context. Add EbookShop context to dependency injection container and set database provider and also connection string. 
            services.AddDbContext<EbookShopContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalDb")));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Customize password options:
                options.Password.RequiredLength = 8;

            }).AddEntityFrameworkStores<EbookShopContext>()
              .AddDefaultTokenProviders();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddScoped<IAuthenticationService, AuthAuthenticationService>();

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            // Does RequestLogger : IRequestPreProcessor is auto registered via assembly scan? 
            // {\__ /}
            //  (●_●)
            //  ( >🌮 Want a taco?
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            // well yes, it works auto-magically for all Requests and : 
                //typeof(INotificationHandler<>),
                //typeof(IRequestPreProcessor<>),
                //typeof(IRequestPostProcessor<,>)
            services.AddMediatR(typeof(UserService).GetTypeInfo().Assembly);

            // Register validators
            services.AddScoped<IValidator<RegistrationDto>, RegistrationDTOValidator>();
            services.AddHttpContextAccessor();

            // Configure JWT
            ConfigureJWTSecurity(services);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Make autogenerated client libraries possible.
            services.AddAutoMapper(x => x.AddProfile<MappingProfile>());

            // configure fluent validation
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EbookShopContext context)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            EbookShopInitializer.Initialize(context, env); 

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
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
        
        /// <summary>
        /// Configure JWT authorization and authentication services.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureJWTSecurity(IServiceCollection services)
        {
            #region JwtIssuerOptions class config
            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
           
            // Configure JwtIssuerOptions. This class will be injected via DI Container with the following settings assigned to properties:
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            }); // More about configuration and options pattern: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-2.1

            #endregion

            #region Set JWT Token validation options parameters

            SetTokenValidationParameters(out TokenValidationParameters tokenValidationParameters, jwtAppSettingOptions);
            #endregion

            #region Add JWT authentication to the request pipeline
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
                
            });
            #endregion

            #region Add authorization policy
            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });
            #endregion
        }

        /// <summary>
        /// Specifies the validation parameters to dictate how tokens received from users gets validated
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="jwtAppSettingOptions"></param>
        private void SetTokenValidationParameters(out TokenValidationParameters parameters, IConfigurationSection jwtAppSettingOptions)
        {
            parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
