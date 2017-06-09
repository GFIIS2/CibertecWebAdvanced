using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cibertec.Web.Models;
using Microsoft.EntityFrameworkCore;
using Cibertec.UnitOfWork;

namespace Cibertec.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CASO 1: ENTITY FRAMEWORK
            // Add framework services.

            //services.AddDbContext<NorthwinddbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Northwind")));


            //CASO 2: USANDO PATRON REPOSITORIO Y UNIT OF WORK
            //services.AddScoped<>; Es creado una vez por cada requerimiento.
            //services.AddSingleton<>; No nos conviene por como trabaja ENTITY FRAMEWORK, en otros framework si se podria utilizar
            //services.AddTransient<>; Cada vez que es requerido.Es lo mejor por ahora

            //services.AddTransient<IUnitOfWork>(
            //    options => new EFUnitOfWork(
            //        new NorthwinddbContext(
            //            new DbContextOptionsBuilder<NorthwinddbContext>().UseSqlServer(Configuration.GetConnectionString("Northwind")).Options
            //        )
            //    )
            //);

            //CASO 3: USANDO DAPPER
            services.AddSingleton<IUnitOfWork>(options => new CibertecUnitOfWork(Configuration.GetConnectionString("Northwind")));
            

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
