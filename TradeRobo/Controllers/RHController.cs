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


namespace TradeRobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RHController : ControllerBase
    {

        private IJwtToken _token;

        public RHController(IJwtToken token)
        {
            _token = token;
        }


        [HttpPost]
        [Route("login")]
        public JwtToken Login([FromBody] Credentials loginDetails)
        {
            LoginService service = new LoginService();
            service.Login(loginDetails, _token);
            return (JwtToken)_token;
        }


        private void RetrieveAccessToken()
        {
            _token.accessToken = Request.Headers["AccessToken"];

        }


        [HttpPost]
        [Route("order")]
        public Order PlaceOrder(Order poco)
        {
            RetrieveAccessToken();

            OrderService service = new OrderService(_token);

            service.PlaceOrder(poco);
            return poco;
        }


        [HttpPost]
        [Route("order/folio")]
        public List<Order> PlaceFolioOrder(Order poco)
        {
            RetrieveAccessToken();

            OrderService service = new OrderService(_token);

            return service.PlaceOrder(poco.Folio, poco.Amount);

        }



        [HttpGet]
        [Route("account")]
        public string Get()
        {
            RetrieveAccessToken();

            OrderService service = new OrderService(_token);

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
        [Route("folio")]
        public List<string> GetFolios()
        {
            string webRootPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/Content/";
            FolioService service = new FolioService();
            return service.GetFolios(webRootPath);
        }

        [HttpGet]
        [Route("path")]
        public string GetPath()
        {

            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/Content/";
        }
    }
}
