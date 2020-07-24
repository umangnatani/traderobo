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

        TradeService _service;

        public RHController(IWebHostEnvironment env, MyDatabaseContext context) : base(env, context)
        {
        }


        [HttpPost]
        [Route("login")]
        public RHAuthResponse Login([FromBody] Credentials loginDetails)
        {
            InitTradeService();
            return _service.RHLogin(loginDetails);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        private void InitTradeService()
        {
            var UserId = GetUserId();

            _service = new TradeService(_context, UserId);

        }


        [HttpPost]
        [Route("order")]
        public ReturnType PlaceOrder(Order poco)
        {
            InitTradeService();

            return _service.PlaceOrder(poco);
        }


        [HttpPost]
        [Route("order/pie")]
        public ReturnType PlacePieOrder(PieOrder poco)
        {
            InitTradeService();

            return _service.PlaceOrder(poco);

        }


              

        [HttpGet]
        [Route("account")]
        public string Get()
        {
            InitTradeService();

            return _service.GetAccountProfile();
            //return "Hello";

        }



     


    }
}
