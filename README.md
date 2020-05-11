
# 集成.NET Core+Swagger+Consul+Polly+Ocelot+IdentityServer4+Exceptionless+Apollo的微服务开发框架

## Apollo配置中心
Apollo（阿波罗）是携程框架部门研发的分布式配置中心，能够集中化管理应用不同环境、不同集群的配置，配置修改后能够实时推送到应用端，并且具备规范的权限、流程治理等特性，适用于微服务配置管理场景。
由于各个项目配置都需要读取基础的配置信息，这边在内网的Centos(101)上部署了Apollo的环境，并为项目添加了一些基础配置信息，配置如图
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/apollo.png" width="900" height="350" /><br/>

## Consul
Consul是一种服务网格解决方案，提供具有服务发现，健康检查，Key/Value存储，多数据中心等功能。
在内网101启动Consul服务，在本地将用户服务实例分别在三个端口启动，启动时在consul中进行一次注册，这个就是经常说的‘服务注册与发现’中的服务注册，
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/serviceRsg.png" width="900" height="350" /><br/>
