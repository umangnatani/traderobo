using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeRobo.Service;
using System.Web;

namespace TradeRobo.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]


    public class StringResponse
    {
        public string response { get; set; }
    }

    public class StringRequest
    {
        public string code { get; set; }
    }

    public class TDController : BaseController
    {

        public TDController(IWebHostEnvironment env, MyDatabaseContext context) : base(env, context)
        {

        }

        [HttpPost]
        [Route("order/{accountId}")]
        public ReturnType PlaceOrder(OrderGroup poco, string accountId)
        {
            var Id = GetUserId();

            poco.Broker = "TD";
            poco.TDAccountId = accountId;

            TradeService service = new TradeService(_context, Id);
            return service.PlaceOrder(poco);

        }

        [HttpPost]
        [Route("order/multi/{accountId}")]
        public ReturnType PlaceMultiOrder(MultiOrder poco, string accountId)
        {
            var Id = GetUserId();

            TradeService service = new TradeService(_context, Id);

            return service.PlaceMultiOrder(poco, "TD", accountId);

        }

        [HttpGet]
        [Route("accounts")]
        public List<TDAccount> GetAccounts()
        {

            var Id = GetUserId();

            TradeService service = new TradeService(_context, Id);

            return service.GetAccounts(Id);
        }


        [HttpGet]
        [Route("authurl")]
        public StringResponse GetAuthURL()
        {

            return  new StringResponse { response = @"https://auth.tdameritrade.com/auth?response_type=code&redirect_uri=https%3A%2F%2F127.0.0.1&client_id=KXXLVYJ1EECUFV8DPGMA2IEHIISTGM6J%40AMER.OAUTHAP" };
            // return _service.GePositions();
        }


        [HttpPost]
        [Route("login")]
        public StringResponse Login(StringRequest poco)
        {
            var Id = GetUserId();

            TradeService service = new TradeService(_context, Id);
            service.TDLogin(HttpUtility.UrlDecode(poco.code));
            return new StringResponse { response = "Successful" };
        }
    }
}
