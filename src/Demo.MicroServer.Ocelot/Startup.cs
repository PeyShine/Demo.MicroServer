using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace Demo.MicroServer.Ocelot
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
            ////IdentityService4
            //Action<IdentityServerAuthenticationOptions> isaOptClient = option =>
            //{
            //    option.Authority = Configuration["IdentityService4.Uri"];
            //    option.ApiName = "UserService";
            //    option.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService4.UseHttps"]);
            //    option.SupportedTokens = SupportedTokens.Both;
            //    //option.ApiSecret = Configuration["IdentityService4:ApiSecrets:DesignerService"];
            //};

            ////Action<IdentityServerAuthenticationOptions> isaOptProduct = option =>
            ////{
            ////    option.Authority = Configuration["IdentityService4:Uri"];
            ////    option.ApiName = "ProductService";
            ////    option.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService4:UseHttps"]);
            ////    option.SupportedTokens = SupportedTokens.Both;
            ////    option.ApiSecret = Configuration["IdentityService4:ApiSecrets:productservice"];
            ////};

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication("designerserverkey", isaOptClient);
            ////.AddIdentityServerAuthentication("ProductServiceKey", isaOptProduct);

            //Ocelot
            services.AddOcelot()
                .AddConsul()
                .AddPolly();

            services.AddMvcCore()
                .AddApiExplorer();

            //Swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var apiList = Configuration["Swagger.ServiceDocNames"].Split(',').ToList();

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    apiList.ForEach(apiItem =>
                    {
                        options.SwaggerEndpoint($"/doc/{apiItem}/swagger.json", apiItem);
                    });
                });

            app.UseOcelot();
        }
    }
}
