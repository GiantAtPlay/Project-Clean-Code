using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IoC;
using Application.WWW.Middleware;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace Application.WWW
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
            services.AddControllersWithViews();
                
            // [SQ]: Add Seq to logging configuration
            services.AddLogging(builder =>
            {
                builder.AddSeq();
            });
            
            // [HF]: Register Hangfire configuration
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseLightInjectActivator(HangfireIoC.BackgroundTaskServiceContainer())
                .UseSqlServerStorage(Configuration.GetConnectionString("Hangfire"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            
            // [MP]: Register Mini Profiler configuration
            services.AddMiniProfiler();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // [LG]: Logging when when the application starts up can be useful to see in some scenarios.
            logger.Log(LogLevel.Information, "Application is starting.");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                // [MP]: Add miniprofiler only when in development environment
                app.UseMiniProfiler();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // [SEC]: Ensure Hsts is enabled, configure as required
                app.UseHsts();
            }
            
            // [SEC]: Ensure site uses only https 
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            // [SEC]: Configure secure cookies
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Strict
            });

            app.UseRouting();

            app.UseAuthorization();
            
            // [DB] : Register database update middleware so database gets updated on startup
            app.UseDataBaseUpdater(Configuration.GetConnectionString("Default"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            // [HF]: Register App as hangfire server
            app.UseHangfireServer();
            
            // [HF]: Enable hangfire dashboard
            app.UseHangfireDashboard();
        }
    }
}