
# 集成.NET Core+Swagger+Consul+Polly+Ocelot+IdentityServer4+Exceptionless+Apollo的微服务开发框架

## Apollo配置中心
Apollo（阿波罗）是携程框架部门研发的分布式配置中心，能够集中化管理应用不同环境、不同集群的配置，配置修改后能够实时推送到应用端，并且具备规范的权限、流程治理等特性，适用于微服务配置管理场景。
由于各个项目配置都需要读取基础的配置信息，这边在内网的Centos(101)上部署了Apollo的环境，并为项目添加了一些基础配置信息，配置如图
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/apollo.png" width="900" height="350" /><br/>

## Consul
Consul是一种服务网格解决方案，提供具有服务发现，健康检查，Key/Value存储，多数据中心等功能。
在内网101启动Consul服务，这里为了测试，直接在本地将用户服务实例分别在三个端口启动起来，实际生产中这些服务可能部署在不同的机房不同的机器，他们之间组成一个服务的集群，服务提供一个心跳检测的方法，用于consul定时检测服务实例是否健康，启动时在consul中进行一次注册，这个就是经常说的‘服务注册与发现’中的服务注册，三个服务实例截图如下
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/serviceRsg.png" width="900" height="350" /><br/>
注册完成之后打开consul的ui界面可以看到，列表中存在多出一个用户服务的集群组名称：Demo.MicroServer.UserService，如图
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/consul_main.png" width="900" height="350" /><br/>
点击Demo.MicroServer.UserService进去之后如图，显示三个服务实例的信息
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/consul_service_instance_list.png" width="900" height="350" /><br/>

## Swagger
Swagger提供了一个可视化的UI页面展示描述文件。接口的调用方、测试等都可以在该页面中对相关接口进行查阅和做一些简单的接口请求。当然Swagger的功能远不止这些，项目中已经在服务实例中接入swagger，如图
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/swagger_port_6891.png" width="900" height="350" /><br/>
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/swagger_port_6892.png" width="900" height="350" /><br/>
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/swagger_port_6893.png" width="900" height="350" /><br/>
因为三个服务实例是同样一份代码，所以可以看到打开三个端口的swagger地址，看到的接口信息完全一致。

##Ocelot 网关
Ocelot是一个.NET API网关，它提供了路由,请求聚合,服务发现、鉴权、限流熔断、负载均衡器等一系列强大的功能，而这些功能只需要在配置文件中完成即可使用.
比如上面的swagger，我们在三个服务实例的端口打开都可以看到api相关文档信息，但是我们能否在api网关中直接集成呢，答案是肯定的，这依赖于ocelot强大的路由功能，如图，简单的几行配置，我们便将swagger配置到了网关当中
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/ocelot_config_swagger.png" width="900" height="250" /><br/>

网关内置的负载均衡器使用来一泼，如图我在网关中对同一个接口进行了三次调用，可以看到结果分别来自三个不同的端口中，因为我选用了负载均衡器中的轮询策略
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/ocelot_LoadBalancer_6891.png" width="900" height="150" /><br/>
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/ocelot_LoadBalancer_6892.png" width="900" height="150" /><br/>
<img src="https://raw.githubusercontent.com/PeyShine/Demo.MicroServer/master/doc/images/ocelot_LoadBalancer_6893.png" width="900" height="150" /><br/>

