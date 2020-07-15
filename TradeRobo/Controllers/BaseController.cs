using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeRobo.Service;

namespace TradeRobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IWebHostEnvironment _environment { get; set; }
        protected MyDatabaseContext _context;
        protected IJwtToken _token;

        public BaseController(IWebHostEnvironment env, MyDatabaseContext context, IJwtToken token)
        {
            _token = token;
            _context = context;
            _environment = env;

        }


        protected int GetUserId()
        {
            var currentUser = HttpContext.User;
            int value;
            if (int.TryParse(currentUser.FindFirstValue(ClaimTypes.Name), out value))
                return value;
            else
                return 0;
        }


    }
}
