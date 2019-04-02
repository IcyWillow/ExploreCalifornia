using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfigurationRoot configuration;

        public Startup(IHostingEnvironment env) {
            var configuration = new ConfigurationBuilder()
                              .AddEnvironmentVariables()
                              .AddJsonFile(env.ContentRootPath + "/configuration.json")
                              .AddJsonFile(env.ContentRootPath + "/config.development.json", true)
                              .Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggles>(x => new FeatureToggles {
                EnableDeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              FeatureToggles feature)
        {

            app.UseExceptionHandler("/error.html");

          

            //if (configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions"))

            if(feature.EnableDeveloperExceptions)
                app.UseDeveloperExceptionPage();


            /*
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.StartsWith("/hello")) { 
                string test = "Fleisch";
                await context.Response.WriteAsync($"Hello sss! {test}");
                }
                await next();
            });

            app.Run(async (context) =>
            {

                await context.Response.WriteAsync($" test");
            });
                         
            */

            app.Use(async (context, next) => {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default",
                        "{controller=Home}/{action=Index}/{id?}");
            }
            );

            app.UseFileServer();
        }
    }
}
