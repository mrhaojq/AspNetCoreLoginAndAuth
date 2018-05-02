using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLoginAndAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .Build();

        /*
        Asp.Net Core应用程序默认提供IIS服务和Kestrel服务两种寄宿方式，
        意味着Asp.Net Core应用程序可以脱离IIS运行，这也是跨平台的基础
        */

        public static IWebHost BuildWebHost(string[] args)
        {
            //Visual Studio的默认启动选项为IIS Express，即采用IIS服务方式
            //var host = WebHost.CreateDefaultBuilder(args)
            //      .UseStartup<Startup>()
            //      .Build();

            var host = new WebHostBuilder()
                  .UseKestrel()
                  .UseUrls("http://localhost:8000")
                  .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseIISIntegration()
                  .UseStartup<Startup>()
                  .Build();

            return host;
        }
    }
}
