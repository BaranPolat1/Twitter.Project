using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using FinalProject.Business.AutoMapper;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Business.UnitOfWork.Concrete;
using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.DataAccess.Repository.Concrete;
using FinalProject.Entities.Entity;
using FinalProject.Web.Areas.Member.Controllers;
using FinalProject.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smidge;

namespace FinalProject.Web
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

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddDbContext<ProjectContext>();

            // ===== Add Identity ========
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ProjectContext>()
                .AddDefaultTokenProviders();
            services.AddRazorPages();
            services.AddMvc(options => { options.EnableEndpointRouting = false; }) ;
            services.AddSignalR();
            services.AddAuthentication()
                .AddGoogle(x =>
                {
                    x.ClientId = Configuration["GoogleClientId"];
                    x.ClientSecret = Configuration["GoogleClientSecret"];

                });
            services.AddSmidge(Configuration.GetSection("smidge"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProjectContext dbContext)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            dbContext.Database.EnsureCreated();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSmidge(bundle =>
            {
              
                bundle.CreateCss("cssbundle", "~/css/site.css", "~/lib/bootstrap/dist/css/_all-skins.min.css", "~/lib/bootstrap/dist/css/AdminLTE.min.css", "~/lib/bootstrap/dist/css/bootstrap.min.css", "~/lib/bootstrap/dist/css/bootstrap-grid.min.css", "~/lib/bootstrap/dist/css/bootstrap-reboot.min.css", "~/lib/bootstrap/dist/css/ionicons.min.css", "~/lib/bootstrap/dist/css/font-awesome.min.css", "~/lib/bootstrap/dist/css/blue.css", "~/css/font-awesome.min.css", "https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapControllerRoute(
             name: "areas",
             pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
