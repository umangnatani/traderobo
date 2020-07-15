using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using TradeRobo.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace TradeRobo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RHController : BaseController
    {


        public RHController(IWebHostEnvironment env, MyDatabaseContext context, IJwtToken token) : base(env, context, token)
        {
            
        }


        [HttpPost]
        [Route("login")]
        public JwtToken Login([FromBody] Credentials loginDetails)
        {
            LoginService service = new LoginService();
            service.Login(loginDetails, _token);
            return (JwtToken)_token;
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        private void RetrieveAccessToken()
        {
            var id = GetUserId();
            DBService _service = new DBService(_context);
            _token.accessToken = _service.GetUser(id).RHToken;

            //_token.accessToken = Request.Headers["AccessToken"];

        }


        [HttpPost]
        [Route("order")]
        public Order PlaceOrder(Order poco)
        {
            RetrieveAccessToken();

            RHClient service = new RHClient(_token, _context);

            service.PlaceOrder(poco);
            return poco;
        }


        [HttpPost]
        [Route("order/pie")]
        public List<Order> PlacePieOrder(Order poco)
        {
            RetrieveAccessToken();

            RHClient service = new RHClient(_token, _context);

            return service.PlaceOrder(poco.PieId, poco.Amount);

        }



        [HttpPost]
        [Route("order/folio")]
        public List<Order> PlaceFolioOrder(Order poco)
        {
            RetrieveAccessToken();

            RHClient service = new RHClient(_token, _context);

            return service.PlaceOrder(poco.Folio, poco.Amount);

        }



        [HttpGet]
        [Route("account")]
        public string Get()
        {
            RetrieveAccessToken();

            RHClient service = new RHClient(_token, _context);

            return service.GetAccountProfile();
            //return "Hello";

        }



        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return "Hello";
        }


        [HttpGet]
        [Route("path")]
        public string GetPath()
        {

            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/Content/";
        }
    }
}
