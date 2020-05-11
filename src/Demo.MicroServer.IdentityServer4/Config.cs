using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.MicroServer.IdentityServer4
{
    public static class Config
    {
        /// <summary>
        /// 下方定义的api资源和客户端访问密钥等，都可以动态的从配置文件中读取
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 添加对OpenID Connect的支持
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(), 
                new IdentityResources.Profile()
            };


        /// <summary>
        /// 需要保护的api资源
        /// </summary>
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("UserService","userservices api"),                
                new ApiResource("ProductService","productservice api")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //客户端模式
                new Client
                {
                    ClientId="Web.Client",
                    ClientName="Web.Client.Name",
                    ClientSecrets=new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AccessTokenLifetime=3600*8,    //token过期时间设置为8小时
                    //允许访问api的范围
                    AllowedScopes = new [] { "UserService", "ProductService",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile },
                    AllowOfflineAccess=true
                },
                //密码模式
                new Client
                {
                    ClientId="Web.Manager",
                    ClientName="Web.Manager.Name",
                    ClientSecrets=new [] { new Secret("managersecret".Sha256()) },
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime=3600*8,    //token过期时间设置为8小时
                    //允许访问api的范围
                    AllowedScopes = new [] { "UserService",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile },
                    AllowOfflineAccess=true
                }                
            };
    }

}
