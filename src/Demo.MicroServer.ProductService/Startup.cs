using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Demo.MicroServer.ProductService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Ìí¼Ó Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(Configuration["Swagger.DocName"], new OpenApiInfo()
                {
                    Title = Configuration["Swagger.Title"],
                    Version = Configuration["Swagger.Version"],
                    Description = Configuration["Swagger.Description"],
                    Contact = new OpenApiContact()
                    {
                        Name = Configuration["Swagger.Contact.Name"],
                        Email = Configuration["Swagger.Contact.Email"]
                    }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, Configuration["Swagger.XmlFile"]);
                s.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // swagger
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "doc/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint($"/doc/{Configuration["Swagger.DocName"]}/swagger.json",
                        $"{Configuration["Swagger.Name"]} {Configuration["Swagger.Version"]}");
                });
            }

            //Exceptionless
            ExceptionlessClient.Default.Configuration.ApiKey = Configuration["Exceptionless.ApiKey"];
            ExceptionlessClient.Default.Configuration.ServerUrl = Configuration["Exceptionless.ServerUrl"];
            app.UseExceptionless();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseConsul(Configuration);
        }
    }
}
