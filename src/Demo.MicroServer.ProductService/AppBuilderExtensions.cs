using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Demo.MicroServer.ProductService
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            ConsulClient _client = new ConsulClient(c =>
            {
                c.Address = new Uri(configuration["Consul.ServerUrl"]);
                c.Datacenter = "Demo.MicroServer";
            });

            string ip = configuration["ip"];
            int port = int.Parse(configuration["port"]);
            int weight = string.IsNullOrEmpty(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

            _client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "ProductService-" + Guid.NewGuid(),
                Name = "Demo.MicroServer.ProductService",
                Address = ip,
                Port = port,
                Tags = new string[] { string.IsNullOrEmpty(configuration["tags"]) ? "" : configuration["tags"] },   //标签
                Check = new AgentServiceCheck()                                                                     //健康检查
                {
                    Interval = TimeSpan.FromSeconds(10),                                                            //每隔多久检测一次
                    HTTP = $"http://{ip}:{port}/api/health/check",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60)                                       //在遇到异常后关闭自身服务通道
                }
            });

            return app;
        }

    }
}