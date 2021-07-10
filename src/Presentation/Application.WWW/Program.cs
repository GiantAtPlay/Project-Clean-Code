using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.WWW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // [LI] Configure application to use Light Inject as DI container and register services in Composition root.
                .UseLightInject(services => services.RegisterFrom<CompositionRoot>())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // [SEC]: Remove Server Header when using Kestrel
                    //webBuilder.UseKestrel(options => options.AddServerHeader = false);
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>(); 
                });
    }
}