using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArtShop.Data;
using Microsoft.AspNetCore;

namespace ArtShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            if (args.Length == 1 && args[0].ToLower() == "/seed")
            {
                RunSeeding(host);

            }
            else
            {
                host.Run();

            }
            
        }

        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory= host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DataSeeder>();
                seeder.SeedAsync().Wait();
            }

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration(AddConfiguration)
                .UseStartup<Startup>().Build();

        private static void AddConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder bldr)
        {
            bldr.Sources.Clear();
            bldr.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json").AddEnvironmentVariables();
        }
    }
}
