using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TradeRobo.Controllers
{
    [Route("")]
    [ApiController]
    public class PingController : ControllerBase
    {
        protected IWebHostEnvironment _environment { get; set; }
        public PingController(IWebHostEnvironment env)
        {
            _environment = env;
        }

        [HttpGet]
        public string Get()
        {
            return $"TradeRobo API running fine on {_environment.EnvironmentName}";
        }

        [HttpGet]
        [Route("api/ping")]
        public string GetPing()
        {
            return "pong";
        }

        [HttpGet("api/ping/{city}")]
        public string Get(string city)
        {
            if (!string.Equals(city?.TrimEnd(), "wpb", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    $"We don't offer a weather forecast for {city}.", nameof(city));
            }

            return "Hello WPB";
        }

    }
}
