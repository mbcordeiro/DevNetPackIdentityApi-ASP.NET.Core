using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NetDevPack.Identity;
using NetDevPack.Identity.Jwt;

namespace DevNetPackIdentityApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddIdentityEntityFrameworkContextConfiguration(Configuration, GetType().Namespace, "DefaultConnection");
            services.AddJwtConfiguration(Configuration, "AppSettings");
            services.AddIdentityConfiguration();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo() 
                {
                    Title = "NetDevPack Identity Sample API",
                    Description = "Development by Matheus Cordeiro", 
                    Contact = new OpenApiContact() { Name = "Matheus Cordeiro", Email = "matheusdebarroscordeiro@gmail.cocm"},
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/mit") }

                });
            });

           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
