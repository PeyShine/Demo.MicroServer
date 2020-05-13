using Demo.MicroServer.Repository.Mongo;
using Demo.MicroServer.UserService.Models;
using Demo.MicroServer.UserService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.MicroServer.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;
        private readonly IMongoService _mongoService;
        public UserController(IUserServices userServices, IConfiguration configuration, IMongoService mongoService)
        {
            _userServices = userServices;
            _configuration = configuration;
            _mongoService = mongoService;
        }

        /// <summary>
        /// 通过id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<string> GetUserinfo(string id)
        {
            var result = await _userServices.GetUserByIdAsync(id);
            return $"请求来自:{_configuration["ip"]}:{_configuration["port"]},{Newtonsoft.Json.JsonConvert.SerializeObject(result)}";
        }

        /// <summary>
        /// 测试从mongodb中获取用户数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<long> GetMongoUserCount()
        {
            var list = new List<FilterDefinition<user_mongo>>();
            list.Add(Builders<user_mongo>.Filter.Exists("user_id", true));
            var filter = Builders<user_mongo>.Filter.And(list);

             return await _mongoService.CountAsync(filter, "users");
        }
        
        /// <summary>
        /// 临时测试mongo用户实体
        /// </summary>
        public class user_mongo
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string _id { set; get; }
            public string user_id { get; set; }
            /// <summary>
            /// 用户名
            /// </summary>
            public string user_name { get; set; }
            /// <summary>
            /// 账户密码
            /// </summary>
            public string pass_word { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public string add_time { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public string update_time { get; set; }
        }

    }
}