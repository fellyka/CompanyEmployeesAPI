using CompanyEmployeesAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployeesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Use NLog's LogManager static class used with the LoadConfiguration method
            //to provide a path to the configuration file
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
                "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //The following methods definition are found in the Extensions folder in the 
            //CompanyEmployeesAPI solution
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else { app.UseHsts(); }
            
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("CorsPolicy"); 
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
