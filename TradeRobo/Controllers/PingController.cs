using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeRobo.Service;

namespace TradeRobo.Controllers
{
    [Route("")]
    [ApiController]
    public class PingController : ControllerBase
    {
        protected IWebHostEnvironment _environment { get; set; }
        protected MyDatabaseContext _context;
        public PingController(IWebHostEnvironment env, MyDatabaseContext context)
        {
            _environment = env;
            _context = context;
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

        [HttpGet]
        [Route("api/ping/db")]
        public List<Pie> GetDBPing()
        {
            return _context.Pie.ToList();
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
