using System;
using EsbaBlazorApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EsbaBlazorApp.Areas.Identity.IdentityHostingStartup))]
namespace EsbaBlazorApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {          
            builder.ConfigureServices((context, services) => {
            });        
        }
    }
}