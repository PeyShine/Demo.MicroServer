using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.MicroServer.ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController: Controller
    {
        private readonly IConfiguration _configuration;
        public HealthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("check")]
        public IActionResult check()
        {
            System.Console.WriteLine($"{_configuration["ip"]}:{_configuration["port"]} is alive");
            return Ok();
        }

        /// <summary>
        /// 测试Exceptionless错误收集和告警
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ActionResult<string> Test(int id)
        {
            try
            {
                throw new Exception("test exceptionless,from userservcie");                
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
            }
            return $"value {id}";
        }
    }
}