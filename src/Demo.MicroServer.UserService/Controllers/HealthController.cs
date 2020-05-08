using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.MicroServer.UserService.Controllers
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
        /// Ω°øµºÏ≤È
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
        /// ≤‚ ‘Exceptionless¥ÌŒÛ ’ºØ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ActionResult<string> Test(int id)
        {
            try
            {
                throw new Exception("test exceptionless,from productservice");                
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
            }
            return $"value {id}";
        }
    }
}