using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data;
using Microsoft.EntityFrameworkCore;
using Authentication.Data;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Authentication.Core.Authorization;
using Data.Repository.Interface;
using Core.IServices;
using Core.Service;

namespace Authentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public const string AuthenticationSchemeName = "MyApplication_AuthenticationScheme";
        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"], b => b.MigrationsAssembly("Authentication"));
            });
            services.AddAuthentication(AuthenticationSchemeName)
                .AddCookie(AuthenticationSchemeName, (op) =>
             {
                 op.AccessDeniedPath = "/Denied";
                 op.LoginPath = "/Login";
             });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("OperationPolicy", policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new OperationAuthorizationRequirement());
                });
            });
            services.AddMvc(options =>
            {
                //options.Filters.Add(new InternalAuthorizeFilter());
            });
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddSingleton<IAuthorizationHandler, OperationAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitalizer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //创建种子数据
            try
            {
                databaseInitalizer.SeedAsync().Wait();
            }
            catch
            {

                throw;
            }
        }
    }
}
