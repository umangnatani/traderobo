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
using Newtonsoft.Json;
using TradeRobo.Service;

namespace TradeRobo.Controllers
{

    public class FuseMenu
    {
        public string id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public bool hidden { get; set; }
        public string url { get; set; }

        public List<FuseMenu> children { get; set; } = new List<FuseMenu>();

    }





    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private DBService _service;
        public UserController(IWebHostEnvironment env, MyDatabaseContext context) : base(env, context)
        {

            _service = new DBService(_context);

        }


        [HttpPost]
        public ReturnType Save(User user)
        {
            var id = GetUserId();
           
              return  _service.SaveUser(user, id);
            
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public AuthenticateResponse Authenticate([FromBody] User model)
        {
            var response = _service.Authenticate(model);

            return response;
        }


        [HttpGet]
        [Route("menu")]
        public List<FuseMenu> GetMenu()
        {
            var fuseMenus = new List<FuseMenu>();

            var id = GetUserId();
            var list = _service.GetMenu(id);

            foreach (var item in list)
            {
                var fuseMenu = new FuseMenu { id = item.Title, title = item.Title, icon = item.Icon, type = item.Type, url = item.Url };
                
                foreach (var child in item.Children )
                {
                    fuseMenu.children.Add(new FuseMenu { id = child.Title, title = child.Title, icon = child.Icon, type = child.Type, url = child.Url});
                }

                fuseMenus.Add(fuseMenu);
            }


            return fuseMenus;
        }


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
