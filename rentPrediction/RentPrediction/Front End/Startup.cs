using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using RentPrediction.BEModels.Mappings;
using RentPrediction.Business;
using RentPrediction.Business.Contracts;
using RentPrediction.Business.ML;
using RentPrediction.Data;
using RentPrediction.Repo;
using RentPrediction.Repo.Contracts;
using System;
using DbContext = RentPrediction.Data.DbContext;

namespace Front_End
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

            services.AddAutoMapper(typeof(AddressMappingProfile));
            services.AddControllersWithViews()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            RegisterAppTypes(services);
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

        }

        private void RegisterAppTypes(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            //Data Model
            services.AddDbContext<DbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WebApi")),
                ServiceLifetime.Scoped);
            services.AddTransient<DbConfiguration>();

            //Services
            //--------Repositories
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IGalleryRepository, GalleryRepository>();
            services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPartitioningRepository, PartitioningRepository>();
            //--------Managers
            services.AddScoped<IPropertyManager, PropertyManager>();
            services.AddScoped<IAddressManager, AddressManager>();
            services.AddScoped<IFavoriteManager, FavoriteManager>();
            services.AddScoped<IFeatureManager, FeatureManager>();
            services.AddScoped<IGalleryManager, GalleryManager>();
            services.AddScoped<IPropertyTypeManager, PropertyTypeManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IScraperManager, ScraperManager>();
            services.AddScoped<IMLManager, MainMLProgram>();


            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            //var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            //services.AddAuthentication(x =>
            //    {
            //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.SaveToken = true;
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(key),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbConfig = serviceScope.ServiceProvider.GetService<DbConfiguration>();
                dbConfig.Seed();
            }
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                //todo: check this
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(200); // <-- add this line
                }
            });
        }
    }
}
