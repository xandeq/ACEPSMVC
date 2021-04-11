using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACEPSMVC.DataAccess.Data;
using ACEPSMVC.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using ACEPSMVC.DataAccess.Data.Repository;
using ACEPSMVC.Utility;

namespace ACEPSMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // AddRazorRuntimeCompilation
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });

            services.AddDbContext<ContextoDBAplicacao>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ConexaoBancoDeDados")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ContextoDBAplicacao>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddDbContext<ContextoDBAplicacao>(option => option.UseSqlServer(Configuration.GetConnectionString("ConexaoBancoDeDados")));
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IDbInitializer, DbInitializer>();
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = $"/Identity/Account/Login";

                options.LogoutPath = $"/Identity/Account/Logout";

                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

            });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Home}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
