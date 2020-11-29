using Global_Intern.Data;
using Global_Intern.Models;
using Global_Intern.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;


namespace Global_Intern
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
            services.AddScoped<GlobalDBContext>();

            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            //services.AddMvc().Add
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(1800);
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication("Basic").AddScheme<BasicAuthOptions, CustomAuthHandler>("Basic", null);
            services.AddSingleton<ICustomAuthManager, CustomAuthManager>();
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

            // Seed Data
            // If you your system SQL server don't have databse. It will create Automatically  
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var _context = scope.ServiceProvider.GetService<GlobalDBContext>();
                try
                {
                    
                    _context.Database.EnsureCreated();
                    _context.EnsureDataBaseSeeded();
                    //_context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Db exits");
                    Console.WriteLine(e);
                }

            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();





            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
