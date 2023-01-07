using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {


        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetTime()
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _logger.LogWarning($"当前时间： {currentDate}");

            return currentDate;
        }
    }
}
