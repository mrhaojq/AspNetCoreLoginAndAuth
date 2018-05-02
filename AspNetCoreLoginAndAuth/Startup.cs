using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AspNetCoreLoginAndAuth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Data.Repositories;
using AspNetCoreLoginAndAuth.Services.UserApp;

namespace AspNetCoreLoginAndAuth
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //获取数据库连接字符串
            //var sqlConectionString = Configuration.GetConnectionString("DefaultConnection");
            var sqlConectionString = Configuration.GetConnectionString("DefaultConnection");

            //添加数据库上下文
            services.AddDbContext<AspNetCoreLoginAndAuthDbContext>(options =>
            options.UseNpgsql(sqlConectionString));
           
            /*
            注意：Asp.Net Core提供的依赖注入拥有三种生命周期模式，由短到长依次为：
            Transient ServiceProvider总是创建一个新的服务实例。
            Scoped ServiceProvider创建的服务实例由自己保存，（同一次请求）所以同一个ServiceProvider对象提供的服务实例均是同一个对象。
            Singleton 始终是同一个实例对象
            */
            //添加仓储及服务进行依赖注入的实现
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();

            //添加MVC服务
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //添加日志
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                //开发环境错误页
                //Asp.net Core为我们提供了统一的错误处理机制，在Startup.cs中的Configure方法中，已经默认添加了以下开发环境的错误处理代码。
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境错误页
                //在生产环境我们一般不会将上述页面直接呈现给我们的客户，而是选择制作一个提示友好的错误页。我们首先修改Startup.cs中Configure方法，添加对生产环境的错误处理。
                app.UseExceptionHandler("/Shared/Error");
            }

            //使用静态文件
            app.UseStaticFiles();

            //使用MVC并使用默认路由设置
            //app.UseMvcWithDefaultRoute();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            //使用MVC 设置默认路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });


            //初始化数据
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
