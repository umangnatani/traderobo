using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeRobo.Service;

namespace TradeRobo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TDController : BaseController
    {

        public TDController(IWebHostEnvironment env, MyDatabaseContext context) : base(env, context)
        {

        }

        [HttpPost]
        [Route("order")]
        public ReturnType PlaceOrder(TDOrder poco)
        {
            var Id = GetUserId();

            TradeService service = new TradeService(_context);
            return service.PlaceTDOrder(poco, Id);

        }
    }
}
