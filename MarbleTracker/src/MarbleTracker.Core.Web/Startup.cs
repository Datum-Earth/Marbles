using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarbleTracker.Core.Web
{
    public class Startup
    {
        public readonly Version CurrentVersion = new Version(0, 1, 0);

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(o =>
                o.SwaggerDoc(this.CurrentVersion.ToString(), new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Description = "Provides core functionality for the MarbleTracker app.",
                    Title = "MarbleTracker.Core.Web",
                    Version = this.CurrentVersion.ToString()
                })
            );;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint($"/swagger/{this.CurrentVersion.ToString()}/swagger.json", "MarbleTracker.Core.Web"));

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("", o => o.Response.WriteAsync("gotem"));
                endpoints.MapControllers();
            });
        }
    }
}
