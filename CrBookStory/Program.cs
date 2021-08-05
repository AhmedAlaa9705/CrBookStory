using CrBookStory.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webhost = CreateWebHostBuilder(args).Build();
            RunMigration(webhost);
            webhost.Run();
        }

        private static void RunMigration(IWebHost webhost)
        {
            using(var scope = webhost.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MyDBContext>();
                db.Database.Migrate();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
