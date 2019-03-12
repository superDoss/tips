using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tips.Areas.Identity.Data;

[assembly: HostingStartup(typeof(Tips.Areas.Identity.IdentityHostingStartup))]
namespace Tips.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TipsIdentityDbContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("TipsIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<TipsIdentityDbContext>();
            });
        }
    }
}