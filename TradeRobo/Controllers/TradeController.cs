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
using Newtonsoft.Json;
using System.IO;

namespace TradeRobo.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : BaseController
    {

        TradeService _service;

        public TradeController(IWebHostEnvironment env, MyDatabaseContext context) : base(env, context)
        {
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        private void InitTradeService()
        {
            var UserId = GetUserId();

            _service = new TradeService(_context, UserId);

        }


        [HttpPost]
        [Route("order")]
        public ReturnType PlaceOrder(OrderGroup poco)
        {
            InitTradeService();
            return _service.PlaceOrder(poco);
        }


        [HttpPost]
        [Route("order/multi")]
        public ReturnType PlaceMultiOrder(MultiOrder poco)
        {
            InitTradeService();
            return _service.PlaceMultiOrder(poco);

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
        public Account GetAccount()
        {
            InitTradeService();
            return _service.GetAccount();
        }

    }
}
