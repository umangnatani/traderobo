using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using TradeRobo.Service;

namespace TradeRobo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private DBService _service;
        public UserController(IWebHostEnvironment env, MyDatabaseContext context, IJwtToken token) : base(env, context, token)
        {

            _service = new DBService(_context);

        }


        [HttpGet]
        [Route("save")]
        public string Save()
        {
            var user = new User { UserName = "umang", Password = "first", Role = "admin" };

            _service.SaveUser(user);

            return "Done";
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public AuthenticateResponse Authenticate([FromBody] User model)
        {
            var response = _service.Authenticate(model);

            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("user")]
        public User GetCurrentUser()
        {
            var id = GetUserId();
            return _service.GetUser(id);
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _service.GetAllUsers();
            return Ok(users);
        }


    }
}
