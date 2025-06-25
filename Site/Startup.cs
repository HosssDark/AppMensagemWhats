using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Site.Libraries;
using Repository.Interface;
using Repository;

namespace Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IDiscenteRepository, DiscenteRepository>();
            services.AddScoped<IMensagemWhatsRepository, MensagemWhatsRepository>();
            services.AddScoped<IUniversidadeRepository, UniversidadeRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioSenhaRepository, UsuarioSenhaRepository>();

            services.AddSingleton<IHostedService, TaskBackground>();

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            services.AddScoped<Session>();
            services.AddScoped<LoginUser>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                             .AddSessionStateTempDataProvider();

            services.AddSession(option =>
            {
                option.Cookie.IsEssential = true;
                option.Cookie.HttpOnly = false;
                option.IdleTimeout = TimeSpan.FromHours(12);
            });

            services.AddMvc();

        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}