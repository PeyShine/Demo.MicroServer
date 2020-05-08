using Demo.MicroServer.UserService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Demo.MicroServer.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;
        public UserController(IUserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _configuration = configuration;
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

    }
}