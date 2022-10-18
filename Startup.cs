using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EsbaBlazorAppAuth.Areas.Identity;
using EsbaBlazorAppAuth.Data;
using Blazored.Toast;
using EsbaBlazorAppAuth.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using EsbaBlazorAppAuth.Areas.Identity.Data;

namespace EsbaBlazorAppAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddDbContext<ApplicationDbContext>(options =>
                options.UseFirebird(
                    Configuration.GetConnectionString("DefaultConnection")));*/
            ApplicationDbContext.LoadConfig(Configuration);
            services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Transient);        

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<AuthSignInManager<ApplicationUser>>();

            
            services.AddRazorPages();
            services.AddServerSideBlazor()
            .AddHubOptions(o =>
             {
                 o.MaximumReceiveMessageSize = 1024000;
             });

            services.AddTransient<IEmailSender, SmtpEmailSender>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddScoped<AppSession>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddBlazoredToast();  

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}